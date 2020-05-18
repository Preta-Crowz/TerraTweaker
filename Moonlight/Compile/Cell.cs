using System;
using Terraria.ModLoader;
using Moonlight.Variable;

namespace Moonlight.Compile{
    class Cell{
        public dynamic Value;
        public char Action;
        public bool isEnd;
        public bool isMerged;
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
            this.isMerged = false;
            tt.Value.Logger.Debug("New cell : "+v.ToString());
            tt.Value.Logger.Debug("Action : " + action);
        }

        public bool CanMerge(Cell other){
            return (this.Priority >= other.Priority) || ((this.Action == '(') && (other.Action == ')'));
        }

        public void MergeCell(Cell other){
            switch(this.Action){
                case ':':
                    if(this.Value is MLArray && !this.Value[this.Value.Count-1].isEnded){
                        int cnt = this.Value.Value.Count-1;
                        if(this.Value.Value[cnt] is MLArray){
                            int icnt = this.Value.Value[cnt].Count-1;
                            this.Value.Value[cnt][icnt] = this.Value.Value[cnt][icnt].GetItem(other.Value.GetValue());
                            break;
                        }
                        this.Value.Value[cnt] = this.Value.Value[cnt].GetItem(other.Value.GetValue());
                        break;
                    }
                    this.Value = this.Value.GetItem(other.Value.GetValue());
                    break;
                case '$':
                    if(this.Value is MLArray && !this.Value[this.Value.Count-1].isEnded){
                        int cnt = this.Value.Value.Count-1;
                        if(this.Value.Value[cnt] is MLArray){
                            int icnt = this.Value.Value[cnt].Count-1;
                            this.Value.Value[cnt][icnt] = this.Value.Value[cnt][icnt].GetTile(other.Value.GetValue());
                            break;
                        }
                        this.Value.Value[cnt] = this.Value.Value[cnt].GetItem(other.Value.GetValue());
                        break;
                    }
                    this.Value = this.Value.GetTile(other.Value.GetValue());
                    break;
                case '*':
                    if(this.Value is MLArray && !this.Value[this.Value.Count-1].isEnded){
                        int cnt = this.Value.Value.Count-1;
                        if(this.Value.Value[cnt] is MLArray){
                            int icnt = this.Value.Value[cnt].Count-1;
                            this.Value.Value[cnt][icnt].Multiply(other.Value);
                            break;
                        }
                        this.Value.Value[cnt].Multiply(other.Value);
                        break;
                    }
                    this.Value.Multiply(other.Value);
                    break;
                case '(':
                    if(!Merge.isValidProcess(this.Value.GetValue())) throw new ArgumentException(this.Value + " is not valid process name");
                    this.Value = Merge.Process(this.Value.GetValue(), other.Value);
                    break;
                case ',':
                    if(this.Value is MLArray && !this.Value[this.Value.Value.Count-1].isEnded &&
                            !this.Value[this.Value.Value.Count-1][this.Value[this.Value.Value.Count-1].Count-1].isEnded){
                        int cnt = this.Value.Value.Count-1;
                        if(!(this.Value.Value[cnt] is MLArray)){
                            dynamic V = this.Value;
                            this.Value.Value[cnt] = new MLArray();
                            this.Value.Value[cnt].Add(V);
                        }
                        this.Value.Value[cnt].Add(other.Value);
                        tt.Value.Logger.Debug("Array Count : "+this.Value.Value[cnt].Value.Count);
                        tt.Value.Logger.Debug("Last Value : "+this.Value.Value[cnt].Value[this.Value.Value[cnt].Value.Count-1].ToString());
                        break;
                    }
                    else if(!(this.Value is MLArray)){
                        dynamic V = this.Value;
                        this.Value = new MLArray();
                        this.Value.Add(V);
                    }
                    this.Value.Add(other.Value);
                    tt.Value.Logger.Debug("Array Count : "+this.Value.Value.Count);
                    tt.Value.Logger.Debug("Last Value : "+this.Value.Value[this.Value.Value.Count-1].ToString());
                    break;
            }
            tt.Value.Logger.Debug("Changed Value : " + this.Value.ToString());
            this.Action = other.Action;
            other.isMerged = true;
            tt.Value.Logger.Debug("Changed Action : " + this.Action);
        }
    }
}