using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TradeCupcakeTowers : MonoBehaviour, IPointerClickHandler
{
    protected static SugarMeterScript sugarMeter;
    protected static CupcakeTower currentSelectTower;

    // Start is called before the first frame update
    void Start()
    {
        if(sugarMeter == null)
        {
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }
    }

    public static void setSelectTower(CupcakeTower cupcakeTower)
    {
        currentSelectTower = cupcakeTower;
    }

    public static CupcakeTower getSelectTower()
    {
        return currentSelectTower;
    }
    public abstract void OnPointerClick(PointerEventData eventData);
}
