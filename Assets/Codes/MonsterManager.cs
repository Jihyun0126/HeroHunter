using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    public List<MonsterData> monsters; // 몬스터 데이터 리스트
    public Transform spawnedMonstersParent; // 스폰된 몬스터의 부모 오브젝트
    private string initialSceneName; // 초기 씬 이름 저장

    private void Awake()
    {
        // MonsterManager가 씬 전환 시 삭제되지 않도록 설정
        DontDestroyOnLoad(this.gameObject);

        // 스폰된 몬스터 부모도 삭제되지 않도록 설정
        if (spawnedMonstersParent == null)
        {
            GameObject parentObject = new GameObject("SpawnedMonstersParent");
            spawnedMonstersParent = parentObject.transform;
        }
        DontDestroyOnLoad(spawnedMonstersParent);
    }

    private void Start()
    {
        initialSceneName = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장
        SceneManager.activeSceneChanged += OnSceneChanged; // 씬 전환 이벤트 등록

        LoadMonsterData(); // 데이터 불러오기
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged; // 씬 전환 이벤트 해제
        SaveMonsterData(); // 데이터 저장
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        string nextSceneName = next.name;

        // 씬이 전환되었을 때 소환된 몬스터 활성화/비활성화 관리
        if (nextSceneName != initialSceneName)
        {
            // 다른 씬으로 이동했을 때 몬스터 비활성화
            SetMonstersActive(false);
        }
        else
        {
            // 원래 씬으로 돌아왔을 때 몬스터 활성화
            SetMonstersActive(true);
        }
    }

    // 몬스터 해금
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
            SaveMonsterData(); // 변경된 데이터 저장
            return true;
        }

        Debug.Log("Not enough Jewelry or already unlocked.");
        return false;
    }

    // 몬스터 구매 및 프리팹 스폰
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

            // 프리팹 스폰
            SpawnMonster(monster.prefab);

            Debug.Log($"{monster.monsterName} purchased!");
            SaveMonsterData(); // 변경된 데이터 저장
            return true;
        }

        Debug.Log("Not enough Gold.");
        return false;
    }

    // 스폰된 몬스터를 생성하고 부모로 설정
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

    // 소환된 몬스터 활성화/비활성화
    private void SetMonstersActive(bool isActive)
    {
        foreach (Transform child in spawnedMonstersParent)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    // 몬스터 데이터 저장
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

    // 몬스터 데이터 불러오기
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
        return StaticCurrency.Jewelry >= monster.unlockCost; // StaticCurrency는 정적 변수로 재화를 관리하는 클래스
    }
}
