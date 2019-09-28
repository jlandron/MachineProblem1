using UnityEngine;

public class EggBehavior : MonoBehaviour {
    [SerializeField]
    private float speed = 40f;
    [SerializeField]
    private float lifeSpan = 2f;

    private float _timeAlive = 0f;
    private Bounds _bounds;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    private bool _isDead = false;

    private void Start( ) {
        _bounds = Camera.main.GetWorldBounds( );
        _minX = -( _bounds.extents.x );
        _maxX = _bounds.extents.x;
        _minY = -( _bounds.extents.y );
        _maxY = _bounds.extents.y;
    }
    // Update is called once per frame
    void Update( ) {
        transform.position += speed * transform.up * Time.deltaTime;
        _timeAlive += Time.deltaTime;
    }
    private void LateUpdate( ) {
        if( _isDead ) {
            Destroy( gameObject );
        }
    }
    internal bool CheckLife( ) {
        bool isOutOfBounds = CheckIfOutOfBounds( );
        if( _timeAlive >= lifeSpan || isOutOfBounds ) {
            _isDead = true;
            return true;
        }
        return false;
    }
    private bool CheckIfOutOfBounds( ) {
        if( transform.position.x >= _maxX || transform.position.x <= _minX ||
            transform.position.y >= _maxY || transform.position.y <= _minY ) {
            return true;
        }
        return false;
    }
}