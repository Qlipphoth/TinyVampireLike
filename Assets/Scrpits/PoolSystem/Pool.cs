using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject Prefab => prefab;  // 相当于 { get {return prefab}; }
    public int Size => size;
    public int RuntimeSize => queue.Count;  // 对象池实际大小
    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;  // 对象池大小
    Queue<GameObject> queue;
    Transform parent;

    // 对象池初始化
    public void Initialize(Transform parent) {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for (var i = 0; i < size; i++) {
            queue.Enqueue(Copy());
        }
    }

    // 生成新的对象
    GameObject Copy() {
        var copy = GameObject.Instantiate(prefab, parent);  // 这个重载的第二个参数就是为物体设置父节点
        copy.SetActive(false);
        return copy;
    }

    // 从对象池中返回一个对象
    GameObject AvailableObject() {
        GameObject availableObject = null;
        // 当队列不为空且第一个元素未被使用时，返回队首对象，否则重新生成对象
        availableObject = (queue.Count > 0 && !queue.Peek().activeSelf) ? queue.Dequeue() : Copy();
        queue.Enqueue(availableObject);  // 启用后再返回队列，但是要避免重复启用的情况
        return availableObject;
    }

    // 启用对象池中的对象
    public GameObject preparedObject() {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position) {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position, Quaternion rotation) {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }

    public GameObject preparedObject(Vector3 position, Quaternion rotation, Vector3 localScale) {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        return preparedObject;
    }

}
