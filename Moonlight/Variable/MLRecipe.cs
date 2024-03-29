using System.Collections.Generic;

using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLRecipe : IVariable, IWorkable{
        MLItem Value;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public List<IIngredient> Ingredient = new List<IIngredient>();
        public List<MLTile> Requirement = new List<MLTile>();

        bool ReqWater = false;
        bool ReqLava = false;
        bool ReqHoney = false;

        public MLRecipe(MLItem item){
            this.Value = item;
        }

        public void AddIngredient(IIngredient item){
            Ingredient.Add(item);
        }

        public void AddIngredient(IIngredient[] items){
            foreach(IIngredient item in items)
                Ingredient.Add(item);
        }

        public void SetIngredient(List<IIngredient> ingredient){
            Ingredient = ingredient;
        }

        public void AddRequirement(MLTile tile){
            Requirement.Add(tile);
        }

        public void AddRequirement(MLTile[] tiles){
            foreach(MLTile tile in tiles)
                Requirement.Add(tile);
        }

        public void SetRequirement(List<MLTile> req){
            Requirement = req;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            return "<Recipe:" + Value.ToString() + ">";
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Recipe";
        }
    }
}