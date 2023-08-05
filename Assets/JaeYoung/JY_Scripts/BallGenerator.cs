using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public AnimationCurve curve;
    public List<GameObject> ball;
    public GameObject fake;
    public Transform spawnPoint;
    public List<Ball> GeneratedBalls;
    public List<Ball> FakeBalls;
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
        GeneratedBalls[GeneratedBalls.Count-1].Init(line[index]);
    }

    public void GenerateFake()
    {
        FakeBalls.Add(Instantiate(ball[Random.Range(0, ball.Count)], spawnPoint.position, transform.rotation, transform).GetComponent<Ball>());
        FakeBalls[FakeBalls.Count - 1].Init("FAKE");
        FakeBalls[FakeBalls.Count - 1].tag="Fake";
    }
    /// <summary> 공이 골인했으면 호출, 골인하면 리스트에서 삭제/ball에서 Goal실행, 인덱스 증가 </summary>
    public void GoalIn()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Goal();
        GeneratedBalls.RemoveAt(0);
        index++;
    }
    
    /// <summary> 공이 파괴되었으면 호출, destroy실행 </summary>
    public void DestroyBall()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Destroy();
        GeneratedBalls.RemoveAt(0);
    }
}
