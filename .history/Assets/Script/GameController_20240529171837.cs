using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject canvas; 
    public CanvasController canvasController; // CanvasController对象
    
    public SampleAnimation1 sampleAnimation1;
    private bool isPaused = false; // 游戏是否暂停
    void Start() 
    {
        canvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检测碰撞对象是否具有标签 "Finish"
        if (other.CompareTag("Finish"))
        {
            // 触发器触发时，暂停游戏，激活Canvas并调用CanvasController的ActiveTime函数传递true
            PauseGame();
            canvas.gameObject.SetActive(true);
            canvasController.ActiveTime(true);
        }
        if (other.CompareTag("End"))
        {
            sampleAnimation1.EndGame(true);
            canvas.gameObject.SetActive(true);
            canvasController.ActiveTime(true, true);
        }
    }

    private void Update()
    {
        // 检测玩家按下 ESC 键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 暂停游戏，激活Canvas并调用CanvasController的ActiveTime函数传递false
            PauseGame();
            canvas.gameObject.SetActive(true);
            canvasController.ActiveTime(false);
        }
    }

    public void Resume(bool isPlayerReset)
    {
        if (isPlayerReset)
        {
            // 若接收到True，则将角色位置重置为0,0,0
            ResetPlayerPosition();
            canvas.gameObject.SetActive(false);
            ResumeGame();
        }
        else
        {
            // 若接收到False，则隐藏Canvas，结束暂停
            canvas.gameObject.SetActive(false);
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        // 暂停游戏
        Time.timeScale = 0;
        isPaused = true;
    }

    private void ResumeGame()
    {
        // 恢复游戏
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ResetPlayerPosition()
    {
        // 获取角色对象上的 CharacterController 组件
        CharacterController characterController = GetComponent<CharacterController>();

        // 避免回旋 状态重置
        sampleAnimation1.ReStart(true);

        // 暂时禁用 CharacterController
        characterController.enabled = false;

        // 设置角色对象的新位置
        Vector3 newPosition = new Vector3(2.5f, 0, 0);

        // 设置角色对象的新朝向
        Quaternion newRotation = Quaternion.Euler(0, 0, 0);

        // 移动角色对象到新的位置
        transform.position = newPosition;

        // 设置角色对象的新朝向
        transform.rotation = newRotation;

        // 重新启用 CharacterController
        characterController.enabled = true;

        
    }
}
