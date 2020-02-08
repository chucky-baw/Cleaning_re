using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    //ゲームステート
    public enum GameState
    {
        Title,
        Tutorial,
        Playing,
        MultiPlay,
        Clear,
        GameOver
    }

    //現在のゲームの進行状態
    public GameState currentState = GameState.Title;

    //ボタンのキャンバス
    public Canvas ButtonCanvas;

    //ステージ
    public int stage = 0;

    int oldTrashCount;

    //ステージ名管理
    private string[] stageName = { "Stage1", "Stage2", "Stage3" };


    GameObject[] Trashes;
    BGMController bgmController;
    GameState oldState;
    StageNumberController stageNumCont;

    AudioSource audioSource;

    public AudioClip TapButtonSound;
    public AudioClip ClearMusic;
    public AudioClip GameOverMusic;

    GameObject resultCanvas;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Trashes = GameObject.FindGameObjectsWithTag("Trash");
        oldTrashCount = 0;
        stage = 0;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        bgmController = FindObjectOfType<BGMController>();
        if(SceneManager.GetActiveScene().name == "Title") ButtonCanvas.gameObject.SetActive(false);
    }



    //ゲームステート切り替え関数
    public void dispatch(GameState state)
    {
        oldState = currentState;

        currentState = state;

        switch (state)
        {
            case GameState.Title:
                GameOpening();
                break;

            case GameState.Tutorial:
                Debug.Log("Tutorial");
                Tutorial();
                break;

            case GameState.Playing:
                Debug.Log("State start");
                GameStart();
                break;

            case GameState.MultiPlay:
                Debug.Log("MultiPlay Start!");
                MultiPlayStart();
                break;

            case GameState.Clear:
                GameClear();
                break;

            case GameState.GameOver:
                if (oldState == GameState.Playing)
                {
                    GameOver();
                }
                break;
        }
    }



    void GameOpening()
    {
        currentState = GameState.Title;
        Destroy(this.gameObject);
        SceneManager.LoadScene("Title");
    }

    void GameStart()
    {
        if (!bgmController.gameObject.activeSelf)
        {
            bgmController.gameObject.SetActive(true);
        }

        Debug.Log("GameStart");
        SceneManager.LoadScene(stageName[stage]);
    }

    void MultiPlayStart()
    {
        if (!bgmController.gameObject.activeSelf)
        {
            bgmController.gameObject.SetActive(true);
        }

        Debug.Log("MultiPlayStart");
        SceneManager.LoadScene("MultiPlay");
    }

    void Tutorial()
    {
        StartCoroutine(longWait());
        SceneManager.LoadScene("Tutorial");
    }

    void GameClear()
    {
        stage++;



        if(stage == stageName.Length)
        {
            stage = 0;
            bgmController.gameObject.SetActive(false);
            audioSource.PlayOneShot(ClearMusic);
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            // stageName配列に入っているシーンを読み込む
            dispatch(GameState.Playing);
        }
    }

    void GameOver()
    {
        //stage = 0;
        bgmController.gameObject.SetActive(false);
        audioSource.PlayOneShot(GameOverMusic);
        Debug.Log("GameOver");
        SceneManager.LoadScene("GameOver");
        
    }

    int GetTrashCount()
    {
        Trashes = GameObject.FindGameObjectsWithTag("Trash");

        if (Trashes == null)
        {
            Debug.Log("null!");
            return 0;
        }
        else
        {
            Debug.Log("Notnull!");
            return Trashes.Length;
        }
    }



    // Update is called once per frame
    void Update()
    {


        if (currentState == GameState.Playing)
        {
            stageNumCont = FindObjectOfType<StageNumberController>();

            if (stageNumCont != null)
            {
                stageNumCont.changeStageText(stage);
            }
            int trashCount = GetTrashCount();
            Debug.Log("old: " + oldTrashCount + "current: " + trashCount);

            if (trashCount == 0 && oldTrashCount != 0)
            {
                StartCoroutine(wait(currentState));
                //dispatch(GameState.Clear);
            }

            oldTrashCount = trashCount;

        }

        if(currentState == GameState.MultiPlay)
        {
            if (resultCanvas == null)
            {
                resultCanvas = GameObject.Find("ResultCanvas");
            } 

            int trashCount = GetTrashCount();

            if (trashCount == 0 && oldTrashCount != 0)
            {
                //結果のキャンバスを出現させる
                resultCanvas.SetActive(true);
            }
            else
            {
                resultCanvas.SetActive(false);
                oldTrashCount = trashCount;

            }

        }

        //タイトルシーンにて、クリックしたらスタート
        if (currentState == GameState.Title && Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(TapButtonSound);
            StartCoroutine(wait(currentState));
        }


        if(currentState == GameState.Tutorial && Input.GetMouseButtonUp(0))
        {
            audioSource.PlayOneShot(TapButtonSound);
            dispatch(GameState.Playing);
        }


        if (currentState == GameState.Clear)
        {
            if (Input.GetMouseButtonUp(0))
            {
                audioSource.PlayOneShot(TapButtonSound);
                Destroy(this.gameObject);
                dispatch(GameState.Title);
            }
        }

        if(currentState == GameState.GameOver)
        {
            oldTrashCount = 0;
        }

    }

    IEnumerator wait(GameState state)
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(0.2f);
        if (state == GameState.Title)
        {
            //dispatch(GameState.Tutorial);
            if (ButtonCanvas == null) yield return 0;
            ButtonCanvas.gameObject.SetActive(true);
        }
        else if(state == GameState.Playing)
        {
            dispatch(GameState.Clear);
        }
    }

    IEnumerator longWait()
    {
        Debug.Log("longwait");
        yield return new WaitForSeconds(1.0f);

    }


}
