using Moonlight.Variable;

namespace Moonlight.Compile.Processor{
    static class RemoveProcessor{
        static MLMod tt = new MLMod("TerraTweaker");

        public static MLRemove Work(MLArray args){
            return new MLRemove((MLItem)args[0]);
        }
    }
}