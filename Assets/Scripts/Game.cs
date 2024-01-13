using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Entities;
using Model.Factories;
using Model.Storage;
using UnityEngine;

public class Game : MonoBehaviour {
    public GameObject playerTemplate;
    public GameObject ballTemplate;
    public GameObject brickTemplate;

    public GameObject Player;
    public GameObject Ball;
    public List<GameObject?> Field = new();
    public int BrickNumber;
    public List<IBrick> _field;

    private IConfig _config;
    private const float SceneWidth = 4f;
    private const float BrickHeight = 0.4f;


    private void SpawnPlayer() {
        IPlayer playerEntity = new Player(_config.PlayerSpeed, _config.PlayerWidth);
        var player = Instantiate(playerTemplate, new Vector3(0, -4.5f), Quaternion.identity);
        Player = player;
        player.gameObject.transform.localScale = new Vector3(_config.PlayerWidth, 0.35f, 0);
        player.GetComponent<PlayerScript>().boundary = SceneWidth;
        player.GetComponent<PlayerScript>().Player = playerEntity;
        player.GetComponent<PlayerScript>().game = this;
    }

    private void SpawnBall() {
        var ballEntity = new Ball(_config.BallSpeed, _config.BallRadius);
        var ball = Instantiate(ballTemplate, new Vector3(0, -4.1f), Quaternion.identity);
        Ball = ball;
        ball.gameObject.transform.localScale = new Vector3(_config.BallRadius, _config.BallRadius, 0);
        ball.GetComponent<BallScript>().Ball = ballEntity;
        ball.GetComponent<BallScript>().game = this;
    }

    private void SpawnField() {
        var gap = 0.05f;
        _field = FieldRepository.Get();
        BrickNumber = _field.Count - _field.Count(b => b is null);

        var brickWidth = (10f - gap * (_config.NumOfBricks - 1)) / _config.NumOfBricks;
        var factory = new FieldFactory(_config, new EffectFactory(_config));
        if (_field.All(brick => brick == null) || _field.Count == 0) {
            _field = factory.GetField();
            BrickNumber = _field.Count;
        }

        var startX = -5f + brickWidth / 2;
        var startY = 4.8f;

        for (var i = 0; i < _config.NumOfLines; i++) {
            for (var k = 0; k < _config.NumOfBricks; k++) {
                if (i * _config.NumOfBricks + k >= _field.Count || _field[i * _config.NumOfBricks + k] == null) {
                    Field.Add(null);
                    startX += brickWidth + gap;
                    continue;
                }

                var current = _field[i * _config.NumOfBricks + k]!;
                var brick = Instantiate(brickTemplate, new Vector3(startX, startY),
                    Quaternion.identity);
                Field.Add(brick);
                brick.gameObject.transform.localScale = new Vector3(brickWidth, BrickHeight, 0);
                brick.GetComponent<SpriteRenderer>().color = current.Color;
                brick.GetComponent<BrickScript>().health = current.Health;
                brick.GetComponent<BrickScript>().idx = i * _config.NumOfBricks + k;
                brick.GetComponent<BrickScript>().Effect = current.Effect;
                brick.GetComponent<BrickScript>().game = this;

                startX += brickWidth + gap;
            }

            startX = -5f + brickWidth / 2;
            startY -= BrickHeight + gap;
        }
    }

    public void Init(bool destroyField = true) {
        if (Player != null)
            Destroy(Player);
        if (Ball != null)
            Destroy(Ball);
        if (destroyField) {
            foreach (var brick in Field) {
                Destroy(brick);
            }

            FieldRepository.Set(new List<IBrick?>());
        }

        SpawnPlayer();
        SpawnBall();
        SpawnField();
    }

    private void Start() {
        _config = ConfigRepository.Get();

        Init(false);
    }
}