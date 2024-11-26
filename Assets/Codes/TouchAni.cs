using UnityEngine;

public class AnimationOnClick : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트를 연결

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 감지
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // 해당 위치에서 Raycast 발사

            if (hit.collider != null && hit.collider.transform == transform) // Ray가 충돌한 오브젝트가 현재 오브젝트인지 확인
            {
                SetDoTouch(true);
            }
        }
    }

    void SetDoTouch(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("doTouch", value); // doTouch 파라미터 설정
        }
        else
        {
            Debug.LogError("Animator가 설정되지 않았습니다!");
        }
    }
}
