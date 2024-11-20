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
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // speed 값을 업데이트
    }
}
