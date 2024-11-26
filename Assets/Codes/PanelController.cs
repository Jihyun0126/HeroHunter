using System.Collections;
using UnityEngine;
public class PanelController : MonoBehaviour
{
    public GameObject panel;        // ������ Panel
    public Animator animator;       // Animator ����

    private bool isPanelOpen = false; // �ʱ� ����: ���� ����


    public void TogglePanel()
    {
        if (isPanelOpen)
        {
            // â �ݱ�
            animator.SetTrigger("doHide");
            isPanelOpen = false;
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
}