using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using JTools.Sound.Core.Constants;

namespace JTools.Sound.Core
{
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
        #region DECLARATIONS
        private bool _isSound;
        private bool _isMusic;
        [SerializeField] private List<AudioSourceData> _sourceDataList;
        [SerializeField] private AudioGroupMasterData _audioGroupMasterData;

        public static AudioManager Instance;

        #endregion

        #region MONO EVENTS
        private void Awake()
        {
            if(Instance == null)
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
        #endregion

        #region INTERNAL METHODS
        /// <summary>
        /// Returns the status of Sound.
        /// </summary>
        internal bool GetSound() => _isSound;

        /// <summary>
        /// Returns the status of Music.
        /// </summary>
        internal bool GetMusic() => _isMusic;

        /// <summary>
        /// Mute/Unmute the audio source used for Sounds.
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        /// Mute/Unmute the audio source used for Music.
        /// </summary>
        /// <param name="value"></param>
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

        /// <summary>
        /// Pause specified audio source.
        /// </summary>
        /// <param name="sourceName"></param>
        internal void Pause(string sourceName)
        {
            GetAudioSource(sourceName)?.Pause();
        }

        /// <summary>
        /// Stops specified audio source.
        /// </summary>
        /// <param name="sourceName"></param>
        internal void Stop(string sourceName)
        {
            GetAudioSource(sourceName)?.Stop();
        }

        /// <summary>
        /// Returns the length of specified audio clip.
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="groupName"></param>
        internal float GetClipLength(string clipName, string groupName = default)
        {
            var v_clip = GetClip(clipName, groupName);
            return v_clip == null ? 0f : v_clip.length;
        }

        /// <summary>
        /// Loads all audio data of clips of specified audio group.
        /// </summary>
        /// <param name="groupName"></param>
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

        /// <summary>
        /// Unloads all audio data of clips of specified audio group.
        /// </summary>
        /// <param name="groupName"></param>
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

        /// <summary>
        /// Unload all the audio groups.
        /// </summary>
        internal void UnloadAll()
        {
            var v_groups = _audioGroupMasterData.GetGroups();
            if (v_groups == null) { return; }
            foreach (var v_group in v_groups)
            {
                var v_clips = v_group.GetClipsData();
                if (v_clips == null) { continue; }
                foreach (var v_clip in v_clips)
                {
                    if (v_clip._audioClip.loadState != AudioDataLoadState.Unloaded)
                    {
                        v_clip._audioClip.UnloadAudioData();
                    }
                }
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Plays the specified audio clip in specified audio source.
        /// Allows to plays the clip with a delay specified in seconds.
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="clipName"></param>
        /// <param name="groupName"></param>
        /// <param name="delay"></param>
        public void Play(string audioSource, string clipName, string groupName = default, float volume = 0.8f, float delay = 0f)
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
            v_audioSource.volume=volume;
            if (delay > 0f)
            {
                v_audioSource.PlayDelayed(delay);
            }
            else
            {
                v_audioSource.Play();
            }
        }

        /// <summary>
        /// Plays the specified audio clip in specified audio source.
        /// Allows to plays the clip with a delay specified in seconds.
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="clipName"></param>
        /// <param name="groupName"></param>
        /// <param name="volume"></param>
        /// <param name="delay"></param>
        public void PlayOneShot(string sourceName, string clipName, string groupName = default, float volume = 1f, float delay = 0f)
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
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Returns specified audio clip from specified audio group.
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="audioGroup"></param>
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

        /// <summary>
        /// Returns specified audio source. 
        /// </summary>
        /// <param name="sourceName"></param>
        private AudioSource GetAudioSource(string sourceName)
        {
            if (_sourceDataList == null)
            {
                return null;
            }
            return _sourceDataList.Find(x => x.GetAudioSourceName().Equals(sourceName))?.GetSource();
        }

        /// <summary>
        /// Used to delay any specified event with specified delay in seconds.
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="onComplete"></param>
        private IEnumerator delayedCall(float delay, Action onComplete)
        {
            yield return new WaitForSecondsRealtime(delay);
            onComplete?.Invoke();
        }
        #endregion

        

        
    }
}