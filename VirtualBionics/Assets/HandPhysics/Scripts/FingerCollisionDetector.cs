using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FingerCollisionDetector : MonoBehaviour
{
    public HandPart ThisHandPart;
    public HandPhysicsController handPhysicsController;

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.GetComponent<Rigidbody>() != null)
        {
            if (!col.gameObject.GetComponent<Rigidbody>().isKinematic)
            {                
                ThisHandPart.TouchObject(col.gameObject);
                ThisHandPart.CollidedObjects.Add(col.gameObject);

                if (!handPhysicsController.collision)
                {
                    handPhysicsController.collision = true;
                    handPhysicsController.vibrate();
                }
            }
        } 
    }

    void OnTriggerStay(Collider col)
    {

    }

    void OnTriggerExit(Collider col)
    {
        ThisHandPart.CollidedObjects.Remove(col.gameObject);
    }

    void Update()
    {
        if (ThisHandPart.CollidedObjects.Count == 0)
        {
            if (!ThisHandPart.IsBending)
            {
                ThisHandPart.IsTouchedObject = false;
                if (ThisHandPart.PrevFingerBone.IsRoot)
                    ThisHandPart.PrevFingerBone.IsTouchedObject = false;
            }

        }
    }
}
