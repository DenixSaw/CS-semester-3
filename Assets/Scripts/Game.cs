using System.Collections.Generic;
using Model;
using Model.Factories;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        var effectDictionary = new Dictionary<string, float>() {
            { "BallSpeedUpEffect", 50 },
            { "PlayerSpeedUpEffect", 100 }
        };
        
        var config = new Config(effectDictionary, 8,3,1,1);
        var scene = new Scene(new FieldFactory(config, new EffectFactory(config)));
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
