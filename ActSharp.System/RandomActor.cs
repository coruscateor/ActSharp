using System;
using System.Threading.Tasks;
using ActSharp;

namespace ActSharp.System
{

    public sealed class RandomActor : Actor
    {

        readonly Random myRandom;

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

            return ActorSetupNoPreDelegate(myRandom.Next);

        }

        public ActorTask<int> Next(int maxValue)
        {

            return ActorSetupNoPreDelegate(() => myRandom.Next(maxValue));

        }

        public ActorTask<int> Next(int minValue, int maxValue)
        {

            return ActorSetupNoPreDelegate(() => myRandom.Next(minValue, maxValue));

        }

        public ActorTask NextBytes(byte[] buffer)
        {

            return ActorSetupNoPreDelegate(() => myRandom.NextBytes(buffer));

        }

        public ActorTask<double> NextDouble()
        {

            return ActorSetupNoPreDelegate(myRandom.NextDouble);

        }

    }

}
