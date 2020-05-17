using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLItem : IIngredient{
        ModItem Value;
        public int Code;
        public int Count = 1;
        string Name;
        public bool isEnded = false;

        public MLItem(string mod, string name){
            Mod m = ModLoader.GetMod(mod);
            Value = m.GetItem(name);
        }

        public MLItem(Mod m, string name){
            Value = m.GetItem(name);
        }

        public MLItem(ModItem item){
            Value = item;
        }

        public MLItem(){
            Value = null;
        }

        public ModTile GetTile(){
            return Value.mod.GetTile(Value.Name);
        }

        public Mod Parent(){
            return this.Value.mod;
        }

        public void Multiply(int multi){
            Count *= multi;
        }

        public string ToString(){
            if(Value == null) return Name;
            return Value.Name;
        }

        public dynamic GetValue(){
            if(Value == null) return Code;
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public int ToInt(){return 0;}
    }
}