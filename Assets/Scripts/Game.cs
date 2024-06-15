using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerView _botView;

    private PlayerInput _playerInput;
    private PlayerModel _playerModel;
    private PlayerConroller _playerConroller;

    private BotAI _botInput;
    private PlayerModel _botModel;
    private PlayerConroller _botConroller;

    

    private void Awake()
    {
        _playerModel = new PlayerModel();
        _playerConroller = new PlayerConroller(_playerModel, _playerView);
        _playerInput = _playerView.GetComponent<PlayerInput>();
        _playerInput.SetControllable(_playerConroller);

        _botModel = new PlayerModel();
        _botConroller = new PlayerConroller(_botModel, _botView);
        _botInput = _botView.GetComponent<BotAI>();
        _botInput.SetControllable(_botConroller);
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        _playerModel.Respawned -= () => { _playerModel.ScoreModel.Score++; };
        _playerModel.ScoreModel.ScoreUpdated -= (a) => { };

        _botModel.Respawned -= () => { _botModel.ScoreModel.Score++; };
        _botModel.ScoreModel.ScoreUpdated -= (a) => { };
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
