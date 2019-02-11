using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp.Parameters
{

    public class Ref<T>
        where T : struct
    {

        public T Value
        {

            get;
            set;

        }

    }

}
