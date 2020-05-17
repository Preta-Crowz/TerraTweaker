using System;
using System.Text;
using System.Collections.Generic;

using Moonlight;
using Moonlight.Variable;

namespace Moonlight.Compile{
    class Split{
        public const char END_EXPR = ';';
        public const char START_ARG = '(';
        public const char END_ARG = ')';

        public static IVariable Parse(string data, ref int from, char end = END_EXPR){
            if(from >= data.Length || data[from] == end)
                throw new ArgumentException("Invalid data : " + data);
            List<Cell> cells = new List<Cell>();
            StringBuilder item = new StringBuilder();
            do{
                char ch = data[from++];
                if(StillCollecting(item.ToString(), ch, end)){
                    item.Append(ch);
                    if(from < data.Length && data[from] != end) continue;
                }
                ParserFunction func = new ParserFunction(data, ref from, item.ToString(), ch);
                IVariable V = func.GetValue(data, ref from);
                char action = ValidAction(ch) ? ch : UpdateAction(data, ref from, ch, end);
                cells.Add(new Cell(V, action));
                item.Clear();
            } while (from < data.Length && data[from] != end);
            if(from < data.Length && (data[from] == END_ARG || data[from] == end))
                from++;
            Cell baseCell = cells[0];
            int index = 1;
            return Merge.Work(baseCell, ref index, cells);
        }
        
        public static bool StillCollecting(string item, char ch, char end){
            char stop = end;
            switch(end){
                case END_EXPR: stop = END_EXPR;
                    break;
                case END_ARG: stop = END_ARG;
                    break;
            }
            return (item.Length == 0 && (ch == '-' || ch == END_ARG)) ||
                !(ValidAction(ch) || ch == START_ARG || ch == stop);
        }

        public static bool ValidAction(char ch){
            switch(ch){
                case ':':
                case '$':
                case '*':
                case ',':
                case '(':
                    return true;
                default: return false;
            }
        }

        public static char UpdateAction(string item, ref int from, char ch, char to){
            if(from <= item.Length || item[from] == END_ARG || item[from] == to)
                return END_ARG;
            int index = from;
            char res = ch;
            while(!ValidAction(res) && index < item.Length)
                res = item[index++];
            from = ValidAction(res) ? index : (index > from ? index-1 : from);
            return res;
        }
    }
}