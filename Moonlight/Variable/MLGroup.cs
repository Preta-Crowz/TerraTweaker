using System.Collections.Generic;

using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLGroup : IIngredient{
        List<MLItem> Value = new List<MLItem>();
        string Name;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLGroup(string name){
            this.Name = name;
        }

        public void Add(MLItem item){
            Value.Add(item);
        }

        public void Add(MLItem[] items){
            foreach(MLItem item in items)
                Value.Add(item);
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            return "<TerraTweaker^"+Name+">";
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Group";
        }
    }
}