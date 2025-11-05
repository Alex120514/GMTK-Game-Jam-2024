using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理命名空间
using UnityEngine.UI; // 引入 UI 命名空间

public class LoadSceneOnButtonClick : MonoBehaviour
{
    public Button button; // 用于引用按钮组件

    void Start()
    {
        if (button != null)
        {
            // 为按钮的点击事件添加监听器
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button reference is missing.");
        }
    }

    // 按钮点击事件处理方法
    void OnButtonClick()
    {
        // 加载名为 "scene0" 的场景
        SceneManager.LoadScene("Scene0");
    }
}
