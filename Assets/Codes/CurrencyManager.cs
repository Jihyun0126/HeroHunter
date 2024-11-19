using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    public long maxCurrency = 99999999999; // ��ȭ �ִ�ġ
    private long currentCurrency = 0;     // ���� ���� ��ȭ

    // ���� ��ȭ�� UI�� ǥ�� (UI Text �Ǵ� TMP ���)
    public TMPro.TextMeshProUGUI currencyText;

    void Start()
    {
        UpdateCurrencyUI(); // ���� �� UI �ʱ�ȭ
    }

    // ��ȭ ȹ�� �Լ�
    public void AddCurrency(long amount)
    {
        // ���� ��ȭ + �߰� ��ȭ�� �ִ�ġ�� �ʰ����� �ʵ��� ����
        currentCurrency = System.Math.Min(currentCurrency + amount, maxCurrency);
        UpdateCurrencyUI();
    }

    // ��ȭ ��� �Լ�
    public bool SpendCurrency(long amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateCurrencyUI();
            return true; // ��� ����
        }
        else
        {
            Debug.Log("Not enough currency!"); // ��ȭ ����
            return false; // ��� ����
        }
    }

    // ���� ��ȭ �� ��ȯ
    public long GetCurrency()
    {
        return currentCurrency;
    }

    // UI ������Ʈ �Լ�
    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = currentCurrency.ToString("N0"); // 3�ڸ����� �޸� �߰�
        }
    }
}
