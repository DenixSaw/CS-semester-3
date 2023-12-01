
using System;

namespace Model.Entities {
public class Ball : IBall {
    private int _speed;
    public int Speed {
        get => _speed;
        set {
            if (value < 1)
                throw new Exception("Некорректное значение скорости");
            _speed = value;
        }
    }

    private int _radius;
    public int Radius {
        get => _radius;
        set {
            if (value < 1)
                throw new Exception("Некорректное значение радиуса");
            _radius = value;
        }
    }

    public Ball(int speed, int radius) {
        Speed = speed;
        Radius = radius;
    }
}
}
