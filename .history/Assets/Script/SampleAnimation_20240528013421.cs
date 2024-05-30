using UnityEngine;

public class SampleAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private const string key_ifRun = "ifRun";
    private const string key_isForward = "forward";
    private const string key_isWalkBackward = "walkBackward";
    private const string key_isJump = "jump";
    private const string key_Blend = "Blend";
    private float blendValue = 0.5f;
    private float blendSpeed = 0.1f;
    private float verticalVelocity = 0f;
    private float gravity = -9.81f; // 重力加速度
    public float speed = 3.0f; // 移动速度

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.characterController = GetComponent<CharacterController>();
        this.animator.SetFloat(key_Blend, blendValue);
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

        // 获取输入
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

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
        }

        if (Input.GetKeyDown("w"))    // 前进
        {
            this.animator.SetBool(key_isForward, true);
        }
        else if (Input.GetKeyUp("w"))       
        {
            this.animator.SetBool(key_isForward, false);
        }

        if (Input.GetKeyDown("a"))    // 左转前进 or 向右后退
        {
            ChangeBlend(true)
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
            
        }
        else if (Input.GetKeyUp("d"))
        {
            blendValue = 0.5f;
            this.animator.SetFloat(key_Blend, blendValue);
        }
    }
    void ChangeBlend(bool direction)
    {
        if (direction)
        {
            blendValue -= blendSpeed * Time.deltaTime;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
        }
        else 
        {
            blendValue += blendSpeed * Time.deltaTime;
            this.animator.SetFloat(key_Blend, Mathf.Clamp01(blendValue));
        }
    }
}
