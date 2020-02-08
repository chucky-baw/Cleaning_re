using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextManager : MonoBehaviour
{

    int p1, p2;
    MultiScoreManager msl;

    // Start is called before the first frame update
    void Start()
    {
        msl = FindObjectOfType<MultiScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        p1 = msl.getP1Score();
        p2 = msl.getP2Score();

        if((p1 > p2 && PhotonNetwork.player.ID == 1) || (p2 > p1) && PhotonNetwork.player.ID == 2)
        {
            this.gameObject.GetComponent<Text>().text = "あなたのかち！";
        } else if ((p1 < p2 && PhotonNetwork.player.ID == 1) || (p2 < p1 && PhotonNetwork.player.ID == 2))
        {
            this.gameObject.GetComponent<Text>().text = "あなたのまけ！";

        }
        else
        {
            this.gameObject.GetComponent<Text>().text = "ひきわけ！";

        }
    }
}
