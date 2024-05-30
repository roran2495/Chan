using UnityEngine;
using System.Collections.Generic;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab; // 宝石预制体
    public GameObject line;
    public float minGemSpacing = 0.5f; // 最小宝石间隔
    public int totalNumberOfGems = 100; // 总宝石数量
    public int gemsPerGroupMin = 4; // 每组最少宝石数量
    public int gemsPerGroupMax = 5; // 每组最多宝石数量

    void Start()
    {
        // 生成宝石
        GenerateGems(line.transform);
    }

    void GenerateGems(Transform lineParent)
    {
        // 计算总宝石数量
        int remainingGems = totalNumberOfGems;

        // 获取 line 父对象下的所有 LineRenderer
        LineRenderer[] lineRenderers = lineParent.GetComponentsInChildren<LineRenderer>();

        // 循环直到所有宝石都生成完毕
        while (remainingGems > 0)
        {
            // 随机确定每组宝石数量
            int gemsInGroup = Random.Range(gemsPerGroupMin, gemsPerGroupMax + 1);

            // 随机选择一个 LineRenderer
            LineRenderer selectedLine = lineRenderers[Random.Range(0, lineRenderers.Length)];

            // 遍历该 LineRenderer 的所有点，生成宝石
            for (int i = 0; i < selectedLine.positionCount && remainingGems > 0; i++)
            {
                // 在该点生成宝石
                Vector3 gemPosition = selectedLine.GetPosition(i);
                Instantiate(gemPrefab, gemPosition, Quaternion.identity);

                // 更新剩余宝石数量
                remainingGems--;
            }
        }
    }

    public void ReGenerate(bool )
}
