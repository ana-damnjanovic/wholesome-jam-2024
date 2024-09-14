using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    [SerializeField]
    private float m_frequency = 0.8f;
    [SerializeField]
    private float m_force = 500f;

    private Rigidbody2D rb;
    private bool m_isActive;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    public void StartEngine()
    {
        m_isActive = true;
        StartCoroutine(ApplyForce());
    }

    private IEnumerator ApplyForce()
    {
        while (m_isActive)
        {
            rb.AddForce(new Vector2(m_force, 0f));
            yield return new WaitForSeconds(1f / m_frequency);
        }
    }
}
