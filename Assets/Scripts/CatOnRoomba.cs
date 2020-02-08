using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOnRoomba : Photon.MonoBehaviour
{
    [SerializeField]
    public GameObject Roomba;
    public GameObject HP;
    HPController hpc;
    AudioSource audioSource;
    public AudioClip catVoice;
    public GameObject cat;
    CatController catController;
    Rigidbody2D rb;
    Renderer a;
    [SerializeField]
    GameObject mrc;
    RandomMatchMaker rmm;

    // Start is called before the first frame update
    void Start()
    {
        a = gameObject.GetComponent<SpriteRenderer>();
        hpc = HP.gameObject.GetComponent<HPController>();
        catController = cat.gameObject.GetComponent<CatController>();


    }

    private void Update()
    {
        //if (Roomba == null) Roomba = rmm.roomba.gameObject;
    }

    void destroyCat()
    {


        if (mrc != null)
        {
            //所有権の交代
            //mrc.GetComponent<MultiRoombaController>().onlyChange();
        }

        //rmm = FindObjectOfType<RandomMatchMaker>();

        //Roomba = rmm.roomba.gameObject;
        a.enabled = false;
        cat.gameObject.SetActive(true);
        Roomba.gameObject.SetActive(true);
        rb = Roomba.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(catController.RoombaSpeed * 5f, ForceMode2D.Impulse);

        StartCoroutine(catDisenable());

    }

    void Meow()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(catVoice);
    }

    public void catReduceBattery()
    {
        hpc.reduceHP(20);
    }

    IEnumerator catDisenable()
    {
        cat.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        cat.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        a.enabled = true;
        this.gameObject.SetActive(false);

    }

}
