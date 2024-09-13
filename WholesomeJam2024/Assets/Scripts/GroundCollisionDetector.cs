using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionDetector : MonoBehaviour
{
    public event System.Action<GroundCollisionDetector> GroundCollisionDetected = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GroundCollisionDetected.Invoke(this);
        }
    }
}
