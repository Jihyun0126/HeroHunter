using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    public MonsterManager monsterManager; // 몬스터 관리 시스템

    [System.Serializable]
    public class MonsterUI
    {
        public TextMeshProUGUI unlockCostText; // 해금 비용 텍스트
        public Button unlockButton;           // 해금 버튼
        public TextMeshProUGUI buyCostText;   // 구매 비용 텍스트
        public Button buyButton;              // 구매 버튼
        public GameObject unlockObject;       // 해금 전 상태를 표시하는 오브젝트
    }

    public MonsterUI[] monsterUIs; // 몬스터 UI 배열 (Inspector에서 설정)

    public Button resetPurchaseButton; // 구매 이력 초기화 버튼 (Inspector에서 연결)

    void Start()
    {
        UpdateAllUI();
        SetupButtonListeners();
    }

    // 모든 UI 업데이트
    private void UpdateAllUI()
    {
        for (int i = 0; i < monsterManager.monsters.Count; i++)
        {
            UpdateUI(monsterManager.monsters[i], monsterUIs[i]);
        }
    }

    // 개별 몬스터 UI 업데이트
    private void UpdateUI(MonsterData monster, MonsterUI ui)
    {
        ui.unlockCostText.text = $"{monster.unlockCost}";
        ui.buyCostText.text = $"{monster.price}";

        ui.unlockButton.interactable = !monster.isUnlocked && monsterManager.CanAffordUnlock(monster);
        ui.buyButton.interactable = monster.isUnlocked && !monster.isPurchased;

        if (monster.isUnlocked && ui.unlockObject != null)
        {
            ui.unlockObject.SetActive(false); // 해금 상태 업데이트
        }
    }

    // 버튼 이벤트 설정
    private void SetupButtonListeners()
    {
        for (int i = 0; i < monsterManager.monsters.Count; i++)
        {
            MonsterData monster = monsterManager.monsters[i];
            MonsterUI ui = monsterUIs[i];

            // Unlock 버튼 이벤트 설정
            ui.unlockButton.onClick.AddListener(() =>
            {
                if (monsterManager.UnlockMonster(monster, ui.unlockObject))
                {
                    UpdateUI(monster, ui);
                }
            });

            // Buy 버튼 이벤트 설정
            ui.buyButton.onClick.AddListener(() =>
            {
                if (monsterManager.BuyMonster(monster))
                {
                    monster.isPurchased = true;
                    Debug.Log($"{monster.monsterName} purchased successfully!");

                    UpdateUI(monster, ui);
                }
            });
        }

        // 구매 이력 초기화 버튼 이벤트 설정
        resetPurchaseButton.onClick.AddListener(ResetPurchaseHistory);
    }

    // 구매 이력 초기화 함수
    private void ResetPurchaseHistory()
    {
        // 모든 몬스터의 구매 상태 초기화
        foreach (var monster in monsterManager.monsters)
        {
            monster.isPurchased = false; // 구매 상태 초기화
        }

        // UI 업데이트
        UpdateAllUI();

        Debug.Log("Purchase history reset successfully!");
    }
}