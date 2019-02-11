using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp.Async
{

    public static class IEnumerable_Extensions
    {

        //Tasks

        public static void WaitAll(this IEnumerable<Task> items)
        {

            foreach(var item in items)
            {

                item.Wait();

            }

        }

        public static void WaitAll(this IEnumerable<Task> items, int millisecondsTimeout)
        {

            bool waited = false;

            foreach (var item in items)
            {

                bool result;

                result = item.Wait(millisecondsTimeout);

                if (!waited)
                    waited = result;

            }

        }

        public static void WaitAll(this IEnumerable<Task> items, CancellationToken cancellationToken)
        {

            foreach (var item in items)
            {

                item.Wait(cancellationToken);

            }

        }

        public static void WaitAll(this IEnumerable<Task> items, TimeSpan timeOut)
        {

            bool waited = false;

            foreach (var item in items)
            {

                bool result;

                result = item.Wait(timeOut);

                if (!waited)
                    waited = result;

            }

        }

        public static void WaitAll(this IEnumerable<Task> items, int millisecondsTimeout, CancellationToken cancellationToken)
        {

            bool waited = false;

            foreach (var item in items)
            {

                bool result;

                result = item.Wait(millisecondsTimeout, cancellationToken);

                if (!waited)
                    waited = result;

            }

        }

        //IActorTasks

        public static void WaitAll(this IEnumerable<IActorTask> items)
        {

            foreach (var item in items)
            {

                item.Wait();

            }

        }

        public static void WaitAll(this IEnumerable<IActorTask> items, int millisecondsTimeout)
        {

            bool waited = false;

            foreach (var item in items)
            {

                bool result;

                result = item.Wait(millisecondsTimeout);

                if (!waited)
                    waited = result;

            }

        }

        public static void WaitAll(this IEnumerable<IActorTask> items, CancellationToken cancellationToken)
        {

            foreach (var item in items)
            {

                item.Wait(cancellationToken);

            }

        }

        public static void WaitAll(this IEnumerable<IActorTask> items, TimeSpan timeOut)
        {

            bool waited = false;

            foreach (var item in items)
            {

                bool result;

                result = item.Wait(timeOut);

                if (!waited)
                    waited = result;

            }

        }

        public static void WaitAll(this IEnumerable<IActorTask> items, int millisecondsTimeout, CancellationToken cancellationToken)
        {

            bool waited = false;

            foreach (var item in items)
            {

                bool result;

                result = item.Wait(millisecondsTimeout, cancellationToken);

                if (!waited)
                    waited = result;

            }

        }

    }

}
