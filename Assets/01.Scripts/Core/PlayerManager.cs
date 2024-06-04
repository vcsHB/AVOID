using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private Transform _playerTrm;
    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
            {
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
                if (_playerTrm == null)
                {
                    Debug.LogError("Player dose not exist but still try access it.");
                }
            }

            return _playerTrm;
        }
    }

    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = PlayerTrm.GetComponent<Player>();
            }

            return _player;
        }
    }
    
}