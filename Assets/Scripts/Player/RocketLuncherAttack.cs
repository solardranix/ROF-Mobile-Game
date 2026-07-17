using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketLuncherAttack : MonoBehaviour
{
    public static RocketLuncherAttack rocketLuncherAttack;

    public float RocketLuncherCoolDown = 0.1f;
    //=========== Shooting Value ====================
    public Transform bulletSpawnerPos;
    public int bulletPrefabNumInPool;
    public float fireRate = 2.0f;
    float timeToFire;

    
    public float rotationSpeed = 200.0f;
    public float radarRadius = 400.0f;
    private float targetDistance;
    public LayerMask targetLayer;
    public AudioClip bulletAudioClip;

    private Transform target;
    private bool rotateToTarget;
    private bool rocketAttack;

    public Image energyBarImages;
    public Button energyBarButton;


    // Use this for initialization
    void Start()
    {
        rocketLuncherAttack = this;
        rotateToTarget = false;
        timeToFire = 0f;
        targetDistance = radarRadius;
        target = null;
        rocketAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateToTarget)
        {
            energyBarImages.fillAmount = 0.0f;
            energyBarButton.interactable = false;
            RadarRotation();
        }

        if (energyBarImages.fillAmount < 1.0f)
        {
            energyBarImages.fillAmount += ((float)RocketLuncherCoolDown / 60);
        }
        else if(energyBarImages.fillAmount >= 1.0f)
        {
            energyBarImages.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
            //energyBarImages.fillAmount = 0.0f;
            energyBarImages.fillAmount = 1.0f;
            energyBarButton.interactable = true;
            
        }
    }

    public void touchAttack()
    {
        if (energyBarImages.fillAmount >= 1.0f)
        {

            //Pak Shavad
            if (target)
            {
                if (target.gameObject.activeInHierarchy == false)
                {
                    target = null;
                }
            }

            if (!target)
            {
                BulletFindTarget();
            }
            rotateToTarget = true;
            energyBarImages.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
        }
    }

    void BulletFindTarget()
    {
        Collider2D[] go = Physics2D.OverlapCircleAll(transform.position, radarRadius, targetLayer);

        float currentTargetDis = 250.0f;

        for (int i = 0; i < go.Length; i++)
        {
            currentTargetDis = (go[i].transform.position - transform.position).sqrMagnitude;
            if (currentTargetDis < targetDistance)
            {
                targetDistance = currentTargetDis;
                target = go[i].transform;
            }
        }
        targetDistance = radarRadius;
    }

    public void RadarRotation()
    {
        float angle;

        if (target)
        {
            angle = FindAngle(transform.right, target.position - transform.position, transform.forward);
        }
        else
        {
            angle = 0.0f;
        }

        if(angle > 0.2f || angle < -0.2f)
        {
            transform.Rotate(Vector3.forward, angle * rotationSpeed * Time.deltaTime);
        }

        if(angle < 0.2f && angle > -0.2f)
        {
            rotateToTarget = false;
            Attack(); 
        }
        
    }

    float FindAngle(Vector3 fromVec, Vector3 toVec, Vector3 upVec)
    {
        if (toVec == Vector3.zero)
        {
            return 0.0f;
        }

        float angle = Vector3.Angle(fromVec, toVec);
        Vector3 normal = Vector3.Cross(fromVec, toVec);

        angle *= Mathf.Sign(Vector3.Dot(normal, upVec));
        angle *= Mathf.Deg2Rad;

        return angle;
    }
    //============================================================
    public void Attack()
    {
        GameObject obj = ObjectPoolingScript.objectPoolingScript.GetPooledObject(bulletPrefabNumInPool);
        if (obj == null) return;
        //Pool Shoot
        obj.transform.position = bulletSpawnerPos.position;
        obj.transform.rotation = bulletSpawnerPos.rotation;
        obj.SetActive(true);
        //Play Shooting Sound
    }
}
