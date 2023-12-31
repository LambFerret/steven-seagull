﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.UIElements;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [Header("Settings")] public float splitForce = 19;
    public float angle = 0.2f;

    [Header("Game Objects")]
    // public AnimationCurve curve;
    public List<GameObject> ball;

    public GameObject ballPlaceHolder;

    public GameObject bomb;
    public GameObject spawnPoint;
    public List<Ball> GeneratedBalls;
    public List<Ball> FakeBalls;
    private PhysicsMaterial2D material;

    [Header("Info")] private Vector3 _spawnPoint;

    /// <summary> 공에 넣을 문장 단어 List </summary>
    public List<string> line = new List<string>();

    public int index;

    private void Start()
    {
        _spawnPoint = spawnPoint.transform.position;
    }

    public IEnumerator SpawnBall(string sentence)
    {
        List<string> words = new List<string>(sentence.Split(' '));
        int index = 0;
        while (index < words.Count)
        {
            Debug.Log("CURSOR");
            GenerateBall(words[index++]);
            if (index % 2 == 0) yield return new WaitForSeconds(4F);
        }
    }

    /// <summary> 공 생성 </summary>
    public void GenerateBall(string word)
    {
        var ballGameObject = Instantiate(ball[Random.Range(0, ball.Count)], _spawnPoint, transform.rotation,
            ballPlaceHolder.transform).GetComponent<Ball>();
        ballGameObject.Init(word);
        ballGameObject.GetComponent<Rigidbody2D>().mass *= Random.Range(0.5f, 1f);
        material = ballGameObject.GetComponent<Rigidbody2D>().sharedMaterial;
        material.friction = 5f;
        material.bounciness = 0.7f;
        ballGameObject.GetComponent<Rigidbody2D>().sharedMaterial = material;
        GeneratedBalls.Add(ballGameObject);

        ballGameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1F, Random.Range(-angle, angle)) * splitForce);

        Debug.Log(material.bounciness + " / " + ballGameObject.GetComponent<Rigidbody2D>().mass);
    }

    /// <summary> 가짜 공 생성 </summary>
    public void GenerateFake()
    {
        var selectedBall = ball[Random.Range(0, ball.Count)];
        var fakeBall = Instantiate(selectedBall, _spawnPoint, transform.rotation, ballPlaceHolder.transform)
            .GetComponent<Ball>();
        fakeBall.Init("FAKE");
        fakeBall.tag = "Fake";
        FakeBalls.Add(fakeBall);
    }

    /// <summary> 폭탄 생성 </summary>
    public void GenerateBomb()
    {
        Instantiate(bomb, _spawnPoint, transform.rotation, transform).GetComponent<Bomb>();
    }

    /// <summary> 공이 골인했으면 호출, 골인하면 리스트에서 삭제/ball에서 Goal실행, 인덱스 증가 </summary>
    public void GoalIn()
    {
        if (GeneratedBalls.Count < 1) return;
        GeneratedBalls[0].Goal();
        GeneratedBalls.RemoveAt(0);
    }

    /// <summary> 공이 파괴되었으면 호출, destroy실행 </summary>
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