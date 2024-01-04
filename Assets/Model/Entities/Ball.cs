
using System;

namespace Model.Entities {
public class Ball : IBall {
    private float _speed;
    public float Speed {
        get => _speed;
        set {
            if (value < 1)
                throw new Exception("Некорректное значение скорости");
            _speed = value;
        }
    }

    private float _radius;
    public float Radius {
        get => _radius;
        set {
            if (value < 0)
                throw new Exception("Некорректное значение радиуса");
            _radius = value;
        }
    }

    public Ball(float speed, float radius) {
        Speed = speed;
        Radius = radius;
    }
}
}
