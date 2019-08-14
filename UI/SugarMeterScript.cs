using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SugarMeterScript : MonoBehaviour
{
    //캐시
    Text sugarMeter; 
    int sugar; // 자원의 양

    // Start is called before the first frame update
    void Start()
    {
        sugarMeter = GetComponentInChildren<Text>();

        updateSugarMeter();
    }

    public void ChangeSugar(int value)
    {
        sugar += value;

        if(sugar < 0)
        {
            sugar = 0;
        }

        updateSugarMeter();
    }

    public int getSugarAmount()
    {
        return sugar;
    }

    void updateSugarMeter()
    {
        sugarMeter.text = sugar.ToString();
    }
}
