using UnityEngine;

public class SampleAnimation1 : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private const string key_ifRun = "ifRun";
    private const string key_isForward = "forward";
    private const string key_slide = "slide";
    private const string key_isJump = "jump";
    private const string key_Blend = "Blend";
    private float blendValue = 0.5f;
    private float blendSpeed = 1f;
    private int flagBlend;  // 不改变 ：0 ； 变大 ：1 ；变小 ： 2
    private float verticalVelocity = 0f;
    private float gravity = -9.81f; // 重力加速度
    private float speed; // 移动速度
    private bool shouldRotate = false;
    private Quaternion targetRotation;
    private float rotationSpeed = 10.0f; // 旋转速度
    private float jumpForce = 5f;

    private Vector3 originalCenter; // 角色胶囊体中心
    private float originalHeight; // 角色胶囊体高度

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.characterController = GetComponent<CharacterController>();
        this.animator.SetFloat(key_Blend, blendValue);
        this.animator.SetBool(key_ifRun, true);
        this.animator.SetBool(key_isForward, true);
        flagBlend = 0;
        speed = 10.0f;
        
        originalCenter = characterController.center;
        originalHeight = characterController.height;
    }

    void Update()
    {
        // 检测是否在地面上
        if (characterController.isGrounded)
        {
            // 跳跃
            if (Input.GetKeyDown("space")) 
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                // 在地面上，重置垂直速度到一个小的负值以保持角色贴地
                verticalVelocity = -1f; ;
            }
            
        }
        else
        {
            // 不在地面上，应用重力
            verticalVelocity += gravity * Time.deltaTime;
        }

        // 获取输入 // 修改移动，令同步Blend(动画变化)
        float moveDirectionX = Input.GetAxis("Horizontal");

        // 始终沿着角色的z轴正方向前进
        Vector3 moveDirection = transform.forward;
        // 加上x轴方向的移动
        moveDirection += transform.right * moveDirectionX;

        // 移动方向（不包括垂直方向）
        moveDirection *= speed;

        // 将垂直速度应用到移动方向上
        moveDirection.y = verticalVelocity;

        // 移动角色
        characterController.Move(moveDirection * Time.deltaTime);

        // 设置动画参数
        if (Input.GetKeyDown("w"))  // 点击w切换行走or跑步（默认跑步）
        {
            this.animator.SetBool(key_ifRun, !this.animator.GetBool(key_ifRun));
            if (speed > 9f)
            {
                speed = 3f;
            }
            else
            {
                speed = 10f;
            }
        }

        if (Input.GetKeyDown("a"))    // 左前进 or 向左后退
        {
            flagBlend = 2;
        }
        else if (Input.GetKeyDown("d")) // 右前进 or 向右后退
        {
            flagBlend = 1;
        }
        else if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            flagBlend = 0;
        }

        if (Input.GetKeyDown("q"))  // 左转
        {
            Debug.Log("turn left");
            // 设置目标旋转
            targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
            shouldRotate = true;
        }
        else if (Input.GetKeyDown("e")) // 右转
        {
            // 设置目标旋转
            Debug.Log("turn right");
            targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
            shouldRotate = true;
        }



        if (Input.GetKeyDown("s") && this.animator.GetBool(key_ifRun))       // 滑行
        {
            this.animator.SetBool(key_slide, true);

            // 调整胶囊体以适应滑行
            characterController.center = new Vector3(characterController.center.x, characterController.center.y / 4, characterController.center.z);
            characterController.height /= 2;

            speed = 5.0f;
        }
        else if (Input.GetKeyUp("s"))       
        {
            this.animator.SetBool(key_slide, false);
            // 恢复胶囊体的原始值
            characterController.center = originalCenter;
            characterController.height = originalHeight;

            speed = 10.0f;
        }

        if (Input.GetKeyDown("space"))    // 跳跃
        {
            this.animator.SetBool(key_isJump, true);
            // 调整胶囊体以适应滑行
            characterController.height += 0.6f;

        }
        else if (Input.GetKeyUp("space"))    
        {
            this.animator.SetBool(key_isJump, false);
            // 恢复胶囊体的原始值
            characterController.height = originalHeight;
        }


        // 根据标记调整 blendValue
        switch (flagBlend)
        {
            case 1:
                blendValue += blendSpeed * Time.deltaTime;
                break;
            case 2:
                blendValue -= blendSpeed * Time.deltaTime;
                break;
            default:
                blendValue = 0.5f; // 重置
                break;
        }

        this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));

        // 如果需要旋转，则进行插值旋转
        if (shouldRotate)
        {
            // 使用 Lerp 函数进行平滑旋转
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // 当接近目标旋转时，停止旋转
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                shouldRotate = false;
            }
        }
    }
    public void ReStart(bool flag)
    {
        // 避免回旋
        if (flag)
        {
            shouldRotate = false;
        }

        //状态重置
        this.animator.SetFloat(key_Blend, blendValue);
        this.animator.SetBool(key_ifRun, false);
        speed = 0f;
        this.animator.SetBool(key_isForward, false);
        flagBlend = 0;
    }
    public void EndGame(bool flag)
    {
        if (flag)
        {
            speed = 0f;
            this.animator.SetBool(key_ifRun, false);
            this.animator.SetBool(key_isForward, false);
        }
    }
    
    public void StartGame(bool flag)
    {
        if (flag)
        {
            this.animator.SetBool(key_ifRun, true);
            speed = 10f;
            this.animator.SetBool(key_isForward, true);
        }
    }
}
