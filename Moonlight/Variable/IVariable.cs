// using Terraria.ModLoader;

namespace Moonlight.Variable{
    interface IVariable{
        string ToString();
        string ToString(bool rec);
        MLItem GetItem(string name);
        MLTile GetTile(string name);
        void Multiply(MLInteger input);
        int ToInt();
        dynamic GetValue();
        bool isEnd();
    }
}