using UnityEngine;
using System.Collections;

public class TimerObj : MonoBehaviour
{
    public float waitingTime;

    // Use this for initialization
    void OnEnable()
    {
        DisAjir();
    }

    IEnumerator DisAjir()
    {
        yield return new WaitForSeconds(waitingTime);
        gameObject.SetActive(false);
    }
}
