using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Status status;
    public UnitCode unitCode;
    
    public GameObject prfHpBar;
    public GameObject canvas;

    RectTransform hpBar;

    // 체력바를 적의 머리 위에 위치시키기 위함
    public float height = 2.7f;

    public Sword_Man sword_man;
    Image nowHpbar;
    public Animator enemyAnimator;

    void Start()
    {
        // 체력바를 생성하되 canvas의 자식으로 생성하고, 체력바의 위치 변경을 쉽게 하기
        // 위해 hpBar에 저장한다.
        // Instantiate(게임오브젝트, 부모의 transform); 게임오브젝트를 생성하는 함수
        // 체력바 오브젝트의 위치를 바꿔주기 위해 GetComponent로 RectRansform를 받아주었다.
        // 체력바 오브젝트는 UI이기 때문에 transform.position이 아닌 RectRansform을 사용
        // GetComponent<컴포넌트 이름>(); 게임 오브젝트에 붙어있는 컴포넌트를 받아올 수 있음
        // 컴포넌트: RectTransform, Canvas Renderer, Image 등등 
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();

        status = new Status();
        // enemy 마다 선택할 수 있도록 하기 위함
        status = status.SetUnitStatus(unitCode);
        
        SetAttackSpeed(status.atkSpeed);
    }

    void Update()
    {
        // Camera.main.WorldToScreenPoint(월드 좌표 값); 
        // 월드 좌표를 스크린 좌표, 즉 UI좌표로 바꿔주는 함수
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(
            new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Weapon"))
        {
            if (sword_man.attacked)
            {
                status.nowHp -= sword_man.status.atkDmg;
                sword_man.attacked = false;
                // 몬스터 사망
                if (status.nowHp <= 0)
                {
                    Die();
                }
            }
        }
    }

    void Die()
    {
        // die 애니메이션 실행
        enemyAnimator.SetTrigger("die");
        // 추적 비활성화
        GetComponent<EnemyAI>().enabled = false;
        // 충돌체 비활성화
        GetComponent<Collider2D>().enabled = false;
        // 중력 비활성화
        Destroy(GetComponent<Rigidbody2D>());
        // 3초 후 제거
        Destroy(gameObject, 3);
        // 3초 후 체력 바 제거
        Destroy(hpBar.gameObject, 3);
    }

    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}
