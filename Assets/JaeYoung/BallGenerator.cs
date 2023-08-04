using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPoint;
    public List<Ball> balls;
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
        balls.Add(Instantiate(ball, spawnPoint.position,transform.rotation,transform).GetComponent<Ball>());
        balls[balls.Count-1].Init(line[index++]);
    }

    public void DestroyBall()
    {
        balls[0].Destroy();
        balls.RemoveAt(0);
    }
}
