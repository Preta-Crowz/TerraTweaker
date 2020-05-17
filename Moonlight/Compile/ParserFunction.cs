using Moonlight.Variable;
using Moonlight.Compile.Functions;
using System.Collections.Generic;

namespace Moonlight.Compile{
    class ParserFunction{
        private static IdentityFunction s_idFunction = new IdentityFunction();
        private static StrtodFunction s_strtodFunction = new StrtodFunction();
        private ParserFunction m_impl;
        private static Dictionary<string, ParserFunction> m_functions =
            new Dictionary<string, ParserFunction>();

        MLMod tt = new MLMod("TerraTweaker");

        private bool inited = false;

        internal ParserFunction(string data, ref int from, string item, char ch){
            // if(!inited && m_functions.Count == 0) init();
            // tt.Value.Logger.Debug("New ParserFunc With Data : "+data);

            if(item.Length == 0 && ch == Split.START_ARG){
                m_impl = s_idFunction;
                return;
            }
            if(m_functions.TryGetValue(item, out m_impl))
                return;
            s_strtodFunction.Item = item;
            m_impl = s_strtodFunction;
        }

        public static void AddFunction(string name, ParserFunction function){
            m_functions[name] = function;
        }

        public IVariable GetValue(string data, ref int from){
            return m_impl.Eval(data, ref from);
        }

        protected virtual IVariable Eval(string data, ref int from){
            return new MLInteger(0);
        }

        public ParserFunction(){
            m_impl = this;
        }

        private void init(){
            if(inited) return;
            inited = true;
            m_functions.Add("recipe", new RecipeFunction());
        }
    }
}