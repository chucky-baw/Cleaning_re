using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RandomMatchMaker : Photon.PunBehaviour
{
    //HPController hp;
    GameObject canvas;
    [SerializeField]
    CatOnRoomba cor;
    [SerializeField]
    CatOnRoomba cor2;
    public GameObject roomba;
    void Start()
    {
        //hp = FindObjectOfType<HPController>();
        //cor = FindObjectOfType<CatOnRoomba>();
        canvas = GameObject.Find("WaitingCanvas");
        canvas.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        //hp.setFlag(false);


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



    //他プレイヤーが到着した時
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Other player arrived");
        //二人目のプレイヤーが到着した時、ゲームを開始する
        //hp.setFlag(true);
        //StartCoroutine(wait());
        canvas.gameObject.SetActive(false);
        //roomba = PhotonNetwork.Instantiate("MultiRoomba", new Vector3(-0.11f, -4.93f, -2.896f), Quaternion.identity, 0) as GameObject;
        //cor.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
        //cor2.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;

        //PhotonView pv = roomba.GetPhotonView();
        //pv.TransferOwnership(2);
        //cor.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
        //cor2.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
        //pv.TransferOwnership(1);

        //StartCoroutine(wait());

    }


    //部屋に入室した時
    public override void OnJoinedRoom()
    {
        //playerが揃った時
        //ゲームを開始すれば良い
        if (PhotonNetwork.room.PlayerCount == 2)
        {
            //PhotonView pv = roomba.GetPhotonView();
            //pv.TransferOwnership(2);
            cor.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
            cor2.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
            //pv.TransferOwnership(1);
            Debug.Log("I'm second player!");
            canvas.gameObject.SetActive(false);
            //hp.setFlag(true);

        }
        else
        {
            roomba = PhotonNetwork.Instantiate("MultiRoomba", new Vector3(-0.11f, -4.93f, -2.896f), Quaternion.identity, 0) as GameObject;

            cor.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
            cor2.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
            canvas.gameObject.SetActive(true);

            //もう一人のプレイヤーを待つ
            Debug.Log("Waiting for another player");
        }
    }


    // ルームのランダム入室に失敗
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
        PhotonNetwork.CreateRoom(null);

        Debug.Log("Cant join random room!");

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);

    }

    public void aaa()
    {
        cor.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
        cor2.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
    }

}
