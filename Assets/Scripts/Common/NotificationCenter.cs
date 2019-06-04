using System;
using System.Collections.Generic;


public static class NotificationCenter
{

    private static Dictionary<Type, _NotificationCenter> managers;

    public static void AddListener<T>(Action<T> listener) where T : Notification
    {
        Type eventType = typeof(T);

        if (managers == null)
        {
            managers = new Dictionary<Type, _NotificationCenter>();
        }

        if (managers.ContainsKey(eventType) == false || managers[eventType] == null)
        {
            managers[eventType] = new _NotificationCenter<T>();
        }

        ((_NotificationCenter<T>)managers[eventType]).Add(listener);

    }

    public static void RemoveListener<T>(Action<T> listener) where T : Notification
    {
        Type eventType = typeof(T);
        if (managers == null || managers[eventType] == null)
        {
            return;
        }
        ((_NotificationCenter<T>)managers[eventType]).Remove(listener);
    }

    public static void FireEvent<T>(T notification) where T : Notification
    {
        Type eventType = typeof(T);

        // only the projectile subscribes to the addListener function
        // if we call this on the hero or enemy units it throws an error
        if (!managers.ContainsKey(eventType))
        {
            return;
        }
        else
        if (managers == null || managers[eventType] == null)
        {
            return;
        }
        ((_NotificationCenter<T>)managers[eventType]).Fire(notification);
    }

    private abstract class _NotificationCenter { };
    private class _NotificationCenter<T> : _NotificationCenter where T : Notification
    {
        private event Action<T> listeners;

        public void Add(Action<T> listener)
        {
            listeners += listener;
        }

        public void Remove(Action<T> listener)
        {
            listeners -= listener;
        }

        public void Fire(T Notification)
        {
            listeners(Notification);
        }
    }
}