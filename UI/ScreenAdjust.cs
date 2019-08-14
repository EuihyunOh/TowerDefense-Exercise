using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour
{
    public float aspectWH = 1.6f;
    public float aspectAdd = 0.05f;

    public bool screenAdjust = true;
    public bool updateScreenAdjust = false;

    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        if (screenAdjust)
        {
            Adjust();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Adjust()
    {
        float wh = (float)Screen.width / (float)Screen.height;
        Debug.Log(string.Format("aspectWH:{0} wh:{1}", aspectWH, wh));
        if(wh < aspectWH)
        {
            transform.localScale = new Vector3(localScale.x - (aspectWH - wh) + aspectAdd, localScale.y, localScale.z);
        }
        else
        {
            transform.localScale = localScale;
        }
    }
}
