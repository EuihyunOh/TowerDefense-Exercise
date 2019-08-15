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
        int sellPrice = currentSelectTower.sellPrice;
        sugarMeter.ChangeSugar(sellPrice);
        currentSelectTower.ScoreEffect(sellPrice);

        Destroy(currentSelectTower.gameObject);
    }
}
