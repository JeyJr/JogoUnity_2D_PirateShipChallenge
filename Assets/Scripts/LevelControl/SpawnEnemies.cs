using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> boats;
    public List<Transform> spawnPoints;
    [SerializeField] private float axisVariation;

    private readonly int maxToSpawn = 10;
    public int boatsSpawned { get; set; }

    public Queue<GameObject> boatsPool = new();
    private readonly float rangeToSpawn = 5;

    public bool startSpawn;

    private void Start()
    {
        int index = 0;

        for (int i = 0; i < maxToSpawn; i++)
        {
            if (index > boats.Count - 1)
            {
                index = 0;
            }

            var obj = Instantiate(boats[index], transform.position, Quaternion.identity);
            obj.GetComponent<BoatStats>().SpawnEnemies = this;
            Enqueue(obj);
            index++;
        }
    }

    private void Update()
    {
        if(boatsPool.Count > 0)
        {
            Dequeue();
        }
    }

    public void Dequeue()
    {
        var obj = boatsPool.Dequeue();
        obj.SetActive(true);
        obj.GetComponent<BoatStats>().SetInitialValues();

        int index = Random.Range(0, spawnPoints.Count);
        float x = Random.Range(spawnPoints[index].position.x - axisVariation, spawnPoints[index].position.x + axisVariation);
        float y = Random.Range(spawnPoints[index].position.y - axisVariation, spawnPoints[index].position.y + axisVariation);
        obj.transform.position = new Vector2(x, y);
        obj.GetComponent<EnemyMovement>().MoveSpeed = Random.Range(.1f, 1);
    }
    public void Enqueue(GameObject obj)
    {
        obj.SetActive(false);
        boatsPool.Enqueue(obj);
    }
}
