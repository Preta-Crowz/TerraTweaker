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

        private IVariable Parse(string item){
            int a;
            dynamic V;
            tt.Value.Logger.Debug("Parse : "+item);
            if(item.StartsWith("@")){
                string name = item.Substring(1);
                V = name == "Vanilla" ? MLMod.GetVanilla() : new MLMod(name);
            }
            else if(item.StartsWith("[")){
                V = new MLArray();
                V.Add(Parse(item.Substring(1)));
            }
            else if(item.StartsWith("(")){
                V = new MLArgs();
                V.Add(Parse(item.Substring(1)));
            }
            else if(int.TryParse(item, out a)){
                V = new MLInteger(a);
            }
            else{
                V = new MLRaw(item);
            }
            tt.Value.Logger.Debug("Parsed to "+V);
            return V;
        }
    }
}