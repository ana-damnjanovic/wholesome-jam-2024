using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_mainAudioSource;

    [SerializeField]
    private AudioSource m_secondaryAudioSource;

    [SerializeField]
    private AudioLowPassFilter m_lowPassFilter;

    [SerializeField]
    private AudioClip m_backgroundMusic;

    [SerializeField]
    private AudioClip m_gameOverMusic;

    [SerializeField]
    private AudioClip m_policeSirenClip;

    [SerializeField]
    private List<AudioClip> m_levelStartSounds;

    private Coroutine m_secondaryClipDelayCoroutine;

    public void MuffleSound(bool shouldMuffle)
    {
        m_lowPassFilter.enabled = shouldMuffle;
    }

    public void PlayGameOverMusic()
    {
        if (null != m_secondaryClipDelayCoroutine)
        {
            StopCoroutine(m_secondaryClipDelayCoroutine);
        }
        m_mainAudioSource.Stop();
        m_secondaryAudioSource.Stop();
        m_mainAudioSource.clip = m_gameOverMusic;
        m_mainAudioSource.Play();
        m_secondaryClipDelayCoroutine = StartCoroutine(WaitAndPlaySecondaryAudio(m_policeSirenClip, 0.3f));
    }

    public void PlayBackgroundMusic()
    {
        if (null != m_secondaryClipDelayCoroutine)
        {
            StopCoroutine(m_secondaryClipDelayCoroutine);
        }
        m_mainAudioSource.Stop();
        m_secondaryAudioSource.Stop();
        m_mainAudioSource.clip = m_backgroundMusic;
        m_mainAudioSource.Play();
    }

    public void PlayLevelMusic(int level)
    {
        if (0 <= level && level < m_levelStartSounds.Count)
        {
            m_secondaryClipDelayCoroutine = StartCoroutine(WaitAndPlaySecondaryAudio(m_levelStartSounds[level], 0.5f));
        }
    }

    private IEnumerator WaitAndPlaySecondaryAudio(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_secondaryAudioSource.PlayOneShot(clip);
    }
}
