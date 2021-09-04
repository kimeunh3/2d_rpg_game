using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sword_Man : MonoBehaviour
{
    Animator animator;
    public Status status;

    // 여러번 공격 방지용
    public bool attacked = false;
    public Image nowHpbar;

    bool inputRight = false;
    bool inputLeft = false;
    Rigidbody2D rigid2D;

    BoxCollider2D col2D;

    public float jumpPower = 40;
    bool inputJump = false;

    bool isSwordManDead = false;

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    public void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        status.atkSpeed = speed;
    }
    public float GetAttackSpeed()
    {
        return status.atkSpeed;
    }

    public void SetMoveSpeed(float speed)
    {
        status.moveSpeed = speed;
    }

    public float GetMoveSpeed()
    {
        return status.moveSpeed;
    }
    
    void Start()
    {
        // 오브젝트의 위치 초기화
        transform.position = new Vector3(0,0,0);
        // 오브젝트의 Animator 데이터를 받아옴
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<BoxCollider2D>();

        // 소드맨 스테이터스 설정
        status = new Status();
        status = status.SetUnitStatus(UnitCode.swordman);

        SetAttackSpeed(status.atkSpeed);
        StartCoroutine(CheckSwordManDeath());
    }

    void Update()
    {
        if (isSwordManDead) return;

        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;
        // 좌우 방향키에 따른 값 받기        
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            inputRight = true;
            // 이동하는 방향에 따라 좌우반전
            transform.localScale = new Vector3(-1, 1, 1);
            // animator 상태 연결
            animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            inputLeft = true;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("moving", true);
        }
        else animator.SetBool("moving", false);
        
        // A키를 누르면 true 반환
        // "Attack" 애니메이션이 실행되고 있다면 true 반환
        if (Input.GetKey(KeyCode.A) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // "attack" 트리거 실행
            animator.SetTrigger("attack");
            // 사운드 재생
            SFXManager.Instance.PlaySound(SFXManager.Instance.playerAttack);
        }
        // Raycast로 땅과 충돌 체크
        RaycastHit2D raycastHit = 
            Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 
            0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            animator.SetBool("jumping", false);
        else animator.SetBool("jumping", true);

        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("jumping"))
        {
            inputJump = true;
        }
    }
    void FixedUpdate()
    {
        if (inputRight)
        {
            inputRight = false;
            rigid2D.AddForce(Vector2.right * status.moveSpeed);
        }
        if (inputLeft)
        {
            inputLeft = false;
            rigid2D.AddForce(Vector2.left * status.moveSpeed);
        }

        // 속도 제한
        if (rigid2D.velocity.x >= 2.5f) rigid2D.velocity = new Vector2(2.5f, rigid2D.velocity.y);
        else if (rigid2D.velocity.x <= -2.5f) rigid2D.velocity = new Vector2(-2.5f, rigid2D.velocity.y);
        
        // 점프 구현
        if(inputJump)
        {
            inputJump = false;
            rigid2D.AddForce(Vector2.up * jumpPower);
        }
    }

    IEnumerator CheckSwordManDeath()
    {
        while (true)
        {
            // 땅 밑으로 떨어졌다면
            if (transform.position.y < -8)
            {
                // Scene 재시작
                SceneManager.LoadScene("Main");
            }

            if (status.nowHp <= 0)
            {
                isSwordManDead = true;
                animator.SetTrigger("die");
                // 2초 기다리기
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("Main");
            }
            // 매 프레임의 마지막 마다 실행
            yield return new WaitForEndOfFrame();
        }
    }
}
