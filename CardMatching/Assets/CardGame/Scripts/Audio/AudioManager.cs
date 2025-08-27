using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

    [System.Serializable]
    internal class AudioSourceData
    {
        [SerializeField] private string _audioSourceName;
        [SerializeField] private string _group;
        [SerializeField] private AudioSource _audioSource;

        internal string GetAudioSourceName() => _audioSourceName; 
        internal string GetGroup() => _group;
        internal AudioSource GetSource() => _audioSource; 
    }

    public class AudioManager : MonoBehaviour
    {

        private bool _isSound;
        private bool _isMusic;
        [SerializeField] private List<AudioSourceData> _sourceDataList;
        [SerializeField] private AudioGroupMasterData _audioGroupMasterData;

        public static AudioManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            var v_soundKey = PlayerPrefs.GetInt("SoundValue", 1);
            var v_musicKey = PlayerPrefs.GetInt("MusicValue", 1);
            _isSound = v_soundKey == 0 ? false : true;
            _isMusic = v_musicKey == 0 ? false : true;
            SetSound(_isSound);
            SetMusic(_isMusic);
        }

        internal bool GetSound() => _isSound;
        internal bool GetMusic() => _isMusic;

        internal void SetSound(bool value)
        {
            _isSound = value;
            PlayerPrefs.SetInt("SoundValue", value ? 1 : 0);
            foreach (var v_sourceData in _sourceDataList)
            {
                if (v_sourceData.GetGroup().Equals(AudioGroupConstants.BGM))
                {
                    v_sourceData.GetSource().mute = !value;
                }
            }
        }

        internal void SetMusic(bool value)
        {
            _isMusic = value;
            PlayerPrefs.SetInt("MusicValue", value ? 1 : 0);
            foreach (var v_sourceData in _sourceDataList)
            {
                if (v_sourceData.GetGroup().Equals(AudioGroupConstants.GAMEPLAYSFX))
                {
                    v_sourceData.GetSource().mute = !value;
                }
            }
        }

        internal void Pause(string sourceName)
        {
            GetAudioSource(sourceName)?.Pause();
        }

        internal void Stop(string sourceName)
        {
            GetAudioSource(sourceName)?.Stop();
        }

        internal float GetClipLength(string clipName, string groupName = default)
        {
            var v_clip = GetClip(clipName, groupName);
            return v_clip == null ? 0f : v_clip.length;
        }

        internal void LoadAudioGroup(string groupName)
        {
            var v_audioGroup = _audioGroupMasterData.GetGroup(groupName);
            var v_clips = v_audioGroup?.GetClipsData();
            foreach (var v_clip in v_clips)
            {
                if (v_clip._audioClip.loadState != AudioDataLoadState.Loaded)
                {
                    v_clip._audioClip.LoadAudioData();
                }
            }
        }

        internal void UnloadAudioGroup(string groupName)
        {
            var v_audioGroup = _audioGroupMasterData.GetGroup(groupName);
            var v_clips = v_audioGroup?.GetClipsData();
            foreach (var v_clip in v_clips)
            {
                if (v_clip._audioClip.loadState != AudioDataLoadState.Unloaded)
                {
                    v_clip._audioClip.UnloadAudioData();
                }
            }
        }

        internal void UnloadAll()
        {
            var v_groups = _audioGroupMasterData.GetGroups();
            if (v_groups == null)
            {
                return;
            }

            foreach (var v_group in v_groups)
            {
                var v_clips = v_group.GetClipsData();
                if (v_clips == null)
                {
                    continue;
                }

                foreach (var v_clip in v_clips)
                {
                    if (v_clip._audioClip.loadState != AudioDataLoadState.Unloaded)
                    {
                        v_clip._audioClip.UnloadAudioData();
                    }
                }
            }
        }

        public void Play(string audioSource, string clipName, string groupName = default, float volume = 0.8f,
            float delay = 0f)
        {
            var v_clip = GetClip(clipName, groupName);
            if (v_clip == null || v_clip.loadState != AudioDataLoadState.Loaded)
            {
                Debug.LogFormat("Audio Clip is either null or not loaded {0}, {1}", v_clip, v_clip?.loadState);
                return;
            }

            var v_audioSource = GetAudioSource(audioSource);
            if (v_audioSource == null)
            {
                return;
            }

            v_audioSource.Stop();
            v_audioSource.clip = v_clip;
            v_audioSource.volume = volume;
            if (delay > 0f)
            {
                v_audioSource.PlayDelayed(delay);
            }
            else
            {
                v_audioSource.Play();
            }
        }

        public void PlayOneShot(string sourceName, string clipName, string groupName = default, float volume = 1f,
            float delay = 0f)
        {
            var v_clip = GetClip(clipName, groupName);
            if (v_clip == null || v_clip.loadState != AudioDataLoadState.Loaded)
            {
                Debug.LogFormat("Audio Clip is either null or not loaded {0}, {1}", v_clip, v_clip?.loadState);
                return;
            }

            var v_audioSource = GetAudioSource(sourceName);

            if (v_audioSource == null)
            {
                return;
            }

            if (delay > 0f)
            {
                StartCoroutine(delayedCall(delay, () => { v_audioSource.PlayOneShot(v_clip, volume); }));
            }
            else
            {
                v_audioSource.PlayOneShot(v_clip, volume);
            }
        }

        private AudioClip GetClip(string clipName, string audioGroup)
        {
            var v_clip = default(AudioClip);
            if (string.IsNullOrEmpty(audioGroup))
            {
                var v_groups = _audioGroupMasterData.GetGroups();
                foreach (var v_data in v_groups)
                {
                    v_clip = v_data.GetClip(clipName);
                    if (v_clip != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                var v_group = _audioGroupMasterData.GetGroup(audioGroup);
                v_clip = v_group?.GetClip(clipName);
            }

            return v_clip;
        }

        private AudioSource GetAudioSource(string sourceName)
        {
            if (_sourceDataList == null)
            {
                return null;
            }

            return _sourceDataList.Find(x => x.GetAudioSourceName().Equals(sourceName))?.GetSource();
        }

        private IEnumerator delayedCall(float delay, Action onComplete)
        {
            yield return new WaitForSecondsRealtime(delay);
            onComplete?.Invoke();
        }
    }