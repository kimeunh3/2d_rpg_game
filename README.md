## 간단한 2D RPG 게임 만들기  

## 프로젝트 소개
이 프로젝트는 아래의 링크의 강좌를 따라하며 진행하였습니다.  
https://hoil2.tistory.com/5  

### 프로젝트 동기
Unity사용에 보다 익숙해지기 위해서 이 프로젝트를 진행하게 되었습니다.

### 프로젝트 언어 및 환경
프로그래밍 언어: C#  
게임엔진 : Unity (2020.3.17f1 LTS)

### 주요 기능
* 맵 생성
* 소드맨이 움직이는대로 카메라 따라오게 설정
* BGM 추가

![gameview_1](https://user-images.githubusercontent.com/59808674/132137408-7cb3efad-4e05-4c67-9296-735d50a735f2.gif)
* 소드맨 좌우로 이동 가능
* 소드맨 공격 모션 가능
* 사운드 이펙트 추가
  * 소드맨 공격 시 사운드 이펙트 추가

![gameview_2](https://user-images.githubusercontent.com/59808674/132137441-bc57c2c4-d335-4f74-af63-bbcd5c772c80.gif)
* 소드맨 점프 모션 가능
* 파란 젬스톤: 이동속도 100% 증가

![gameview_3](https://user-images.githubusercontent.com/59808674/132137442-4c5556ac-d790-4452-a887-c44f265e4645.gif)
* 함정 "가시" 추가
  * 밟으면 HP 10 감소

![gameview_4](https://user-images.githubusercontent.com/59808674/132137445-abe9e392-6b18-41d0-ae39-d7f69f5cbaf4.gif)
* 몬스터 추가
* 몬스터의 HP 바 추가 (몬스터를 따라다님)
* 소드맨 공격에 몬스터가 맞으면 HP 바 감소
* 초록 젬스톤: 공격속도 50% 증가
* 몬스터AI 적용
  * 범위 내에 소드맨이 들어오면 소드맨 쪽을 향해 움직임
  * 공격 사거리에 들어온 소드맨을 공격
* 몬스터의 공격에 소드맨 HP 바 감소

![gameview_5](https://user-images.githubusercontent.com/59808674/132137448-ba8035db-4608-4067-9e4f-ac696d15473f.gif)
* 몬스터 HP가 0 이하 일시 사망
* 빨간 젬스톤: HP 50 회복

![gameview_6](https://user-images.githubusercontent.com/59808674/132137449-b5384189-caef-4326-aa37-44709dc5cca3.gif)
* 소드맨 HP가 0 이하 일시 사망 후 게임 재시작

![gameview_7](https://user-images.githubusercontent.com/59808674/132137467-7d06c55f-9f79-450c-bb35-87047fbdb390.gif)
* 소드맨 맵 밖으로 떨어지면 게임 재시작
