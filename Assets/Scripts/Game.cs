using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    const int Freeq = 3;
    public GameObject RubyPrefab;

    public Slider TimerBossSleder;
    public Slider HealthSlider;
    public Transform StartPosition;

    public GameObject ImageGoldMonsterBonus;
    public GameObject RubyIndexTextON;
    public GameObject RubyImageIndexON;
    public GameObject BossSpawn;
    public GameObject OnOffBossSlender;
    public GameObject ShopPanel;
    public GameObject GoldPrefab;
    public GameObject[] MonstersPrefabs;
    public GameObject[] MonstersGoldPrefabs;
    public GameObject[] MonstersBossPrefabs;
    public HealthHelper[] healthHelpers;
    public HealthHelper[] MonstersGoldPrefabsUp;
    public GameObject MonsterBossObj;
    public GameObject MonsterObj;
    public GameObject MonsterGoldObj;

    public Text TextGoldMonsterBonus;
    public Text TextGoldMonsterChance;
    public Text RubyIndexText;
    public Text DamageText;
    public Text PlayerGoldUI;
    public Text RubyText;
    public Text RubyСhanceText;
    public Text CountBossText;


    public int MonsterGoldUp = 100;
    public int MonsterGoldChance;
    public int RubyDropText = 1;
    public int TemporaryRubyChanse;
    public int UpHealthPrefabs = 1;
    public int BossTime;
    public int PlayerGold;
    public int PlayerRuby;
    public int PlayerDamage = 1;

    public bool Alive;
    public bool BossLosing;
    public bool IsNextBoss;

    int _count;
    int _countBoss;
    public int UpHealthIndex;

    HealthHelper _healthHelper;

    void Start()
    {
        _healthHelper = GameObject.FindObjectOfType<HealthHelper>();
        for (int i = 0; i < healthHelpers.Length; i++)
        {
            healthHelpers[i].RubyDrop = 1;
        }

        for (int i = 0; i < MonstersGoldPrefabsUp.Length; i++)
        {
            MonstersGoldPrefabsUp[i].GoldDrop = 100;
        }

        SpawnMonster();
    }
    void Update()
    {
        PlayerGoldUI.text = PlayerGold.ToString(); //убрать
        DamageText.text = PlayerDamage.ToString(); //убрать
        RubyText.text = PlayerRuby.ToString();
        RubyСhanceText.text = TemporaryRubyChanse.ToString() + "%";

        //Debug.Log(CountBossDead);

    }

    public void TakeRuby(int ruby)
    {
        PlayerRuby += ruby;
        GameObject rubyObj = Instantiate(RubyPrefab) as GameObject;
        Destroy(rubyObj, 3);

    }
    public void TakeGold(int goldDrop)
    {
        PlayerGold += goldDrop;
        GameObject goldObj = Instantiate(GoldPrefab) as GameObject;
        Destroy(goldObj, 2);
        if (!BossLosing)
        {

            if (_countBoss  % 100 == 0 && _countBoss != 0)
            {
                RubyIndexTextON.SetActive(true);
                RubyImageIndexON.SetActive(true);
                RubyDropText += 1;
                RubyIndexText.text = RubyDropText.ToString() + "X";
                for (int i = 0; i < healthHelpers.Length; i++)
                {
                    healthHelpers[i].RubyDrop += 1;
                }
                


            }

            if (++_countBoss % 10 == 0)
            {
                SpawnBoss();
                Alive = true;
                StopAllCoroutines();
                StartCoroutine(TimeBoss(BossTime));

                OnOffBossSlender.SetActive(true);
                TimerBossSleder.maxValue = BossTime;
                TimerBossSleder.value = BossTime;
            }

            else
            {

                CheckMonsterGold();

            }


        }
        else
        {
            CheckMonsterGold();
        }
        

    }
    public void SpawnMonster()
    {
        _count++;

        OnOffBossSlender.SetActive(false);

        int randomMaxValue = _count / Freeq + 1;
        if (randomMaxValue >= MonstersPrefabs.Length)
        {
            randomMaxValue = MonstersPrefabs.Length;
        }

        int index = Random.Range(0, randomMaxValue);

        GameObject monsterObj = Instantiate(MonstersPrefabs[index]) as GameObject;
        monsterObj.transform.position = StartPosition.position;
        monsterObj.GetComponent<HealthHelper>().MaxHealth *= UpHealthPrefabs;
        monsterObj.GetComponent<HealthHelper>().CurrentHealth *= UpHealthPrefabs;
        monsterObj.GetComponent<HealthHelper>().MaxHealth += UpHealthIndex;
        monsterObj.GetComponent<HealthHelper>().CurrentHealth += UpHealthIndex;
        monsterObj.GetComponent<HealthHelper>().GoldDrop *= UpHealthPrefabs;
        monsterObj.GetComponent<HealthHelper>().RubyChanse += TemporaryRubyChanse;


        MonsterObj = monsterObj;


        CountBossText.text = _countBoss.ToString();

    }
    public void SpawnBoss()
    {

        int index = Random.Range(0, MonstersBossPrefabs.Length);

        GameObject monsterBossObj = Instantiate(MonstersBossPrefabs[index]) as GameObject;
        monsterBossObj.GetComponent<HealthHelper>().MaxHealth *= UpHealthPrefabs;
        monsterBossObj.GetComponent<HealthHelper>().CurrentHealth *= UpHealthPrefabs;
        monsterBossObj.GetComponent<HealthHelper>().MaxHealth += UpHealthIndex;
        monsterBossObj.GetComponent<HealthHelper>().CurrentHealth += UpHealthIndex;
        monsterBossObj.GetComponent<HealthHelper>().GoldDrop *= UpHealthPrefabs;




        MonsterBossObj = monsterBossObj;

        monsterBossObj.transform.position = StartPosition.position;
        CountBossText.text = "Boss";

    }
    public void SpawnMonsterGold()
    {
        _count++;

        OnOffBossSlender.SetActive(false);

        int randomMaxValue = _count / Freeq + 1;
        if (randomMaxValue >= MonstersGoldPrefabs.Length)
        {
            randomMaxValue = MonstersGoldPrefabs.Length;
        }

        int index = Random.Range(0, randomMaxValue);

        GameObject monsterGoldObj = Instantiate(MonstersGoldPrefabs[index]) as GameObject;
        monsterGoldObj.transform.position = StartPosition.position;
        monsterGoldObj.GetComponent<HealthHelper>().MaxHealth *= UpHealthPrefabs;
        monsterGoldObj.GetComponent<HealthHelper>().CurrentHealth *= UpHealthPrefabs;
        monsterGoldObj.GetComponent<HealthHelper>().MaxHealth += UpHealthIndex;
        monsterGoldObj.GetComponent<HealthHelper>().CurrentHealth += UpHealthIndex;
        monsterGoldObj.GetComponent<HealthHelper>().GoldDrop *= UpHealthPrefabs;
        monsterGoldObj.GetComponent<HealthHelper>().RubyChanse += TemporaryRubyChanse;


        MonsterGoldObj = monsterGoldObj;


        CountBossText.text = _countBoss.ToString();
    }


    public void CheckMonsterGold()
    {
        int random = Random.Range(0, 100);
        if (random < MonsterGoldChance)
        {
            SpawnMonsterGold();
        }
        else
        {
            SpawnMonster();
        }
    }

    public void ButtonBossSpawn()
    {
        BossLosing = false;

        BossSpawn.SetActive(false);
        Destroy(MonsterObj);
        Destroy(MonsterGoldObj);
        _countBoss++;
        SpawnBoss();
        Alive = true;
        StopAllCoroutines();
        StartCoroutine(TimeBoss(BossTime));

        OnOffBossSlender.SetActive(true);
        TimerBossSleder.maxValue = BossTime;
        TimerBossSleder.value = BossTime;

    }

    public void ShopPanelShowAndHide()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }
    IEnumerator TimeBoss(int seconds)
    {
        int counter = BossTime;
        while (counter > 0)
        {
            TimerBossSleder.value = counter;

            yield return new WaitForSeconds(0.5f);
            counter--;
        }
        if (counter == 0 && Alive)
        {
            BossSpawn.SetActive(true);
            BossLosing = true;
            _countBoss--;
            Destroy(MonsterBossObj);
            CheckMonsterGold();
            CountBossText.text = _countBoss.ToString();

        }
        else
        {
            counter = 0;
            yield break;
        }

        //metod

    }
}