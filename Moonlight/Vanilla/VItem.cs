using System;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VItem : MLItem{
        string Name;

        public VItem(string name){
            Name = name;
            Code = (int)Enum.Parse(typeof(ItemID), name);
        }

        public dynamic GetValue(){
            return Code;
        }

        public string ToString(){
            return Name;
        }
    }
}