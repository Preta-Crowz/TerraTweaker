using System;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VTile : MLTile{

        public VTile(string name){
            isVanilla = true;
            Name = name;
            Code = (int)Enum.Parse(typeof(TileID), name);
        }

        public dynamic GetValue(){
            return Code;
        }
    }
}