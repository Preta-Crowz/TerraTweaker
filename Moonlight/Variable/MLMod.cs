using System;
using Terraria.ModLoader;
using Moonlight.Vanilla;

namespace Moonlight.Variable{
    class MLMod : IVariable{
        public Mod Value;
        public bool isEnded = false;
        public bool isVanilla = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLMod(string name){
            this.Value = ModLoader.GetMod(name);
        }

        public MLMod(Mod Value){
            this.Value = Value;
        }

        public MLMod(){
            Value = null;
        }

        public MLItem GetItem(IVariable raw){
            string name = raw.GetValue();
            this.isEnded = raw.isEnd();
            if(isVanilla) return new VItem(name);
            MLItem item = new MLItem(this.Value.GetItem(name));
            item.isEnded = this.isEnded;
            return item;
        }

        public MLTile GetTile(IVariable raw){
            string name = raw.GetValue();
            this.isEnded = raw.isEnd();
            if(isVanilla) return new VTile(name);
            MLTile item = new MLTile(this.Value.GetTile(name));
            item.isEnded = this.isEnded;
            return item;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            if(isVanilla) return "<Mod^Terraria>";
            else if(Value == null) return "<UnknownMod>";
            return "<Mod^" + Value.Name + ">";
        }

        public static MLMod GetVanilla(){
            return Vanilla.Vanilla.Get();
        }

        public dynamic GetValue(){
            return Value;
        }

        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Mod";
        }
    }
}