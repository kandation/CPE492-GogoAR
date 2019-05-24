using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class water_tank_bhv : MonoBehaviour {

    public AnimationState clipState;
    public Animator anim;
    public Animation ak;

    public GameObject sacleUp;
    public GameObject sacleDown;
    public GameObject modify;
    public GameObject modObj;
    public GameObject tank;
    public GameObject modpump;

    void Start()
    {
        modObj.SetActive(false);
        modpump.SetActive(false);
        anim = GetComponent<Animator>();
        sacleUp.GetComponent<Button>().onClick.AddListener(scaleup);
        sacleDown.GetComponent<Button>().onClick.AddListener(scaledown);
        modify.GetComponent<Button>().onClick.AddListener(moda);
        //foreach (AnimationState state in anim)
        //{
        //    state.speed = 0.5F;
        //}
    }

    void scaleup()
    {
        tank.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    void scaledown()
    {
        tank.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    void moda()
    {
        modObj.SetActive(!modObj.active);
        modpump.SetActive(!modpump.active);

    }
    // Update is called once per frame
    void Update () {
        //anim.Play("waterTranform");

		
	}
}
