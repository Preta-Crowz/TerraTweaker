namespace Moonlight.Variable{
    class MLRaw : IVariable{
        public string Value;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLRaw(string Value){
            this.Value = Value;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            return "<Raw^" + Value + ">";
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "Raw";
        }
    }
}