using UnityEngine;

namespace SoundManage
{

    [System.Serializable]
    public struct SoundCell
    {
        public int id;
        public string name;
        public AudioClip _AudioClip;
    }
    
    [CreateAssetMenu(menuName = "SO/SoundPack")]
    public class SoundPack : ScriptableObject
    {
        public string packName;
        public SoundCell[] _audioCells;

        public SoundCell FindSound(int id)
        {
            for (int i = 0; i < _audioCells.Length; i++)
            {
                if (_audioCells[i].id == id)
                {
                    return _audioCells[i];
                }
            }

            return new SoundCell { id = -1 };
        }
        public SoundCell FindSound(string name)
        {
            for (int i = 0; i < _audioCells.Length; i++)
            {
                if (_audioCells[i].name.Equals(name))
                {
                    return _audioCells[i];
                }
            }
            return new SoundCell { id = -1 };
        }
        
    }

}