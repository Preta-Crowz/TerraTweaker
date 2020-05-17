using System;
using System.Collections.Generic;

using Moonlight.Compile;
using Moonlight.Variable;

namespace Moonlight.Compile.Functions{
    class RecipeFunction : ParserFunction{
        protected override IVariable Eval(string data, ref int from){
            throw new ArgumentException("test\n"+data);
            return new MLRecipe(new MLItem());
    }}
}