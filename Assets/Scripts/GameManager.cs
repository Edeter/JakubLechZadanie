using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField]
    public float MinHight, MaxHight;

    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject ScorePanel;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool IsPlaying = false;
   public  int currentScore;
    public int prevMaxScore;

    [SerializeField]float PointDelayMax;
    float PointDelay;

    [SerializeField] public float TimeToEnd = 60f;



    // Start is called before the first frame update
    void Start()
    {
        if (ScoreText != null) ScoreText.text = prevMaxScore.ToString();
        Screen.SetResolution(1920, 1080,true);
        SaveDataManager.LoadJsonScore(out prevMaxScore);


       // DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsPlaying && Application.isFocused && Input.anyKeyDown)
        {
            IsPlaying = true;
            PlayGame();
        }
        
        if(IsPlaying)
        {
            if(PointDelay<0)
            {
                AddPoint();
                PointDelay = PointDelayMax;
            }
            PointDelay -= Time.deltaTime;

            TimeToEnd -= Time.deltaTime;
            if(TimeToEnd<0)
            {
                onDeath();
            }
        }
            if(ScoreText!=null) ScoreText.text = prevMaxScore.ToString();

        
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void AddPoint()
    {
        currentScore++;
    }
    public void onDeath()
    {
            IsPlaying = false;
            SceneManager.LoadScene("Menu");
        if(currentScore > prevMaxScore)
        {
            prevMaxScore = currentScore;
            SaveDataManager.SaveJsonScore(currentScore);
        }
    }
    
}
