using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    [SerializeField]
    private float speed = 20;
    // Update is called once per frame
    void Update( ) {
        transform.position += speed * transform.up * Time.deltaTime;
    }
}
