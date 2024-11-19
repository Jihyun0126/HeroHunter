using System.Collections;
using UnityEngine;

public class JellyMovement : MonoBehaviour
{
    // �̵� ������ �ӵ��� Inspector���� ���� ����
    [Header("Movement Range")]
    public float minX = -5f; // X�� �ּҰ�
    public float maxX = 5f;  // X�� �ִ밪
    public float minY = -5f; // Y�� �ּҰ�
    public float maxY = 5f;  // Y�� �ִ밪

    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;   // �̵� �ӵ�
    public float waitTimeMin = 0.5f; // �ּ� ��� �ð�
    public float waitTimeMax = 2.0f; // �ִ� ��� �ð�

    private Vector3 targetPosition; // �̵��� ��ǥ ��ġ

    void Start()
    {
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

            // �̵��� �ε巴�� ó��
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // �� ������ ���
            }

            // �̵� �� ���
            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
