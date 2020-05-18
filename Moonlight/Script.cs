using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System;

using Moonlight.Compile;
using Moonlight.Variable;

namespace Moonlight{
    class Script{
        MLMod tt = new MLMod("TerraTweaker");

        public string dir;
        public string script;

        private bool loaded = false;
        private bool compiled = false;

        private List<MLRecipe> recipes = new List<MLRecipe>();
        private List<MLItem> removes = new List<MLItem>();
        private List<string> errors = new List<string>();

        public IVariable vv;

        public Script(string dir){
            this.dir = dir;
        }

        public void Load(){
            this.script = File.ReadAllText(dir);
            this.loaded = true;
        }

        public void Compile(){
            if(!this.IsLoaded()) this.Load();
            foreach(string r_expr in script.Split(';')){
                if(r_expr == "" || r_expr == " ") continue;
                string expr = Regex.Replace(r_expr, "\r?\n+[ \t]*", " ");
                if(expr == "" || expr == " ") continue;
                int index = 0;
                tt.Value.Logger.Debug("Start Parsing Expr : "+expr);
                IVariable Value = Split.Parse(expr, ref index, ';');
                tt.Value.Logger.Debug("Parsed Expr to : "+Value.ToString());
            }
        }

        public bool IsLoaded(){
            return loaded;
        }

        public bool IsCompiled(){
            return compiled;
        }

        public List<MLRecipe> GetRecipes(){
            if(!this.IsCompiled()) this.Compile();
            return this.recipes;
        }
    }
}