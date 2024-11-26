using System.Collections;
using UnityEngine;
public class PanelController : MonoBehaviour
{
    public GameObject panel;        // 제어할 Panel
    public Animator animator;       // Animator 연결

    private bool isPanelOpen = false; // 초기 상태: 닫혀 있음


    public void TogglePanel()
    {
        if (isPanelOpen)
        {
            // 창 닫기
            animator.SetTrigger("doHide");
            isPanelOpen = false;
        }
        else
        {
            // 창 열기
            panel.SetActive(true); // Panel 활성화
            animator.Play("Hidden"); // Hidden 상태로 초기화
            animator.SetTrigger("doShow");
            isPanelOpen = true;
        }
    }
}