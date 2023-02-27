using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHelper : MonoBehaviour
{
    public GameObject BulletPrefab;

    public int Damage = 1;
    public float AttackSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackSpeed);

        GameObject bullet = Instantiate(BulletPrefab) as GameObject;
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletHelper>().Damage = Damage;

        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
