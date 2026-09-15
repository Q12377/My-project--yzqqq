using UnityEngine;
using Vuforia;

public class SpinOnTrack : MonoBehaviour
{
    public Transform targetToRotate;          // 指向 ContentRoot（不设则默认旋转自己）
    public Vector3 axis = Vector3.up;         // 平放在桌面就用 up；竖直海报可用 Vector3.forward
    public float speed = 45f;                 // 旋转速度（度/秒）

    private ObserverBehaviour observer;
    private bool tracking;

    void Awake()
    {
        observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
            observer.OnTargetStatusChanged += OnStatusChanged;

        if (targetToRotate == null) targetToRotate = transform; // 兜底
    }

    void OnDestroy()
    {
        if (observer != null)
            observer.OnTargetStatusChanged -= OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour b, TargetStatus s)
    {
        var st = s.Status;
        tracking = (st == Status.TRACKED || st == Status.EXTENDED_TRACKED);
    }

    void Update()
    {
        if (tracking && targetToRotate != null)
            targetToRotate.Rotate(axis, speed * Time.deltaTime, Space.Self);
    }
}
