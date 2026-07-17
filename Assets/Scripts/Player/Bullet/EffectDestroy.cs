using UnityEngine;
using System.Collections;

public class EffectDestroy : MonoBehaviour
{
    public float destroyTime = 1.0f;
    void OnEnable()
    {
        Invoke("Destroy", destroyTime);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
