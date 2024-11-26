using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public string monsterName;  // ���� �̸�
    public int unlockCost;      // �ر� ��� (Jewelry)
    public int price;           // ���� ��� (Gold)
    public bool isUnlocked;     // �ر� ����
    public bool isPurchased;    // ���� ���� (���ο� �ʵ�)
    public GameObject prefab;   // ������ ������
}
