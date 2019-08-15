using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreEffect : MonoBehaviour
{
    public float fadeTime = 0.3f;
    public float moveSpeed = 1.0f;

    TextMeshPro text;
    Color origin;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        origin = text.color;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void moveUp()
    {
        rb2D.velocity = new Vector2(0.1f, moveSpeed);
    }

    void setText(string txt)
    {
        text.text = txt;
    }

    //스코어 점수 나타나는 포인트를 받아온다
    public void ScoreFadeoutUp(int score, Transform scoreParts)
    {
        setText((score >= 0 ? "+" : "-") + score.ToString());
        transform.position = scoreParts.position;

        StartCoroutine("FadeOut");
        moveUp();
        Destroy(gameObject, fadeTime);
    }


    IEnumerator FadeOut()
    {
        float time = 0.0f;
        
        Color fadeColor = origin;
        fadeColor.a = Mathf.Lerp(1.0f, 0.0f, time);

        while (fadeColor.a > 0.0f)
        {
            time += Time.deltaTime / fadeTime;
            fadeColor.a = Mathf.Lerp(1.0f, 0.0f, time);
            text.color = fadeColor;
            
            yield return null;
        }        
    }
}
