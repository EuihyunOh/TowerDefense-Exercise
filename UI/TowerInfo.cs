using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    static Text atk;
    static Text dps;
    static Text upgradeCost;
    static Text[] textInfo;

    // Start is called before the first frame update
    void Awake()
    {
        textInfo = GetComponentsInChildren<Text>();

    }

    void Update()
    {
        if (TradeCupcakeTowers.getSelectTower() == null)
        {
            foreach (Text text in textInfo)
            {
                text.enabled = false;
            }
            return;
        }
        else
        {
            foreach (Text text in textInfo)
            {
                text.enabled = true;
            }
        }
    }


    public static void UpdateInfo()
    {
        CupcakeTower tower = TradeCupcakeTowers.getSelectTower();
        ProjectileScript projectile = tower.projectilePrefab.GetComponent<ProjectileScript>();

        foreach (Text text in textInfo)
        {
            switch (text.name)
            {
                case "Atk":
                    text.text = projectile.damage.ToString();
                    break;
                case "Dps":
                    text.text = (projectile.damage * 1 / tower.reloadTime).ToString();
                    break;
                case "UpgradeCost":
                    text.text = tower.upgradingCost.ToString();
                    break;
            }
        }
    }
}
