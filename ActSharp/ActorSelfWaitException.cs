using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    class ActorSelfWaitException : Exception
    {

        public ActorSelfWaitException()
            : base("Actor cannot wait for itself to compete its work")
        {
        }

        public ActorSelfWaitException(string message) 
            : base(message)
        {
        }

    }

}
