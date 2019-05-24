using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_TouchZoom : MonoBehaviour
{
    public float zoomSpeed = 0.1f;
    public float maxClaim = 10000;

    private GameObject missionController_obj;
    public GameObject SimObj;
    // Start is called before the first frame update
    void Start()
    {
        checkMissionController();
        init_SimObj();

    }



    // Update is called once per frame
    void Update()
    {
        zoom();
    }
    private void init_SimObj()
    {
        SimObj = missionController_obj.GetComponent<cs_MissionController>().Model;
    }
    private void checkMissionController()
    {
        missionController_obj = GameObject.Find("MissionCore").gameObject;
        if (missionController_obj == null)
        {
            Application.Quit();
        }


    }

    private void zoom()
    {

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float scalableSpeed = deltaMagnitudeDiff * zoomSpeed;



            ////SimObj.transform.localScale -= new Vector3(scalableSpeed, scalableSpeed, scalableSpeed);
            //float ox = SimObj.transform.localScale.x - scalableSpeed;
            //float oy = SimObj.transform.localScale.y - scalableSpeed;
            //float oz = SimObj.transform.localScale.z - scalableSpeed;

            //ox = Mathf.Clamp(ox, 0, maxClaim);
            //oy = Mathf.Clamp(oy, 0, maxClaim);
            //oz = Mathf.Clamp(oz, 0, maxClaim);

            SimObj.transform.localScale -= new Vector3(scalableSpeed, scalableSpeed, scalableSpeed);

        }
    }
}
