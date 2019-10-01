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

    // Start is called before the first frame update
    void Start( ) {
        eggSpawner = FindObjectOfType<EggSpawner>( );

        transform.position = new Vector3( 0, 0, 10 );
        transform.rotation = new Quaternion( 0, 0, 0, 0 );

        _speed = startSpeed;
        _rotationSpeed = rotationDegrees / rotationTime;
    }
    // Update is called once per frame
    void Update( ) {
        MoveForward( );
        GetKeyboardInput( );
        GetMouseInput( );
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

