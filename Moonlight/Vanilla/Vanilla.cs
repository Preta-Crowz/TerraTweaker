using Moonlight.Variable;

namespace Moonlight.Vanilla{
    class Vanilla : MLMod{
        private static Vanilla instance = null;

        public static Vanilla Get(){
            if(instance == null)
                instance = new Vanilla();
            return instance;
        }

        public bool HasInstance(){
            return instance != null;
        }

        public Vanilla(){
            isVanilla = true;
        }
    }
}