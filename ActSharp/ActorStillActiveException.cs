using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public class ActorStillActiveException : Exception
    {

        public ActorStillActiveException()
            : base("Actor is still active")
        {
        }

    }

}
