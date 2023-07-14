using UnityEngine;

[System.Serializable]  // 序列化标签，用于在unity面板中编辑
public struct MapBound
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}


// 实现摄像机跟随功能，也可以使用cinemaMachine
public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;  // 摄像机跟随的玩家
    public float smoothTime = 0.4f; 
    public MapBound mapBound;

    private float _offsetZ;  // Z轴的偏移值
    private Vector3 _currentVelocity;

    private void Start() 
    {    
        if (target == null)
        {
            Debug.LogError("Can't find player, please check it!");
            return;
        }

        _offsetZ = (transform.position - target.position).z;  // Z轴的偏移值
    }

    private void FixedUpdate() {
        if (target == null) return;
        // 计算出摄像机平面里对应的目标位置，即摄像机应该移动到的位置
        Vector3 targetPosition = target.position + Vector3.forward * _offsetZ;
        // ref 关键字用于将变量的引用传递给函数，因此函数可以修改原变量的值
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, 
        ref _currentVelocity, smoothTime);

        // 限制摄像机跟随边界
        newPosition.x = Mathf.Clamp(newPosition.x, mapBound.xMin, mapBound.xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBound.yMin, mapBound.yMax);

        // 更新摄像机位置
        transform.position = newPosition;
    }

    public void ResetCamera() {
        transform.position = new Vector3(0f, 0f, -10f);
    }

}
