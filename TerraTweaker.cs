using System;
using System.IO;
using System.Collections.Generic;

using Terraria.ModLoader;
using Terraria.ID;

using Moonlight;
using Moonlight.Variable;

namespace TerraTweaker{
    class TerraTweaker : Mod{
        public string DisplayName;
        public TerraTweaker(){
            this.DisplayName = "TerraTweaker";
        }
        public readonly ModProperties Properties = ModProperties.AutoLoadAll;

        private string DataPath, ScriptPath;
        private List<string> ScriptFiles = new List<string>();
        private List<string> Errors = new List<string>();

        private List<MLRecipe> recipes = new List<MLRecipe>();

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
                foreach(MLRecipe recipe in script.GetRecipes()){
                    this.Logger.Debug("TEST : " + (i++) + " / " + recipe.ToString());
                    recipes.Add((MLRecipe)recipe);
                }
            }
        }

        public override void AddRecipes(){
            foreach(MLRecipe recipe in recipes)
                RegisterRecipe((MLRecipe)recipe);
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
    }
}