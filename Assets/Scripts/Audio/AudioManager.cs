using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _SFXSource;

    [Header("Audio Clip")]
    public AudioClip _background;
    public AudioClip _birdFly;
    public AudioClip _bossFight;
    public AudioClip _bossTakeDamage;
    public AudioClip _complate;
    public AudioClip _deth;
    public AudioClip _enemyDeth;
    public AudioClip _heal;
    public AudioClip _jump;
    public AudioClip _openBox;
    public AudioClip _takeDamage;
    public AudioClip _victory;
    public AudioClip _walk;

    public static AudioManager instance;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        _musicSource.clip = _background;
        _musicSource.Play();
    }

    public void InitLevelAudio(int levelIndex)
    {
        if (levelIndex == 3 && _musicSource.clip != _bossFight)
        {
            _musicSource.Stop();
            _musicSource.clip = _bossFight;
            _musicSource.Play();
        }
        else if (levelIndex != 3 && _musicSource.clip != _background)
        {
            _musicSource.Stop();
            _musicSource.clip = _background;
            _musicSource.Play();
        }
    }

    public void PlayeSFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }
}
