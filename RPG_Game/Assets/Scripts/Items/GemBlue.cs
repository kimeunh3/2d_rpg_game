using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBlue : MonoBehaviour
{    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    // 10�ʰ� �̵��ӵ� 100% ����
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Sword_Man swordMan = col.GetComponent<Sword_Man>();
            StartCoroutine(IncreaseMoveSpeed(swordMan));

            // Destroy�� �ϸ� �ڷ�ƾ�� �����ǹǷ�, �ӽ÷� �׸��� ���ֱ�
            GetComponent<SpriteRenderer>().enabled = false;
            // �浹ü ����
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseMoveSpeed(Sword_Man swordMan)
    {
        float moveSpeed = swordMan.GetMoveSpeed();
        swordMan.SetMoveSpeed(moveSpeed * 2f);

        yield return new WaitForSeconds(10);

        swordMan.SetMoveSpeed(moveSpeed);
        Destroy(gameObject);
    }
}
