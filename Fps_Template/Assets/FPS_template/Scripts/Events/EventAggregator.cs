using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventAggregator 
{
    private static readonly Dictionary<Type, object> _events = new Dictionary<Type, object>();

    public static TEventType GetEvent<TEventType>() where TEventType : class, new()
    {
        if (_events.TryGetValue(typeof(TEventType), out object result))
        {
            return (TEventType)result;
        }
        else
        {
            TEventType newEvent = new TEventType();
            _events[typeof(TEventType)] = newEvent;
            return newEvent;
        }
    }

    public static void Subscribe(Action action)
    {
        object eventContainer;
        if (!_events.TryGetValue(typeof(PubSubEvent), out eventContainer))
        {
            eventContainer = new PubSubEvent();
            _events[typeof(PubSubEvent)] = eventContainer;
        }

        ((PubSubEvent)eventContainer).Subscribe(action);
    }
    public static void Subscribe<TEvent>(Action<TEvent> action)
    {
        object eventContainer;
        if (!_events.TryGetValue(typeof(TEvent), out eventContainer))
        {
            eventContainer = new PubSubEvent<TEvent>();
            _events[typeof(TEvent)] = eventContainer;
        }

        ((PubSubEvent<TEvent>)eventContainer).Subscribe(action);
    }

    public static void Unsubscribe(Action action)
    {
        object eventContainer;
        if(_events.TryGetValue(typeof(PubSubEvent), out eventContainer))
        {
            ((PubSubEvent)eventContainer).Unsubscribe(action);
        }
    }
    public static void Unsubscribe<TEvent>(Action<TEvent> action)
    {
        object eventContainer;
        if (_events.TryGetValue(typeof(TEvent), out eventContainer))
        {
            ((PubSubEvent<TEvent>)eventContainer).Unsubscribe(action);
        }
    }

    public static void Publish()
    {
        object eventContainer;
        if (_events.TryGetValue(typeof(PubSubEvent), out eventContainer))
        {
            ((PubSubEvent)eventContainer).Publish();
        }
    }
    public static void Publish<TEvent>(TEvent payload)
    {
        object eventContainer;
        if (_events.TryGetValue(typeof(TEvent), out eventContainer))
        {
            ((PubSubEvent<TEvent>)eventContainer).Publish(payload);
        }
    }
}

public class PubSubEvent
{
    private readonly List<Action> _subscribers = new List<Action>();

    public void Subscribe(Action action)
    {
        _subscribers.Add(action);
    }

    public void Unsubscribe(Action action)
    {
        _subscribers.Remove(action);
    }

    public void Publish()
    {
        _subscribers.ForEach(subscriber => subscriber());
    }
}


public class PubSubEvent<TEvent>
{
    private readonly List<Action<TEvent>> _subscribers = new List<Action<TEvent>>();

    public void Subscribe(Action<TEvent> action)
    {
        _subscribers.Add(action);
    }

    public void Unsubscribe(Action<TEvent> action)
    {
        _subscribers.Remove(action);
    }

    public void Publish(TEvent payload)
    {
        _subscribers.ForEach(subscriber => subscriber(payload));
    }
}
