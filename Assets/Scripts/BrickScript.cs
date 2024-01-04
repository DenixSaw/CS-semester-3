using Model.Effects;
using Model.Storage;
using UnityEngine;

public class BrickScript : MonoBehaviour {
    public int health;
    public int idx;
    public GameObject effectTemplate;
    public IEffect Effect;
    public Game game;
    
    void OnCollisionEnter2D(Collision2D _) {
        health--;
        if (health > 0) {
            if (game._field[idx] != null)
                game._field[idx].Health--;
            return;
        }
        if (Effect != null) {
            var effect = Instantiate(effectTemplate, transform.position, Quaternion.identity);
            effect.GetComponent<EffectScript>().Effect = Effect;
        }
        Destroy(gameObject);
        
        game.BrickNumber--;
        game._field[idx] = null;
        FieldRepository.Set(game._field);
        
        if (game.BrickNumber <= 0) {
            Debug.Log("Victory");
            game.Init();
        }
    }
}
