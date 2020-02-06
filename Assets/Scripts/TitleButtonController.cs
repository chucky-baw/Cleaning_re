using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonController : MonoBehaviour
{
    //ゲームマネージャークラス
    GManager gm;
    AudioSource audio;
    private void Start()
    {
        gm = FindObjectOfType<GManager>();
        audio = gm.GetComponent<AudioSource>();
    }

    public void SoloButtonClicked()
    {
        StartCoroutine(goTutorial());

        //gm.dispatch(GManager.GameState.Tutorial);
    }

    public void MultiButtonClicked()
    {
        StartCoroutine(goMulti());
        Debug.Log("multi clicked!");
    }

    IEnumerator goTutorial()
    {
        audio.PlayOneShot(gm.TapButtonSound);
        yield return new WaitForSeconds(0.5f);
        gm.dispatch(GManager.GameState.Tutorial);

    }

    IEnumerator goMulti()
    {
        audio.PlayOneShot(gm.TapButtonSound);
        yield return new WaitForSeconds(0.5f);
        gm.dispatch(GManager.GameState.MultiPlay);

    }
}
