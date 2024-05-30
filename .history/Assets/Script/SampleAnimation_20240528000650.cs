using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    private Animator animator;
    private const string key_ifRun = "ifRun";
    private const string key_isForward = "forward";
    private const string key_isWalkBackward = "walkBackward";
    private const string key_isJump = "jump";
    private const string key_Blend = "Blend";
    private float blendValue = 0.5f;
    private float blendSpeed = 0.1f;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.animator.SetFloat(key_Blend, blendValue);
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
        }
        else if (Input.GetKeyUp("w"))       
        {
            this.animator.SetBool(key_isForward, false);
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
    }
    void FixedUpdate()
    {
        // 右转前进或向左后退
        if (Input.GetKeyDown("d"))
        {
            isDKeyPressed = true;
            dKeyDownTime = Time.time;
        }
        else if (Input.GetKeyUp("d"))
        {
            isDKeyPressed = false;
            blendValue = 0.5f;
            this.animator.SetFloat(key_Blend, blendValue);
        }

        // 更新 blendValue
        if (isDKeyPressed)
        {
            float elapsedTime = Time.time - dKeyDownTime;
            blendValue += blendSpeed * elapsedTime;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
            Debug.Log(blendValue);
        }
}
