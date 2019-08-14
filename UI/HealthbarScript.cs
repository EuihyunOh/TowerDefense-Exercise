using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    //inspector용 외부 파라미터
    public int maxHealth;

    //내부 파라미터
    protected Image fillingImage;
    protected int health;

    // Start is called before the first frame update
    void Start()
    {
        fillingImage = GetComponentInChildren<Image>();
        health = maxHealth;

        updateHealthBar();
    }
     
    //플레이어에게 데미지 적용
    public bool ApplyDamage(int value)
    {
        health -= value;

        if(health > 0)
        {
            updateHealthBar();
            return false;
        }

        health = 0;
        updateHealthBar();
        return true;
    }

    void updateHealthBar()
    {
        float percentage = health * 1.0f / maxHealth;

        fillingImage.fillAmount = percentage;
    }
}
