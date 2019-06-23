using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ActSharp
{
    public static class IActorExtensions
    {

        public static void ActorWaitAll(this IEnumerable<IActor> actors)
        {

            foreach (var actor in actors)
                actor.ActorWaitForIdle();

        }

        public static void ActorWaitAll(params IActor[] actors)
        {

            ActorWaitAll(actors);

        }

        public static int ActorWaitAny(this IEnumerable<IActor> actors)
        {

            int idleActorIndex = -1;

            SpinWait sw = new SpinWait();

            do
            {

                int i = -1;

                foreach (var actor in actors)
                {

                    if (!actor.ActorIsActive)
                    {

                        idleActorIndex = i;

                        break;

                    }

                    i++;

                    sw.SpinOnce();

                }

            } while (idleActorIndex < 0);

            return idleActorIndex;

        }

        public static int ActorWaitAny(params IActor[] actors)
        {

            return ActorWaitAny(actors);

        }

    }

}
