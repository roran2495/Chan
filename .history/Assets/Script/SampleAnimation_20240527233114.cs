using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    private Animator animator;
    private const string key_ifRun = "ifRun";
    private const string key_isForward = "forward";
    private const string key_isWalkBackward = "walkBackward";
    private const string key_isJump = "jump";
    private const string key_Blend = "Blend";
    private float blendValue;
    private float blendSpeed = 0.01f;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 设置动画参数
        if (Input.GetKeyDown(KeyCode.LeftControl))  // 点击左ctrl切换行走or跑步（默认跑步）
        {
            this.animator.SetBool(key_ifRun, !this.animator.GetBool(key_ifRun));
        }

        if (Input.GetKeyDown("w"))    // 前进
        {
            this.animator.SetBool(key_isForward, true);
            ble
        }
        else if (Input.GetKeyUp("w"))       
        {
            this.animator.SetBool(key_isForward, false);
        }

        if (Input.GetKeyDown("a"))    // 左转前进 or 向右后退
        {
            blendValue -= blendSpeed;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
        } 
        else if (Input.GetKeyUp("a"))
        {
            blendValue = 0.5f;
            this.animator.SetFloat(key_Blend, blendValue);
        }

        if (Input.GetKeyDown("s"))       // 后退
        {
            this.animator.SetBool(key_isWalkBackward, true);
        }
        else if (Input.GetKeyUp("s"))       
        {
            this.animator.SetBool(key_isWalkBackward, false);
        }

        if (Input.GetKeyDown("space"))    // 跳跃
        {
            this.animator.SetBool(key_isJump, true);
        }
        else if (Input.GetKeyUp("space"))    
        {
            this.animator.SetBool(key_isJump, false);
        }

        if (Input.GetKeyDown("d"))    // 右转前进 or 向左后退
        {
            blendValue += blendSpeed;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
        }
        else if (Input.GetKeyUp("d"))
        {
            blendValue = 0.5f;
            this.animator.SetFloat(key_Blend, blendValue);
        }
    }
}
