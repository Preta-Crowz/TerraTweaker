using System;
using Terraria.ID;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VTile : MLTile{
        string Name;
        int Value;

        public VTile(string name){
            Name = name;
            Value = (int)Enum.Parse(typeof(TileID), name);
        }

        public dynamic GetValue(){
            return Value;
        }
    }
}