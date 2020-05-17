namespace Moonlight.Variable{
    class MLInteger : IVariable{
        int Value;

        public MLInteger(int Value){
            this.Value = Value;
        }

        public void Multiply(int multi){
            this.Value *= multi;
        }

        public string ToString(){
            return this.Value.ToString();
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public int ToInt(){return Value;}
    }
}