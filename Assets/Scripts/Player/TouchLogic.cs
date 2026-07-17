using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchLogic : MonoBehaviour 
{
	public static int currTouch = 0;//so other scripts can know what touch is currently on screen
	[HideInInspector]
	public int touch2Watch = 64;

	public Vector2 touchPositionForAll;
	
	public virtual void Update()//If your child class uses Update, you must call base.Update(); to get this functionality
	{
		//is there a touch on screen?
		if(Input.touches.Length <= 0)
		{
			OnNoTouches();
		}
		else //if there is a touch
		{
			//loop through all the the touches on screen
			for(int i = 0; i < Input.touchCount; i++)
			{
				currTouch = i;

				//Here I Have Touch Position For Touch Number i
				touchPositionForAll = Input.GetTouch(i).position;

				//executes this code for current touch (i) on screen
				if(this.GetComponent<GUITexture>() != null && (this.GetComponent<GUITexture>().HitTest(Input.GetTouch(i).position)))
                {
					//if current touch hits our guitexture, run this code
					if(Input.GetTouch(i).phase == TouchPhase.Began)
					{
						OnTouchBegan();
						touch2Watch = currTouch;
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended)
					{
						OnTouchEnded();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Moved)
					{
						OnTouchMoved();
					}
					if(Input.GetTouch(i).phase == TouchPhase.Stationary)
					{
						OnTouchStayed();
					}
				}
				
				//outside so it doesn't require the touch to be over the guitexture
				switch(Input.GetTouch(i).phase)
				{
				case TouchPhase.Began:
					OnTouchBeganAnywhere();
					break;
				case TouchPhase.Ended:
					OnTouchEndedAnywhere();
					break;
				case TouchPhase.Moved:
					OnTouchMovedAnywhere();
					break;
				case TouchPhase.Stationary:
					OnTouchStayedAnywhere();
					break;
				}
			}
		}


		#if !UNITY_ANDROID && !UNITY_IPHON && !UNITY_BLACKBERRY && !UNITY_WINRT
		touchPositionForAll = Input.mousePosition;
		if(this.GetComponent<GUITexture>() != null && (this.GetComponent<GUITexture>().HitTest(Input.mousePosition)))
		{
			if(Input.GetMouseButtonDown(0))
			{
				OnTouchBegan();
				touch2Watch = 0;
			}
			if(Input.GetMouseButtonUp(0))
			{
				OnTouchEnded();
			}
			if(Input.GetMouseButton(0))
			{
				OnTouchMoved();
				OnTouchStayed();
			}
		}


		//outside so it doesn't require the touch to be over the guitexture
		if(Input.GetMouseButtonDown(0))
			OnTouchBeganAnywhere();
		if(Input.GetMouseButtonUp(0))
			OnTouchEndedAnywhere();
		if(Input.GetMouseButton(0))
		{
			OnTouchMovedAnywhere();
			OnTouchStayedAnywhere();
		}
		#endif
	}
	
	//the default functions, define what will happen if the child doesn't override these functions
	public virtual void OnNoTouches(){}
	public virtual void OnTouchBegan(){}
	public virtual void OnTouchEnded(){}
	public virtual void OnTouchMoved(){}
	public virtual void OnTouchStayed(){}
	public virtual void OnTouchBeganAnywhere(){}
	public virtual void OnTouchEndedAnywhere(){}
	public virtual void OnTouchMovedAnywhere(){}
	public virtual void OnTouchStayedAnywhere(){}


	
	public virtual void OnMouseDown1(){}
}