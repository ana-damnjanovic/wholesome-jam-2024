using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    [SerializeField]
    private float m_revFrequency = 0.8f;
    [SerializeField]
    private float m_force = 500f;

    [SerializeField]
    private float m_maxVelocity = 20f;

    [SerializeField]
    private float m_brakeTime = 3f;

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
    private Coroutine m_brakeCoroutine;

    [SerializeField]
    private Vector2 m_initialVelocity = new Vector2(2f, 0f);

    public event System.Action Stopped = delegate { };

    private void Awake()
    {
        m_bodyRb = m_motorcycleBody.GetComponent<Rigidbody2D>();
        m_motorcycleBodyStartPosition = m_motorcycleBody.transform.position;
    }

    public void StartEngine()
    {
        m_isActive = true;

        m_bodyRb.velocity = m_initialVelocity;
        m_wheel1.velocity = m_initialVelocity;
        m_wheel2.velocity = m_initialVelocity;

        m_bodyRb.isKinematic = false;
        m_bodyRb.constraints = RigidbodyConstraints2D.None;
        m_wheel1.isKinematic = false;
        m_wheel2.isKinematic = false;

        m_motionCoroutine = StartCoroutine(ApplyForce());
    }

    public void ApplyBrakes()
    {
        if (m_motionCoroutine != null)
        {
            StopCoroutine(m_motionCoroutine);
            m_motionCoroutine = null;
        }
        m_brakeCoroutine = StartCoroutine(Brake());

    }

    public void StopMotorcycle()
    {
        m_isActive = false;

        if (m_motionCoroutine != null)
        {
            StopCoroutine(m_motionCoroutine);
            m_motionCoroutine = null;
        }

        if (m_brakeCoroutine != null)
        {
            StopCoroutine(m_brakeCoroutine);
            m_brakeCoroutine = null;
        }

        m_bodyRb.velocity = Vector3.zero;
        m_bodyRb.constraints = RigidbodyConstraints2D.FreezeAll;
        m_wheel1.velocity = Vector3.zero;
        m_wheel2.velocity = Vector3.zero;

        m_bodyRb.isKinematic = true;
        m_wheel1.isKinematic = true;
        m_wheel2.isKinematic = true;
    }

    public void ResetMotorcycle()
    {
        StopMotorcycle();

        m_motorcycleBody.transform.position = m_motorcycleBodyStartPosition;

        m_bodyRb.velocity = m_initialVelocity;
        m_wheel1.velocity = m_initialVelocity;
        m_wheel2.velocity = m_initialVelocity;
    }

    private IEnumerator ApplyForce()
    {
        while (m_isActive)
        {
            m_bodyRb.AddForce(new Vector2(m_force, 0f));
            m_bodyRb.velocity = new Vector2(Mathf.Clamp(m_bodyRb.velocity.x, 0f, m_maxVelocity), m_bodyRb.velocity.y);
            m_motorcycleAudioSource.PlayOneShot(m_motorcycleRev);
            yield return new WaitForSeconds(1f / m_revFrequency);
        }
    }

    private IEnumerator Brake()
    {
        if (m_isActive)
        {
            float timeElapsed = 0f;
            Vector2 startVelocity = m_bodyRb.velocity;
            while (timeElapsed < m_brakeTime)
            {
                timeElapsed += Time.deltaTime;
                float progress = timeElapsed / m_brakeTime;
                float currentVelocity = Mathf.Lerp(startVelocity.x, 0f, progress);
                m_bodyRb.velocity = new Vector2(currentVelocity, 0f);
                yield return null;
            }
            m_bodyRb.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            Stopped.Invoke();
        }
    }
}