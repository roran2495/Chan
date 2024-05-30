using UnityEngine;
using System.Collections.Generic;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab; // 宝石预制体
    public float minGemSpacing = 0.5f; // 最小宝石间隔
    public int totalNumberOfGems = 100; // 总宝石数量

    void Start()
    {
        // 获取场景中所有使用 LineRenderer 组件的游戏对象
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();

        // 生成宝石
        GenerateGems(lineRenderers);
    }

    void GenerateGems(LineRenderer[] lineRenderers)
    {
        // 用于存储已生成宝石的位置
        List<Vector3> gemPositions = new List<Vector3>();

        // 计算总宝石数量
        int remainingGems = totalNumberOfGems;

        // 循环直到所有宝石都生成完毕
        while (remainingGems > 0)
        {
            // 遍历所有 LineRenderer
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                // 遍历当前 LineRenderer 上的所有点
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    Vector3 point = lineRenderer.GetPosition(i);

                    // 检查该点与其他宝石之间的距离是否大于最小间距
                    bool validPosition = true;
                    foreach (Vector3 gemPosition in gemPositions)
                    {
                        if (Vector3.Distance(point, gemPosition) < minGemSpacing)
                        {
                            validPosition = false;
                            break;
                        }
                    }

                    // 如果位置有效，则在该位置生成宝石
                    if (validPosition)
                    {
                        Instantiate(gemPrefab, point, Quaternion.identity);
                        gemPositions.Add(point);
                        remainingGems--;

                        // 如果宝石已经生成完毕，则退出循环
                        if (remainingGems <= 0)
                            break;
                    }
                }

                // 如果宝石已经生成完毕，则退出循环
                if (remainingGems <= 0)
                    break;
            }
        }
    }
}
