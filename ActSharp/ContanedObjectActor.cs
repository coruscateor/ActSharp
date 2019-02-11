using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public class ContanedObjectActor<T> : Actor
    {

        protected T Obj
        {

            get;
            set;

        }

    }

}
