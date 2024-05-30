using UnityEngine;
using System.Collections;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab; // 参考到 SphereGemLarge 预制体
    public int numberOfGems = 10; // 生成宝石的数量
    public float spawnDistance = 10f; // 生成宝石的距离
    public float spawnInterval = 2f; // 生成宝石的间隔时间

    private void Start()
    {
        StartCoroutine(SpawnGems());
    }

    private IEnumerator SpawnGems()
    {
        for (int i = 0; i < numberOfGems; i++)
        {
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance * (i + 1);
            Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
