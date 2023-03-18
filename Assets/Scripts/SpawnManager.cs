using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float fastest = 0f;
    public float longest = 3f;
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(40, 1, 5);
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("SpawnObstacle", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (!_gameManager.gameOver) StartCoroutine(RandomSpawn());
        else return;
    }

    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(Random.Range(fastest, longest));
        int spawnIndex = Random.Range(0, 3);
        Instantiate(obstaclePrefabs[spawnIndex], spawnPos, obstaclePrefabs[spawnIndex].transform.rotation);

    }
}
