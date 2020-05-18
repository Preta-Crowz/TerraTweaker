using System;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VItem : MLItem{

        public VItem(string name){
            isVanilla = true;
            Name = name;
            Code = (int)Enum.Parse(typeof(ItemID), name);
        }

        public dynamic GetValue(){
            return Code;
        }

        public string ToString(){
            return "<Terraria:" + Name + ">";
        }
    }
}