using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ
    public float speed;       // �̵� �ӵ�

    void Update()
    {
        // speed ���� ���� Animator �Ķ���� ����
        if (speed > 0)
        {
            animator.SetBool("isWalk", true); // Walk ���·� ��ȯ
        }
        else
        {
            animator.SetBool("isWalk", false); // Idle ���·� ��ȯ
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // speed ���� ������Ʈ
    }
}
