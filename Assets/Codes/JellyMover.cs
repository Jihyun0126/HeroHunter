using System.Collections;
using UnityEngine;

public class JellyMovement : MonoBehaviour
{
    [Header("Movement Range")]
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    public float waitTimeMin = 0.5f;
    public float waitTimeMax = 2.0f;

    private Vector3 targetPosition;
    private Animator animator; // Animator ������Ʈ
    private SpriteRenderer spriteRenderer; // SpriteRenderer ������Ʈ
    private float speed;

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator ��������
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            // ������ ��ġ ����
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            targetPosition = new Vector3(randomX, randomY, transform.position.z);

            // ��ǥ ��ġ�� �̵�
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // �̵� ���� ���
                Vector3 moveDirection = (targetPosition - transform.position).normalized;

                // ���⿡ ���� flipX ����
                if (moveDirection.x > 0)
                    spriteRenderer.flipX = false; // ���������� �̵�
                else if (moveDirection.x < 0)
                    spriteRenderer.flipX = true;  // �������� �̵�

                // �̵� �ӵ� ���
                speed = moveDirection.magnitude * moveSpeed;

                // Animator�� isWalk ����
                animator.SetBool("isWalk", speed > 0);

                // �̵�
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                yield return null;
            }

            // ��� �ð� ���� isWalk ��Ȱ��ȭ
            animator.SetBool("isWalk", false);
            speed = 0;

            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
