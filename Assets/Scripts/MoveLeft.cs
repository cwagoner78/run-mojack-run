using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.gameOver) transform.Translate(Vector3.left * _gameManager.gameSpeed * Time.deltaTime);

        if (transform.position.y <= -5) Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.score += 1;
            _gameManager.gameSpeed += 1;
            PlayerController._currentSpeed += 1;
        } 
        
    }
}
