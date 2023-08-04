using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallUp()
    {
        StartCoroutine(UP());
    }
    public IEnumerator UP()
    {
        transform.Translate(Vector3.up);
        yield return new WaitForSeconds(1f);
        transform.Translate(Vector3.down);
    }
}
