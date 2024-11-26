using UnityEngine;

public class AnimationOnClick : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ�� ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ����
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // �ش� ��ġ���� Raycast �߻�

            if (hit.collider != null && hit.collider.transform == transform) // Ray�� �浹�� ������Ʈ�� ���� ������Ʈ���� Ȯ��
            {
                SetDoTouch(true);
            }
        }
    }

    void SetDoTouch(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("doTouch", value); // doTouch �Ķ���� ����
        }
        else
        {
            Debug.LogError("Animator�� �������� �ʾҽ��ϴ�!");
        }
    }
}
