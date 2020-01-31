using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RandomMatchMaker : Photon.PunBehaviour
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
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

    // ルームのランダム入室に失敗
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
        Debug.Log("Cant join random room!");
    }

}
