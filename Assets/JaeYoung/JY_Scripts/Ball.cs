using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary> ���� ���� ������ �ִ� �ܾ� </summary>
    public string word;

    /// <summary> ���۽� �θ� ball�� ��üȭ�ϰ� �ܾ� ����Ʈ���� �ܾ �־��ְ� ���� �־���</summary>
    /// <param name="word"></param>
    public void Init(string word)
    {
        this.word = word;
        GetComponentInChildren<TextMeshPro>().text = word;
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    /// <summary> ���������� ������, �θ𿡼� ȣ���ϱ� ������ ������ ���� </summary>
    public void Goal()
    {
        Destroy(gameObject);
    }

    /// <summary> �ı��Ǿ����� �ٽ� ���� </summary>
    public void Destroy()
    {
        transform.parent.GetComponent<BallGenerator>().GenerateBall(word);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}