using System.Collections.Generic;

using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLArgs : IVariable{
        public List<IVariable> Value = new List<IVariable>();
        string Name;

        public MLArgs(){
        }

        public void Add(IVariable item){
            Value.Add(item);
        }

        public void Add(IVariable[] items){
            foreach(IVariable item in items)
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