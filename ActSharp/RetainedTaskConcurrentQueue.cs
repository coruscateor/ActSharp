using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ActSharp.Async;

namespace ActSharp
{

    public class RetainedTaskConcurrentQueue
    {

        ConcurrentQueue<IRetainedTaskContainer> myQueue = new ConcurrentQueue<IRetainedTaskContainer>();

        public int Count
        {

            get
            {

                return myQueue.Count;

            }

        }

        public bool IsEmpty
        {

            get
            {

                return myQueue.IsEmpty;

            }

        }

        public void Enqueue(Task task, Action<Task, ContinuationContext> action = null)
        {

            CheckTaskIsScheduled(task);

            myQueue.Enqueue(new RetainedTaskContainer(task, action));

        }

        public bool Check()
        {

            IRetainedTaskContainer t; //<TTask>

            bool found = false;

            while (myQueue.TryPeek(out t) && t.IsCompleted)
            {

                if (myQueue.TryDequeue(out t) && t.IsCompleted)
                {

                    //Action<IRetainedTaskContainer> act = myQueue.Enqueue;

                    t.Check(myQueue.Enqueue);

                    found = true;

                }
                else
                {

                    myQueue.Enqueue(t);

                    break;

                }

            }

            return found;

        }

        public bool Check(Action<IRetainedTaskContainer> actorExQueueAction)
        {

            IRetainedTaskContainer t;

            bool found = false;

            while (myQueue.TryPeek(out t) && t.IsCompleted)
            {

                if (myQueue.TryDequeue(out t) && t.IsCompleted)
                {

                    t.Check(actorExQueueAction);

                    found = true;

                }
                else
                {

                    myQueue.Enqueue(t);

                    break;

                }

            }

            return found;

        }

        void CheckTaskIsScheduled(Task task)
        {

            if (task.Status == TaskStatus.Created)
                throw new TaskNotScheduledException();

        }

    }

}
