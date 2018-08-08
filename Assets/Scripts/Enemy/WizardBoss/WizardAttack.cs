using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour {

    [SerializeField]
    private int numOfIceBullet;
    [SerializeField]
    private float rangeIceBullet;
    [SerializeField]
    private float speedIceBullet;
    [SerializeField]
    private int magazine;
    [SerializeField]
    private float reloadShot;
    [SerializeField]
    private float startShot;
    

    ProjectilePool pool;
    private WizardMovement wizardMovement;

    public GameObject bullet;

    public enum AttackElementType
    {
        ice,
        fire,
        thunder
    }
    public AttackElementType attackElementType;

    private void Start()
    {
        pool = ProjectilePool.Instance;
        wizardMovement = GetComponent<WizardMovement>();
        reloadShot = startShot;
    }

    public void AttackState()
    {
        switch (attackElementType)
        {
            case AttackElementType.ice:
                if(reloadShot <= 0)
                {
                    IceAttack(numOfIceBullet, rangeIceBullet, speedIceBullet);
                    reloadShot = startShot;
                }
                else
                {
                    reloadShot -= Time.deltaTime;
                }
                
                break;
        }
    }

    private void IceAttack(int numOfP, float range, float speedBullet)
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

                bullet = pool.GetElementInPool("ice", transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);

                angle += angleTotal;
            }
        wizardMovement.state = WizardMovement.State.Move;
    }

    
}
