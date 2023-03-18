using TMPro;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        GetComponent<TMP_Text>().SetText(gameManager.gameSpeed.ToString(" Speed: " + 0));
    }
}
