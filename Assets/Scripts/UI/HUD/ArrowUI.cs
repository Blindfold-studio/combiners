using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArrowUI : MonoBehaviour {

    private int arrow;
    private int maxArrow;
    [SerializeField]
    private string playerTarget;
    private PlayerAttribute playerScript;

    public Image[] arrowImage;
    public Sprite[] arrowSprite;

    void Start()
    {

        playerScript = GameObject.Find(playerTarget).GetComponent<PlayerAttribute>();
        maxArrow = playerScript.MaxArrow;
        arrow = playerScript.Arrow;

        for (int i = 0; i < arrowImage.Length; i++)
        {
            if (i >= maxArrow)
            {
                arrowImage[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        CheckArrow();
    }

    void CheckArrow()
    {
        arrow = playerScript.Arrow;

        for (int i = 0; i < maxArrow; i++)
        {
            if (i < arrow)
            {
                arrowImage[i].sprite = arrowSprite[1];
            }
            else
            {
                arrowImage[i].sprite = arrowSprite[0];
            }
        }
        //SetHealthImages();
    }

}
