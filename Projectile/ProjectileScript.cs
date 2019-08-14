using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //외부 파라미터
    public float damage;
    public float speed = 1.0f;
    public Vector3 direction;
    public float lifeDuration = 10.0f;

    //내부 파라미터
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        direction = direction.normalized;
        float angle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Destroy(gameObject, lifeDuration);
    }

    void fixedUpdate()
    {
        rb2D.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += direction * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(string.Format("Trigger"));
        if (other.CompareTag("Enemy"))
        {
            Hit(other.GetComponent<PandaController>());
            //Debug.Log(string.Format("Hit"));
        }
    }

    void Hit(PandaController Target)
    {
        Target.Hit(damage);
        Destroy(gameObject);
    }

}
