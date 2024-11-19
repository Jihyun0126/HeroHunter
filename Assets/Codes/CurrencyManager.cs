using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    public long maxCurrency = 99999999999; // 재화 최대치
    private long currentCurrency = 0;     // 현재 보유 재화

    // 현재 재화를 UI로 표시 (UI Text 또는 TMP 사용)
    public TMPro.TextMeshProUGUI currencyText;

    void Start()
    {
        UpdateCurrencyUI(); // 시작 시 UI 초기화
    }

    // 재화 획득 함수
    public void AddCurrency(long amount)
    {
        // 현재 재화 + 추가 재화가 최대치를 초과하지 않도록 제한
        currentCurrency = System.Math.Min(currentCurrency + amount, maxCurrency);
        UpdateCurrencyUI();
    }

    // 재화 사용 함수
    public bool SpendCurrency(long amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateCurrencyUI();
            return true; // 사용 성공
        }
        else
        {
            Debug.Log("Not enough currency!"); // 재화 부족
            return false; // 사용 실패
        }
    }

    // 현재 재화 값 반환
    public long GetCurrency()
    {
        return currentCurrency;
    }

    // UI 업데이트 함수
    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = currentCurrency.ToString("N0"); // 3자리마다 콤마 추가
        }
    }
}
