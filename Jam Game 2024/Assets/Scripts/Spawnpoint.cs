using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public ResizeGrounds11 resizeGrounds; // 引用 ResizeGrounds11 组件
    private bool isCollidingWithPlayer = false; // 用于检测是否正在与玩家碰撞

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否与标签为 "Player" 的对象发生碰撞
        if (other.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 当玩家离开碰撞区时重置标志
        if (other.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }

    private void Update()
    {
        // 检查是否按下了 E 键，并且正在与玩家碰撞
        if (isCollidingWithPlayer && Input.GetKeyDown(KeyCode.E))
        {
            if (resizeGrounds != null)
            {
                // 输出 ResizeGrounds11 中的 buttonNumber
                Debug.Log($"Current buttonNumber from ResizeGrounds11: {resizeGrounds.Currentbutton}");
            }
            else
            {
                Debug.LogError("ResizeGrounds11 component is not assigned.");
            }
        }
    }
}
