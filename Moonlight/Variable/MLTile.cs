using Terraria.ModLoader;
using Terraria.ID;

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
            Value = ModContent.Find<ModTile>(mod, name);
            Name = name;
        }

        public MLTile(Mod m, string name){
            Value = ModContent.Find<ModTile>(m.Name, name);
            Name = name;
        }

        public MLTile(ModTile tile){
            Value = tile;
        }

        public MLTile(){
            Value = null;
        }

        public ModItem GetItem(){
            int intItem = this.Value.ItemDrop;
            string name = ItemID.Search.GetName(intItem);
            return ModContent.Find<ModItem>(this.Parent().Name, name);
        }

        public Mod Parent(){
            return this.Value.Mod;
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