using UnityEngine;
using System.Collections;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class HandPhysicsControllerInput : MonoBehaviour
{

    [Header("Myo")]
    public GameObject myoGameObject;
    [Header("Settings")]
    public float checkNewMyoPoseRate = 1f;
    ThalmicMyo myo;
    private HandPhysicsController _handController;
    private float nextMyoPoseCheck = 0f;
    Pose lastMyoPose;

    void Start ()
	{
        myo = myoGameObject.GetComponent<ThalmicMyo>();
        _handController = GetComponent<HandPhysicsController>();
    }

    void FixedUpdate()
    {
        if (Camera.main != null)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _handController.HandParts[0][0].transform.position + new Vector3(0, 7, 6), Time.deltaTime * 15);
    }

    void GetCurrentPose()
    {
        if (Time.time > nextMyoPoseCheck)
        {
            lastMyoPose = myo.pose;

            nextMyoPoseCheck = Time.time + checkNewMyoPoseRate;
        }
    }

    void Update () 
    {

        GetCurrentPose();
        GetAction();
        
        //Enable or disable control
        if (Input.GetKeyDown(KeyCode.C))
	        _handController.EnableControl = !_handController.EnableControl;

        //Bend or unbend all fingers
        if (Input.GetMouseButtonDown(0))
        {
            _handController.BendAllFingers();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _handController.UnbendAllFingers();
        }

        //Change hand type
	    if (Input.GetKeyUp(KeyCode.G))
	    {
	        if (_handController.HandType == HandTyp.LeftHand)
                _handController.ChangeHandType(HandTyp.RightHand);
            else _handController.ChangeHandType(HandTyp.LeftHand);
	    }

	}

    void GetAction()
    {

        switch (myo.pose)
        {
            case Pose.FingersSpread:
                _handController.UnbendAllFingers();
                break;
            case Pose.Fist:
                _handController.BendAllFingers();
                break;
            case Pose.WaveIn:
                break;
            case Pose.WaveOut:
                break;
            case Pose.DoubleTap:
            case Pose.Rest:
            case Pose.Unknown:
                break;
        }
    }

}
