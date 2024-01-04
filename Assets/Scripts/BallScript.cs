using System;
using Model.Entities;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool _ballIsActive;
    private Vector2 _ballInitialForce;
    public Rigidbody2D ballRigidbody;

    public Game game;
    public IBall Ball;
    
    private int _timeX;
    private int _timeY;
    private float _prevX;
    private float _prevY;
    
    private void Start() {
        var angle = new System.Random().Next(45, 135) / 180f * Math.PI;
        _ballInitialForce = new Vector2((float)(Ball.Speed * Math.Cos(angle)),(float)(Ball.Speed * Math.Sin(angle)));
        _ballIsActive = false;
        ballRigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update () {
        if (Input.GetButtonDown("Jump")) {
            if (!_ballIsActive) {
                ballRigidbody.AddForce(_ballInitialForce);
                _ballIsActive = true;
            }
        }

        var position = transform.position;
        
        if (Math.Abs(position.x - _prevX) < 0.001) {
            _timeX++;
        }
        else {
            _timeX = 0;
        }
        if (Math.Abs(position.y - _prevY) < 0.001) {
            _timeY++;
        }
        else {
            _timeY = 0;
        }

        
        _prevX = position.x;
        _prevY = position.y;
        if (_timeX >= 2500 || _timeY >= 2500) {
            _timeX = _timeY = 0;
            game.Init(false);
        }
        
        if (!_ballIsActive || !(transform.position.y < -4.8)) return;
        game.Init();
        Destroy(gameObject);
        Destroy(this);
    }
}
