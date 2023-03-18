using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        GetComponent<TMP_Text>().SetText(gameManager.score.ToString("Score: " + 0));
    }
}