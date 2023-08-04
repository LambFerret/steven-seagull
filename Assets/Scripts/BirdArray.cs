using System;
using UnityEngine;

public class BirdArray : MonoBehaviour
{
    public GameObject[] birds;

    public float space;
    private float _birdWidth;
    private void Awake()
    {
        birds = GameObject.FindGameObjectsWithTag("Seagull");
        _birdWidth = birds[0].transform.Find("Body").GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Start()
    {
        RelocateBirds();
    }

    private void RelocateBirds()
    {
        int index = 0;
        foreach (var bird in birds)
        {

            var birdPos = bird.transform.position;
            bird.gameObject.transform.position= new Vector3(birdPos.x + _birdWidth * index + space * index, birdPos.y, birdPos.z);
            index++;
        }
    }
}