using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Selling : TradeCupcakeTowers
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if(currentSelectTower == null)
        {
            return;
        }

        sugarMeter.ChangeSugar(currentSelectTower.sellPrice);
        Destroy(currentSelectTower.gameObject);
    }
}
