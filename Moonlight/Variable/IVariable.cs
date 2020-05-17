// using Terraria.ModLoader;

namespace Moonlight.Variable{
    interface IVariable{
        string ToString();
        MLItem GetItem(string name);
        MLTile GetTile(string name);
        void Multiply(int multi);
        int ToInt();
        dynamic GetValue();
    }
}