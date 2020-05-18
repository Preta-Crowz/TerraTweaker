using System.IO;
using System.Collections.Generic;

using Terraria.ModLoader;

using Moonlight;

namespace TerraTweaker{
    class TerraTweaker : Mod{
        public TerraTweaker(){}
        public readonly ModProperties Properties = ModProperties.AutoLoadAll;

        private string DataPath, ScriptPath;
        private List<string> ScriptFiles = new List<string>();
        private List<string> Errors = new List<string>();

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
                script.Compile();
                // this.Logger.Debug(script.vv);
                // this.Logger.Debug(script.vv.ToString());
                this.Logger.Debug(name);
                this.Logger.Debug(script.script);
                // List<MLRecepe> recipes = script.GetRecipes();
            }
        }
    }
}