using System;

namespace Moonlight.Exception {
    class ModNotFoundException : System.Exception {
        public ModNotFoundException() : base("Mod not found on tML. Is the mod disabled?") {}
        public ModNotFoundException(string name) : base("Mod " + name + " not found on tML. Is the mod disabled?") {}
    }
}