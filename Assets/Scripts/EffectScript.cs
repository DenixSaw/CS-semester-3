using Model.Effects;
using UnityEngine;

public class EffectScript : MonoBehaviour {
    public IEffect Effect;

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        
        Effect.Apply(GameObject.FindWithTag("MainCamera").GetComponent<Game>());
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < -4.8) {
            Destroy(gameObject);
        }
    }
}
