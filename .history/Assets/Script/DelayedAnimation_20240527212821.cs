using UnityEngine;

public class DelayAnimator : MonoBehaviour
{
    public Animator animator; // 对应的 Animator 组件
    public string idleAnimationName = "Idle"; // Idle 动画的名称
    public float delayTime = 3f; // 延迟播放时间

    void Start()
    {
        // 启动延迟播放的 Coroutine
        StartCoroutine(PlayDelayedAnimation());
    }

    System.Collections.IEnumerator PlayDelayedAnimation()
    {
        // 等待一段时间
        yield return new WaitForSeconds(delayTime);

        // 播放动画
        if(animator != null)
        {
            animator.Play(idleAnimationName);
        }
    }
}
