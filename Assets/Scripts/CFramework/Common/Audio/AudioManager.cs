using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace CFramework
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioMixer _masterMixer;
        private AudioMixerGroup _musicMixerGroup;
        private AudioMixerGroup _soundEffectMixerGroup;

        public AudioSource MusicSource;
        
        //声音对象池
        private ObjectPool<AudioSource> _audioObjectPool;
        private void Awake()
        {
            _audioObjectPool = new ObjectPool<AudioSource>(FuncAudioSourceOnCreate, ActionAudioSourceOnGet, ActionAudioSourceOnRelease, ActionAudioSourceOnDestroy, false, 10, 20);
            _masterMixer = ResourceLoader.Load<AudioMixer>(ResourceType.Audio, "MasterMixer");
            _musicMixerGroup = _masterMixer.FindMatchingGroups("Music")[0];
            _soundEffectMixerGroup = _masterMixer.FindMatchingGroups("SoundEffect")[0];
            
            GameObject musicGameObject = new GameObject("MusicSource");
            musicGameObject.transform.SetParent(this.transform);
            MusicSource = musicGameObject.AddComponent<AudioSource>();
            MusicSource.playOnAwake = false;
            MusicSource.loop = true;
            MusicSource.outputAudioMixerGroup = _musicMixerGroup;
        }

        public void LoadMusic(string acName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Music/" + acName);
            ResourceLoader.LoadAsync<AudioClip>(ResourceType.Audio, sb.ToString(), LoadMusic);
        }

        public AudioSource LoadSoundEffect(string acName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SoundEffect/" + acName);
            AudioClip clip = ResourceLoader.Load<AudioClip>(ResourceType.Audio, sb.ToString(), true);
            AudioSource source = _audioObjectPool.Get();
            source.clip = clip;
            return source;
        }

        public void LoadSoundEffect(AudioSource source, string acName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SoundEffect/" + acName);
            AudioClip clip = ResourceLoader.Load<AudioClip>(ResourceType.Audio, sb.ToString(), true);
            //Debug.Log("SoundEffect Load and Play");
            source.clip = clip;
        }

        private void LoadMusic(AudioClip musicAudioClip)
        {
            MusicSource.clip = musicAudioClip;
            //Debug.Log("Music Load and Play");
            MusicSource.Play();
        }

        public void SetMasterVolume(float volume)// 控制主音量的函数
        {
            _masterMixer.SetFloat("MasterVolume", volume);
            // MasterVolume为我们暴露出来的Master的参数
        }
 
        public void SetMusicVolume(float volume)// 控制背景音乐音量的函数
        {
            _masterMixer.SetFloat("MusicVolume", volume);
            // MusicVolume为我们暴露出来的Music的参数
        }
 
        public void SetSoundEffectVolume(float volume)// 控制音效音量的函数
        {
            _masterMixer.SetFloat("SoundEffectVolume", volume);
            // SoundEffectVolume为我们暴露出来的SoundEffect的参数
        }

        public void Clear()
        {
            _audioObjectPool.Clear();
        }
        
        AudioSource FuncAudioSourceOnCreate()
        {
            GameObject go = new GameObject("Pooled Audio Source");
            //go.hideFlags = HideFlags.HideInHierarchy;
            go.transform.SetParent(this.transform);
            
            AudioSource source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.outputAudioMixerGroup = _soundEffectMixerGroup;
            
            return source;
        }

        void ActionAudioSourceOnGet(AudioSource source)
        {
            source.gameObject.SetActive(true);
        }

        void ActionAudioSourceOnRelease(AudioSource source)
        {
            source.gameObject.SetActive(false);
        }

        void ActionAudioSourceOnDestroy(AudioSource source)
        {
            GameObject.Destroy(source.gameObject);
        }
    }
}