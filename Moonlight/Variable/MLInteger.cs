namespace Moonlight.Variable{
    class MLInteger : IVariable{
        public int Value;
        public bool isEnded = false;

        public bool isEnd(){
            return isEnded;
        }

        public MLInteger(int Value){
            this.Value = Value;
        }

        public void Multiply(MLInteger input){
            this.Value *= input.Value;
            this.isEnded = input.isEnded;
        }

        public string ToString(){
            return this.ToString(false);
        }

        public string ToString(bool rec = false){
            if(isEnded && !rec) return this.ToString(true)+"!";
            return this.Value.ToString();
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(IVariable raw){return new MLItem();}
        public MLTile GetTile(IVariable raw){return new MLTile();}
        public int ToInt(){return Value;}

        public string GetType(){
            return "Integer";
        }
    }
}