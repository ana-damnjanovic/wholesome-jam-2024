using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event System.Action GoalReached = delegate { };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MotorcycleBase")
        {
            GoalReached.Invoke();
        }
    }
}
