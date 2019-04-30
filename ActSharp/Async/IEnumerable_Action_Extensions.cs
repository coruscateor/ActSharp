using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp.Async
{

    public static class IEnumerable_Action_Extensions
    {

        public static List<Task> Async(this IEnumerable<Action> actions)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach(var item in actions)
            {

                tasks.Add(item.Async());

            }

            return tasks;

        }

        public static List<Task> Async(this IEnumerable<Action> actions, List<Task> tasks)
        {

            tasks.Clear();

            tasks.Capacity = actions.Count();

            foreach (var item in actions)
            {

                tasks.Add(item.Async());

            }

            return tasks;

        }

        //1 Parameter

        public static List<Task> Async<T>(this IEnumerable<Action<T>> actions, T p)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p));

            }

            return tasks;

        }

        public static List<Task> Async<T>(this IEnumerable<Action<T>> actions, T p, TaskCreationOptions creationOptions)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p, creationOptions));

            }

            return tasks;

        }

        public static List<Task> Async<T>(this IEnumerable<Action<T>> actions, T p, CancellationToken cancellationToken)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p, cancellationToken));

            }

            return tasks;

        }

        public static List<Task> Async<T>(this IEnumerable<Action<T>> actions, T p, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

        //2 Parameter

        public static List<Task> Async<T1, T2>(this IEnumerable<Action<T1, T2>> actions, T1 p1, T2 p2)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2>(this IEnumerable<Action<T1, T2>> actions, T1 p1, T2 p2, TaskCreationOptions creationOptions)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, creationOptions));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2>(this IEnumerable<Action<T1, T2>> actions, T1 p1, T2 p2, CancellationToken cancellationToken)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, cancellationToken));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2>(this IEnumerable<Action<T1, T2>> actions, T1 p1, T2 p2, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

        //3 Parameter

        public static List<Task> Async<T1, T2, T3>(this IEnumerable<Action<T1, T2, T3>> actions, T1 p1, T2 p2, T3 p3)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2, T3>(this IEnumerable<Action<T1, T2, T3>> actions, T1 p1, T2 p2, T3 p3, TaskCreationOptions creationOptions)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, creationOptions));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2, T3>(this IEnumerable<Action<T1, T2, T3>> actions, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, cancellationToken));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2, T3>(this IEnumerable<Action<T1, T2, T3>> actions, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

        //4 Parameter

        public static List<Task> Async<T1, T2, T3, T4>(this IEnumerable<Action<T1, T2, T3, T4>> actions, T1 p1, T2 p2, T3 p3, T4 p4)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, p4));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2, T3, T4>(this IEnumerable<Action<T1, T2, T3, T4>> actions, T1 p1, T2 p2, T3 p3, T4 p4, TaskCreationOptions creationOptions)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, p4, creationOptions));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2, T3, T4>(this IEnumerable<Action<T1, T2, T3, T4>> actions, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, p4, cancellationToken));

            }

            return tasks;

        }

        public static List<Task> Async<T1, T2, T3, T4>(this IEnumerable<Action<T1, T2, T3, T4>> actions, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task> tasks = new List<Task>(actions.Count());

            foreach (var item in actions)
            {

                tasks.Add(item.Async(p1, p2, p3, p4, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

    }

}
