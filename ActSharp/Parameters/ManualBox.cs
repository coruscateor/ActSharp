using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp.Parameters
{

    public class ManualBox<T>
        where T : struct
    {

        public T Value
        {

            get;
            set;

        }

        public void SetDefault()
        {

            Value = default(T);

        }

    }

}
