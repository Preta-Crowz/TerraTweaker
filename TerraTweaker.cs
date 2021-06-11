using System;
using System.IO;
using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

using Moonlight;
using Moonlight.Variable;

namespace TerraTweaker{
    class TerraTweaker : Mod{
        public TerraTweaker(){}
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
            RemoveRecipe(removes);
            foreach(MLRecipe recipe in recipes)
                RegisterRecipe(recipe);
        }

        public void RegisterRecipe(MLRecipe data){
            int res = data.GetValue().Value.type;
            this.Logger.Debug("Creating new recipe for : " + data.GetValue().ToString() + "(" + res + ")");
            Recipe r = CreateRecipe(res, data.GetValue().Count);

            foreach(MLItem item in data.Ingredient){
                this.Logger.Debug("Adding Ingredient : " + item.ToString());
                this.Logger.Debug("Raw Data : " + item.GetValue());
                if(item.isVanilla) r = r.AddIngredient(item.GetValue(), item.Count);
                else r = r.AddIngredient(item.GetValue().type, item.Count);
            }

            foreach(MLTile tile in data.Requirement){
                this.Logger.Debug("Adding Requirement : " + tile.ToString());
                this.Logger.Debug("Raw Data : " + tile.GetValue());
                if(tile.isVanilla) r = r.AddTile(tile.GetValue());
                else r = r.AddTile(tile.GetValue().type);
            }
            this.Logger.Debug("Registering recipe for : " + data.GetValue().ToString() + "(" + res + ")");
            r.Register();
            this.Logger.Debug("Registered");
        }

        public void RemoveRecipe(List<MLItem> data){
            List<int> targets = new List<int>();
            foreach(MLItem item in data){
                if(item.isVanilla) targets.Add(item.GetValue().type);
                else targets.Add(item.GetValue());
            }
            for (int i = 0; i < Recipe.numRecipes; i++) {
                Recipe R = Main.recipe[i];
                if(targets.Exists(id => id == R.createItem.type))
                    R.RemoveRecipe();
            }
        }
    }
}