using UnityEngine;
using TMPro;

public static class StaticCurrency
{
    public static long Gold = 0; // 기본값 설정
    public static int Jewelry = 0; // 기본값 설정
}

public class CurrencyManager : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI goldText; // Gold UI Text
    public TextMeshProUGUI jewelryText; // Jewelry UI Text

    private void Start()
    {
        UpdateCurrencyUI(); // UI 초기화
    }

    // Gold 추가
    public void AddGold(long amount)
    {
        StaticCurrency.Gold = System.Math.Min(StaticCurrency.Gold + amount, 99999999999);
        UpdateCurrencyUI();
    }

    // Jewelry 추가
    public void AddJewelry(int amount)
    {
        StaticCurrency.Jewelry = Mathf.Min(StaticCurrency.Jewelry + amount, 99999);
        UpdateCurrencyUI();
    }

    // Gold 사용
    public bool SpendGold(long amount)
    {
        if (StaticCurrency.Gold >= amount)
        {
            StaticCurrency.Gold -= amount;
            UpdateCurrencyUI();
            return true;
        }
        Debug.Log("Not enough Gold!");
        return false;
    }

    // Jewelry 사용
    public bool SpendJewelry(int amount)
    {
        if (StaticCurrency.Jewelry >= amount)
        {
            StaticCurrency.Jewelry -= amount;
            UpdateCurrencyUI();
            return true;
        }
        Debug.Log("Not enough Jewelry!");
        return false;
    }

    // UI 업데이트
    private void UpdateCurrencyUI()
    {
        if (goldText != null)
        {
            goldText.text = StaticCurrency.Gold.ToString("N0");
        }
        if (jewelryText != null)
        {
            jewelryText.text = StaticCurrency.Jewelry.ToString("N0");
        }
    }
}
