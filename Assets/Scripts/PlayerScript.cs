using System;
using Model.Entities;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public Game game;
    public IPlayer Player;
    public float boundary;
    
    private Vector3 _playerPosition;


    // Start is called before the first frame update
    private void Start() {
        _playerPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update() {
        _playerPosition.x += Input.GetAxis("Horizontal") * Player.Speed;

        if (_playerPosition.x < -boundary)
            _playerPosition.x = -boundary;
        if (_playerPosition.x > boundary)
            _playerPosition.x = boundary;
        
        transform.position = _playerPosition;
    }
}