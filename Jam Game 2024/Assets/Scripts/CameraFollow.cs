using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 摄像头要跟随的对象
    public float smoothSpeed = 0.125f; // 平滑速度
    public Vector3 offset; // 摄像头相对于目标的偏移量

    void LateUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
