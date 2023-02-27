using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHelper : MonoBehaviour
{
    Game _game;
    PlayerHelper _playerHelper;

    void Start()
    {
        _game = FindObjectOfType<Game>();
        _playerHelper = FindObjectOfType<PlayerHelper>();


    }
    void OnMouseDown()
    {
        //Debug.Log("+");

        GetComponent<HealthHelper>().GetHit(_game.PlayerDamage);
        _playerHelper.RunAttack();
    }


}
