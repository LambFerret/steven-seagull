using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rotationSpeed = 5f; // 회전 속도
    public float attackRange = 5f; // 공격 범위
    public float attackRate = 2f; // 공격 간격 (초 단위)
    private float attackTimer = 0f; // 공격 타이머

    public GameObject bullet;

    void Update()
    {
        GameObject closestBall = FindClosestBall();

        if (closestBall != null)
        {
            // 공격 범위 안에 들어온 경우에만 플레이어를 바라보고 공격합니다.
            float distanceToPlayer = Vector2.Distance(transform.position, closestBall.transform.position);
            if (distanceToPlayer <= attackRange)
            {
                // 플레이어의 방향을 바라보도록 적 캐릭터를 회전시킵니다.
                Vector2 direction = closestBall.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                Debug.DrawRay( this.transform.position, closestBall.transform.position, Color.red);
                // 공격 타이머 업데이트
                attackTimer += Time.deltaTime;

                // 일정 시간마다 플레이어를 공격합니다.
                if (attackTimer >= attackRate)
                {
                    attackTimer = 0f;
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        // 여기에 공격하는 코드를 추가합니다.
        GameObject _bullet =  Instantiate(bullet, transform.position, transform.rotation);
        _bullet.GetComponent<Rigidbody2D>().AddForce(500*(Vector2)(Quaternion.Euler(0, 0, _bullet.transform.rotation.eulerAngles.z) * Vector2.right));
        // 예를 들어, 총알을 발사하거나 플레이어의 체력을 감소시키는 등의 작업을 수행할 수 있습니다.
        Debug.Log("공격!");
    }

    GameObject FindClosestBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length == 0)
        {
            return null;
        }

        GameObject closestBall = balls[0];
        float closestDistance = Vector2.Distance(transform.position, closestBall.transform.position);

        foreach (GameObject ball in balls)
        {
            float distance = Vector2.Distance(transform.position, ball.transform.position);
            if (distance < closestDistance)
            {
                closestBall = ball;
                closestDistance = distance;
            }
        }

        return closestBall;
    }
}
