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
    private float lightningshot;
    [SerializeField]
    public float beforeLightning;
    [SerializeField]
    public float lightningDuration;
    [SerializeField]
    private float blazeCooldown;
    private float blazeshot;
    [SerializeField]
    public float blazeDuration;
    [SerializeField]
    private bool blazeActivate;
    public int RanX;
    bool randomStateDone;
    
    

    public Vector3 lightningValue;
    public GameObject blazeSprite;
    public GameObject iceSprite;
    public GameObject lightningSprite;
    public GameObject lightPoint;
    public GameObject blazeSpawnArea;

    ProjectilePool pool;
    private WizardMovement wizardMovement;

    #region Singleton

    public static WizardAttack Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public enum AttackElementType
    {
        ice,
        lightning,
        blaze
    }
    public AttackElementType attackElementType;

    private void Start()
    {
        pool = ProjectilePool.Instance;
        wizardMovement = GetComponent<WizardMovement>();
        iceFire = iceCooldown;
        lightningshot = lightningCooldown;
        blazeshot = blazeCooldown;
        randomStateDone = false;
    }

    public void AttackState()
    {
        // If take out if statement you can use more than one elements in once time
        if (!randomStateDone)
        {
            RandomState();
            randomStateDone = true;
        }
        else
        {
            switch (attackElementType)
            {
                case AttackElementType.ice:
                    IceAttack(numOfIceBullet, rangeIceBullet, speedIceBullet);
                    break;

                case AttackElementType.lightning:
                    StartCoroutine("lightingAttack");
                    break;

                case AttackElementType.blaze:
                    blazeAttack();
                    break;
            }
        }
        
    }

    private void IceAttack(int numOfP, float range, float speedBullet)
    {
        if (stateTimer <= 0)
        {
            ResetAllAttack();
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
            ResetAllAttack();
        }
        else if (lightningshot <= 0.2)
        {
            Vector3 lightningPosition = new Vector3(Random.Range(-lightningValue.x, lightningValue.x), 0, 0);
            lightPoint = pool.GetElementInPool("lightPoint", lightningPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            lightningshot = lightningCooldown;
            yield return new WaitForSeconds(beforeLightning);
            lightningSprite = pool.GetElementInPool("lightning", lightningPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
        }
        else
        {
            lightningshot -= Time.deltaTime;
            stateTimer -= Time.deltaTime;
        }
  
    }

    void blazeAttack()
    {
        if(stateTimer <= 0)
        {
            ResetAllAttack();
        }
        else if (blazeshot <= 0.2 && !blazeActivate)
        {
            RanX = Random.RandomRange(0, 1);
            if (wizardMovement.inPlayer1)
            {
                blazeSpawnArea.transform.position = new Vector3(0f, 50f, 0f);
                blazeSprite = pool.GetElementInPool("blaze", blazeSpawnArea.transform.GetChild(RanX).position, gameObject.transform.rotation);
            }
            else
            {
                blazeSpawnArea.transform.position = new Vector3(0f, 0f, 0f);
                blazeSprite = pool.GetElementInPool("blaze", blazeSpawnArea.transform.GetChild(RanX).position, gameObject.transform.rotation);
            }
            blazeActivate = true;
        }
        else
        {
            stateTimer -= Time.deltaTime;
            blazeshot -= Time.deltaTime;
        }

    }

    void RandomState()
    {
        int ranState = Random.Range(2, 3);
        switch (ranState)
        {
            case 0:
                attackElementType = AttackElementType.ice;
                break;
            case 1:
                attackElementType = AttackElementType.lightning;
                break;
            case 2:
                attackElementType = AttackElementType.blaze;
                break;

        }
    }

    void ResetAllAttack()
    {
        randomStateDone = false;
        wizardMovement.state = WizardMovement.State.Move;
        blazeshot = blazeCooldown;
        stateTimer = setStateTimer;
        blazeActivate = false;
        lightningshot = lightningCooldown;
        iceFire = iceCooldown;
    }
    

}
