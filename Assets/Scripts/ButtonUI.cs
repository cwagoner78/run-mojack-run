using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] private string _level;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(_level);
        DontDestroyOnLoad(_gameManager);
        _gameManager.gameSpeed = _gameManager.gameStartSpeed;
        _gameManager.score = 0;
        _gameManager.gameOver = false;
    }

}
