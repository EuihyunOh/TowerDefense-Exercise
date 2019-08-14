using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeTower : MonoBehaviour
{
    //외부 파라미터
    [Range(0.0f,1000.0f)][Header("공격 범위")][Tooltip("원형으로 탐지한다")]public float rangeRadius;
    [Range(0.0f,1000.0f)][Header("공격 간격")][Tooltip("단위:Sec")]public float reloadTime;
    [Header("발사체")]public GameObject projectilePrefab;
    [Header("업그레이드 레벨")]public int upgradeLevel;
    [Header("업그레이드 스프라이트")]public Sprite[] upgradeSprites;
    [Header("업그레이드 가능")]public bool isUpgradable = true;
    public int initialCost;
    public int upgradingCost;
    public int buyPrice;
    public int sellPrice;

    //내부 파라미터
    private float shotTime;

    SpriteOutline outline;
    //CircleCollider2D fireRange;

    void Awake()
    {
        //fireRange = GetComponent<CircleCollider2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<SpriteOutline>();
        buyPrice = initialCost;
        sellPrice = buyPrice / 2;
        //fireRange.radius = rangeRadius;
    }

    // Update is called once per frame
    void Update()
    {

        //선택되었는가 아니면 아웃라인 해제
        if(TradeCupcakeTowers.getSelectTower() != this)
        {
            outlineActive(false);
        }


        if(shotTime >= reloadTime)
        {
            //발사 시간 체크
            shotTime = 0;
            //콜라이더 범위 안에 오브젝트가 있는지 체크
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeRadius);

            //하나 이상의 오브젝트가 감지됐는지 체크
            if(hitColliders.Length != 0)
            {
                //가장 가까운 곳에 있는 적을 판별
                float min = int.MaxValue;
                int index = -1;
                for(int i=0; i < hitColliders.Length; i++)
                {
                    if(hitColliders[i].tag == "Enemy")
                    {
                        if(hitColliders[i].GetComponentInParent<PandaController>().health < 0)
                        {
                            continue;
                        }
                        float distance = Vector2.Distance(hitColliders[i].transform.position, transform.position);
                        if (distance < min)
                        {
                            index = i;
                            min = distance;
                        }
                    }                    
                }

                if (index == -1)
                {
                    return;
                }

                //타깃으로 방향 설정
                Transform target = hitColliders[index].transform;
                Vector2 direction = (target.position - transform.position).normalized;

                //발사체 생성
                GameObject projectile = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<ProjectileScript>().direction = direction;
            }            
        }
        shotTime += Time.deltaTime;
    }

    /*
    void OnTriggerEnter2D(Collider2D cd2D)
    {
        if(tag == "Enemy")
        {
            Transform target = cd2D.transform;
            Vector2 direction = (target.position - transform.position).normalized;

            GameObject projectile = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<ProjectileScript>().direction = direction;
        }
    }
    */

    public void Upgrade()
    {
        //업그레이드 가능한지 체크
        if (!isUpgradable)
        {
            return;
        }

        //레벨업
        upgradeLevel++;

        //최대치 인가?
        if(upgradeLevel > upgradeSprites.Length)
        {
            isUpgradable = false;
        }

        //타워 스탯 업
        rangeRadius += 1.0f;
        reloadTime -= 0.5f;

        // 타워 그래픽 변경
        GetComponent<SpriteRenderer>().sprite = upgradeSprites[upgradeLevel];

        //비용 증가
        buyPrice += 5;
        upgradingCost += 10;
        TowerInfo.UpdateInfo();
    }

    /*
    void OnMouseDown()
    {
        //Debug.Log(string.Format("Select : " + gameObject.name));
        TradeCupcakeTowers.setSelectTower(this);
        outlineActive(true);
        
    }
    */

    public void outlineActive(bool _switch)
    {
        if (_switch)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}
