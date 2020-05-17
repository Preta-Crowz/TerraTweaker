using Moonlight.Compile;
using Moonlight.Variable;

namespace Moonlight.Compile.Functions{
    class IdentityFunction : ParserFunction{
        protected override IVariable Eval(string data, ref int from){
            return Split.Parse(data, ref from, Split.END_ARG);
    }}
}