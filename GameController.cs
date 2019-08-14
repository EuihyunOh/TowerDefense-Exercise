using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject losingScreen;
    public GameObject winningScreen;
    public GameObject pandaPrefab;
    public GameObject spawnPoint;
    public Waypoint firstWaypoint;
    public int numberOfWaves;
    public int numberOfPandasPerWave;
    public int numberOfPandasIncrease;
    public float spawnTime;
    
    HealthbarScript playerHealth;
    RaycastHit2D[] mouseSelects;

    int numberOfEnemy;
    
    

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<HealthbarScript>();
        StartCoroutine("WaveSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseSelects = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));
            Debug.Log(string.Format("Select"));

            if (mouseSelects != null)
            {
                foreach (RaycastHit2D select in mouseSelects)
                {
                    if (select.collider.CompareTag("Tower"))
                    {
                        CupcakeTower selectedTower = select.transform.GetComponent<CupcakeTower>();
                        selectedTower.outlineActive(true);

                        TradeCupcakeTowers.setSelectTower(selectedTower);
                        TowerInfo.UpdateInfo();
                    }
                }
            }
        }
    }

    public void EnemyGoToHell()
    {
        numberOfEnemy--;
    }

    public void BiteTheCake(int damage)
    {
        bool IsCakeAllEaten = playerHealth.ApplyDamage(damage);

        if (IsCakeAllEaten)
        {
            GameOver(false);
        }

        EnemyGoToHell();
    }

    IEnumerator WaveSpawner()
    {
        for(int i = 0; i < numberOfWaves; i++)
        {
            yield return PandaSpawner();
            numberOfPandasPerWave += numberOfPandasIncrease;
        }

        GameOver(true);
    }

    IEnumerator PandaSpawner()
    {
        numberOfEnemy = numberOfPandasPerWave;

        for(int i=0; i<numberOfPandasPerWave; i++)
        {
            Instantiate(pandaPrefab, spawnPoint.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }

        yield return new WaitUntil(() => numberOfEnemy <= 0);
    }

    void GameOver(bool playerHasWon)
    {
        if (playerHasWon)
        {
            winningScreen.SetActive(true);
        }
        else
        {
            losingScreen.SetActive(true);
        }

       // Time.timeScale = 0;
    }
}
