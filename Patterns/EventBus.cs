using System;
using System.Collections.Generic;
using UnityEngine;

namespace PJL.Patterns
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<Action<T>> s_subscribers = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Reset() => s_subscribers.Clear();

        public static void Subscribe(Action<T> subscriber) => s_subscribers.Add(subscriber);
        public static void Unsubscribe(Action<T> subscriber) => s_subscribers.Remove(subscriber);
        public static void Invoke(T eventData)
        {
            foreach (var subscriber in s_subscribers)
                subscriber.Invoke(eventData);
        }
    }
}
