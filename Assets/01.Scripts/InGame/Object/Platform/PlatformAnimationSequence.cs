using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Platform
{
    [Serializable]
    public struct SequenceCell
    {
        public int platformIndex;
        //public UnityEvent eventCell;
        public bool OnOff;
        public float Term;

       
    }

    [Serializable]
    public class PlatformAnimationSequence
    {
        public SequenceCell[] sequenceCells;
        private PlatformController _platformOwner;

        private int _currentIndex = 0;

        public void Initialize(PlatformController owner)
        {
            _platformOwner = owner;
            _currentIndex = 0;

        }

        public Coroutine PlaySequence()
        {
            return _platformOwner.StartCoroutine(SequenceCoroutine());
        }

        private IEnumerator SequenceCoroutine()
        {
            for (int i = 0; i < sequenceCells.Length; i++)
            {
                if (sequenceCells[i].OnOff)
                {
                    _platformOwner._platforms[sequenceCells[i].platformIndex].Generate();
                }
                else
                {
                    _platformOwner._platforms[sequenceCells[i].platformIndex].Destroy();
                }
                yield return new WaitForSeconds(sequenceCells[i].Term);
            }
            
        }

    }
}
