using UnityEngine;

public class HeroMover : MonoBehaviour {
    [SerializeField]
    private float startSpeed = 20f;
    [SerializeField]
    private float maxSpeed = 40f;
    [SerializeField]
    private float rotationDegrees = 90f;
    [SerializeField]
    private float rotationTime = 2f;
    [SerializeField]
    private EggSpawner eggSpawner = null;

    private float _rotationSpeed;
    private float _speed;
    private Bounds _screenBounds;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private float _multipleBounceDelay = 0.001f;
    private float _timeSinceLastBouce = 0f;

    // Start is called before the first frame update
    void Start( ) {
        eggSpawner = FindObjectOfType<EggSpawner>( );

        transform.position = new Vector3( 0, 0, 10 );
        transform.rotation = new Quaternion( 0, 0, 0, 0 );

        _speed = startSpeed;
        _screenBounds = Camera.main.GetWorldBounds( );
        _rotationSpeed = rotationDegrees / rotationTime;

        _minX = -( _screenBounds.extents.x );
        _maxX = _screenBounds.extents.x;
        _minY = -( _screenBounds.extents.y );
        _maxY = _screenBounds.extents.y;
    }
    // Update is called once per frame
    void Update( ) {
        MoveForward( );
        GetKeyboardInput( );
        GetMouseInput( );
    }
    private void LateUpdate( ) {
        BouceOffWalls( );
    }
    private void BouceOffWalls( ) {
        _timeSinceLastBouce += Time.deltaTime;
        if( _timeSinceLastBouce > _multipleBounceDelay ) {
            if( transform.position.x >= _maxX || transform.position.x <= _minX ) {
                transform.up = new Vector3( -transform.up.x,
                                           transform.up.y,
                                           transform.up.z );
            }
            if( transform.position.y >= _maxY || transform.position.y <= _minY ) {
                transform.up = new Vector3( transform.up.x,
                                           -transform.up.y,
                                           transform.up.z );
            }
            _timeSinceLastBouce = 0f;
        }
    }
    private void MoveForward( ) {
        transform.position += _speed * transform.up * Time.deltaTime;
    }
    private void GetMouseInput( ) {
        float angle = Input.GetAxis( "Fire1" ) * ( _rotationSpeed * Time.deltaTime );
        angle -= Input.GetAxis( "Fire2" ) * ( _rotationSpeed * Time.deltaTime );
        transform.Rotate( transform.forward, angle );
    }
    private void GetKeyboardInput( ) {
        _speed += ( 0.5f * Input.GetAxis( "Vertical" ) );
        _speed = _speed < maxSpeed ? _speed : maxSpeed;

        if( Input.GetAxis( "Horizontal" ) > 0 ) {
            transform.Rotate( Vector3.forward * -1 * _rotationSpeed * Time.deltaTime );
        } else if( Input.GetAxis( "Horizontal" ) < 0 ) {
            transform.Rotate( Vector3.forward * _rotationSpeed * Time.deltaTime );
        }
        if( Input.GetKey( KeyCode.Space ) ) {
            eggSpawner.SpawnEggs( transform );
        }

        if( Input.GetKeyDown( KeyCode.P ) ) {
            _speed = 0;
        }
        if( Input.GetKeyDown( KeyCode.Q ) ) {
            Application.Quit( );
        }
    }
}

