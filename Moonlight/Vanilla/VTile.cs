using System;
using System.Reflection;
using Terraria.ID;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VTile : MLTile{

        public VTile(string name){
            isVanilla = true;
            Name = name;
            FieldInfo field = typeof(TileID).GetField(name);
            Code = (int)field.GetValue(null);
        }

        public dynamic GetValue(){
            return Code;
        }
    }
}