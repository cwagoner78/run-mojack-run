using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameStartSpeed = 20;
    public int score= 0;
    public bool gameOver = false;
    public bool gameRestarted = false;

    public Transform startingPoint;
    public float lerpSpeed;

    [HideInInspector]
    public float gameSpeed;

    public UIController _uIController;
    private static GameManager instance;
    PlayerController _playerController;


    // Start is called before the first frame update
    void Awake()
    {
        gameSpeed = gameStartSpeed;
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        StartCoroutine(PlayIntro());
    }


    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (_uIController != null) _uIController.gameObject.SetActive(true);

        }
        else
        {
            if (_uIController != null) _uIController.gameObject.SetActive(false);
        }
    }

    public void RestartGame()
    {
        gameOver = true;
        gameRestarted = true;
        StartCoroutine(PlayIntro());


    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = new Vector3(-8,0,5);
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        _playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            _playerController.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        _playerController.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        gameOver = false;

    }

}
