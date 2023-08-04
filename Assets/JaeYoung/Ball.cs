using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary> ���� ���� ������ �ִ� �ܾ� </summary>
    public string Word;

    public void Init(string word)
    {
        Word = word;
        GetComponentInChildren<TextMeshPro>().text = word;
        Debug.Log(word);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
