// using Terraria.ModLoader;

namespace Moonlight.Variable{
    interface IVariable{
        string ToString();
        string ToString(bool rec);
        MLItem GetItem(IVariable raw);
        MLTile GetTile(IVariable raw);
        void Multiply(MLInteger input);
        int ToInt();
        dynamic GetValue();
        bool isEnd();
        string GetType();
    }
}