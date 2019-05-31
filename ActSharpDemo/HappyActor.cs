using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public Task<string> HelloWorld()
        {

            return ActorEnqueue(() => { WriteThreadId(); return myId.ToString() + " - Hello World!"; });

        }

        public Task<int> Add()
        {

            return ActorEnqueue(() => {

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
