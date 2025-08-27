using System.Collections.Generic;
using UnityEngine;

namespace JTools.Sound.Core
{
    [CreateAssetMenu(fileName = "AudioGroup", menuName = "ScriptableObjects/AudioGroup")]
    internal class AudioGroup : ScriptableObject
    {
        [SerializeField] private string _groupName;
        [SerializeField] private List<AudioClipData> _audioClipsData;

        /// <summary>
        /// Returns all the audio clips present in the audio group.
        /// </summary>
        internal IEnumerable<AudioClipData> GetClipsData() => _audioClipsData; 

        /// <summary>
        /// Returns the audioclip which matches the specified audio clip from the list of audio clips.
        /// </summary>
        /// <param name="match"></param>
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

        /// <summary>
        /// Returns the audio group name.
        /// </summary>
        internal string GetGroupName() => _groupName; 
    }

    [System.Serializable]
    internal class AudioClipData
    {
        [SerializeField] internal string _audioName;
        [SerializeField] internal AudioClip _audioClip;
    }

}