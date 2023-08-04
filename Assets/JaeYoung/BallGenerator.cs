using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public List<GameObject> ball;
    public Transform spawnPoint;
    public List<Ball> GeneratedBalls;
    /// <summary> 공에 넣을 문장 단어 List </summary>
    public List<string> line = new List<string>();
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        index=0;
        spawnPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// 공 생성
    /// </summary>
    public void GenerateBall()
    {
        if (index >= line.Count) return;
        GeneratedBalls.Add(Instantiate(ball[Random.Range(0,ball.Count)], spawnPoint.position,transform.rotation,transform).GetComponent<Ball>());
        GeneratedBalls[GeneratedBalls.Count-1].Init(line[index++]);
    }

    public void DestroyBall()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Destroy();
        GeneratedBalls.RemoveAt(0);
    }
}
