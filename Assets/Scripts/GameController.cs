/***
 *
 *  GameController
 *  author: antai.ted@gmail.com
 *
 **/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static bool isInteracting;

    public Button btnSwitch;
    public Button btnBuild;
    public Button btnDelete;

    public static GameController instance;

    private List<Sprite> UISpirte;
    void Start ()
    {
        instance = this;

        btnSwitch.gameObject.SetActive(false);
        btnBuild.gameObject.SetActive(false);
        btnDelete.gameObject.SetActive(false);

        UISpirte = new List<Sprite>();

        //crate, dirt, grass, sand, stone, water, door, window
        UISpirte.Add(Resources.Load("UI/tex_red_brick", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_dirt", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_grass", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_sand", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_stone", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_water", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_black_coal", typeof(Sprite)) as Sprite);
        UISpirte.Add(Resources.Load("UI/tex_purple_crystal", typeof(Sprite)) as Sprite);
    }

    void Awake()
    {
        Physics.gravity = Vector3.zero;
        Debug.Log("Awake: set gravity to zero");
    }

    private float timeInterval = 1;
    private float timeLastUpdated = 0;
    void Update ()
    {
        // Note that the Gravity direction is constantly changing, need to have customized gravity events
        if((GameObject.Find("UserDefinedTarget-1") as GameObject) != null)
        {
            Vector3 curGravity =  -(GameObject.Find("UserDefinedTarget-1") as GameObject).transform.up;

            if(curGravity != Physics.gravity)
            {
                Physics.gravity = curGravity;
                //Debug.Log("world physics udpate " + Physics.gravity);
            }
        }
        else
        {
            Physics.gravity = Vector3.zero;
//          Debug.Log("gravity is " + Physics.gravity);
        }
    }


    public void OnCreateSandbox()
    {
        Debug.Log("enabling UI");
        btnSwitch.gameObject.SetActive(true);
        btnBuild.gameObject.SetActive(true);
        btnDelete.gameObject.SetActive(true);

        // more flags and logic
    }

    public void NewBlockRaycast()
    { 
        if(JimuSandbox.instance)
        {
            JimuSandbox.instance.NewBlockRaycast();
        }
    }

    public void DeleteBlock()
    {
        if(JimuCamRaycast.sCurrentHitObject != null)
        {
            Destroy(JimuCamRaycast.sCurrentHitObject);
            JimuCamRaycast.sCurrentHitObject = null;
        }
    }

    public const int kTotalBlockTypeCount = 8;
    public static int sCurrentBlockType = 0;
    public void Switch()
    {
        sCurrentBlockType++;
        if(sCurrentBlockType == kTotalBlockTypeCount)
        {
            sCurrentBlockType = 0;
        }

        btnBuild.GetComponentInChildren<Image>().sprite = UISpirte[sCurrentBlockType] as Sprite;
        //Debug.Log("Current type = " + sCurrentBlockType);
    }

    public void DidFinishInteract()
    {
        isInteracting = false;
    }
}