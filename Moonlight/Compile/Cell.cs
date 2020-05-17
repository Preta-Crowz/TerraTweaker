using System;
using Terraria.ModLoader;
using Moonlight.Variable;

namespace Moonlight.Compile{
    class Cell{
        public dynamic Value;
        public char Action;
        public bool isEnd;
        MLMod tt = new MLMod("TerraTweaker");

        private int? cPrio = null;

        int? Priority{
            get{
                if(cPrio == null) cPrio = Merge.Priority(this.Action);
                return cPrio;
            }
        }

        public Cell(dynamic v, char action, bool isEnd = false){
            this.Value = v;
            this.Action = action;
            this.isEnd = isEnd;
            tt.Value.Logger.Debug("New cell : "+v);
            tt.Value.Logger.Debug("Action : " + action);
        }

        public bool CanMerge(Cell other){
            return this.Priority >= other.Priority;
        }

        public void MergeCell(Cell other){
            switch(this.Action){
                case ':':
                    if(this.Value is MLArray){
                        int cnt = this.Value.Value.Count-1;
                        this.Value.Value[cnt] = this.Value.Value[cnt].GetItem(other.Value.ToString());
                        break;
                    }
                    this.Value = this.Value.GetItem(other.Value.ToString());
                    break;
                case '$':
                    if(this.Value is MLArray){
                        int cnt = this.Value.Value.Count-1;
                        this.Value.Value[cnt] = this.Value.Value[cnt].GetItem(other.Value.ToString());
                        break;
                    }
                    this.Value = this.Value.GetTile(other.Value.ToString());
                    break;
                case '*': this.Value.Multiply(other.Value.ToInt());
                    break;
                case '(':
                    throw new ArgumentException("function");
                    break;
                case ',':
                    if(!(this.Value is MLArray)){
                        dynamic V = this.Value;
                        this.Value = new MLArray();
                        this.Value.Add(V);
                    }
                    this.Value.Add(other.Value);
                    break;
            }
            tt.Value.Logger.Debug("Changed Value : " + this.Value.GetValue());
            this.Action = other.Action;
            tt.Value.Logger.Debug("Changed Action : " + this.Action);
        }
    }
}