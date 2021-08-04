using Moonlight.Variable;

namespace Moonlight.Compile.Processor{
    static class RecipeProcessor{
        static MLMod tt = new MLMod("zzzTerraTweaker");

        public static MLRecipe Work(MLArray args){
            tt.Value.Logger.Debug(args[0].ToString());
            tt.Value.Logger.Debug(args[1].ToString());
            tt.Value.Logger.Debug(args[2].ToString());
            MLRecipe recipe = new MLRecipe((MLItem)args[0]);
            foreach(MLItem item in args[1].GetValue())
                recipe.AddIngredient(item);
            if(args[2].GetValue().Count == 1 && args[2].GetValue()[0].GetValue().ToString() == "") return recipe;
            foreach(MLTile tile in args[2].GetValue())
                recipe.AddRequirement(tile);
            return recipe;
        }
    }
}