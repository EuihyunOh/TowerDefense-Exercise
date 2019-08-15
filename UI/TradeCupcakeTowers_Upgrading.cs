using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Upgrading : TradeCupcakeTowers
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if(currentSelectTower.isUpgradable && currentSelectTower.upgradingCost <= sugarMeter.getSugarAmount())
        {
            int upgradeCost = -currentSelectTower.upgradingCost;
            sugarMeter.ChangeSugar(upgradeCost);
            currentSelectTower.ScoreEffect(upgradeCost);
            currentSelectTower.Upgrade();
        }
    }

}
