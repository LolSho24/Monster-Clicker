using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfinityUpButtonHelper : MonoBehaviour
{
    public bool IsRubyChanse;
    public bool IsMonsterGoldChanse;
    public bool IsMonsterGoldUp;


    public Text DamageText;
    public Text PriceText;
    public Text UpRubyChanseText;
    public Text UpMonsterGoldChanseText;
    public Text UpMonsterGoldUpText;

    public HealthHelper[] MonstersGoldPrefabsUp;


    public Button button;

    
    public int Damage;
    public int Price;
    public int UpRubyChanse = 5;
    public int UpMonsterGoldChanse = 5;
    public int UpMonsterGoldUp = 0;
    public int indexGoldUp = 10;




    Game _game;
    // Start is called before the first frame update
    void Start()
    {

        _game = GameObject.FindObjectOfType<Game>();
        if (IsMonsterGoldChanse && !IsRubyChanse && !IsMonsterGoldUp)
        {
            UpMonsterGoldChanseText.text = "+" + UpMonsterGoldChanse.ToString() + "%";
            PriceText.text = Price.ToString();

        }
        if (!IsRubyChanse && !IsMonsterGoldChanse && !IsMonsterGoldUp)
        {
            DamageText.text = "+" + Damage.ToString();
            PriceText.text = Price.ToString();
        }
        if (IsRubyChanse && !IsMonsterGoldChanse && !IsMonsterGoldUp)
        {
            UpRubyChanseText.text = "+" + UpRubyChanse.ToString() + "%";
            PriceText.text = Price.ToString();
        }

    }

 
    public void UpClick()
    {
        if (IsRubyChanse && _game.PlayerGold >= Price && !IsMonsterGoldChanse && !IsMonsterGoldUp)
        {
            _game.PlayerGold -= Price;
            Price *= 6;
            _game.TemporaryRubyChanse += UpRubyChanse;
            UpRubyChanse += 5;
            UpRubyChanseText.text = "+" + UpRubyChanse.ToString() + "%";
            PriceText.text = Price.ToString();
            if (UpRubyChanse == 20 || UpRubyChanse == 35)
            {
                Destroy(gameObject);
            }

        }


        if (!IsRubyChanse && _game.PlayerGold >= Price && !IsMonsterGoldChanse && !IsMonsterGoldUp)
        {
            _game.PlayerGold -= Price;

            _game.PlayerDamage += Damage;
            Price *= 2;
            Damage *= 2;
            PriceText.text = Price.ToString();
            DamageText.text = "+" + Damage.ToString();
        }


        if (IsMonsterGoldChanse && _game.PlayerGold >= Price && !IsRubyChanse && !IsMonsterGoldUp)
        {
            _game.PlayerGold -= Price;
            Price *= 2;
            PriceText.text = Price.ToString();

            _game.ImageGoldMonsterBonus.SetActive(true);

            _game.MonsterGoldChance += 5;
            UpMonsterGoldChanseText.text = "+" + UpMonsterGoldChanse.ToString() + "%";
            _game.TextGoldMonsterChance.text = "+" +_game.MonsterGoldChance.ToString() + "% Chance";

            if (_game.MonsterGoldChance % 15 == 0)
            {
                Destroy(gameObject);
            }
        }


        if (IsMonsterGoldUp && _game.PlayerGold >= Price && !IsRubyChanse && !IsMonsterGoldChanse)
        {
            _game.PlayerGold -= Price;
            Price *= 2;
            PriceText.text = Price.ToString();

            _game.MonsterGoldUp += indexGoldUp;
            indexGoldUp += 5;
            UpMonsterGoldUpText.text = "+" + indexGoldUp.ToString() + "%";


            for (int i = 0; i < MonstersGoldPrefabsUp.Length; i++)
            {
                MonstersGoldPrefabsUp[i].GoldDrop = 100 + _game.MonsterGoldUp;
            }
            _game.TextGoldMonsterBonus.text = "+" + _game.MonsterGoldUp.ToString() + "% Gold drop";


            if (_game.MonsterGoldUp % 25 == 0)
            {
                indexGoldUp = 10;
                Destroy(gameObject);
            }
        }
    }
}
