using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunMovement : TouchLogic
{
    //---------------------------------------
    public static Vector3 finger;
    public Transform playerRight, PlayerLeft;
    public WeaponHolderInGame playerRightW, PlayerLeftW;
    private Transform camTrans;
    //---------------------------------------

    //public Transform TargetBall;

    public float speed = 5.0F;
    public float rotationSpeed = 100.0f;

    public GameObject tochPosObject;

    void Start()
    {
        //---------------------------------------
        camTrans = Camera.main.transform;
        //playerTrans = this.transform;
        //---------------------------------------
    }

    void LookAtFinger()
    {
        Vector3 tempToch = new Vector3(touchPositionForAll.x, touchPositionForAll.y, playerRight.position.z - camTrans.position.z);
        finger = Camera.main.ScreenToWorldPoint(tempToch);

        //Debug.Log(tempToch +  " =========== " + finger);

        if (finger.x >= 3.0f)// && (finger.y > 10.0f || finger.y < 10.0f))
        {
            //tochPosObject.transform.position = finger;
            playerRight.LookAt(finger);
            playerRightW.Attack();
            //WeaponHolderInGame.weaponHolderInGame.Attack();
        }

        if(finger.x <= -3)
        {
            PlayerLeft.LookAt(finger);
            PlayerLeftW.Attack();
            //WeaponHolderInGame.weaponHolderInGame.Attack();
        }
    }


    public override void OnTouchBeganAnywhere()
    {
        touch2Watch = TouchLogic.currTouch;
        //PauseMenu.pauseMenu.OnResumeClick();
    }

    public override void OnTouchMovedAnywhere()
    {
        LookAtFinger();
    }

    public override void OnTouchStayedAnywhere()
    {
        LookAtFinger();
    }

    public override void OnTouchEndedAnywhere()
    {
        LookAtFinger();
        //PauseMenu.pauseMenu.OnPauseClick();
    }
}
