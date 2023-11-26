using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    [Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip audioClip; 
    }


    public Sound[] bgmList, sfxList;
    public AudioSource bgmSource, sfxSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopBGM();

        for (int i = 0; i < bgmList.Length; i++)
        {
            if (scene.name == bgmList[i].soundName)
                PlayBGM(bgmList[i].soundName);
            else
                continue;
        }
    }    

    public void PlayBGM(string name)
    {
        Sound bgmSound = Array.Find(bgmList, x => x.soundName == name);

        if (bgmSound == null)
        {
            Debug.LogWarning(bgmSound + "를 찾을 수 없음");
        }
        else
        {
            if (bgmSource != null)
            {
                bgmSource.clip = bgmSound.audioClip;
                bgmSource.loop = true;
                bgmSource.Play();
            }
            else
            {
                Debug.LogWarning("bgmSource가 null입니다. AudioSource 컴포넌트를 확인해주세요.");
            }
        }
    }

    public void PlaySFX(string name)
    {
        Sound sfxSound = Array.Find(sfxList, x => x.soundName == name);

        if (sfxSound == null)
        {
            Debug.LogWarning(sfxSound + "를 찾을 수 없음");
        }
        else
        {
            sfxSource.PlayOneShot(sfxSound.audioClip); // 지정된 오디오 클립을 한 번만 재생
        }
    }

    public void StopBGM()
    {
        if(bgmSource != null)
            bgmSource.Stop();
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ChangeBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }    
}

// AudioClip : 음원 파일 (실제 소리 데이터)
// AudioSource : 오디오를 재생하고 제어하는 데 사용
// AudioListener : 소리를 듣는 파트