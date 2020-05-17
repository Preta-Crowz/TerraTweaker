using System;
using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class VTile : MLTile{
        string Name;
        int Code;

        public VTile(string name){
            Name = name;
            Code = (int)Enum.Parse(typeof(TileID), name);
        }

        public dynamic GetValue(){
            return Code;
        }

        public string ToString(){
            return Name;
        }
    }
}