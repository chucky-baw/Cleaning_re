using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class MultiRoombaController : Photon.PunBehaviour
{
    Rigidbody2D rb;
    Rigidbody2D rbArrow;
    private float speed;
    Vector2 StartPos;
    Vector2 nowPos;
    Vector2 arrowDirection;
    Vector2 prevPos;
    float preVec;
    Vector3 arrowScale;
    public GameObject arrow;
    public GameObject gauge;
    [SerializeField]
    CatOnRoomba cor;
    [SerializeField]
    CatOnRoomba cor2;
    public GameObject roomba;
    RandomMatchMaker rmm;


    AudioSource audioSource;

    public AudioClip collSound;
    public AudioClip pickTrash;

    public bool changeOwnerFlag = false;

    PhotonTransformView photonTransformView;
    

    // Start is called before the first frame update
    void Start()
    {
        photonTransformView = GetComponent<PhotonTransformView>();
        rmm = FindObjectOfType<RandomMatchMaker>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rbArrow = arrow.gameObject.GetComponent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();
        
    }

    void OnGUI()
    {
        //(new Rect(左上のｘ座標, 左上のｙ座標, 横幅, 縦幅), "テキスト", スタイル（今は省略）)
        GUI.Label(new Rect(50, 50, 50, 50), this.photonView.ownerId.ToString()); //テキスト表示
        GUI.Label(new Rect(50, 75, 50, 50), PhotonNetwork.player.ID.ToString());
        GUI.Label(new Rect(50, 100, 50, 50), photonView.isMine.ToString()); //テキスト表示

    }

    // Update is called once per frame
    void Update()
    {


        float x = this.transform.position.x - prevPos.x;
        float y = this.transform.position.y - prevPos.y;

        Vector2 vec = new Vector2(x, y).normalized;

        if (System.Math.Abs(rb.velocity.magnitude) < 0.2f)
        {
            //ルンバが停止したらオーナーの変更を行う
            if (System.Math.Abs(preVec) > 0.2f) changeOwnerFlag = true;

            if (changeOwnerFlag == true)
            {
                changeOwner();

            }

            //所有者しか触れないようにする
            if (PhotonNetwork.player.ID == this.photonView.ownerId)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    this.StartPos = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    arrow.SetActive(false);
                    Vector2 EndPos = Input.mousePosition;
                    Vector2 startDirction = -1 * (EndPos - StartPos).normalized;
                    this.rb.AddForce(startDirction * speed);



                }
                else if (Input.GetMouseButton(0))
                {
                    arrow.SetActive(true);
                    arrowScale = rbArrow.transform.localScale;
                    nowPos = Input.mousePosition;
                    arrowDirection = -1 * (nowPos - StartPos).normalized;
                    rbArrow.transform.up = -1 * arrowDirection;
                    arrowScale.y = (nowPos - StartPos).magnitude * 0.1f;

                    if (arrowScale.y >= 25f)
                    {
                        arrowScale.y = 25f;
                        StartCoroutine(vib());
                    }

                    this.speed = (nowPos - StartPos).magnitude * 2f;
                    if (this.speed > 1000)
                    {
                        this.speed = 1000;
                    }

                    rbArrow.transform.localScale = arrowScale;
                }
            }

        }

        Debug.Log("現在のオーナーは" + this.photonView.ownerId + "です。　あなたのIDは" + PhotonNetwork.player.ID);

        preVec = rb.velocity.magnitude;

        photonTransformView.SetSynchronizedValues(speed: rb.velocity, turnSpeed: 0);

        //摩擦力
        rb.velocity *= 0.96f;

            prevPos = this.transform.position;

        //Debug.Log(System.Math.Abs(rb.velocity.magnitude));


    }


    IEnumerator vib()
    {
        Vector3 rotate = rbArrow.transform.localEulerAngles;

        while (Input.GetMouseButton(0) && arrowScale.y >= 25f)
        {
            rotate = rbArrow.transform.localEulerAngles;

            rotate.z += 2;
            rbArrow.transform.localEulerAngles = rotate;

            yield return new WaitForSeconds(0.01f);

            rotate.z -= 2;
            rbArrow.transform.localEulerAngles = rotate;

            yield return new WaitForSeconds(0.01f);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            audioSource.PlayOneShot(collSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Trash")
        {
            audioSource.PlayOneShot(pickTrash);
        }
    }

    public void changeOwner()
    {

        if (this.photonView.ownerId == 1)
        {
            this.photonView.TransferOwnership(2);
            Debug.Log("2に渡す");
            changeOwnerFlag = false;
            rb.velocity *= 0;
            preVec = rb.velocity.magnitude;

            return;
        }
        else
        {
            Debug.Log("0に渡す");

            this.photonView.TransferOwnership(1);
            rb.velocity *= 0;
            preVec = rb.velocity.magnitude;

            changeOwnerFlag = false;

            return;

        }
    }

    public void onlyChange()
    {
        if (this.photonView.ownerId == 1)
        {
            this.photonView.TransferOwnership(2);
            Debug.Log("2に渡す");
            changeOwnerFlag = false;


            return;
        }
        else
        {
            Debug.Log("0に渡す");

            this.photonView.TransferOwnership(1);


            changeOwnerFlag = false;

            return;

        }
    }



}
