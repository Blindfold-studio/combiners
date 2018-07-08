using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeliton : MonoBehaviour {
    Rigidbody2D rb2d;
    [SerializeField]
    private LayerMask isGroundnow;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*private bool IsGround()
    {
        if (rb2d.velocity.y <= 0)
        {
            foreach (Transform point in GroundDetect)
            {
                Collider2D[] col2d = Physics2D.OverlapCircleAll(point.position, groundRadius, isGroundnow);

                for (int i = 0; i < col2d.Length; i++)
                {
                    if (col2d[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;

    }
    */
}
