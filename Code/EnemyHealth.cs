using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoint = 5;
    int currentHealthPoint;

    [SerializeField] int difficulty = 1;


    Enemy enemy;
    void OnEnable()
    {
        currentHealthPoint = maxHitPoint;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }


    void OnParticleCollision(GameObject other)
    {
        ProcessOfHit();
    }

    void ProcessOfHit()
    {
        currentHealthPoint-- ;

        if (currentHealthPoint <= 0)
        {
            enemy.GoldReward();
            maxHitPoint += difficulty;
            gameObject.SetActive(false);
        }
    }
}
