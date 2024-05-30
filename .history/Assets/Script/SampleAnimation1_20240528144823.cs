using UnityEngine;

public class SampleAnimation1 : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private const string key_ifRun = "ifRun";
    private const string key_isForward = "forward";
    private const string key_isWalkBackward = "walkBackward";
    private const string key_isJump = "jump";
    private const string key_Blend = "Blend";
    private float blendValue = 0.5f;
    private float blendSpeed = 1f;
    private int flagBlend;  // 不改变 ：0 ； 变大 ：1 ；变小 ： 2
    private float verticalVelocity = 0f;
    private float gravity = -9.81f; // 重力加速度
    private float speed = 10.0f; // 移动速度
    private bool shouldRotate = false;
    private Quaternion targetRotation;
    private float rotationSpeed = 1.0f; // 旋转速度

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.characterController = GetComponent<CharacterController>();
        this.animator.SetFloat(key_Blend, blendValue);
        this.animator.SetBool(key_ifRun, true);
        this.animator.SetBool(key_isForward, true);
        flagBlend = 0;
    }

    void Update()
    {
        // 检测是否在地面上
        if (characterController.isGrounded)
        {
            // 在地面上，重置垂直速度到一个小的负值以保持角色贴地
            verticalVelocity = -1f;
        }
        else
        {
            // 不在地面上，应用重力
            verticalVelocity += gravity * Time.deltaTime;
        }

        // 获取输入 // 修改移动，令同步Blend(动画变化)
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = transform.forward.z;

        // 移动方向（注意不包括垂直方向）
        Vector3 moveDirection = new Vector3(moveDirectionX, 0, moveDirectionZ);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        // 将垂直速度应用到移动方向上
        moveDirection.y = verticalVelocity;

        // 移动角色
        characterController.Move(moveDirection * Time.deltaTime);

        // 设置动画参数
        if (Input.GetKeyDown(KeyCode.LeftControl))  // 点击左ctrl切换行走or跑步（默认跑步）
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

        if (!Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown("a"))    // 左转前进 or 向左后退
            {
                
            }
        }
        if (Input.GetKeyDown("a"))    // 左转前进 or 向左后退
        {
            if (!Input.GetKeyDown(KeyCode.LeftAlt))
            {
                flagBlend = 2;
            }
            else
            {
                // 设置目标旋转
                targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
                shouldRotate = true;
            }
        } 
        else if (Input.GetKeyUp("a"))
        {
            flagBlend = 0;
        }

        if (Input.GetKeyDown("d"))    // 右转前进 or 向右后退
        {
            if (!Input.GetKeyDown(KeyCode.LeftAlt))
            {
                flagBlend = 1;
            }
            else
            {
                // 设置目标旋转
                targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
                shouldRotate = true;
            }
        }
        else if (Input.GetKeyUp("d"))
        {
            flagBlend = 0;
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
}
