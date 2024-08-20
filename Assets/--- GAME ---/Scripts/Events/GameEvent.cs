using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    
    private event Action action = delegate { };

    public void Invoke()
    {
        action?.Invoke();
    }

    public void Add(Action subscriber)
    {
        action += subscriber;
    }

    public void Remove(Action subscriber)
    {
        action -= subscriber;
    }
}
public class GameEvent<T>
{

    private event Action<T> action = delegate { };

    public void Invoke(T param)
    {
        action?.Invoke(param);
    }

    public void Add(Action<T> subscriber)
    {
        action += subscriber;
    }

    public void Remove(Action<T> subscriber)
    {
        action -= subscriber;
    }
}
