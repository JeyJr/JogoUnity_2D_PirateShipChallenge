using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private List<GameObject> boats;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Queue<GameObject> boatsPool = new Queue<GameObject>();
    [SerializeField] private List<GameObject> spawnedBoats = new List<GameObject>();

    [SerializeField] private float axisVariation = 1.2f;
    [SerializeField] private float delayTimeToSpawn = 2f;
    [SerializeField] private int maxEnemiesToSpawn = 3;
    private bool stopSpawn = false;


    public float DelayTimeToSpawn
    {
        get => delayTimeToSpawn; 
        set => delayTimeToSpawn = value; 
    }

    public int MaxEnemiesToSpawn
    {
        get => maxEnemiesToSpawn; 
        set => maxEnemiesToSpawn = value;
    }
    public bool StopSpawn
    {
        get => stopSpawn;
        set => stopSpawn = value; 
    }

    private int boatsSpawned = 0;

    private void Start()
    {
        SetPoolSize();
    }
    void SetPoolSize()
    {
        int index = 0;

        for (int i = 0; i < 18; i++)
        {
            if (index > boats.Count - 1) index = 0;

            var obj = Instantiate(boats[index], transform.position, Quaternion.identity);
            obj.GetComponent<BoatStats>().SpawnEnemies = this;
            obj.GetComponent<BoatStats>().GameManager = gameManager;

            Enqueue(obj);
            index++;

        }
    }

    public void StartSpawnEnemies()
    {
        ResetQueue();
        StartCoroutine(Dequeue());
    }

    IEnumerator Dequeue()
    {
        boatsSpawned = 0;
        yield return new WaitForSeconds(1.5f);
        while (!StopSpawn)
        {
            if(boatsSpawned < MaxEnemiesToSpawn)
            {
                yield return new WaitForSeconds(DelayTimeToSpawn);

                var obj = boatsPool.Dequeue();
                obj.SetActive(true);

                float maxLife = Random.Range(2, 6);
                obj.GetComponent<BoatStats>().SetInitialValues(maxLife);

                int index = Random.Range(0, spawnPoints.Count);
                float x = Random.Range(spawnPoints[index].position.x - axisVariation, spawnPoints[index].position.x + axisVariation);
                float y = Random.Range(spawnPoints[index].position.y - axisVariation, spawnPoints[index].position.y + axisVariation);
                obj.transform.position = new Vector2(x, y);
                obj.GetComponent<EnemyMovement>().MoveSpeed = Random.Range(.1f, .6f);

                spawnedBoats.Add(obj);
                boatsSpawned++;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void Enqueue(GameObject obj)
    {
        obj.SetActive(false);
        boatsPool.Enqueue(obj);

        if(spawnedBoats.Contains(obj))
            spawnedBoats.Remove(obj);
    
        if(boatsSpawned > 0)
            boatsSpawned--;
    }

    public void ResetQueue()
    {
        while (spawnedBoats.Count > 0)
        {
            for (int i = 0; i < spawnedBoats.Count; i++)
            {
                Enqueue(spawnedBoats[i]);
            }
        }
        boatsSpawned = 0;
    }
}
