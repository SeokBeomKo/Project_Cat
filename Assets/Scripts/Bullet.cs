using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f; //탄알 이동속력
    public Rigidbody bulletRigidBody; // 이동에 사용할 리지드바디 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        //게임오브젝트에서 리지드바디 컴포넌트를 얻어와서 할당
        bulletRigidBody = GetComponent<Rigidbody>();
        //리지드바디의 속도 = 입력방향 * 입력속도
        bulletRigidBody.velocity = transform.forward * speed;

        //3초 뒤에 오브젝트 파괴
        Destroy(gameObject, 3f);


    }

    //트리거 충돌시 자동으로 실행되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        //충돌한 상대방 오브젝트가 플레이어 태그를 가진 경우
        if(other.tag == "Player")
        {
            // 상대방 게임 오브젝트에서 PlayerController 컴포넌트 가져오기
            PlayerControl playerControl = other.GetComponent<PlayerControl>();

            //상대방으로부터 PlayerControl을 가져오는데 성공 했다면
            if(playerControl != null)
            {
                // 상대방 PlayerControl 에서 Die 메서드 수행
                playerControl.Die();
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
