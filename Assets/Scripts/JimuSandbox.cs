/***
 *
 *  JimuSandbox
 *  author: antai.ted@gmail.com
 *
 **/

using UnityEngine;
using System.Collections;

public class JimuSandbox : MonoBehaviour
{
    // Assigned in Scene file
    public GameObject prototype;
    public GameObject prototype2;
    public GameObject prototype3;
    public GameObject prototype4;
    public GameObject prototype5;
    public GameObject prototype6;
    public GameObject prototype7;
    public GameObject prototype8;

    public GameObject[] prototypes;

    public static JimuSandbox instance;

    public static int newBlockCount = 0;

    public static float sHalfSize;
    void Start ()
    {
        instance = this;
        prototypes = new GameObject[] {prototype,prototype2,prototype3,prototype4,prototype5,prototype6,prototype7,prototype8};
        sHalfSize = prototype.transform.lossyScale.x * 0.5f;
    }

    public void NewBlockRaycast()
    {
        bool x_p = false;
        bool x_m = false;
        bool y_p = false;
        bool y_m = false;
        bool z_p = false;
        bool z_m = false;

        Vector3 collisionPointLocal = JimuCamRaycast.sCurrentHitObject.transform.parent.InverseTransformPoint(JimuCamRaycast.sLastHitPoint);

        Debug.Log("!!!Local space: collision point" + collisionPointLocal.x + ", " + collisionPointLocal.y + ", " + collisionPointLocal.z);

        float xDiff = collisionPointLocal.x - JimuCamRaycast.sCurrentHitObject.transform.localPosition.x;
        float yDiff = collisionPointLocal.y - JimuCamRaycast.sCurrentHitObject.transform.localPosition.y;
        float zDiff = collisionPointLocal.z - JimuCamRaycast.sCurrentHitObject.transform.localPosition.z;

        float xDiffAbs = xDiff>=0? xDiff:-xDiff;
        float yDiffAbs = yDiff>=0? yDiff:-yDiff;
        float zDiffAbs = zDiff>=0? zDiff:-zDiff;

        if(xDiffAbs > yDiffAbs && xDiffAbs > zDiffAbs)
        {
            if(xDiff >= 0)
            {
                Debug.Log("X +");
                x_p = true;
            } 
            else {
                Debug.Log("X -");
                x_m = true;
            }
        }
        else if(yDiffAbs > xDiffAbs && yDiffAbs > zDiffAbs)
        {
            if(yDiff >= 0)
            {
                Debug.Log("Y +");
                y_p = true;
            }
            else {
                Debug.Log("y -");
                y_m = true;
            }
        }
        else if(zDiffAbs > xDiffAbs && zDiffAbs > yDiffAbs)
        {
            if(zDiff >= 0) 
            {
                Debug.Log("Z +");
                z_p = true;
            }
            else {
                Debug.Log("Z -");
                z_m = true;
            }
        }

        Vector3 newPos = JimuCamRaycast.sCurrentHitObject.transform.localPosition;
        if(x_p)         newPos.x += 2*sHalfSize;
        else if(x_m)    newPos.x -= 2*sHalfSize;
        else if(y_p)    newPos.y += 2*sHalfSize;
        else if(y_m)    newPos.y -= 2*sHalfSize;
        else if(z_p)    newPos.z += 2*sHalfSize;
        else if(z_m)    newPos.z -= 2*sHalfSize;

        GameObject aNewBlock;
        aNewBlock = Instantiate(prototypes[GameController.sCurrentBlockType], newPos, Quaternion.identity) as GameObject;
      
        if(instance && aNewBlock)
        {
            aNewBlock.transform.parent = instance.transform.parent;
            aNewBlock.transform.localPosition = newPos;
            aNewBlock.transform.localRotation = Quaternion.Euler(0,0,0);
            aNewBlock.name = "New Block " + newBlockCount++;
        }

        Debug.Log("Raycast created " + aNewBlock.name + ": global pos = " + aNewBlock.transform.position.x
            + " " + aNewBlock.transform.position.y + " " + aNewBlock.transform.position.z + "; local" 
            + " " + aNewBlock.transform.localPosition.x + " " + aNewBlock.transform.localPosition.y + " " + aNewBlock.transform.localPosition.z);

        JimuCamRaycast.sCurrentHitObject = null; // reset
    }
}
