//Reposition.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public Transform groundSize; //맵의 scale x 값을 불러오기 위함
    // ... 프리팹 보관할 변수
    public GameObject[] prefabs;

    // ... 풀 담당을 하는 리스트들
    //리스트도 프리팹의 개수만큼 생성되어야하니 배열로 선언해준다
    List<GameObject>[] prefabsList;
    
    void Awake() 
    {
        //리스트기때문에 new를 만들어줘야함
        //List 배열의 크기는 prefabs배열과 동일하기때문에 배열에 Prefabs의 길이를 넣어준다
        prefabsList = new List<GameObject>[prefabs.Length];

        //for문으로 배열 내부 오브젝트들을 모두 초기화해준다
        for (int index = 0; index < prefabsList.Length; index++)
        {
            //풀을 담는 배열도 초기화해주고 각각의 리스트들도 전부 초기화해줌
            prefabsList[index] = new List<GameObject>();
        }

        randTile();
    }

    void Update() 
    {
        moveMap();
    }

    void moveMap()
    {
        //프리팹의 크기를 자동으로 계산
        Vector3 mapSize = groundSize.transform.localScale;

        // ... 거리를 구하기 위해 플레이어 위치와 타일맵 위치를 미리 저장
        
        //플레이어의 위치를 가져오기위해 게임매니저에 저장해둔 플레이어에서 위치값을 가져오는 과정
        Vector3 playerPos = GameManager.instance.player.transform.position;
        
        //현재 이 스크립트가 들어있는 타일맵의 위치값
        Vector3 tilePos = transform.position; 

        //플레이어와 타일 사이의 x축의 거리를 구함
        //Mathf 는 수학 라이브러리.. 절대값을 구하기 위해 Abs를 불러옴
        float diffX = Mathf.Abs(playerPos.x - tilePos.x);

        //플레이어의 이동 방향을 저장하기 위한 변수
        //플레이어에서 미리 만들어뒀던 inputVec을 불러온다
        Vector3 playerDir = GameManager.instance.player.inputVec;
        //Player에서 노멀라이즈를 했기때문에 1값이 안나오니 0보다 클때(좌우 이동할때) 값을 받아온다 
        float dirX = playerDir.x < 0 ? -1 : 1;

        //일정거리 이동했을때 플레이어에서 가장 먼 프리팹을 플레이어 방향으로 이동
        //맵이 이동할때마다 맵 디자인이 랜덤으로 변합니다
        if (diffX >= (mapSize.x * 3))
        {
            transform.Translate(Vector3.right * dirX * (mapSize.x * 5));
            randTile();
        }
    }

    //prefab에 등록한 프리팹을 초기화하고 랜덤으로 Active 하는 코드
    void randTile()
    {
        int randIndex = Random.Range(0, 5);

        for (int index = 0; index < prefabsList.Length; index++)
            prefabs[index].SetActive(false);
        
        switch (randIndex)
        {
            case 0:
            case 1:
                prefabs[0].SetActive(true);
                break;
            case 2:
            case 3:
            case 4:
                prefabs[Random.Range(0, prefabsList.Length)].SetActive(true);
                break;
        }
    }
}