using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 3;
    public Vector2 offset;
    // 맵의 최소, 최대 값을 받아서 영역을 지정
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        // 카메라가 이동할 수 있는 영역을 지정하기 위해
        // 카메라의 X축 길이, Y축의 길이를 구함
        // Camera.main.aspect = 해상도 width/height을 계산한 비율
        // Camera.main.orthographicSize = 카메라의 사이즈
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;      
    }

    private void LateUpdate()
    {
        // Mathf.Clamp(값, 최솟값, 최댓값)으로 최솟값, 최댓값을 넘지 않게 지정할 수 있음
        // float num = Mathf.Clamp(150, 100, 200); 
        // ->num의 값은 150
        // float num2 = Mathf.Clamp(50, 100, 200);
        // ->num2의 값은 100
        // float num3 = Mathf.Clamp(250, 100, 200);
        // ->num3의 값은 200
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        // Vector3.Lerp(시작 위치, 도착할 위치, t)를 사용해서 부드럽게 이동가능
        // t가 0에 가까울수록 시작 위치를 반환, 1과 가까울수록 도착할 위치를 반환
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}
