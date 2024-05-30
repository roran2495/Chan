using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject canvas; 
    public CanvasController canvasController; // CanvasController对象
    private bool isPaused = false; // 游戏是否暂停
    void Start() 
    {
        canvas = this.transform.Find("canvas").gameObject;
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
        this.transform.position = Vector3.zero;
    }
}
