using System;
using Terraria.ModLoader;
using Moonlight.Variable;

namespace Moonlight.Compile{
    class Cell{
        public dynamic Value;
        public char Action;
        MLMod tt = new MLMod("TerraTweaker");

        private int? cPrio = null;

        int? Priority{
            get{
                if(cPrio == null) cPrio = Merge.Priority(this.Action);
                return cPrio;
            }
        }

        public Cell(dynamic v, char action){
            this.Value = v;
            this.Action = action;
            tt.Value.Logger.Debug("New cell : "+v);
            tt.Value.Logger.Debug("Action : " + action);
        }

        public bool CanMerge(Cell other){
            return this.Priority >= other.Priority;
        }

        public void MergeCell(Cell other){
            switch(this.Action){
                case ':':
                    this.Value = this.Value.GetItem(other.Value.ToString());
                    break;
                case '$': this.Value = this.Value.GetTile(other.Value.ToString());
                    break;
                case '*': this.Value.Multiply(other.Value.ToInt());
                    break;
                case '(':
                    throw new ArgumentException("function");
                    break;
                case ',':
                    if(!(this.Value is MLArgs)){
                        dynamic V = this.Value;
                        this.Value = new MLArgs();
                        this.Value.Add(V);
                    }
                    this.Value.Add(other.Value);
                    break;
            }
            tt.Value.Logger.Debug("Changed Value : " + this.Value);
            this.Action = other.Action;
            tt.Value.Logger.Debug("Changed Action : " + this.Action);
        }
    }
}