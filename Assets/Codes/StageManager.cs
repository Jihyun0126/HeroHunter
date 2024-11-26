using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public CurrencyManager currencyManager; // ��ȭ ���� �ý���

    // ��ư ���� (�� �������� ���� ��ư)
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        stage1Button.onClick.AddListener(() => CompleteStage(1));
        stage2Button.onClick.AddListener(() => CompleteStage(2));
        stage3Button.onClick.AddListener(() => CompleteStage(3));
    }

    // �������� �Ϸ� ó��
    public void CompleteStage(int stageNumber)
    {
        int earnedJewelry = GenerateJewelryReward(stageNumber);
        long earnedGold = GenerateGoldReward(stageNumber);

        currencyManager.AddJewelry(earnedJewelry);
        currencyManager.AddGold(earnedGold);

        Debug.Log($"Stage {stageNumber} completed! Earned {earnedJewelry} Jewelry and {earnedGold} Gold.");
    }

    // Jewelry ���� ���� (0 ~ 5, ���� ���� ���� Ȯ����)
    private int GenerateJewelryReward(int stageNumber)
    {
        int[] rewards = { 0, 1, 2, 3, 4, 5 };
        float[] probabilities = { 0.4f, 0.3f, 0.15f, 0.1f, 0.04f, 0.01f };

        return GenerateWeightedRandom(rewards, probabilities);
    }

    // Gold ���� ���� (1 ~ 1000, ���� ���� ���� Ȯ����)
    private long GenerateGoldReward(int stageNumber)
    {
        long[] rewards = { 1, 10, 50, 100, 500, 1000 };
        float[] probabilities = { 0.5f, 0.3f, 0.1f, 0.07f, 0.02f, 0.01f };

        return GenerateWeightedRandom(rewards, probabilities);
    }

    // ���� ���� ����
    private T GenerateWeightedRandom<T>(T[] values, float[] probabilities)
    {
        float total = 0;
        foreach (var prob in probabilities)
        {
            total += prob;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probabilities.Length; i++)
        {
            if (randomPoint < probabilities[i])
            {
                return values[i];
            }
            randomPoint -= probabilities[i];
        }

        return values[0];
    }
}
