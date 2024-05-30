using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab; // 宝石预制体
    public GameObject line;
    public float minGemSpacing = 0.5f; // 最小宝石间隔
    public int totalNumberOfGems = 100; // 总宝石数量
    public int gemsPerGroupMin = 4; // 每组最少宝石数量
    public int gemsPerGroupMax = 5; // 每组最多宝石数量

    private List<Vector3> gemPositions = new List<Vector3>();

    void Start()
    {
        // 生成宝石
        StartCoroutine(GenerateGems(line.transform));
    }

    IEnumerator GenerateGems(Transform lineParent)
    {
        // 计算总宝石数量
        int remainingGems = totalNumberOfGems;

        // 获取 Gem 父对象下的所有 LineRenderer
        LineRenderer[] lineRenderers = gemParent.GetComponentsInChildren<LineRenderer>();

        // 循环直到所有宝石都生成完毕
        while (remainingGems > 0)
        {
            // 随机确定每组宝石数量
            int gemsInGroup = Random.Range(gemsPerGroupMin, gemsPerGroupMax + 1);
            gemsInGroup = Mathf.Min(gemsInGroup, remainingGems); // 确保不超过剩余宝石数

            // 随机选择一个 LineRenderer
            LineRenderer selectedLine = lineRenderers[Random.Range(0, lineRenderers.Length)];

            for (int i = 0; i < gemsInGroup; i++)
            {
                // 计算随机位置
                float t = Random.Range(0f, 1f);
                Vector3 gemPosition = GetPointOnLine(selectedLine, t);

                // 检查与其他宝石的距离
                bool validPosition = true;
                foreach (Vector3 existingPosition in gemPositions)
                {
                    if (Vector3.Distance(gemPosition, existingPosition) < minGemSpacing)
                    {
                        validPosition = false;
                        break;
                    }
                }

                if (validPosition)
                {
                    Instantiate(gemPrefab, gemPosition, Quaternion.identity);
                    gemPositions.Add(gemPosition);
                    remainingGems--;
                }

                yield return null; // 避免阻塞主线程
            }
        }
    }

    Vector3 GetPointOnLine(LineRenderer lineRenderer, float t)
    {
        if (lineRenderer.positionCount < 2)
            return lineRenderer.GetPosition(0);

        float totalLength = 0f;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            totalLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        float distance = t * totalLength;
        float accumulatedLength = 0f;

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 p0 = lineRenderer.GetPosition(i);
            Vector3 p1 = lineRenderer.GetPosition(i + 1);
            float segmentLength = Vector3.Distance(p0, p1);

            if (accumulatedLength + segmentLength >= distance)
            {
                float segmentT = (distance - accumulatedLength) / segmentLength;
                return Vector3.Lerp(p0, p1, segmentT);
            }

            accumulatedLength += segmentLength;
        }

        return lineRenderer.GetPosition(lineRenderer.positionCount - 1);
    }

    public void ReGenerate(bool flag)
    {
        if (flag)
        {
            GenerateGems(line.transform);
        }
    }
}
