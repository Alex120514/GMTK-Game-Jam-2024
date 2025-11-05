using UnityEngine;

public class BackgroundFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // 相机对象的 Transform
    public Vector2 parallaxEffect; // 平移效果因子

    private Vector3 lastCameraPosition;

    void Start()
    {
        // 记录相机的初始位置
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // 计算相机移动的增量
        Vector3 cameraMovement = cameraTransform.position - lastCameraPosition;

        // 应用平移效果到背景
        transform.position += new Vector3(cameraMovement.x * parallaxEffect.x, cameraMovement.y * parallaxEffect.y, 0);

        // 更新背景位置
        lastCameraPosition = cameraTransform.position;
    }
}
