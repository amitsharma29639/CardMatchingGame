using System.Collections.Generic;
using UnityEngine;

namespace JTools.Sound.Core
{
    [CreateAssetMenu(fileName = "AudioGroupMasterData", menuName = "ScriptableObjects/AudioGroupMasterData")]
    internal class AudioGroupMasterData : ScriptableObject
    {
        [SerializeField] private List<AudioGroup> _audioGroups;

        /// <summary>
        /// Returns the audio group with specified name.
        /// </summary>
        /// <param name="name"></param>
        internal AudioGroup GetGroup(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return default;
            }
            return _audioGroups.Find(x => x.GetGroupName().ToLower().Equals(name.ToLower()));
        }

        /// <summary>
        /// Returns the list of audio group present.
        /// </summary>
        internal IEnumerable<AudioGroup> GetGroups() => _audioGroups;

        /// <summary>
        /// Returns specified audio clip from specified audio group.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="audioGroup"></param>
        internal AudioClip GetClip(string audioName, string audioGroup)
        {
            if (string.IsNullOrEmpty(audioGroup))
            {
                return null;
            }
            var v_group = GetGroup(audioGroup);
            return v_group?.GetClip(audioName);
        }
    }
}