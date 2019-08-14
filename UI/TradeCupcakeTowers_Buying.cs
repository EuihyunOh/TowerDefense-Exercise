using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowers_Buying : TradeCupcakeTowers
{
    public GameObject cupcakeTowerPrefab;

    public override void OnPointerClick(PointerEventData eventData)
    {
        int price = cupcakeTowerPrefab.GetComponent<CupcakeTower>().buyPrice;

        //자원이 되는지 확인
        if(price <= sugarMeter.getSugarAmount())
        {
            Debug.Log(string.Format("Buy"));
            sugarMeter.ChangeSugar(-price);
            GameObject newTower = Instantiate(cupcakeTowerPrefab);
            currentSelectTower = newTower.GetComponent<CupcakeTower>();
        }
    }
}
