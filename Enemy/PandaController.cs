using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PandaController : MonoBehaviour
{
    //Inspector용 외부파라미터
    public float speed;
    public float health;
    public int cakeEatenPerBite;
    public int giveSugar = 10;
    public GameObject scoreEffect;

    Waypoint currentWaypoint;
    [SerializeField] const float changeDist = 0.001f;

    Animator animator;
    Rigidbody2D rb2D;
    GameController gameController;
    Waypoint firstWaypoint;
    SugarMeterScript sugarMeter;
    Transform scoreEffectPart;

    bool isDead = false;

    //애니메이션 해시
    protected int AnimRunTriggerHash = Animator.StringToHash("Run");
    protected int AnimEatTriggerHash = Animator.StringToHash("Eat");
    protected int AnimHitTriggerHash = Animator.StringToHash("Hit");
    protected int AnimDeadTriggerHash = Animator.StringToHash("Dead");

    

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        firstWaypoint = gameController.firstWaypoint;
        currentWaypoint = firstWaypoint;

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sugarMeter = FindObjectOfType<SugarMeterScript>();
        scoreEffectPart = transform.Find("Score");

    }

    void FixedUpdate()
    {
        if (currentWaypoint == null)
        {
            animator.SetTrigger(AnimEatTriggerHash);
            gameController.BiteTheCake(cakeEatenPerBite);
            Destroy(this);
            return;
        }

        float dist = Vector2.Distance(transform.position, currentWaypoint.GetPosition());

        if (dist <= changeDist)
        {
            currentWaypoint = currentWaypoint.GetNextWaypoint();
        }
        else if(!isDead)
        {
            MoveTo(currentWaypoint.GetPosition());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            Hit(other.GetComponent<ProjectileScript>().Hit());
            //Debug.Log(string.Format("Panda hit"));
        }
    }
    */

    void MoveTo(Vector3 destination)
    {
        float step = speed * Time.fixedDeltaTime;
        rb2D.MovePosition(Vector2.MoveTowards(transform.position, destination, step));
        animator.SetTrigger(AnimRunTriggerHash);
    }

    void ScoreEffect()
    {
        GameObject effect = Instantiate(scoreEffect,scoreEffectPart.position, Quaternion.identity) as GameObject;
        effect.GetComponent<ScoreEffect>().ScoreFadeoutUp(giveSugar, scoreEffectPart);
    }

    //애니메이션
    public void Hit(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger(AnimHitTriggerHash);            
        }
        else if (!isDead)
        {
            Dead();
            gameController.EnemyGoToHell();
            ScoreEffect();
        }
    }
    public void Dead()
    {
        animator.SetTrigger(AnimDeadTriggerHash);
        sugarMeter.ChangeSugar(giveSugar);
        Destroy(gameObject,1);
        isDead = true;
        rb2D.MovePosition(gameObject.transform.position);
    }

}
