using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword_Man : MonoBehaviour
{
    Animator animator;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    // 여러번 공격 방지용
    public bool attacked = false;
    public Image nowHpbar;

    bool inputRight = false;
    bool inputLeft = false;
    Rigidbody2D rigid2D;
    public float moveSpeed = 5;

    BoxCollider2D col2D;

    public float jumpPower = 40;
    bool inputJump = false;

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHp = 50;
        nowHp = 50;
        atkDmg = 10;
        // 오브젝트의 위치 초기화
        transform.position = new Vector3(0,0,0);
        // 오브젝트의 Animator 데이터를 받아옴
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

        SetAttackSpeed(1.5f);
        col2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
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
            rigid2D.AddForce(Vector2.right * moveSpeed);
        }
        if (inputLeft)
        {
            inputLeft = false;
            rigid2D.AddForce(Vector2.left * moveSpeed);
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
}
