using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    public List<MonsterData> monsters; // ���� ������ ����Ʈ
    public Transform spawnedMonstersParent; // ������ ������ �θ� ������Ʈ
    private string initialSceneName; // �ʱ� �� �̸� ����

    private void Awake()
    {
        // MonsterManager�� �� ��ȯ �� �������� �ʵ��� ����
        DontDestroyOnLoad(this.gameObject);

        // ������ ���� �θ� �������� �ʵ��� ����
        if (spawnedMonstersParent == null)
        {
            GameObject parentObject = new GameObject("SpawnedMonstersParent");
            spawnedMonstersParent = parentObject.transform;
        }
        DontDestroyOnLoad(spawnedMonstersParent);
    }

    private void Start()
    {
        initialSceneName = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        SceneManager.activeSceneChanged += OnSceneChanged; // �� ��ȯ �̺�Ʈ ���

        LoadMonsterData(); // ������ �ҷ�����
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged; // �� ��ȯ �̺�Ʈ ����
        SaveMonsterData(); // ������ ����
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        string nextSceneName = next.name;

        // ���� ��ȯ�Ǿ��� �� ��ȯ�� ���� Ȱ��ȭ/��Ȱ��ȭ ����
        if (nextSceneName != initialSceneName)
        {
            // �ٸ� ������ �̵����� �� ���� ��Ȱ��ȭ
            SetMonstersActive(false);
        }
        else
        {
            // ���� ������ ���ƿ��� �� ���� Ȱ��ȭ
            SetMonstersActive(true);
        }
    }

    // ���� �ر�
    public bool UnlockMonster(MonsterData monster, GameObject unlockObject)
    {
        if (!monster.isUnlocked && StaticCurrency.Jewelry >= monster.unlockCost)
        {
            StaticCurrency.Jewelry -= monster.unlockCost;
            monster.isUnlocked = true;

            if (unlockObject != null)
            {
                unlockObject.SetActive(false);
            }

            Debug.Log($"{monster.monsterName} unlocked!");
            SaveMonsterData(); // ����� ������ ����
            return true;
        }

        Debug.Log("Not enough Jewelry or already unlocked.");
        return false;
    }

    // ���� ���� �� ������ ����
    public bool BuyMonster(MonsterData monster)
    {
        if (!monster.isUnlocked || monster.isPurchased)
        {
            Debug.Log("Monster not unlocked or already purchased.");
            return false;
        }

        if (StaticCurrency.Gold >= monster.price)
        {
            StaticCurrency.Gold -= monster.price;
            monster.isPurchased = true;

            // ������ ����
            SpawnMonster(monster.prefab);

            Debug.Log($"{monster.monsterName} purchased!");
            SaveMonsterData(); // ����� ������ ����
            return true;
        }

        Debug.Log("Not enough Gold.");
        return false;
    }

    // ������ ���͸� �����ϰ� �θ�� ����
    public void SpawnMonster(GameObject monsterPrefab)
    {
        if (monsterPrefab != null)
        {
            GameObject spawnedMonster = Instantiate(monsterPrefab, Vector3.zero, Quaternion.identity);

            if (spawnedMonstersParent != null)
            {
                spawnedMonster.transform.SetParent(spawnedMonstersParent);
            }
            else
            {
                Debug.LogError("SpawnedMonstersParent is not assigned!");
            }

            Debug.Log($"{monsterPrefab.name} spawned and added to parent.");
        }
        else
        {
            Debug.LogError("Monster prefab is null!");
        }
    }

    // ��ȯ�� ���� Ȱ��ȭ/��Ȱ��ȭ
    private void SetMonstersActive(bool isActive)
    {
        foreach (Transform child in spawnedMonstersParent)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    // ���� ������ ����
    private void SaveMonsterData()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            PlayerPrefs.SetInt($"Monster_{i}_Unlocked", monsters[i].isUnlocked ? 1 : 0);
            PlayerPrefs.SetInt($"Monster_{i}_Purchased", monsters[i].isPurchased ? 1 : 0);
        }
        PlayerPrefs.Save();
        Debug.Log("Monster data saved.");
    }

    // ���� ������ �ҷ�����
    private void LoadMonsterData()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].isUnlocked = PlayerPrefs.GetInt($"Monster_{i}_Unlocked", 0) == 1;
            monsters[i].isPurchased = PlayerPrefs.GetInt($"Monster_{i}_Purchased", 0) == 1;
        }
        Debug.Log("Monster data loaded.");
    }

    public bool CanAffordUnlock(MonsterData monster)
    {
        return StaticCurrency.Jewelry >= monster.unlockCost; // StaticCurrency�� ���� ������ ��ȭ�� �����ϴ� Ŭ����
    }
}
