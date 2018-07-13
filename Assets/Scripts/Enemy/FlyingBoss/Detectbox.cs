using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectbox : MonoBehaviour {

    [SerializeField]
    private float amplitudeX = 10.0f;
    [SerializeField]
    private float amplitudeY = 1.0f;
    [SerializeField]
    private float omegaX = 1f;
    [SerializeField]
    private float omegaY = 2.5f;
    private float index;

    private bool backward;
    private int count = 0;
    void Awake()
    {
        backward = false;
    }

    void Update () {
        Controll();
    }

    void Controll()
    {
        if (count%2==0)
        {
            index -= Time.deltaTime;
        }
        else if(count%2==1)
        {
            index += Time.deltaTime;
        }
        
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = amplitudeY * Mathf.Sin(omegaY * index) + 3;
        transform.localPosition = new Vector3(x, y, 0);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            count++;
            Debug.Log("testttttttttttttttttttttt"+count);
        }
        Debug.Log("testttttttttttttttttttttt");
    }

}
