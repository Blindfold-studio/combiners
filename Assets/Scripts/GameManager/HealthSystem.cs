using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField]
    private int playerHP;

	public int PlayerHP
    {
        get
        {
            return PlayerHP;
        }

        set
        {
            playerHP += value;
        }
    }
}
