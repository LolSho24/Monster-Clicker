using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHelper : MonoBehaviour
{
    public bool IsBoss;

    public int RubyChanse = 5;
    public int CountBossDead = 1;

    public int MaxHealth = 5;
    public int CurrentHealth = 5;
    public int GoldDrop = 55;
    public int RubyDrop = 1;



    public GameObject UpPrefabs;

    bool _IsDead;

    Game _game;
    
    void Start()
    {
        
        _game = FindObjectOfType<Game>();
        _game.HealthSlider.maxValue = MaxHealth;
        _game.HealthSlider.value = MaxHealth;

    }
    public void GetHit(int Damage)
    {
        if (_IsDead)
        {
            return;
        }

        int health = CurrentHealth - Damage;

        if (health <= 0 && !IsBoss)
        {
            _game.Alive = false;
            _IsDead = true;
            _game.TakeGold(GoldDrop);

            int random = Random.Range(0, 100);
            if (random < RubyChanse)
            {
                _game.TakeRuby(RubyDrop);
            }

            Destroy(gameObject);
        }

        //boss
        if (health <= 0 && IsBoss)
        {
            _game.UpHealthPrefabs++;
            _game.UpHealthIndex += 10;
            CountBossDead++;


            _game.Alive = false;
            _IsDead = true;
            _game.TakeGold(GoldDrop);

            int random = Random.Range(0, 100);
            if (random < RubyChanse)
            {
                _game.TakeRuby(RubyDrop);
            }
            Destroy(gameObject);

        }

        GetComponent<Animator>().SetTrigger("Hit");

        CurrentHealth = health;
        _game.HealthSlider.value = CurrentHealth;
        //Debug.Log("Health" + health);
    }
    

}