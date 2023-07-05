using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component  // 限定泛型必须继承自Compent
{
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance == null) {
            Instance = this as T;  // 强转为 T    
        } 
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
