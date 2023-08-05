using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.collider.GetComponent<Rigidbody2D>() == null)
            return;
            
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 50000f);
        Destroy(gameObject);
    }
}
