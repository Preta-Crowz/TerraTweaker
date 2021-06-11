using Terraria.ModLoader;
using Terraria.ID;

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
            Value = ModContent.Find<ModItem>(mod, name);
            Name = name;
        }

        public MLItem(Mod m, string name){
            Value = ModContent.Find<ModItem>(m.Name, name);
            Name = name;
        }

        public MLItem(ModItem item){
            Value = item;
        }

        public MLItem(){
            Value = null;
        }

        public ModTile GetTile(){
            int intTile = Value.Item.createTile;
            string name = TileID.Search.GetName(intTile);
            return ModContent.Find<ModTile>(this.Parent().Name, name);
        }

        public Mod Parent(){
            return this.Value.Mod;
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

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Item";
        }
    }
}