using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightingSpotScript : MonoBehaviour, IFPoolObject
{
    private WizardAttack wizardAttackScript;

    public void ObjectSpawn()
    {
        wizardAttackScript = WizardAttack.Instance;
        StartCoroutine(lightningSpotDuration(wizardAttackScript.lightningDuration + wizardAttackScript.beforeLightning));
    }

    IEnumerator lightningSpotDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
