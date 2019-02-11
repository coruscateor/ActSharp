using System;
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

        public ActorTask<int> Next()
        {

            return ActorEnqueueDelegate(myRandom.Next);

        }

        public ActorTask<int> Next(int maxValue)
        {

            return ActorEnqueueDelegate(myRandom.Next, maxValue);

        }

        public ActorTask<int> Next(int minValue, int maxValue)
        {

            return ActorEnqueueDelegate(myRandom.Next, minValue, maxValue);

        }

        public ActorTask NextBytes(byte[] buffer)
        {

            return ActorEnqueueDelegate(myRandom.NextBytes, buffer);

        }

        public ActorTask<double> NextDouble()
        {

            return ActorEnqueueDelegate(myRandom.NextDouble);

        }

    }

}
