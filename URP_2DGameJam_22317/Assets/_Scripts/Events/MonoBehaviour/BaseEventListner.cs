using System;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 挂载收到这个事件广播的基类
/// <summary>

public class BaseEventListner<T> : MonoBehaviour
{
    public BaseEventSO<T> eventSO;
    public UnityEvent<T> response;

    private void OnEnable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        if (eventSO != null)
            eventSO.OnEventRaised -= OnEventRaised;
    }

    public void OnEventRaised(T value)
    {
        response.Invoke(value);
    }
}
