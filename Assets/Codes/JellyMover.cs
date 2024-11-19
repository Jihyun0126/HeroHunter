using System.Collections;
using UnityEngine;

public class JellyMovement : MonoBehaviour
{
    // 이동 범위와 속도를 Inspector에서 설정 가능
    [Header("Movement Range")]
    public float minX = -5f; // X축 최소값
    public float maxX = 5f;  // X축 최대값
    public float minY = -5f; // Y축 최소값
    public float maxY = 5f;  // Y축 최대값

    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;   // 이동 속도
    public float waitTimeMin = 0.5f; // 최소 대기 시간
    public float waitTimeMax = 2.0f; // 최대 대기 시간

    private Vector3 targetPosition; // 이동할 목표 위치

    void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            // 무작위 위치 생성
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            targetPosition = new Vector3(randomX, randomY, transform.position.z);

            // 이동을 부드럽게 처리
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // 한 프레임 대기
            }

            // 이동 후 대기
            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
