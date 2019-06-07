# ActSharp

A light-weight actor and asynchronous programming framework for .NET.

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

		public Task HelloWorld()
		{

			return ActorEnqueue(() => Console.WriteLine("Hello World!"));

		}

		public Task<int> TwoPlusTwo()
		{

			return ActorEnqueue(() => 2 + 2);

		}

		public Task<int> Add(int a, int b)
		{

			return ActorEnqueue(() => {

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

		var hwTask = demo.HelloWorld();

		var tptTask = demo.TwoPlusTwo();

		var add1_2Task = demo.Add(1, 2);

		//demo.HelloWorld() is likely done by now

		Console.WriteLine("hwTask is done: " + hwTask.IsCompleted);

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

### To Do:

* Add a unit tests project
* Finalise the API and bump to 1.0 (it's practically finalised now)
* Re-do the expamle project

### Changelog:

Version 0.3.0:

* Added an ActorIsIdle property to the Actor class
* Added UnSafeAsync static methods to the Async Action extensions
* Added AsyncWait and UnsafeAsyncWait static methods to the Async Action extensions
* All projects now target .NET Core 2.2

Version 0.2.0:

* Refactored the Actor class to reduce complexity
* Fixed a WriteThreadId bug in the ActSharpDemo
* Added happyActor.ActorIsActive output to the ActSharpDemo

Version 0.1.6:

* Added a Call_ method to ActionAsyncEvent to return a list of tasks when called

Version 0.1.5:

* ActorEnqueueFailFast and ActorEnqueueFailFastNoTaskListCheck now FailFast immediately on a caught exception
* ActionAsyncEvent now has a Task returning Call method
* Async_Action_Extensions void returning methods now call ThreadPool.QueueUserWorkItem
* RetainedTaskList no longer requires a ConcurrentQueue<Task> instance on initialisation, now has a HasPrerequisites property, can be checked with or without a ConcurrentQueue<Task> instance

Version 0.1.4:

* Simplified the API

Version 0.1.3:

* Added Async Events
* Added more IEnumerable delegate extension methods

Version 0.1.2:

* Added ActorEnqueueNoTaskList methods which set up actor tasks that don't check the retained task list when executed
* Added an ActorCheckRetainedTasks method to check retained tasks independently from the ActorEnqueue task setup

Version 0.1.1:

* Added void returning overrides for methods that return Task and Task
* Added ContinuationContext to RetainedTaskList for more fine-grained control over where a continuation takes place



#### ActSharp.System:

A library for wrapping appropriate standard library objects in actors.



#### Note:

This Library is in a pre-1.0 state and may be subject to significant changes. 


