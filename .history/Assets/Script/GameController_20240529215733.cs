using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    public GameObject canvas; 
    public GameObject text;
    public CanvasController canvasController; // CanvasController对象
    
    public SampleAnimation1 sampleAnimation1;
    private bool isPaused = false; // 游戏是否暂停
    void Start() 
    {
        canvas.gameObject.SetActive(false);
        PauseGame();
        StartCountdown(text.GetComponent<TMP_Text>());
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检测碰撞对象是否具有标签 "Finish"
        if (other.CompareTag("Finish"))
        {
            // 触发器触发时，暂停游戏，激活Canvas并调用CanvasController的ActiveTime函数传递true
            PauseGame();
            canvas.gameObject.SetActive(true);
            canvasController.ActiveTime(true, false);
            
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
            canvasController.ActiveTime(false, false);
        }
    }

    public void Resume(bool isPlayerReset)
    {
        if (isPlayerReset)
        {
            // 若接收到True，则将角色位置重置为0,0,0
            ResetPlayerPosition();
        }
        canvas.gameObject.SetActive(false);
        StartCountdown(text.GetComponent<TMP_Text>());
        ResumeGame();
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

     public void StartCountdown(TMP_Text countdownText)
    {
        // Start the countdown coroutine
        MonoBehaviour monoBehaviour = countdownText.gameObject.GetComponent<MonoBehaviour>();
        monoBehaviour.StartCoroutine(CountdownRoutine(countdownText));
    }

    private System.Collections.IEnumerator CountdownRoutine(TMP_Text countdownText)
    {
        int remainingTime = 5;

        // Countdown loop
        while (remainingTime > 0)
        {
            // Update the countdown text
            countdownText.text = remainingTime.ToString();

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease the remaining time
            remainingTime--;
        }

        // Update the text to show "0" after countdown finishes
        countdownText.text = "0";
        Resu
    }
}
