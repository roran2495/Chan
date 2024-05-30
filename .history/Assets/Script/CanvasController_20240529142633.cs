using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public GameController gameController; // GameController对象
    public GameObject textObject; // 文本对象
    private GameObject button3Object; // Button3对象
    private bool flag; // 标志位

    // 从其他脚本调用此函数来设置flag
    public void ActiveTime(bool flag)
    {
        this.flag = flag;

        // 获取子对象的TMP_Text组件
        TMP_Text textComponent = textObject.GetComponent<TMP_Text>();

        // 根据flag设置文本内容和Button3的激活状态
        if (flag)
        {
            textComponent.text = "You are failed!";
            button3Object.SetActive(true);
        }
        else
        {
            textComponent.text = "Time is paused.";
            button3Object.SetActive(false);
        }
    }

    // 点击button1时调用此函数
    public void OnButton1Click()
    {
        // 调用GameController的Resume函数，传递true作为参数
        gameController.Resume(true);
    }

    // 点击button2时调用此函数
    public void OnButton2Click()
    {
        // 退出游戏
        Application.Quit();
    }

    // 点击button3时调用此函数
    public void OnButton3Click()
    {
        // 继续游戏，停止时间暂停
        gameController.Resume(false);
    }
}
