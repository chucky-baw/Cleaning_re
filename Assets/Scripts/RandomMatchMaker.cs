﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RandomMatchMaker : Photon.PunBehaviour
{
    HPController hp;

    void Start()
    {
        hp = FindObjectOfType<HPController>();
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        hp.setFlag(false);
    }

    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    //ロビーに入室した時
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        // ルームのどれかに入室する
        PhotonNetwork.JoinRandomRoom();
    }

    //部屋に入室した時
    public override void OnJoinedRoom()
    {
        //playerが揃った時
        //ゲームを開始すれば良い
        if (PhotonNetwork.room.PlayerCount == 2)
        {
            Debug.Log("I'm second player!");
            hp.setFlag(true);

        }
        else
        {
            //もう一人のプレイヤーを待つ
            Debug.Log("Waiting for another player");
        }
    }

    //他プレイヤーが到着した時
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Other player arrived");
        //二人目のプレイヤーが到着した時、ゲームを開始する
        hp.setFlag(true);
    }


    // ルームのランダム入室に失敗
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
        PhotonNetwork.CreateRoom(null);

        Debug.Log("Cant join random room!");

    }

}
