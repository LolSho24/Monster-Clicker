using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    public Transform AttackStartPosition;
    public GameObject[] AttackPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RunAttack()
    {
        GetComponent<Animator>().SetTrigger("attack");
        int index = Random.Range(0, AttackPrefabs.Length);
        GameObject effect = Instantiate(AttackPrefabs[index]) as GameObject;
        effect.transform.position = AttackStartPosition.position;
        Destroy(effect, 0.15f);

    }
}
