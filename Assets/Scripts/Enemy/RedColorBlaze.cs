using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedColorBlaze : MonoBehaviour,IFPoolObject {

    [SerializeField]
    private float Duration;

    SpriteRenderer renderer;

    public void ObjectSpawn()
    {
        renderer = GetComponent<SpriteRenderer>();
        Color c = renderer.material.color;
        c.a = 0f;
        renderer.material.color = c;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        for(float i = 0.05f; i <= 1.5; i += 0.05f)
        {
            Color c = renderer.material.color;
            c.a = i;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.05f);

            if (i >= 1.4)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerable FadeOut()
    {
        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            Color c = renderer.material.color;
            c.a = i;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
