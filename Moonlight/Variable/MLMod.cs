using System;
using Terraria.ModLoader;
using Moonlight.Vanilla;

namespace Moonlight.Variable{
    class MLMod : IVariable{
        public Mod Value;

        public MLMod(string name){
            this.Value = ModLoader.GetMod(name);
        }

        public MLMod(Mod Value){
            this.Value = Value;
        }

        public MLMod(){
            Value = null;
        }

        public MLItem GetItem(string name){
            return new MLItem(this.Value.GetItem(name));
        }

        public MLTile GetTile(string name){
            return new MLTile(this.Value.GetTile(name));
        }

        public string ToString(){
            return Value.Name;
        }

        public static MLMod GetVanilla(){
            return Vanilla.Vanilla.Get();
        }

        public dynamic GetValue(){
            return Value;
        }

        public void Multiply(int multi){}
        public int ToInt(){return 0;}
    }
}