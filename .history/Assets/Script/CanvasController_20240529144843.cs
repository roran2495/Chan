using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CanvasController : MonoBehaviour
{
    public GameController gameController; // GameController对象
    private GameObject textObject; // 文本对象

    private GameObject button1Object; // Button1对象
    private GameObject button2Object; // Button2对象
    private GameObject button3Object; // Button3对象
    private bool flag; // 标志位 区分canvas显示时期。若为true说明游戏失败了，否则只是简单的暂停

    void Start()
    {
        
        // 获取按钮组件
        button1Object = this.transform.Find("Button1").gameObject;
        button2Object = this.transform.Find("Button2").gameObject;
        button3Object = this.transform.Find("Button3").gameObject;

        // 添加按钮点击事件
        button1Object.AddListener(OnButton2Click);

        textObject = this.transform.Find("Text").gameObject;
    }

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
            button3Object.SetActive(false);
        }
        else
        {
            textComponent.text = "Time is paused.";
            button3Object.SetActive(true);
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
