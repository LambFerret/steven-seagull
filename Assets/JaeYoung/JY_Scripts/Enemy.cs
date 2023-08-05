using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rotationSpeed = 5f; // 회전 속도
    public float attackRange = 5f; // 공격 범위
    public float attackRate = 2f; // 공격 간격 (초 단위)
    private float attackTimer = 0f; // 공격 타이머
    public float spreadAngle; // 공격 각도
    public GameObject bullet;

    public float angle;

    void Update()
    {
        GameObject closestBall = FindClosestBall();

        if (closestBall != null)
        {
            
            // 공격 범위 안에 들어온 경우에만 플레이어를 바라보고 공격합니다.
            float distanceToPlayer = Vector2.Distance(transform.position, closestBall.transform.position);
            if (distanceToPlayer <= attackRange)
            {
                transform.localScale = new Vector3(-1, -1, 1);
                // 플레이어의 방향을 바라보도록 적 캐릭터를 회전시킵니다.
                Vector2 direction = closestBall.transform.position - transform.position;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                float maxAngle = (angle + spreadAngle) * Mathf.Deg2Rad;
                float minAngle = (angle - spreadAngle) * Mathf.Deg2Rad;

                Debug.DrawRay(this.transform.position, direction, Color.red);
                Debug.DrawRay(this.transform.position, attackRange * new Vector2(Mathf.Cos(maxAngle), Mathf.Sin(maxAngle)), Color.blue);
                Debug.DrawRay(this.transform.position, attackRange * new Vector2(Mathf.Cos(minAngle), Mathf.Sin(minAngle)), Color.blue);
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

        float randomAngle = UnityEngine.Random.Range(-spreadAngle, spreadAngle);

        // 라디안 각도로 변환합니다.
        float radianAngle = randomAngle * Mathf.Deg2Rad;

        // 현재 방향에 랜덤한 라디안 각도를 더하여 총알 발사 방향을 결정합니다.
        float totalAngle = Mathf.Atan2(transform.right.y, transform.right.x) + radianAngle;

        // 라디안 각도를 디그리로 변환합니다.
        float finalAngle = totalAngle * Mathf.Rad2Deg;

        // 최종 발사 방향을 결정하는 Quaternion을 생성합니다.
        Quaternion spreadRotation = Quaternion.AngleAxis(finalAngle, Vector3.forward);

        // 총알을 생성하고 발사합니다.
        GameObject _bullet = Instantiate(bullet, transform.position, spreadRotation);
        //GameObject _bullet =  Instantiate(bullet, transform.position, transform.rotation);
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
