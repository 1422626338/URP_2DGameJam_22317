using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BaseEventSO<>))]
/// <summary>
/// 只是帮助开发，最后不会被打包。
/// <summary>

public class BaseEventSOEditor<T>: Editor
{
    private BaseEventSO<T> baseEventSO;

    private void OnEnable()
    {
        if (baseEventSO == null)
            baseEventSO = target as BaseEventSO<T>;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("订阅数量:" + GetLiseners().Count);
        foreach (var lisener in GetLiseners())
        {
            EditorGUILayout.LabelField(lisener.ToString());
        }
    }

    //获得所有监听这个事件的listner
    private List<MonoBehaviour> GetLiseners()
    {
        List<MonoBehaviour> liseners = new();

        if (baseEventSO == null || baseEventSO.OnEventRaised == null)
            return liseners;

        var subscribres = baseEventSO.OnEventRaised.GetInvocationList();

        foreach (var sub in subscribres)
        {
            var obj = sub.Target as MonoBehaviour;
            if (!liseners.Contains(obj))
            {
                liseners.Add(obj);
            }
        }
        return liseners;
    }
}
