using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{   
    /// <summary> 현재 공의 가지고 있는 단어 </summary>
    public string word;

    public SpriteRenderer tipSprite;

    /// <summary> 시작시 부모가 ball을 객체화하고 단어 리스트에서 단어를 넣어주고 색도 넣어줌</summary>
    /// <param name="word"></param>
    public void Init(string word)
    {
        this.word = word;
        GetComponentInChildren<TextMeshPro>().text = word;
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    /// <summary> 골인했으면 삭제만, 부모에서 호출하기 때문에 삭제만 해줌 </summary>
    public void Goal()
    {
        Destroy(gameObject);
    }

    /// <summary> 파괴되었으면 다시 생성 </summary>
    public void Destroy()
    {
        transform.parent.gameObject.GetComponent<BallGenerator>().StartCoroutine(transform.parent.gameObject.GetComponent<BallGenerator>().GenerateBall(word));
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<SpriteRenderer>().color = newColor;
        tipSprite.color = newColor;
    }
}