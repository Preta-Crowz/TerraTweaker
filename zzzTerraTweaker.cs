using System;
using System.IO;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

using Moonlight;
using Moonlight.Variable;

namespace zzzTerraTweaker{
    class zzzTerraTweaker : Mod{
        public zzzTerraTweaker(){}
        public readonly ModProperties Properties = ModProperties.AutoLoadAll;

        private string DataPath, ScriptPath;
        private List<string> ScriptFiles = new List<string>();
        private List<string> Errors = new List<string>();

        private List<MLRecipe> recipes = new List<MLRecipe>();
        private List<MLItem> removes = new List<MLItem>();

        public override void Load(){
            this.DataPath = Path.GetDirectoryName(Logging.LogDir);
            this.ScriptPath = Path.Combine(this.DataPath, "Scripts");
            this.Logger.Debug("Script Path : " + this.ScriptPath);
            Directory.CreateDirectory(this.ScriptPath);
            string[] allFiles = Directory.GetFiles(this.ScriptPath);
            foreach(string file in allFiles){
                if(Path.GetExtension(file) == ".moon"){
                    string fileName = Path.GetFileName(file);
                    this.ScriptFiles.Add(fileName);
            }}
            this.Logger.Debug("Script List : " + string.Join(",", this.ScriptFiles));
            SetupScripts();
        }

        public void SetupScripts(){
            foreach(string name in this.ScriptFiles){
                Script script = new Script(Path.Combine(this.ScriptPath, name));
                this.Logger.Info("Start Compile " + name);
                script.Compile();
                this.Logger.Info("Compiled " + name);
                int i = 0;
                foreach(MLRecipe recipe in script.GetRecipes())
                    recipes.Add((MLRecipe)recipe);
                foreach(MLItem item in script.GetRemoves())
                    removes.Add(item);

            }
        }

        public override void PostAddRecipes(){
            foreach(MLItem item in removes)
                RemoveRecipe(item);
            foreach(MLRecipe recipe in recipes)
                RegisterRecipe(recipe);
        }

        public void RegisterRecipe(MLRecipe data){
            ModRecipe recipe = new ModRecipe(this);
            this.Logger.Debug("Register new recipe for : " + data.GetValue().ToString());
            foreach(MLItem item in data.Ingredient){
                this.Logger.Debug("Add Ingredient : " + item.ToString());
                this.Logger.Debug("Raw Data : " + item.GetValue());
                recipe.AddIngredient(item.GetValue(), item.Count);
            }
            foreach(MLTile tile in data.Requirement){
                this.Logger.Debug("Add Requirement : " + tile.ToString());
                this.Logger.Debug("Raw Data : " + tile.GetValue());
                recipe.AddTile(tile.GetValue());
            }
            this.Logger.Debug("Setting Result : " + data.GetValue().ToString());
            this.Logger.Debug("Raw Data : " + data.GetValue().GetValue());
            recipe.SetResult(data.GetValue().GetValue(), data.GetValue().Count);
            recipe.AddRecipe();
            this.Logger.Debug("Added recipe for : " + data.GetValue().ToString());
        }

        public void RemoveRecipe(MLItem data){
            RecipeFinder finder = new RecipeFinder();
            int code = 0;
            if (data.isVanilla) code = data.GetValue();
            else code = data.Parent().ItemType(data.GetValue().Name);
            finder.SetResult(code);
            foreach(Recipe recipe in finder.SearchRecipes()){
                RecipeEditor editor = new RecipeEditor(recipe);
                editor.DeleteRecipe();
            }
        }
    }
}
