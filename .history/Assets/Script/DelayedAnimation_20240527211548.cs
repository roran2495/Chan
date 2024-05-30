using UnityEngine;
using System.Collections;

public class DelayedAnimation : MonoBehaviour
{
    public Animation animationComponent; // 用于播放动画的组件
    public string animationName; // 动画名称
    public float delayTime = 3f; // 延迟播放时间

    void Start()
    {
        // 启动延迟播放的 Coroutine
        StartCoroutine(PlayDelayedAnimation());
    }

    IEnumerator PlayDelayedAnimation()
    {
        // 等待一段时间
        yield return new WaitForSeconds(delayTime);

        // 播放动画
        if(animationComponent != null)
        {
            animationComponent.Play(animationName);
        }
    }
}
