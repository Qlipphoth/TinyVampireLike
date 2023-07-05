using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component  // 限定泛型必须继承自Compent
{
    public static T Instance { get; private set; }

    /// <summary>
    /// 获取单例
    /// </summary>
    protected virtual void Awake() {
        Instance = this as T;  // 强转为 T
    }
}
