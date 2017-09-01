/***
 *
 *  JimuCamRaycast
 *  author: antai.ted@gmail.com
 *  This class is attached to Vuforia Camera, it will constantly cache the object/point last hit
 *
 **/

using UnityEngine;
using System.Collections;

public class JimuCamRaycast : MonoBehaviour {

    public static GameObject sCurrentHitObject;
    public static Vector3 sLastHitPoint;

    void FixedUpdate ()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit hit; // in world space
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
        {
            if(sCurrentHitObject == null || sCurrentHitObject != hit.transform.gameObject)
            {
                Debug.Log("hit " + hit.transform.name +
                          ", WorldSpace: hit object = " + hit.transform.position.x + "," + hit.transform.position.y + "," + hit.transform.position.z + 
                          ", Hit Point = " + hit.point.x + ", " + hit.point.y + ", " + hit.point.z);

                sCurrentHitObject = hit.transform.gameObject;

                // Convert to local coordinates
                //Vector3 collisionPointLocal = JimuCamRaycast.sCurrentHitObject.transform.parent.InverseTransformPoint(hit.point);

                //Debug.Log("Local space: hit object = " + JimuCamRaycast.sCurrentHitObject.transform.localPosition.x + " " + JimuCamRaycast.sCurrentHitObject.transform.localPosition.y + " " + JimuCamRaycast.sCurrentHitObject.transform.localPosition.z
                // +  "Hit Point" + collisionPointLocal.x + ", " + collisionPointLocal.y + ", " + collisionPointLocal.z);
                  
            }
            sLastHitPoint = hit.point;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false);
        }
    }
}
