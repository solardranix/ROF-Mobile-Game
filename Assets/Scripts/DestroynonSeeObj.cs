using UnityEngine;
using System.Collections;

public class DestroynonSeeObj : MonoBehaviour {

	void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
    }
}
