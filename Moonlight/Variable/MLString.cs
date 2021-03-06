namespace Moonlight.Variable{
    class MLString : IVariable{
        string Value;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLString(string Value){
            this.Value = Value;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            return Value;
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public void Multiply(MLInteger input){}
        public int ToInt(){return 0;}

        public string GetType(){
            return "String";
        }
    }
}