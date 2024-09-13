using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 먹이 생성 로직

public class Feed : MonoBehaviour
{
    [SerializeField] GameObject feed;
    [SerializeField] GameObject map;
    [SerializeField] float ReleaseTime;

    private float spawnTime = 0f;

    void Update()
    {
        spawnTime += Time.deltaTime;
        RandomFeed();
    }

    private void RandomFeed()
    {
        Vector3 originPos = map.transform.position;

        float RangeX = Random.Range(-21, 21);
        float RangeZ = Random.Range(-21, 21);
        float RangeY = feed.transform.position.y;

        Vector3 RandomPos = new Vector3(RangeX, RangeY, RangeZ);
        Vector3 Spawn = originPos + RandomPos;

        if (spawnTime > ReleaseTime)
        {
            Instantiate(feed, Spawn, Quaternion.identity);
            spawnTime = 0;
        }
    }
}
