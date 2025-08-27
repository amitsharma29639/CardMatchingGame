using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "AudioGroupMasterData", menuName = "ScriptableObjects/AudioGroupMasterData")]
    internal class AudioGroupMasterData : ScriptableObject
    {
        [SerializeField] private List<AudioGroup> _audioGroups;
        internal AudioGroup GetGroup(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return default;
            }
            return _audioGroups.Find(x => x.GetGroupName().ToLower().Equals(name.ToLower()));
        }
        internal IEnumerable<AudioGroup> GetGroups() => _audioGroups;
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
