using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActSharp.Async
{

    public static class IEnumerable_Func_Extensions
    {

        public static List<Task<TResult>> Async<TResult>(this IEnumerable<Func<TResult>> funcs)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async());

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<TResult>(this IEnumerable<Func<TResult>> funcs, List<Task<TResult>> tasks)
        {

            tasks.Clear();

            tasks.Capacity = funcs.Count();

            foreach (var item in funcs)
            {

                tasks.Add(item.Async());

            }

            return tasks;

        }

        //1 Parameter

        public static List<Task<TResult>> Async<T, TResult>(this IEnumerable<Func<T, TResult>> funcs, T p)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T, TResult>(this IEnumerable<Func<T, TResult>> funcs, T p, TaskCreationOptions creationOptions)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p, creationOptions));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T, TResult>(this IEnumerable<Func<T, TResult>> funcs, T p, CancellationToken cancellationToken)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p, cancellationToken));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T, TResult>(this IEnumerable<Func<T, TResult>> funcs, T p, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

        //2 Parameter

        public static List<Task<TResult>> Async<T1, T2, TResult>(this IEnumerable<Func<T1, T2, TResult>> funcs, T1 p1, T2 p2)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, TResult>(this IEnumerable<Func<T1, T2, TResult>> funcs, T1 p1, T2 p2, TaskCreationOptions creationOptions)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, creationOptions));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, TResult>(this IEnumerable<Func<T1, T2, TResult>> funcs, T1 p1, T2 p2, CancellationToken cancellationToken)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, cancellationToken));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, TResult>(this IEnumerable<Func<T1, T2, TResult>> funcs, T1 p1, T2 p2, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

        //3 Parameter

        public static List<Task<TResult>> Async<T1, T2, T3, TResult>(this IEnumerable<Func<T1, T2, T3, TResult>> funcs, T1 p1, T2 p2, T3 p3)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, T3, TResult>(this IEnumerable<Func<T1, T2, T3, TResult>> funcs, T1 p1, T2 p2, T3 p3, TaskCreationOptions creationOptions)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, creationOptions));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, T3, TResult>(this IEnumerable<Func<T1, T2, T3, TResult>> funcs, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, cancellationToken));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, T3, TResult>(this IEnumerable<Func<T1, T2, T3, TResult>> funcs, T1 p1, T2 p2, T3 p3, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

        //4 Parameter

        public static List<Task<TResult>> Async<T1, T2, T3, T4, TResult>(this IEnumerable<Func<T1, T2, T3, T4, TResult>> funcs, T1 p1, T2 p2, T3 p3, T4 p4)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, p4));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, T3, T4, TResult>(this IEnumerable<Func<T1, T2, T3, T4, TResult>> funcs, T1 p1, T2 p2, T3 p3, T4 p4, TaskCreationOptions creationOptions)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, p4, creationOptions));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, T3, T4, TResult>(this IEnumerable<Func<T1, T2, T3, T4, TResult>> funcs, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, p4, cancellationToken));

            }

            return tasks;

        }

        public static List<Task<TResult>> Async<T1, T2, T3, T4, TResult>(this IEnumerable<Func<T1, T2, T3, T4, TResult>> funcs, T1 p1, T2 p2, T3 p3, T4 p4, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {

            List<Task<TResult>> tasks = new List<Task<TResult>>(funcs.Count());

            foreach (var item in funcs)
            {

                tasks.Add(item.Async(p1, p2, p3, p4, cancellationToken, creationOptions, scheduler));

            }

            return tasks;

        }

    }

}
