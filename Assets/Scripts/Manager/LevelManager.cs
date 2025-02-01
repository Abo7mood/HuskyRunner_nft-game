using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    #region Constrctuer
//General References
    PlayerController player; 
    GameObject cam; 
    GameObject playerObject;
    GameObject BackGround;
    [Header("Map")]
   [SerializeField] GameObject mapPrefab; //this is the real map
    [Space(25)]
    [Header("Input")]
    [SerializeField] KeyCode SceneLoad; //to load the scene 
    GameObject[] mapTransforms; //get all the mapPrefabs
    #endregion
    #region float 
[Header("CameraValues")]
    [SerializeField] float cameraValueX; //camera x position
    [SerializeField] float cameraValueY;//camera t position
    [Header("MapValues")]
    [SerializeField] float timeRepeating; //value to perfect generate
    public float destroyTimeMap; //time to destroy the map after the player left it 

    #endregion
    #region booleans
  [HideInInspector] public bool canGenerate=true;
    #endregion
    #region strings
    const string MAP = "Map";
    #endregion
    private void Awake()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;
            }
        }

    }
    private void Start()
    {
        mapTransforms = GameObject.FindGameObjectsWithTag(MAP);
        Reference();
        InvokeRepeating("GenerateMap", 0.00f, timeRepeating);
    }
    private void Update()
    {
#if UNITY_EDITOR
        InputMapper(SceneLoad);
#endif

     
        MapFollow(playerObject.transform,cam.transform);
    }
    public void GenerateMap() { if (canGenerate) BackGround = Instantiate(mapPrefab, mapTransforms[mapTransforms.Length - 1].transform.position, Quaternion.identity, null); } 
         
    private void MapFollow(Transform Player,Transform Camera)
    {
        mapTransforms = GameObject.FindGameObjectsWithTag("Map");
        // change cam transform to player transform
        cam.transform.position = new Vector3(Player.transform.position.x+cameraValueX, Camera.transform.position.y+ cameraValueY, -10f);
    }
    private void Reference()
    {
       cam = Camera.main.gameObject;
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        playerObject = FindObjectOfType<PlayerController>().gameObject;
      

    }
  //reset the scene
    private void InputMapper(KeyCode SceneLoader)
    {
        if (Input.GetKey(SceneLoader))
            SceneManager.LoadScene(0);
    }

}
