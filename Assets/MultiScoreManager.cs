using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiScoreManager : MonoBehaviour
{
    private int P1Score = 0;
    private int P2Score = 0;

    PhotonView _photonView;
    GameObject roomba;

    Text p1ScoreText;
    Text p2ScoreText;
    Text turnText;

    // Start is called before the first frame update
    void Start()
    {
        p1ScoreText = transform.GetChild(0).gameObject.GetComponent<Text>();
        p2ScoreText = transform.GetChild(1).gameObject.GetComponent<Text>();
        turnText = transform.GetChild(2).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roomba == null)
        {
            roomba = GameObject.Find("MultiRoomba(Clone)");
        }
        else
        {
            _photonView = roomba.gameObject.GetPhotonView();

            if (_photonView.ownerId == 1)
            {
                turnText.text = ("P1のターン");
            }
            else
            {
                turnText.text = ("P2のターン");
            }
        }

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
