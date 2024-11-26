using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트
    public float speed;       // 이동 속도

    void Update()
    {
        // speed 값에 따라 Animator 파라미터 변경
        if (speed > 0)
        {
            animator.SetBool("isWalk", true); // Walk 상태로 전환
        }
        else
        {
            animator.SetBool("isWalk", false); // Idle 상태로 전환
        }

        // 마우스 클릭 시 doTouch 애니메이션 실행
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked!"); // 디버그 메시지 출력
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
            Debug.DrawRay(mousePos, Vector2.zero, Color.red, 1.0f); // Ray 시각화
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity); // Raycast 발사

            if (hit.collider != null)
            {
                Debug.Log($"Raycast hit: {hit.collider.name}"); // 충돌된 오브젝트 이름 출력

                if (hit.collider.transform == transform) // Ray가 충돌한 오브젝트가 현재 오브젝트인지 확인
                {
                    SetDoTouch(true); // doTouch 애니메이션 실행
                }
            }
            else
            {
                Debug.Log("No collider hit!"); // Ray가 아무것도 감지하지 못한 경우
            }
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // speed 값을 업데이트
    }

    void SetDoTouch(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("doTouch", value); // doTouch 파라미터 설정
            Debug.Log("doTouch animation triggered!"); // 애니메이션 트리거 메시지
        }
        else
        {
            Debug.LogError("Animator가 설정되지 않았습니다!");
        }
    }
}
