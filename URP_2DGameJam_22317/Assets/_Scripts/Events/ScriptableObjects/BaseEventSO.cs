using UnityEngine;
using UnityEngine.Events;

public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    //描述你这个事件
    public string description;

    public UnityAction<T> OnEventRaised;

    //最后一个事件发送者
    public string lastSender;
    //value发送的值，sender发送者一般是this（自己）
    public void RaiseEvent(T value, object sender)
    {
        OnEventRaised?.Invoke(value);
        lastSender = sender.ToString();
    }

}
