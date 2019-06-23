using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public enum ContinuationContext
    {

        Actor,
        Async,
        Immediate

    }

    public static class EnumsEtc
    {

        public static bool IsActor(this ContinuationContext continuationContext)
        {

            return continuationContext == ContinuationContext.Actor;

        }

        public static bool IsAsync(this ContinuationContext continuationContext)
        {

            return continuationContext == ContinuationContext.Async;

        }

        public static bool IsImmediate(this ContinuationContext continuationContext)
        {

            return continuationContext == ContinuationContext.Immediate;

        }

        public static bool CheckIsActor(this ContinuationContext continuationContext, Action isActorAction, Action elseAction = null)
        {

            bool isActor = continuationContext == ContinuationContext.Actor;

            if (isActor)
                isActorAction();
            else elseAction?.Invoke();

            return isActor;

        }

    }

}
