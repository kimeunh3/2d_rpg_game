using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    float attackDelay;

    Enemy enemy;
    Animator enemyAnimator;
    void Start()
    {
        // print("here");
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;

    }

    
    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        // 타겟과 자신의 거리를 확인
        float distance = Vector3.Distance(transform.position, target.position);
        // 공격 딜레이(쿨타임)가 0일 때, 시야 범위안에 들어올 때
        if (attackDelay == 0 && distance <= enemy.status.fieldOfVision)
        {
            // 타겟 바라보기
            FaceTarget();

            // 공격 범위 안에 들어올 때 공격
            if (distance <= enemy.status.atkRange)
            {
                AttackTarget();
            }
            else // 공격 애니메이션 실행 중이 아닐 때 추적
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else // 시야 범위 밖에 있을 때 Idle 애니메이션으로 전환
        {
            enemyAnimator.SetBool("moving", false);
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * enemy.status.moveSpeed * Time.deltaTime);
        enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        // 타겟이 왼쪽에 있을 때
        if (target.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void AttackTarget()
    {
        target.GetComponent<Sword_Man>().status.nowHp -= enemy.status.atkDmg;
        // 공격 애니메이션 실행
        enemyAnimator.SetTrigger("attack");
        // 딜레이 충전
        attackDelay = enemy.status.atkSpeed;
    }
}
