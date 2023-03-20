//Player.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed;
    public CinemachineVirtualCamera VCamera;
    Rigidbody rigid;
    Animator anim;
    bool isBorder;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    //FixedUdate는 물리 연산 프레임마다 호출되는 함수다
    void FixedUpdate() 
    {
        Move();
        Turn();
        CameraFov();
    }

    //플레이어 이동 구현
    void Move()
    {
        //input키에 Horizontal 은 [left, right] Vertical 은 [up, down] 키가 매핑되어있음
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");

        //어느 방향이든 벡터값을 1로 고정
        Vector3 nextVec = inputVec.normalized * speed * Time.deltaTime;

        // ... 위치 이동
        //MovePostion은 위치 이동이라 현재 위치를 더해줘야함
        //이 코드에서 현재 위치는 rigid.postion 이다
        //인풋값과 현재위치를 더해주면 플레이어가 나아가야 할 방향을 계산한다
        rigid.MovePosition(rigid.position + nextVec);
        if(nextVec != Vector3.zero)
            anim.SetFloat("RunState", 0.5f);
        else
            anim.SetFloat("RunState", 0.0f);
    }

    //플레이어 회전 구현
    void Turn()
    {
        if (inputVec.x != 0)
            this.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        if (inputVec.x < 0)
            this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    //캐릭터의 -z방향에서 벽을 만났을경우, Camera의 z값을 고정하고 FOV값을 조정
    void CameraFov()
    {
        // Raycast의 거리
        float rayDist = -15.5f;
        
        //Raycast 시각화
        Debug.DrawRay(transform.position, transform.forward * rayDist, Color.green);

        Ray ray = new Ray(transform.position, transform.forward * rayDist);
        isBorder = Physics.Raycast(transform.position, transform.forward, rayDist, LayerMask.GetMask("Wall"));
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (!isBorder)
            {
                Vector3 cameraPosition = VCamera.transform.position;
                cameraPosition.z = hit.point.z;
                VCamera.transform.position = cameraPosition;

                // Calculate the distance between player and wall
                float distance = Vector3.Distance(transform.position, hit.point);

                //최소화각 최대화각 설정
                float fov = Mathf.Lerp(25f, 45f, distance / 7f);
                VCamera.m_Lens.FieldOfView = fov;
            }
        }
    }


}