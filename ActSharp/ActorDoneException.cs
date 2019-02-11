using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public class ActorDoneException : Exception
    {

        public ActorDoneException()
        {
        }

        public ActorDoneException(string message) 
            : base(message)
        {
        }

    }

}
