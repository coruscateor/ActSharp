using System;
using System.Threading.Tasks;
using ActSharp;

namespace ActSharp.System
{

    public sealed class RandomActor : Actor
    {

        Random myRandom;

        public RandomActor()
        {

            myRandom = new Random();

        }

        public RandomActor(int seed)
        {

            myRandom = new Random(seed);

        }

        public Task<int> Next()
        {

            return ActorEnqueueNoTaskListCheck(myRandom.Next);

        }

        public Task<int> Next(int maxValue)
        {

            return ActorEnqueueNoTaskListCheck(() => myRandom.Next(maxValue));

        }

        public Task<int> Next(int minValue, int maxValue)
        {

            return ActorEnqueueNoTaskListCheck(() => myRandom.Next(minValue, maxValue));

        }

        public Task NextBytes(byte[] buffer)
        {

            return ActorEnqueueNoTaskListCheck(() => myRandom.NextBytes(buffer));

        }

        public Task<double> NextDouble()
        {

            return ActorEnqueueNoTaskListCheck(myRandom.NextDouble);

        }

    }

}
