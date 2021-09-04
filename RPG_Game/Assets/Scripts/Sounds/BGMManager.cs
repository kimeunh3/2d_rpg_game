using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // 클래스를 전역 객체로 사용하기 위함
    // public으로 선언된 변수와 함수는 다른 클래스에서도 사용이 가능해짐
    public static BGMManager Instance { get; private set; }

    public AudioClip bgm;
    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        audioSource.loop = true;

        PlaySound(bgm);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
