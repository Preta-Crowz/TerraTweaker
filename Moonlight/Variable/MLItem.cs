using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLItem : IIngredient{
        ModItem Value;
        public int Code;
        public int Count = 1;
        public string Name;
        public bool isEnded = false;
        public bool isVanilla = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLItem(string mod, string name){
            Mod m = ModLoader.GetMod(mod);
            Value = m.GetItem(name);
            Name = name;
        }

        public MLItem(Mod m, string name){
            Value = m.GetItem(name);
            Name = name;
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

        public void Multiply(MLInteger input){
            this.Count *= input.Value;
            this.isEnded = input.isEnded;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            if(isVanilla) return "<Terraria:" + Name + "*" + Count +">";
            else if(Value == null) return "<UnknownItem^" + Name + "*" + Count + ">";
            return "<" + this.Parent().Name + ":" + Value.Name + "*" + Count + ">";
        }

        public dynamic GetValue(){
            if(isVanilla) return Code;
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Item";
        }
    }
}