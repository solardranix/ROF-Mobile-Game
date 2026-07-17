using UnityEngine;
using System.Collections;

public class RucketBulletClass : MonoBehaviour
{
    //This is Not Very Strong & Fast, But That Chases Targets (Enemy Or Destructble Object ,...)
    //stats
    public float bulletMovementSpeed = 7.0f;
    public float rotationSpeed = 140.0f;
    public float radarRadius = 40.0f;
    private float targetDistance;
    public LayerMask targetLayer;
    public AudioClip bulletAudioClip;
    

    private Transform target;
    //===============
    Vector3 direction;

    void Start()
    {
        InitFunc();
    }

    void OnEnable()
    {
        InitFunc();
    }

    void InitFunc()
    {
        direction = transform.right;
        targetDistance = radarRadius;
        target = null;
        //AudioSource.PlayClipAtPoint(bulletAudioClip, transform.position);
    }
    //============================================================
    void Update()
    {
        RadarRotation();
        direction = transform.right;
        transform.position += direction * bulletMovementSpeed * Time.deltaTime;
        

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
            transform.Rotate(Vector3.forward , angle * rotationSpeed * Time.deltaTime);
        }
        else
        {
            angle = 0.0f;
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
}
