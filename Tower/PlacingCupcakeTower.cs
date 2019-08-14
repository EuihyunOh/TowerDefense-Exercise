using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingCupcakeTower : MonoBehaviour
{
    Color originColor;
    DeployAreaCheck deployAreaCheck;
    // Start is called before the first frame update

    void Start()
    {
        originColor = GetComponent<SpriteRenderer>().color;
        deployAreaCheck = FindObjectOfType<DeployAreaCheck>();    
        GetComponent<CupcakeTower>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 포인터 위치
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));

        //배치 가능 영역 체크
        if (Input.GetMouseButtonDown(0) && deployAreaCheck.isPointerOnAllowedArea())
        {
            GetComponent<CupcakeTower>().enabled = true;
            GetComponent<CupcakeTower>().outlineActive(true);
            gameObject.AddComponent<BoxCollider2D>();

            TowerInfo.UpdateInfo();
            Destroy(this);        
        }

        //배치 안되면 빨간색
        if (!deployAreaCheck.isPointerOnAllowedArea())
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = originColor;
        }
    }
}
