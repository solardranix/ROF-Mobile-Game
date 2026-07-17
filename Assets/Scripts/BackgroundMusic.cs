using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    static public bool InitOn = false;
    [HideInInspector]public GameObject p;
    public GameObject p2;

    // Use this for initialization
    void Start ()
    {
        if (!InitOn)
        {
            p = Instantiate(p2)as GameObject;
            DontDestroyOnLoad(p);
            InitOn = true;
        }
    }
}
