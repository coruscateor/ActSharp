using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp.Parameters
{

    public class Param<T>
    {

        public T P
        {

            get;
            set;

        }

        public void SetDefault()
        {

            P = default(T);

        }

    }

    public class Params<T1, T2>
    {

        public T1 P1
        {

            get;
            set;

        }

        public T2 P2
        {

            get;
            set;

        }

        public virtual void SetDefaults()
        {

            P1 = default(T1);

            P2 = default(T2);

        }

    }

    public class Params<T1, T2, T3> : Params<T1, T2>
    {

        public T3 P3
        {

            get;
            set;

        }

        public override void SetDefaults()
        {

            base.SetDefaults();

            P3 = default(T3);

        }

    }

    public class Params<T1, T2, T3, T4> : Params<T1, T2, T3>
    {

        public T4 P4
        {

            get;
            set;

        }

        public override void SetDefaults()
        {

            base.SetDefaults();

            P4 = default(T4);

        }

    }

}
