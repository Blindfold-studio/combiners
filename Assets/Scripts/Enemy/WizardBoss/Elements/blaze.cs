using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blaze : MonoBehaviour, IFPoolObject
{
    WizardAttack wizardAttackScript;
    WizardMovement wizardMovementScript;
    float distance;
    float startTime;
    float reachSpeed;
    [SerializeField]
    float speed;

    public void ObjectSpawn()
    {
        wizardAttackScript = WizardAttack.Instance;
        wizardMovementScript = WizardMovement.Instance;
        distance = Vector3.Distance(wizardAttackScript.blazeSpawnArea.transform.GetChild(wizardAttackScript.RanX).position, wizardAttackScript.blazeSpawnArea.transform.GetChild(1).position);
        startTime = Time.time;
        Debug.Log("WIZARDPOINT" + wizardAttackScript.blazeSpawnArea.transform.GetChild(wizardAttackScript.RanX));
        StartCoroutine(Disappear(wizardAttackScript.blazeDuration));
    }

    void Update()
    {
        float speedX = (Time.time - startTime) * speed;

        reachSpeed = speedX / distance;

        transform.position = Vector3.Lerp(wizardAttackScript.blazeSpawnArea.transform.GetChild(wizardAttackScript.RanX).position, wizardAttackScript.blazeSpawnArea.transform.GetChild(1).position, reachSpeed);

        Debug.Log(reachSpeed);
    }

    IEnumerator Disappear(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }


}
