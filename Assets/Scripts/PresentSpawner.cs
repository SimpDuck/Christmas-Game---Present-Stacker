using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentSpawner : MonoBehaviour
{
    [SerializeField] float delay = 0f;
    [SerializeField] GameObject presentPrefab;
    [SerializeField] List<Sprite> presentSprites;


    [SerializeField] [Range(0, 50)] int poolSize = 10;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    public bool canSpawn = true;
    public bool toldMeToStart = false;

    private void Awake()
    {
        PopulatePool();
        canSpawn = true;
        toldMeToStart = false;
    }


    void Start()
    {        
        Invoke("StartAfterDoneDelay", delay);
    }

    public void StartAfterDoneDelay()
    {
        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(SpawnPresent());
        }
        else
        {
            return;
        }
     
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(presentPrefab, transform);
            pool[i].GetComponentInChildren<SpriteRenderer>().sprite = presentSprites[Random.Range(0, presentSprites.Count)];
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                pool[i].GetComponentInChildren<SpriteRenderer>().sprite = presentSprites[Random.Range(0, presentSprites.Count)];
                return;
            }
        }
    }

    IEnumerator SpawnPresent()
    {
        toldMeToStart = true;
        while (canSpawn == true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    public void stopSpawning()
    {
        canSpawn = false;

    }

    public void startSpawning()
    {
        if (canSpawn == true && toldMeToStart == false)
        {
            StartCoroutine(SpawnPresent());
        }
    }
}
