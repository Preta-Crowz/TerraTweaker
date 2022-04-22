using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System;

using Moonlight.Compile;
using Moonlight.Variable;

namespace Moonlight{
    class Script{
        MLMod tt = new MLMod("zzzTerraTweaker");

        public string dir;
        public string script;
        public byte[] hash;
        public string hexHash {
            get => Convert.ToHexString(hash);
        }

        private bool loaded = false;
        private bool compiled = false;

        private List<IWorkable> works = new List<IWorkable>();
        private List<string> errors = new List<string>();

        public IVariable vv;

        public Script(string dir){
            this.dir = dir;
        }

        public void Load(){
            this.script = File.ReadAllText(dir);
            MD5 hashCalc = MD5.Create();
            hashCalc.ComputeHash(Encoding.UTF8.GetBytes(this.script));
            this.hash = hashCalc.Hash;
            this.loaded = true;
        }

        public void Compile(){
            if(!this.IsLoaded()) this.Load();
            this.compiled = true;
            foreach(string r_expr in script.Split(';')){
                if(r_expr == "" || r_expr == " ") continue;
                string expr = Regex.Replace(r_expr, "\r?\n+[ \t]*", " ");
                if(expr == "" || expr == " ") continue;
                int index = 0;
                tt.Value.Logger.Debug("Start Parsing Expr : "+expr);
                IVariable Value = Split.Parse(expr, ref index, ';');
                tt.Value.Logger.Debug("Parsed Expr to : "+Value.ToString());
                string vt = Value.GetType();
                if(Value is MLCondition && !Value.GetValue()) return;
                if(Value is IWorkable) works.Add((IWorkable)Value);
            }
        }

        public bool IsLoaded(){
            return loaded;
        }

        public bool IsCompiled(){
            return compiled;
        }

        public List<IWorkable> GetWorks(){
            if(!this.IsCompiled()) this.Compile();
            return this.works;
        }
    }
}