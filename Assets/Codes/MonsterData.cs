using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public string monsterName;  // 몬스터 이름
    public int unlockCost;      // 해금 비용 (Jewelry)
    public int price;           // 구매 비용 (Gold)
    public bool isUnlocked;     // 해금 여부
    public bool isPurchased;    // 구매 여부 (새로운 필드)
    public GameObject prefab;   // 몬스터의 프리팹
}
