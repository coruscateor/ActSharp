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



### Changelog:

Version 0.1.3:

* Added Async Events
* Added more IEnumerable delegate extension methods

Version 0.1.2:

* Added ActorEnqueueDelegateNoTaskList methods which set up actor tasks that don't check the retained task list when executed
* Added an ActorCheckRetainedTasks method to check retained tasks independently from the ActorEnqueueDelegate task setup

Version 0.1.1:

* Added void returning overrides for methods that return Task and ActorTask
* Added ContinuationContext to RetainedTaskList for more fine-grained control over where a continuation takes place



#### ActSharp.System:

A library for wrapping appropriate standard library objects in actors.



#### Note:

This Library is in a pre-1.0 state and may be subject to significant changes. 


