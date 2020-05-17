namespace Moonlight.Variable{
    class MLString : IVariable{
        string Value;

        public MLString(string Value){
            this.Value = Value;
        }

        public string ToString(){
            return Value;
        }

        public dynamic GetValue(){
            return Value;
        }

        public MLItem GetItem(string name){return new MLItem();}
        public MLTile GetTile(string name){return new MLTile();}
        public void Multiply(int multi){}
        public int ToInt(){return 0;}
    }
}