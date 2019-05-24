using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_Bokory : MonoBehaviour {
    public GameObject Farm;
    [Range(0,1)]
    public float size = 0;
    public float minSize = 0;
    public float maxSize = 0.7f;
    [Range(0,1)]
    public float hp = 1f;

    public bool isDry = false;
    public bool isWet = false;
    public bool isDie = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        changeColor();

    }

    void changeColor()
    {
        for (int i = 0; i < this.gameObject.transform.GetChildCount(); i++)
        {
            Transform tc = this.gameObject.transform.GetChild(i).gameObject.transform;
            growth(tc);
            for (int j = 0; j < tc.GetChildCount(); j++)
            {

                Renderer rend = tc.GetChild(j).gameObject.GetComponent<Renderer>();
                //rend.material.shader = Shader.Find("_Color");
                //rend.material.SetColor("_Color", Color.HSVToRGB(hp, 0f, 1f));
                rend.material.color = Color.HSVToRGB(0.12f, 1 - hp, hp);

            }

        }
    }

    public void reduceHP(float rmv_hp_percentage)
    {
        hp -= rmv_hp_percentage;
        hp = Mathf.Clamp01(hp);
    }

    public void incressHP(float inc_hp_percentage)
    {
        hp += inc_hp_percentage;
        hp = Mathf.Clamp01(hp);
    }

    void growth(Transform tc)
    {
        tc.localScale = new Vector3(getCurrentSize(), getCurrentSize(), getCurrentSize());
    }

    void WetBHV(Renderer rend)
    {        
        rend.material.color = Color.HSVToRGB(0.12f, 1 - hp, hp);           
    }

    float getCurrentSize()
    {
        return (maxSize - minSize)*size;
    }
}
