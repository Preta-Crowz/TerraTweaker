using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLRemove : IIngredient{
        MLItem Value;
        public string Name;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLRemove(MLItem item){
            Value = item;
        }

        public MLRemove(){
            Value = null;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            return "<Remove:" + Value.ToString() + ">";
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Remove";
        }
    }
}