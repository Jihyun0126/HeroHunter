using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    public MonsterManager monsterManager; // ���� ���� �ý���

    [System.Serializable]
    public class MonsterUI
    {
        public TextMeshProUGUI unlockCostText; // �ر� ��� �ؽ�Ʈ
        public Button unlockButton;           // �ر� ��ư
        public TextMeshProUGUI buyCostText;   // ���� ��� �ؽ�Ʈ
        public Button buyButton;              // ���� ��ư
        public GameObject unlockObject;       // �ر� �� ���¸� ǥ���ϴ� ������Ʈ
    }

    public MonsterUI[] monsterUIs; // ���� UI �迭 (Inspector���� ����)

    public Button resetPurchaseButton; // ���� �̷� �ʱ�ȭ ��ư (Inspector���� ����)

    void Start()
    {
        UpdateAllUI();
        SetupButtonListeners();
    }

    // ��� UI ������Ʈ
    private void UpdateAllUI()
    {
        for (int i = 0; i < monsterManager.monsters.Count; i++)
        {
            UpdateUI(monsterManager.monsters[i], monsterUIs[i]);
        }
    }

    // ���� ���� UI ������Ʈ
    private void UpdateUI(MonsterData monster, MonsterUI ui)
    {
        ui.unlockCostText.text = $"{monster.unlockCost}";
        ui.buyCostText.text = $"{monster.price}";

        ui.unlockButton.interactable = !monster.isUnlocked && monsterManager.CanAffordUnlock(monster);
        ui.buyButton.interactable = monster.isUnlocked && !monster.isPurchased;

        if (monster.isUnlocked && ui.unlockObject != null)
        {
            ui.unlockObject.SetActive(false); // �ر� ���� ������Ʈ
        }
    }

    // ��ư �̺�Ʈ ����
    private void SetupButtonListeners()
    {
        for (int i = 0; i < monsterManager.monsters.Count; i++)
        {
            MonsterData monster = monsterManager.monsters[i];
            MonsterUI ui = monsterUIs[i];

            // Unlock ��ư �̺�Ʈ ����
            ui.unlockButton.onClick.AddListener(() =>
            {
                if (monsterManager.UnlockMonster(monster, ui.unlockObject))
                {
                    UpdateUI(monster, ui);
                }
            });

            // Buy ��ư �̺�Ʈ ����
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

        // ���� �̷� �ʱ�ȭ ��ư �̺�Ʈ ����
        resetPurchaseButton.onClick.AddListener(ResetPurchaseHistory);
    }

    // ���� �̷� �ʱ�ȭ �Լ�
    private void ResetPurchaseHistory()
    {
        // ��� ������ ���� ���� �ʱ�ȭ
        foreach (var monster in monsterManager.monsters)
        {
            monster.isPurchased = false; // ���� ���� �ʱ�ȭ
        }

        // UI ������Ʈ
        UpdateAllUI();

        Debug.Log("Purchase history reset successfully!");
    }
}