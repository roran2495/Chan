﻿using System.Collections;
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
        // 获取当前角色的朝向
        Vector3 forward = transform.forward;

        // 检测是否有输入，并让角色前进
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            // 应用重力
            if (!characterController.isGrounded)
            {
                forward.y = Physics.gravity.y;
            }
            characterController.Move(forward * Time.deltaTime * 3.0f);
        }

        // 设置角色的朝向
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = Quaternion.Euler(0, -45, 0) * newForward;
        }else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = Quaternion.Euler(0, 45, 0) * newForward;
        }else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = Quaternion.Euler(0, -135, 0) * newForward;
        }else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = Quaternion.Euler(0, 135, 0) * newForward;
        }else if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = newForward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = -newForward;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = Quaternion.Euler(0, -90, 0) * newForward;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 newForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            transform.forward = Quaternion.Euler(0, 90, 0) * newForward;
        }


        // 设置动画参数
        if (input.GetKey(KeyCode.LeftControl))  // 点击左ctrl切换行走or跑步（默认跑步）
        {
            runOrWalk = !runOrWalk;
        }

        if (Input.GetKeyUp("w"))    // 前进
        {
            this.animator.SetBool(key_isRun, !runOrWalk);
            this.animator.SetBool(key_isWalkForward, runOrWalk);
        }
        else
        {
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isWalkForward, false);
        }

        if (Input.GetKeyUp("a"))    // 左转前进 or 向右后退
        {
            this.animator.SetFloat(key_Blend, blendValue + blendSpeed );
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
            this.animator.SetBool(key_isDamage, true);
        }
        else
        {
            this.animator.SetBool(key_isDamage, false);
        }

    }
}
