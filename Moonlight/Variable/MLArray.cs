using System.Collections.Generic;

using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLArray : IVariable{
        public List<IVariable> Value = new List<IVariable>();
        string Name;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public int Count{ get{
            return Value.Count;
        }}

        public IVariable this[int index] {
            get{
                return this.Value[index];
            }
            set{
                this.Value[index] = value;
        }}

        public MLArray(){
        }

        public void Add(IVariable item){
            Value.Add(item);
        }

        public void Add(IVariable[] items){
            foreach(IVariable item in items)
                Value.Add(item);
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            string s_val = "";
            for(int i=0;i<this.Value.Count;i++){
                s_val += this.Value[i].ToString();
                if(i<this.Value.Count-1) s_val += ", ";
            }
            return "[" + s_val + "]";
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(string name){
            return this[this.Count-1].GetItem(name);
        }

        public MLTile GetTile(string name){
            return this[this.Count-1].GetTile(name);
        }
        
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Array";
        }
    }
}