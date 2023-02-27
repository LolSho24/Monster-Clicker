using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpButtonHelper : MonoBehaviour
{
    public bool IsRuby;

    public bool IsHero;

    public GameObject UpPrefab;
    public GameObject HeroPrefab;


    public Text DamageText;
    public Text PriceText;
    public Image IcoImage;

    int Index = 0;
    public int Damage = 1;
    public int Price = 100;

    Game _game;
    public HeroHelper _heroHelper;

    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindObjectOfType<Game>();

        if (!IsHero)
        {
            DamageText.text = "+" + Damage.ToString();
            PriceText.text = Price.ToString();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     
    public void UpClick()
    {
        if (!IsRuby && _game.PlayerGold >= Price || IsRuby && _game.PlayerRuby >= Price)
        {
            if (!IsRuby)
            {
                _game.PlayerGold -= Price;

            }
            else
            {
                _game.PlayerRuby -= Price;
                Price = (Price * 2) + Index;
                Index++;
                PriceText.text = Price.ToString();
                


            }
            if (!IsHero)
            {
                _game.PlayerDamage += Damage;
            }
            else
            {
                HeroPrefab.SetActive(true);
                _heroHelper.Damage *= 2;
                DamageText.text = "+" + _heroHelper.Damage.ToString();

            }

            if (!IsHero)
            {
                GameObject upEffect = Instantiate(UpPrefab) as GameObject;

                Transform canvas = GameObject.Find("Canvas").transform;
                upEffect.transform.SetParent(canvas);
                upEffect.GetComponent<Image>().sprite = IcoImage.sprite;

                Destroy(upEffect, 1.20f);
                Destroy(gameObject); 
            }
        }
    }
}
