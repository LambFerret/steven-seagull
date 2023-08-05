using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void Init()
    {
        GetComponentInChildren<TextMeshPro>().text = "BOMB!";

    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            Debug.Log("BOOM!");
            collision.transform.GetComponent<Ball>().Destroy();
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Fake")
        {
            Debug.Log("BOOM!");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
