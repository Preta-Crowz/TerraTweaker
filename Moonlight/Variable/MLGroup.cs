using System.Collections.Generic;

using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLGroup : IIngredient{
        List<MLItem> Value = new List<MLItem>();
        string Name;
        public bool isEnded = false;

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
            return Name;
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public void Multiply(int multi){}
        public int ToInt(){return 0;}
    }
}