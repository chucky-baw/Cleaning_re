using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiScoreManager : MonoBehaviour
{
    private int P1Score = 0;
    private int P2Score = 0;

    Text p1ScoreText;
    Text p2ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        p1ScoreText = transform.GetChild(0).gameObject.GetComponent<Text>();
        p2ScoreText = transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        p1ScoreText.text = P1Score.ToString();
        p2ScoreText.text = P2Score.ToString();
    }

    public int getP1Score()
    {
        return P1Score;
    }

    public int getP2Score()
    {
        return P2Score;
    }

    public void SetP1Score(int _P1Score)
    {
        P1Score = _P1Score;
        return;
    }

    public void SetP2Score(int _P2Score)
    {
        P2Score = _P2Score;
        return;
    }

}
