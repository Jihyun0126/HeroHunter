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

        // ���콺 Ŭ�� �� doTouch �ִϸ��̼� ����
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked!"); // ����� �޽��� ���
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Debug.DrawRay(mousePos, Vector2.zero, Color.red, 1.0f); // Ray �ð�ȭ
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity); // Raycast �߻�

            if (hit.collider != null)
            {
                Debug.Log($"Raycast hit: {hit.collider.name}"); // �浹�� ������Ʈ �̸� ���

                if (hit.collider.transform == transform) // Ray�� �浹�� ������Ʈ�� ���� ������Ʈ���� Ȯ��
                {
                    SetDoTouch(true); // doTouch �ִϸ��̼� ����
                }
            }
            else
            {
                Debug.Log("No collider hit!"); // Ray�� �ƹ��͵� �������� ���� ���
            }
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // speed ���� ������Ʈ
    }

    void SetDoTouch(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("doTouch", value); // doTouch �Ķ���� ����
            Debug.Log("doTouch animation triggered!"); // �ִϸ��̼� Ʈ���� �޽���
        }
        else
        {
            Debug.LogError("Animator�� �������� �ʾҽ��ϴ�!");
        }
    }
}
