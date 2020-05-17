using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLTile : IVariable{
        ModTile Value;
        public bool isEnded = false;

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
            return Value.Name;
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public void Multiply(int multi){}
        public int ToInt(){return 0;}
    }
}