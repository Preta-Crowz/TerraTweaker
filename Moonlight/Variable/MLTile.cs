using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLTile : IVariable{
        ModTile Value;
        public string Name;
        public int Code;
        public bool isEnded = false;
        public bool isVanilla = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLTile(string mod, string name){
            Mod m = ModLoader.GetMod(mod);
            Value = m.GetTile(name);
        }

        public MLTile(Mod m, string name){
            Value = m.GetTile(name);
        }

        public MLTile(ModTile tile){
            Value = tile;
        }

        public MLTile(){
            Value = null;
        }

        public ModItem GetItem(){
            return Value.mod.GetItem(Value.Name);
        }

        public Mod Parent(){
            return this.Value.mod;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            if(isVanilla) return "<Terraria$" + Name + ">";
            else if(this.Parent() == null) return "<UnknownTile^" + Value.Name + ">";
            return "<" + this.Parent().Name + "$" + Value.Name + ">";
        }

        public dynamic GetValue(){
            if(isVanilla) return Code;
            return Value;
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Tile";
        }
    }
}