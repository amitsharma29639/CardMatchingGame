using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "AudioGroup", menuName = "ScriptableObjects/AudioGroup")]
    internal class AudioGroup : ScriptableObject
    {
        [SerializeField] private string _groupName;
        [SerializeField] private List<AudioClipData> _audioClipsData;
        internal IEnumerable<AudioClipData> GetClipsData() => _audioClipsData; 
        
        internal AudioClip GetClip(string name)
        {
            if (_audioClipsData != null)
            {
                foreach(var _audioData in _audioClipsData)
                {
                    if(_audioData._audioName.Equals(name))
                        return _audioData._audioClip;
                }
            }
            return null;
        }
        
        internal string GetGroupName() => _groupName; 
    }

    [System.Serializable]
    internal class AudioClipData
    {
        [SerializeField] internal string _audioName;
        [SerializeField] internal AudioClip _audioClip;
    }