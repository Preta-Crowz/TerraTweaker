using System;

namespace Moonlight.Exception {
    class ObjectNotCompleteException : System.Exception {
        public ObjectNotCompleteException() : base("Referenced object is not complete. Some values may missing.") {}
    }
}