using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    private Animator animator;
    private const string key_isRun = "Run";
    private const string key_isWalkForward = "walkForward";
    private const string key_isWalkBackward = "walkBackward";
    private const string key_isJump = "Jump";
    private const string key_Blend = "Blend";
    private CharacterController characterController;
    private float blendValue = 0.5f;
    private float blendSpeed = 0.01f;
    private bool runOrWalk = false; // false : run

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        // 设置动画参数
        if (Input.GetKey(KeyCode.LeftControl))  // 点击左ctrl切换行走or跑步（默认跑步）
        {
            runOrWalk = !runOrWalk;
        }

        if (Input.GetKeyUp("w"))    // 前进
        {
            Debug.Log(runOrWalk);
            this.animator.SetBool(key_isRun, !runOrWalk);
            this.animator.SetBool(key_isWalkForward, runOrWalk);
            Debug.Log(this.animator.Get);
        }
        else
        {
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isWalkForward, false);
        }

        if (Input.GetKeyUp("a"))    // 左转前进 or 向右后退
        {
            this.animator.SetFloat(key_Blend, Mathf.Clamp(blendValue - blendSpeed * Time.deltaTime , 0f , 1f));
        }
        else
        {
            this.animator.SetFloat(key_Blend, blendValue);
        }

        if (Input.GetKeyUp("s"))       // 后退
        {
            this.animator.SetBool(key_isWalkBackward, true);
        }
        else
        {
            this.animator.SetBool(key_isWalkBackward, false);
        }

        if (Input.GetKeyUp("space"))    // 跳跃
        {
            this.animator.SetBool(key_isJump, true);
        }
        else
        {
            this.animator.SetBool(key_isJump, false);
        }

        if (Input.GetKeyUp("d"))    // 右转前进 or 向左后退
        {
            this.animator.SetFloat(key_Blend, Mathf.Clamp(blendValue - blendSpeed * Time.deltaTime , 0f , 1f));
        }
        else
        {
            this.animator.SetFloat(key_Blend, blendValue);
        }

    }
}
