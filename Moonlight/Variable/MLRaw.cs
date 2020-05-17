namespace Moonlight.Variable{
    class MLRaw : IVariable{
        string Value;

        public MLRaw(string Value){
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