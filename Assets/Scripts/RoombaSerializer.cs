using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaSerializer : Photon.MonoBehaviour
{
    GameObject roomba;
    [SerializeField]
    GameObject cor1;
    [SerializeField]
    GameObject cor2;



    private void Update()
    {
        if (roomba == null) roomba = GameObject.Find("MultiRoomba(Clone)");

        if (roomba != null && (cor1.GetComponent<CatOnRoomba>().Roomba == null || cor2.GetComponent<CatOnRoomba>().Roomba == null))
        {
            Debug.Log("ok!");
            cor1.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
            cor2.GetComponent<CatOnRoomba>().Roomba = roomba.gameObject;
        }

    }


}
