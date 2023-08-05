using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [Header("Settings")] public float splitForce = 19;
    public float angle = 0.2f;

    [Header("Game Objects")]
    // public AnimationCurve curve;
    public List<GameObject> ball;

    public GameObject bomb;
    public GameObject spawnPoint;
    public List<Ball> GeneratedBalls;
    public List<Ball> FakeBalls;
    private PhysicsMaterial2D material;

    /// <summary> ���� ���� ���� �ܾ� List </summary>
    [Header("Info")] private Vector3 _spawnPoint;

    private void Start()
    {
        _spawnPoint = spawnPoint.transform.position;
    }

    public void SpawnBall(string sentence)
    {
        List<string> words = new List<string>(sentence.Split(' '));
        int index = 0;
        while (index < words.Count)
        {
            StartCoroutine(GenerateBall(words[index++]));
        }
    }

    /// <summary> �� ���� </summary>
    public IEnumerator GenerateBall(string word)
    {
        var ballGameObject = Instantiate(ball[Random.Range(0, ball.Count)], _spawnPoint, transform.rotation, transform)
            .GetComponent<Ball>();
        ballGameObject.Init(word);
        ballGameObject.GetComponent<Rigidbody2D>().mass *= Random.Range(0.5f, 1f);
        material = ballGameObject.GetComponent<Rigidbody2D>().sharedMaterial;
        material.friction = 0f;
        material.bounciness = Random.Range(0.5f, 1f);
        ballGameObject.GetComponent<Rigidbody2D>().sharedMaterial = material;
        GeneratedBalls.Add(ballGameObject);

        ballGameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1F, Random.Range(-angle, angle)) * splitForce);
        yield return new WaitForSeconds(3f);

        Debug.Log(material.bounciness + " / " + ballGameObject.GetComponent<Rigidbody2D>().mass);
    }

    /// <summary> ��¥ �� ���� </summary>
    public void GenerateFake()
    {
        var selectedBall = ball[Random.Range(0, ball.Count)];
        var fakeBall = Instantiate(selectedBall, _spawnPoint, transform.rotation, transform).GetComponent<Ball>();
        fakeBall.Init("FAKE");
        fakeBall.tag = "Fake";
        FakeBalls.Add(fakeBall);
    }

    /// <summary> ��ź ���� </summary>
    public void GenerateBomb()
    {
        Instantiate(bomb, _spawnPoint, transform.rotation, transform).GetComponent<Bomb>();
    }

    /// <summary> ���� ���������� ȣ��, �����ϸ� ����Ʈ���� ����/ball���� Goal����, �ε��� ���� </summary>
    public void GoalIn()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Goal();
        GeneratedBalls.RemoveAt(0);
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