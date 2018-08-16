using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningScritp : MonoBehaviour, IFPoolObject {

    private WizardAttack wizardAttackScript;

    public void ObjectSpawn()
    {
        wizardAttackScript = WizardAttack.Instance;
        StartCoroutine(lightningDuration(wizardAttackScript.lightningDuration));
    }

    IEnumerator lightningDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
