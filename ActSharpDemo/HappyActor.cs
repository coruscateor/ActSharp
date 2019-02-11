using System;
using System.Collections.Generic;
using System.Text;
using ActSharp;

namespace ActSharpDemo
{

    sealed class HappyActor : Actor
    {

        Guid myId;

        public HappyActor()
        {

            myId = Guid.NewGuid();

        }

        public ActorTask<string> HelloWorld()
        {

            WriteThreadId();

            return ActorEnqueueDelegate(() => myId.ToString() + " - Hello World!");

        }

        public ActorTask<int> Add()
        {

            return ActorEnqueueDelegate(() => {

                Random rnd = new Random();

                WriteThreadId();

                return rnd.Next() + rnd.Next();

            });

        }

        void WriteThreadId()
        {

            Console.WriteLine();

            Console.WriteLine(myId.ToString() + " - ActorManagedThreadId: " + ActorManagedThreadId);

            Console.WriteLine();

        }

    }

}
