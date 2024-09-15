using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    [SerializeField]
    private float m_frequency = 0.8f;
    [SerializeField]
    private float m_force = 500f;

    [SerializeField]
    private GameObject m_motorcycleBody;

    [SerializeField]
    private Rigidbody2D m_wheel1;

    [SerializeField]
    private Rigidbody2D m_wheel2;

    [SerializeField]
    private AudioSource m_motorcycleAudioSource;

    [SerializeField]
    private AudioClip m_motorcycleRev;

    private Vector3 m_motorcycleBodyStartPosition;

    private Rigidbody2D m_bodyRb;
    private bool m_isActive;

    private Coroutine m_motionCoroutine;

    private void Awake()
    {
        m_bodyRb = m_motorcycleBody.GetComponent<Rigidbody2D>();
    }

    public void StartEngine()
    {
        m_isActive = true;
        m_motorcycleBodyStartPosition = m_motorcycleBody.transform.position;
        m_bodyRb.isKinematic = false;
        m_bodyRb.constraints = RigidbodyConstraints2D.None;
        m_wheel1.isKinematic = false;
        m_wheel2.isKinematic = false;
        m_motionCoroutine = StartCoroutine(ApplyForce());
    }

    public void StopMotorcycle()
    {
        m_isActive = false;
        if (null != m_motionCoroutine)
        {
            StopCoroutine(m_motionCoroutine);
            m_motionCoroutine = null;
        }
        m_bodyRb.isKinematic = true;
        m_bodyRb.velocity = Vector3.zero;
        m_bodyRb.constraints = RigidbodyConstraints2D.FreezeAll;
        m_wheel1.isKinematic = true;
        m_wheel2.isKinematic = true;
    }

    public void ResetMotorcycle()
    {
        StopMotorcycle();
        m_motorcycleBody.transform.position = m_motorcycleBodyStartPosition;
    }

    private IEnumerator ApplyForce()
    {
        while (m_isActive)
        {
            m_bodyRb.AddForce(new Vector2(m_force, 0f));
            m_motorcycleAudioSource.PlayOneShot(m_motorcycleRev);
            yield return new WaitForSeconds(1f / m_frequency);
        }
    }
}
