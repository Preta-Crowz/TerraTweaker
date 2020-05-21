using System;
using System.Reflection;
using Terraria.ID;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VItem : MLItem{

        public VItem(string name){
            isVanilla = true;
            Name = name;
            FieldInfo field = typeof(ItemID).GetField(name);
            Code = Convert.ToInt32(field.GetValue(null));
        }

        public dynamic GetValue(){
            return Code;
        }

        public string ToString(){
            return "<Terraria:" + Name + ">";
        }
    }
}