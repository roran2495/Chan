using UnityEngine;
using System.Collections.Generic;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab; // 宝石预制体
    public int totalNumberOfGems = 100; // 总宝石数量
    public float minGemSpacing = 1f; // 最小宝石间隔

    private List<BezierSpline> splines = new List<BezierSpline>(); // 多条曲线路径
    private List<float> splineLengths = new List<float>(); // 多条曲线路径长度

    void Start()
    {
        // 获取所有曲线路径组件
        BezierSpline[] allSplines = GetComponentsInChildren<BezierSpline>();
        if (allSplines.Length == 0)
        {
            Debug.LogError("No BezierSpline components found.");
            return;
        }

        // 初始化曲线路径和长度列表
        foreach (BezierSpline spline in allSplines)
        {
            splines.Add(spline);
            splineLengths.Add(spline.Length);
        }

        // 开始生成宝石
        StartCoroutine(SpawnGems());
    }

    IEnumerator SpawnGems()
    {
        int remainingGems = totalNumberOfGems;
        while (remainingGems > 0)
        {
            // 在随机选定的曲线路径上随机选择一个位置
            int randomSplineIndex = Random.Range(0, splines.Count);
            float randomT = Random.Range(0f, 1f);
            Vector3 position = splines[randomSplineIndex].GetPoint(randomT);

            // 检查该位置是否与其他宝石位置的距离大于最小间隔
            bool validPosition = true;
            foreach (GameObject gem in GameObject.FindGameObjectsWithTag("Gem"))
            {
                if (Vector3.Distance(position, gem.transform.position) < minGemSpacing)
                {
                    validPosition = false;
                    break;
                }
            }

            // 如果位置有效，则生成宝石
            if (validPosition)
            {
                Instantiate(gemPrefab, position, Quaternion.identity);
                remainingGems--;
            }

            yield return null;
        }
    }
}

