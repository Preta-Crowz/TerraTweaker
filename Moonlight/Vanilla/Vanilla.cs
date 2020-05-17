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
        }

        public VItem GetItem(string name){
            return new VItem(name);
        }

        public VTile GetTile(string name){
            return new VTile(name);
        }

        public string ToString(){
            return "Vanilla";
        }
    }
}