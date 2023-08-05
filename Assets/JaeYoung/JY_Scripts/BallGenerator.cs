using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public AnimationCurve curve;
    public List<GameObject> ball;
    public GameObject bomb;
    public Transform spawnPoint;
    public List<Ball> GeneratedBalls;
    public List<Ball> FakeBalls;
    private PhysicsMaterial2D material;
    /// <summary> ���� ���� ���� �ܾ� List </summary>
    public List<string> line = new List<string>();
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        spawnPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary> �� ���� </summary>
    public void GenerateBall()
    {
        if (index >= line.Count) return;
        GeneratedBalls.Add(Instantiate(ball[Random.Range(0, ball.Count)], spawnPoint.position, transform.rotation, transform).GetComponent<Ball>());
        GeneratedBalls[GeneratedBalls.Count - 1].Init(line[index]);
        GeneratedBalls[GeneratedBalls.Count - 1].GetComponent<Rigidbody2D>().mass *= Random.Range(0.5f,1f);
        material = GeneratedBalls[GeneratedBalls.Count - 1].GetComponent<Rigidbody2D>().sharedMaterial;
        material.friction = 0f;
        material.bounciness = Random.Range(0.5f, 1f);
        GeneratedBalls[GeneratedBalls.Count - 1].GetComponent<Rigidbody2D>().sharedMaterial = material;

        Debug.Log(material.bounciness.ToString()+""+ GeneratedBalls[GeneratedBalls.Count - 1].GetComponent<Rigidbody2D>().mass.ToString());
    }

    /// <summary> ��¥ �� ���� </summary>
    public void GenerateFake()
    {
        FakeBalls.Add(Instantiate(ball[Random.Range(0, ball.Count)], spawnPoint.position, transform.rotation, transform).GetComponent<Ball>());
        FakeBalls[FakeBalls.Count - 1].Init("FAKE");
        FakeBalls[FakeBalls.Count - 1].tag = "Fake";
    }

    /// <summary> ��ź ���� </summary>
    public void GenerateBomb()
    {
        Instantiate(bomb, spawnPoint.position, transform.rotation, transform).GetComponent<Bomb>();
    }

    /// <summary> ���� ���������� ȣ��, �����ϸ� ����Ʈ���� ����/ball���� Goal����, �ε��� ���� </summary>
    public void GoalIn()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Goal();
        GeneratedBalls.RemoveAt(0);
        index++;
    }

    /// <summary> ���� �ı��Ǿ����� ȣ��, destroy���� </summary>
    public void DestroyBall()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Destroy();
        GeneratedBalls.RemoveAt(0);
    }
    float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
}
