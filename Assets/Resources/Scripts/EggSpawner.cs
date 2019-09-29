using UnityEngine;
using UnityEngine.UI;

public class EggSpawner : MonoBehaviour {

    [Header( "Prefabs" )]
    [SerializeField]
    private GameObject eggPrefab;
    [Header( "Text output" )]
    public Text textMesh = null;
    public Text fireRateText = null;
    [Header( "Sliders" )]
    public Slider fireRateSelector = null;
    public Slider spawningBar = null;
    public Slider maxSpawnSelector = null;

    private float _maxEggs;
    private float _cooldown;
    private float _timeSinceLastEggSpawned = 0;
    private float _eggCount = 0;

    private void Start( ) {
        if( eggPrefab == null ) {
            eggPrefab = Resources.Load( "Prefabs/Egg" ) as GameObject ;
            if(eggPrefab == null ) {
                Debug.Log( "Failed to Load egg prefab" );
            } else {
                Debug.Log( "Loaded prefab" );
            }
        }

        Text[] sceneTexts = Resources.FindObjectsOfTypeAll<Text>( );
        foreach( Text text in sceneTexts ) {
            if( text.gameObject.name == "Text" ) {
                textMesh = text;
            } else if( text.gameObject.name == "FireRateText" ) {
                fireRateText = text;
            }
        }
        Slider[] sceneSliders = Resources.FindObjectsOfTypeAll<Slider>( );
        foreach( Slider slider in sceneSliders ) {
            if( slider.gameObject.name == "FireRateSelector" ) {
                fireRateSelector = slider;
            } else if( slider.gameObject.name == "SpawningBar" ) {
                spawningBar = slider;
            } else if( slider.gameObject.name == "MaxSpawnSelector" ) {
                maxSpawnSelector = slider;
            }
        }

        fireRateSelector.maxValue = 1f;
        fireRateSelector.minValue = 0.001f;
        fireRateSelector.value = 0.5f;
        _cooldown = fireRateSelector.value;

        maxSpawnSelector.wholeNumbers = true;
        maxSpawnSelector.maxValue = 100;
        maxSpawnSelector.minValue = 1;
        maxSpawnSelector.value = 60;
        _maxEggs = maxSpawnSelector.value;
    }
    // Update is called once per frame
    void Update( ) {
        cullEggs( );
        _cooldown = fireRateSelector.value;
        _maxEggs = maxSpawnSelector.value;

        textMesh.text = "Number of Eggs: " + _eggCount;
        fireRateText.text = ( "Max Eggs: " + _maxEggs );

        spawningBar.value = 1.1f - ( _timeSinceLastEggSpawned / _cooldown );
        _timeSinceLastEggSpawned += Time.deltaTime;
    }
    public void SpawnEggs( Transform l_Transform ) {
        if( ( _timeSinceLastEggSpawned >= _cooldown ) && ( _eggCount < _maxEggs ) ) {
            _ = Instantiate( eggPrefab,
                        l_Transform.position,
                        l_Transform.rotation );
            _timeSinceLastEggSpawned = 0;
            _eggCount++;
        }

    }
    private void cullEggs( ) {
        EggBehavior[] eggs = FindObjectsOfType<EggBehavior>( );
        for( int i = 0; i < eggs.Length; i++ ) {
            EggBehavior egg = eggs[ i ];
            if( egg.CheckLife( ) ) {
                _eggCount--;
            }
        }
    }
}
