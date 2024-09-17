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
            Motorcycle motorcycle = collision.transform.parent.GetComponent<Motorcycle>();
            StartCoroutine(WaitForMotorcycleStopAndInvoke(motorcycle));
        }
    }

    private IEnumerator WaitForMotorcycleStopAndInvoke(Motorcycle motorcycle)
    {
        bool isStopped = false;
        motorcycle.Stopped += OnMotorcycleStopped;

        void OnMotorcycleStopped()
        {
            motorcycle.Stopped -= OnMotorcycleStopped;
            isStopped = true;

        }
        motorcycle.ApplyBrakes();
        while (!isStopped)
        {
            yield return null;
        }
        GoalReached.Invoke();

    }
}
