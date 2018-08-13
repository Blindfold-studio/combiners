using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour {

    private GameManager gameManager;
    private Animator anim;
    private AnimatorClipInfo[] currentClipInfo;

    private float currentClipLength;

    void Start () {
        gameManager = GameManager.instance;
        anim = GetComponent<Animator>();
	}
	
	public void FadeToGameOverScene ()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade ()
    {
        anim.SetTrigger("IsLose");
        currentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        currentClipLength = currentClipInfo[0].clip.length;
        yield return new WaitForSeconds(currentClipLength);
        gameManager.LoadGameOverScene();
        yield return null;
    }
}
