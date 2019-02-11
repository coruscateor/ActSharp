# ActSharp

A light-weight actor and asynchronous method call framework.

#### ActSharp namespace:

```
using System;
using ActSharp;

namespace ActSharpDemoImplementation
{

    sealed class DemoActor : Actor
    {

        public DemoActor()
        {
        }

		public ActorTask HelloWorld()
        {

            ActorEnqueueDelegate(() => Console.WriteLine("Hello World!"));

        }

        public ActorTask<int> TwoPlusTwo()
        {

            return ActorEnqueueDelegate(() => 2 + 2);

        }

        public ActorTask<int> Add(int a, int b)
        {

            return ActorEnqueueDelegate(() => {

                return a + b;

            });

        }

    }

}
```

Usage:

```

	static void Main(string[] args)
	{

		DemoActor demo = new DemoActor();

		var hwActorTask = demo.HelloWorld();

		var tptTask = demo.TwoPlusTwo();

		var add1_2Task = demo.Add(1, 2);

		//demo.HelloWorld() is likely done by now

		Console.WriteLine("hwActorTask is done: " + hwActorTask.IsCompleted);

		Console.WriteLine("tptTask is: " + tptTask.Result);

		Console.WriteLine("add1_2Task is: " + add1_2Task.Result);

		Console.ReadLine();

		demo.Dispose();

	}

```

#### ActSharp.Async namespace:

```

	static void Main(string[] args)
	{

		Action<int> iPlusOne = (int i) => { i + 1 };

		//Call iPlusOne on a different thread by starting a new task

		var iPlusOneTask = iPlusOne.Async(1);

		Console.WriteLine("iPlusOneTask: " + iPlusOneTask.Result);

	}

```

As indicated by the example ActSharp.Async is an extension method namespace for executing delegates on thread pool threads.



#### Likely Todos:

* Add ref and out parameter permutations to the ActSharp.Async delegate extensions.
* Add ref and out parameter permutations to the ActorEnqueueDelegate method overloads in the Actor class.



#### ActSharp.System:

A library for wrapping appropriate standard library objects in actors.

More will likely be added.



#### Note:

This Library is in a pre-1.0 state and may be subject to significant changes. 


