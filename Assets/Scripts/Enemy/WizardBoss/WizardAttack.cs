using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour {

    [SerializeField]
    private float stateTimer;
    [SerializeField]
    private float setStateTimer;
    [SerializeField]
    private int numOfIceBullet;
    [SerializeField]
    private float rangeIceBullet;
    [SerializeField]
    private float speedIceBullet;
    [SerializeField]
    private float iceCooldown;
    private float iceFire;
    [SerializeField]
    private float lightningCooldown;
    [SerializeField]
    private float lightningshot;
    [SerializeField]
    private float beforeLightning;
    [SerializeField]
    private float lightningDuration;

    
    

    public Vector3 lightningValue;
    public GameObject blazeSprite;
    public GameObject iceSprite;
    public GameObject lightningSprite;
    public GameObject lightPoint;

    ProjectilePool pool;
    private WizardMovement wizardMovement;

    public enum AttackElementType
    {
        ice,
        blaze,
        lightning
    }
    public AttackElementType attackElementType;

    private void Start()
    {
        pool = ProjectilePool.Instance;
        wizardMovement = GetComponent<WizardMovement>();
        iceFire = iceCooldown;
        lightningshot = lightningCooldown;
    }

    public void AttackState()
    {
        switch (attackElementType)
        {
            case AttackElementType.ice:
               
                IceAttack(numOfIceBullet, rangeIceBullet, speedIceBullet);
                break;

            case AttackElementType.lightning:

                StartCoroutine("lightingAttack");
                
                break;
        }
    }

    private void IceAttack(int numOfP, float range, float speedBullet)
    {
        if (stateTimer <= 0)
        {
            wizardMovement.state = WizardMovement.State.Move;
            stateTimer = setStateTimer;
            iceFire = iceCooldown;
        }
        else if (iceFire <= 0.2)
        {
            float angleTotal = 360f / numOfP;
            float angle = 0f;
            Vector3 projectileVec;
            Vector3 projectileDir;
            for (int j = 0; j < numOfP; j++)
            {
                float projectileX = wizardMovement.FindTheClosestPlayer().transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * range;
                float projectileY = wizardMovement.FindTheClosestPlayer().transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * range;

                projectileVec = new Vector3(projectileX, projectileY, 0);
                projectileDir = Vector3.Normalize(projectileVec - this.transform.position) * speedBullet;

                iceSprite = pool.GetElementInPool("ice", transform.position, Quaternion.identity);
                iceSprite.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);

                angle += angleTotal;
            }

            iceFire = iceCooldown;
        }
        
        else
        {
            iceFire -= Time.deltaTime;
            stateTimer -= Time.deltaTime;
            
        }
       
    }

    IEnumerator lightingAttack()
    {
        if (stateTimer <= 0)
        {
            wizardMovement.state = WizardMovement.State.Move;
            stateTimer = setStateTimer;
            lightningshot = lightningCooldown;
        }
        else if (lightningshot <= 0.2)
        {
            Vector3 lightningPosition = new Vector3(Random.Range(-lightningValue.x, lightningValue.x), 0, 0);
            lightPoint = pool.GetElementInPool("lightPoint", lightningPosition + transform.TransformPoint(0, 4.5f, 0), gameObject.transform.rotation);
            lightningshot = lightningCooldown;
            yield return new WaitForSeconds(beforeLightning);
            lightningSprite = pool.GetElementInPool("lightning", lightningPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(lightningDuration);
            lightPoint.SetActive(false);
            lightningSprite.SetActive(false);
            
        }
        else
        {
            lightningshot -= Time.deltaTime;
            stateTimer -= Time.deltaTime;
        }
  
    }

    

}
