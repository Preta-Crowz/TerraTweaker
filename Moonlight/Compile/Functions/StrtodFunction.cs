using System;
using Moonlight;
using Moonlight.Compile;
using Moonlight.Variable;

namespace Moonlight.Compile.Functions{
    class StrtodFunction : ParserFunction{
        MLMod tt = new MLMod("TerraTweaker");

        protected override IVariable Eval(string data, ref int from){
            dynamic V;
            V = Parse(Item);
            return V;
        }
        public string Item{ private get; set; }

        private IVariable Parse(string item, bool isEnd = false){
            int a;
            dynamic V;
            item = item.Trim();
            tt.Value.Logger.Debug("Parse : "+item);
            if(item.StartsWith("@")){
                string name = item.Substring(1);
                V = (name == "Terraria") ?
                    MLMod.GetVanilla() : new MLMod(name);
            }
            else if(item.StartsWith("[") || item.StartsWith("(")){
                V = new MLArray();
                V.Add(Parse(item.Substring(1)));
            }
            else if(item.EndsWith("]") || item.EndsWith(")")){
                V = Parse(item.Substring(0, item.Length-1), true);
            }
            else if(int.TryParse(item, out a)){
                V = new MLInteger(a);
            }
            else{
                V = new MLRaw(item);
            }
            V.isEnded = (isEnd || V.isEnded);
            tt.Value.Logger.Debug("Parsed to "+V.ToString());
            return V;
        }
    }
}