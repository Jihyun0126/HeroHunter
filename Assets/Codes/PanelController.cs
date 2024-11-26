using System.Collections;
using UnityEngine;
public class PanelController : MonoBehaviour
{
    public GameObject panel;        // ������ Panel
    public Animator animator;       // Animator ����

    private bool isPanelOpen = false; // �ʱ� ����: ���� ����

    void Start()
    {
        // Panel�� ó���� ��Ȱ��ȭ
        panel.SetActive(false);
    }

    public void TogglePanel()
    {
        if (isPanelOpen)
        {
            // â �ݱ�
            animator.SetTrigger("doHide");
            isPanelOpen = false;
            StartCoroutine(DeactivatePanelAfterAnimation());
        }
        else
        {
            // â ����
            panel.SetActive(true); // Panel Ȱ��ȭ
            animator.Play("Hidden"); // Hidden ���·� �ʱ�ȭ
            animator.SetTrigger("doShow");
            isPanelOpen = true;
        }
    }

    private IEnumerator DeactivatePanelAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        panel.SetActive(false);
    }
}