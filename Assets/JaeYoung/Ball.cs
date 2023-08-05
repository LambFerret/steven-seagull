using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{   
    public SpriteRenderer tipSprite;
    /// <summary> ���� ���� ������ �ִ� �ܾ� </summary>
    public string Word;

    public void Init(string word)
    {
        Word = word;
        GetComponentInChildren<TextMeshPro>().text = word;
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<SpriteRenderer>().color = newColor;
        tipSprite.color = newColor;
    }
}