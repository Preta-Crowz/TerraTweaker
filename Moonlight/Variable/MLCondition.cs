using Moonlight.Exception;
using Terraria.ModLoader;

namespace Moonlight.Variable{
    class MLCondition : IVariable{
        public string Type, Name;
        public bool Value { get => GetValue(); }
        public bool isEnded = false;
        public bool isComplete = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLCondition(string Type){
            this.Type = Type;
        }

        public void SetName(string Name){
            this.Name = Name;
            this.isComplete = true;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            if(!isComplete) return "<Condition^" + Type + ">";
            return "<Condition^" + Type + ":" + Name + ">";
        }

        public dynamic GetValue(){
            if (!isComplete) throw new ObjectNotCompleteException();
            switch(Type) {
                case "HasMod": return ModLoader.HasMod(Name);
                default: return false;
            }
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Condition";
        }
    }
}