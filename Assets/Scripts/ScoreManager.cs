using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int prevMaxScore;
    [SerializeField]
    TextMeshProUGUI ScoreText;
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    TextMeshProUGUI LivesText;
    // Start is called before the first frame update

    private void Update()
    {
        ScoreText.text = GameManager.Instance.currentScore.ToString();
        TimeText.text = Mathf.FloorToInt(GameManager.Instance.TimeToEnd).ToString();
        LivesText.text = PlayerController.Instance.Lives.ToString() ;
    }

}
