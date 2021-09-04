using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;

    RectTransform hpBar;

    // 체력바를 적의 머리 위에 위치시키기 위함
    public float height = 1.7f;

    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;

    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }
    
    public Sword_Man sword_man;
    Image nowHpbar;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (sword_man.attacked)
            {
                nowHp -= sword_man.atkDmg;
                Debug.Log(nowHp);
                sword_man.attacked = false;
                if (nowHp <= 0)
                {
                    Destroy(gameObject);
                    Destroy(hpBar.gameObject);
                }
            }
        }
    }
    // Start is called before the first frame update
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
        if (name.Equals("Enemy1"))
        {
            SetEnemyStatus("Enemy1", 100, 10, 1);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Camera.main.WorldToScreenPoint(월드 좌표 값); 
        // 월드 좌표를 스크린 좌표, 즉 UI좌표로 바꿔주는 함수
        Vector3 _hpBarPos = 
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }
}
