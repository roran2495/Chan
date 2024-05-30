using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    private Animator animator;
    private const string key_ifRun = "ifRun";
    private const string key_isForward = "forward";
    private const string key_isWalkBackward = "walkBackward";
    private const string key_isJump = "Jump";
    private const string key_Blend = "Blend";
    private CharacterController characterController;
    private float blendValue = 0.5f;
    private float blendSpeed = 0.01f;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 设置动画参数
        if (Input.GetKeyDown(KeyCode.LeftControl))  // 点击左ctrl切换行走or跑步（默认跑步）
        {
            this.animator.SetBool(key_ifRun, !this.animator);
        }

        if (Input.GetKeyDown("w"))    // 前进
        {
            this.animator.SetBool(key_isRun, isRunning);
            this.animator.SetBool(key_isWalkForward, !isRunning);
            Debug.Log(this.animator.GetBool(key_isRun));
            Debug.Log(this.animator.GetBool(key_isWalkForward));
            Debug.Log(this.animator.GetBool(key_isJump));
            Debug.Log(this.animator.GetBool(key_isWalkBackward));
        }

        if (Input.GetKeyDown("a"))    // 左转前进 or 向右后退
        {
            blendValue -= blendSpeed;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
        }

        if (Input.GetKeyDown("s"))       // 后退
        {
            this.animator.SetBool(key_isWalkBackward, true);
        }

        if (Input.GetKeyDown("space"))    // 跳跃
        {
            this.animator.SetBool(key_isJump, true);
        }

        if (Input.GetKeyDown("d"))    // 右转前进 or 向左后退
        {
            blendValue += blendSpeed;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
        }

        // 重置行走后退状态和跳跃状态
        if (Input.GetKeyUp("s"))       
        {
            this.animator.SetBool(key_isWalkBackward, false);
        }

        if (Input.GetKeyUp("space"))    
        {
            this.animator.SetBool(key_isJump, false);
        }
    }
}
