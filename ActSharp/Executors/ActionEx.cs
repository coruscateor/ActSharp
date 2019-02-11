using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp.Executors
{

    class ActionEx : BaseEx
    {

        Action myAction;

        public ActionEx(Action action)
        {

            myAction = action;

        }

        public override void Execute()
        {

            myAction();

        }

    }

}
