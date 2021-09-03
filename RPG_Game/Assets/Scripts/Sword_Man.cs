using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Man : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트의 위치 초기화
        transform.position = new Vector3(0,0,0);
        // 오브젝트의 Animator 데이터를 받아옴
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌우 방향키에 따른 값 받기        
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            // 이동하는 방향에 따라 좌우반전
            transform.localScale = new Vector3(-1, 1, 1);
            // animator 상태 연결
            animator.SetBool("moving", true);
            // 방향 값에 따라 움직이게 하기
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("moving", true);
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else animator.SetBool("moving", false);
        
        // A키를 누르면 true 반환
        // "Attack" 애니메이션이 실행되고 있다면 true 반환
        if (Input.GetKeyDown(KeyCode.A) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // "attack" 트리거 실행
            animator.SetTrigger("attack");
        }
    }
}
