using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class cs_CheckTrack : MonoBehaviour, ITrackableEventHandler
{
    public TrackableBehaviour theTrackable;
    public GameObject gogoGameObject;
    private GogoAPI gogo;
    public GameObject backGroundBeforeRun;
    private bool mSwapModel = false;
    public GameObject model;
    public bool nextLevel = false;
    private bool firstActiveModel = false;
    public GameObject ARCamera;

    private bool firstReset = true;
    // Use this for initialization
    void Start()
    {
        gogo = gogoGameObject.GetComponent<GogoAPI>();
        

        if (theTrackable == null)
        {
            Debug.Log("Warning: Trackable not set !!");
        }
        if (theTrackable)
        {
            theTrackable.RegisterTrackableEventHandler(this);
        }
    }

    void Update()
    {

        closeFirst();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            Debug.Log(scene.name);
            SceneManager.LoadScene(scene.name);

        }
        
    }

    void closeFirst()
    {
        if (gogo != null)
        {
            if (gogo.isConnected && firstReset)
            {
                gogo.sendCommand("cmd::LogoControl::0");
                firstReset = false;
            }
        }
    }
    /*void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 120, 40), "Swap Model"))
        {
            mSwapModel = true;
        }
    }*/

    public void StartSimulator()
    {
        if (gogo != null)
        {
            if (gogo.isConnected)
            {
                gogo.sendCommand("cmd::LogoControl::0");
                gogo.sendCommand("cmd::LogoControl::1");
            }
        }
        
        if (!mSwapModel)
        {
            mSwapModel = true;
            nextLevel = true;
        }
        if(backGroundBeforeRun != null)
        {
            backGroundBeforeRun.SetActive(false);
        }
     


    }
    public void renew()
    {
        if (nextLevel)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            SceneManager.LoadScene(scene.name);
        }
    }
    private void SwapModel()
    {
        GameObject trackableGameObject = theTrackable.gameObject;
        //disable any pre-existing augmentation
        for (int i = 0; i < trackableGameObject.transform.childCount; i++)
        {
            Transform child = trackableGameObject.transform.GetChild(i);
            child.gameObject.active = false;
        }
        // Create a simple cube object
        if (!model.active)
        {
            model.SetActive(true);
        }
        
        GameObject cube = model;


        // Re-parent the cube as child of the trackable gameObject
        cube.transform.parent = theTrackable.transform;
        // Adjust the position and scale
        // so that it fits nicely on the target
        cube.transform.localPosition = new Vector3(0, 0f, 0);
        cube.transform.localRotation = Quaternion.identity;
        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        // Make sure it is active
        cube.active = true;
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();

        }
        else
        {
            
        }
    }

    void OnTrackingFound()
    {
        if (mSwapModel && theTrackable != null)
        {
            SwapModel();
            mSwapModel = false;
        }
    }
}
