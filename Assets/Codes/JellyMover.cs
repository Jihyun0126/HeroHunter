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
    private Animator animator; // Animator 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트
    private float speed;

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
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

            // 목표 위치로 이동
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // 이동 방향 계산
                Vector3 moveDirection = (targetPosition - transform.position).normalized;

                // 방향에 따라 flipX 설정
                if (moveDirection.x > 0)
                    spriteRenderer.flipX = false; // 오른쪽으로 이동
                else if (moveDirection.x < 0)
                    spriteRenderer.flipX = true;  // 왼쪽으로 이동

                // 이동 속도 계산
                speed = moveDirection.magnitude * moveSpeed;

                // Animator의 isWalk 설정
                animator.SetBool("isWalk", speed > 0);

                // 이동
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                yield return null;
            }

            // 대기 시간 동안 isWalk 비활성화
            animator.SetBool("isWalk", false);
            speed = 0;

            float waitTime = Random.Range(waitTimeMin, waitTimeMax);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
