//Reposition.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{

    public Transform groundSize;

    void Update() 
    {
        moveMap();
    }
    //트리거가 체크된 콜라이더를 벗어났을때
    //이를 위해 플레이어에 Area를 만들었다 (충돌여부를 확인하기 위해)
    void moveMap()
    {
        //프리팹의 크기를 자동으로 계산
        Vector3 mapSize = groundSize.transform.localScale;

        // ... 거리를 구하기 위해 플레이어 위치와 타일맵 위치를 미리 저장
        
        //플레이어 위치
        //플레이어 포지션을 가져오기위해 게임매니저에 저장해둔 플레이어에서 위치값을 가져오는 과정
        Vector3 playerPos = GameManager.instance.player.transform.position;
        
        //타일맵 위치
        //현재 이 스크립트가 들어있는 타일맵의 위치값
        Vector3 tilePos = transform.position; 

        //x축과 z축 각각의 거리를 구하는 코드
        //Mathf 는 수학 라이브러리.. 절대값을 구하기 위해 Abs를 불러옴
        float diffX = Mathf.Abs(playerPos.x - tilePos.x);

        //플레이어의 이동 방향을 저장하기 위한 변수
        //플레이어에서 미리 만들어뒀던 inputVec을 불러온다
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //Player에서 노멀라이즈를 했기때문에 1값이 안나오니 0보다 클때(좌우 이동할때) 값을 받아온다 
        float dirX = playerDir.x < 0 ? -1 : 1;

        //일정거리 이동했을때 플레이어에서 가장 먼 프리팹을 플레이어 방향으로 이동
        if (diffX >= (mapSize.x * 3))
            transform.Translate(Vector3.right * dirX * (mapSize.x * 5));
    }
}