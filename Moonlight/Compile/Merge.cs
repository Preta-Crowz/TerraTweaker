using System;
using System.Collections.Generic;
using Moonlight.Variable;
using Moonlight.Compile.Processor;
using Terraria.ModLoader;

namespace Moonlight.Compile{
    class Merge{
        public static MLMod tt = new MLMod("zzzTerraTweaker");

        public static int Priority(char action){
            switch(action){
                case ':':
                case '$': return 9;
                case '*': return 8;
                case ',': return 3;
                case '(': return 2;
                case ')': return 1;
                default: return 0;
        }}

        public static IVariable Work(Cell curr, ref int index, List<Cell> cells, bool onlyOne = false){
            if(index >= 14){
                index++;
                return curr.Value;
            }
            tt.Value.Logger.Debug("Start Merge Cells with " + cells.Count + " Cells, index " + index);
            while(index < cells.Count){
                // IDK why it's buggy
                Cell next = cells[index++];
                tt.Value.Logger.Debug("Checking possibility");
                tt.Value.Logger.Debug("Left : " + curr.Action + "/" + curr.Value.ToString());
                tt.Value.Logger.Debug("Right : " + next.Action + "/" + next.Value.ToString());
                while(!curr.CanMerge(next)){
                    tt.Value.Logger.Debug("Can\'t merge " + curr.Action + " to " + next.Action + "! try next");
                    Work(next, ref index, cells, true);
                }
                tt.Value.Logger.Debug("Merge two cells");
                tt.Value.Logger.Debug("Left : " + curr.Action + "/" + curr.Value.ToString());
                tt.Value.Logger.Debug("Right : " + next.Action + "/" + next.Value.ToString());
                curr.MergeCell(next);
                string tmp = "";
                foreach(Cell cell in cells){
                    if(cell.isMerged) continue;
                    tmp += cell.Value.ToString();
                    tmp += cell.Action;
                }
                tt.Value.Logger.Debug(tmp);
                if(onlyOne) return curr.Value;
            }
            tt.Value.Logger.Debug("Done Merge : " + curr.Action + " is current action");
            return curr.Value;
        }

        public static bool isValidProcess(string name){
            return (name == "recipe" || name == "remove");
        }

        public static IVariable Process(string name, MLArray args){
            switch(name){
                case "recipe": return RecipeProcessor.Work(args);
                case "remove": return RemoveProcessor.Work(args);
            }
            return new MLRaw("dummy");
        }
    }
}