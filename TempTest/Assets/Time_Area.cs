using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Area : MonoBehaviour
{
    [SerializeField]
    float defaultSpeed = 0;

    private void OnTriggerEnter(Collider other)
    {
        var monster = other.GetComponent<Monster>();
        if(monster != null)
        {
            defaultSpeed = monster.MonsterAgent.speed;
            monster.MonsterAgent.speed *= 0.1f;
            monster.MonsterAnimator.speed = 0.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var monster = other.GetComponent<Monster>();
        if(monster != null)
        {
            monster.MonsterAgent.speed = defaultSpeed;
            monster.MonsterAnimator.speed = 1f;
        }
    }
}
