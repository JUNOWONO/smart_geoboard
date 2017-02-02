/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections;

namespace Vuforia
{

	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class CustomEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES
		
		private TrackableBehaviour mTrackableBehaviour;

		//private bool isRendered = false; // added
		//private GameObject gravityCube; //
		//private Collider coll;//
		
		#endregion // PRIVATE_MEMBER_VARIABLES
		
		//private float ini_x = -0.215f;
		//private float ini_y = -0.004f;
		//private float ini_z = 0.33f;

		#region UNTIY_MONOBEHAVIOUR_METHODS
		public Data data;

		void Start()
		{
			data.LoadData ();
			//gravityCube = GameObject.Find ("gravityCube"); //
			//coll = GetComponent<Collider> ();//
			Physics.gravity = Vector3.zero;


			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}
		}
		
		#endregion // UNTIY_MONOBEHAVIOUR_METHODS
		
		//void Update()
		//{

			//gravityCube.transform.position = new Vector3 (ini_x, ini_y, ini_z);
		//}


		//충돌한 물체의 오브젝트를 반환. 나중에 이용 
		/*void OnTriggerEnter(Collider other)
		{
			if (other.attachedRigidbody)
				other.attachedRigidbody.useGravity = true;
		}*/

		#region PUBLIC_METHODS
		
		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
			    newStatus == TrackableBehaviour.Status.TRACKED ||
			    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();
				data.dataArr [data.dataArr [0].curNum].isSuccess = true; // 현재 커리큘럼 넘버의 isSuccess 값을 true로.
				
				data.SaveData ();
			}
			else
			{
				OnTrackingLost();
			}
		}
		
		#endregion // PUBLIC_METHODS
		
		
		
		#region PRIVATE_METHODS
		
		
		private void OnTrackingFound()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			
			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = true;
			}
			
			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = true;
			}
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

			//isRendered = true;//
			Physics.gravity = new Vector3(0,0,-15.0f);
           

		}
		
		
		private void OnTrackingLost()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			
			// Disable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}
			
			// Disable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");


			//isRendered = false;//
		}
		
		#endregion // PRIVATE_METHODS
	}
}
