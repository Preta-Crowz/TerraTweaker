using System.Collections.Generic;

using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLRecipe : IVariable{
        MLItem Value;
        public bool isEnded = false;

        List<IIngredient> Ingredient = new List<IIngredient>();
        List<MLTile> Requirement = new List<MLTile>();

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
            return "Recipe:" + Value.ToString();
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