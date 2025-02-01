using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerController : MonoBehaviour
{

    #region Constrcuter
   public static PlayerController instance;
    [Header("Constructer")]
    //Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    GroundCheck ground;
#if UNITY_STANDALONE

    VoiceInput _voiceInput;
#endif
    PcInput _pcInput;

    Transform transformsFollowers;
    GameObject groundObject;
   [SerializeField] GameObject groundDetection;
    [SerializeField] GameObject[] healthObjects;
    [Header("MenuManager")]
    public GameObject diePanel;
    public GameObject scorePanel;
    public GameObject healthPanel;
    public GameObject coinsPanel;
#endregion
#region Floats
    [Header("Movement")]
    public float maxVelocity = 10;
    public float speed = 10;
    public float jumpForce;
    public float jumpEnemyForce;
    public float fallMulitplier;
    public float lowJumpMulitplier;
    [Space(25)]
    [Header("Hit")]
    [SerializeField] float damageCooldown;
    [SerializeField] float damageAnimationTime;
    float old_pos;
    int layer_mask;

#endregion
#region vectors&colors
    Color playerColor;
   [SerializeField] Color damageColor;
    Vector2 test;
#endregion
#region Booleans
    bool isDamage;
    bool isEnemyDie;
   public bool canMove=true;
    private bool isDie;
#endregion
#region ints
    int currenthealth = 3;  
#endregion
#region Strings
    //Animations :
    const string JUMPANIM = "IsJumping", FALLANIM = "IsFalling", HIT="Hit";
    //Tags
    const string COINTAG = "Coin", HEADTAG = "Head", BODYTAG = "Body", DIETAG = "Die";
    //Refrences
    const string TRANSFORMFOLLOW = "TransformsFollowers", GROUNDREF = "Ground";
    //Enemies 
    const string GREENENEMY = "GreenEnemy", BIRDENEMY = "BirdEnemy";
#endregion
#region Others
    [Header("Other")]
    [SerializeField] health_Sys healthPhase;
    enum health_Sys   {
        P1, P2, P3, P4
    }
    event EventHandler die;
#endregion
    private void Awake()=>References();
    private void Start()
    {
        playerColor = sprite.color;
        sprite.color=playerColor;
        old_pos = transform.position.x;
        healthPhase = health_Sys.P4;
        die += DieSound;
        #if UNITY_STANDALONE

        if (SaveManager.instance.isMic)
            _pcInput.enabled = false;
        else
            _voiceInput.enabled = false;
#endif
    }

    private void Update()=> Voider();


    private void Voider()
    {
        AnimationVoid();
        MovePlatforms();
        HealthFunc();
        Fading();
       
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }
    private void References()
    {
        instance = this;
           rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        ground = GetComponentInChildren<GroundCheck>();
        transformsFollowers = GameObject.Find(TRANSFORMFOLLOW).transform;
        groundObject = GameObject.Find(TRANSFORMFOLLOW).transform.Find(GROUNDREF).gameObject;
        #if UNITY_STANDALONE
        _voiceInput = GetComponentInChildren<VoiceInput>();
#endif
        _pcInput = GetComponentInChildren<PcInput>();

    }
    private void Fading()
    {
        if(isDamage)
        sprite.color = Color.Lerp(sprite.color, damageColor, damageAnimationTime * Time.deltaTime);
        else
            sprite.color = Color.Lerp(sprite.color, playerColor, damageAnimationTime * Time.deltaTime);
    }
   
   
    private void PlayerMovement()
    {
        //Move
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        if (rb.velocity.magnitude > maxVelocity)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        OnDrawGizmos();
        layer_mask= LayerMask.GetMask("Die");
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.transform.position, Vector2.down,7,layer_mask);
        if (groundInfo.collider)
        {
            groundObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
            groundObject.GetComponent<BoxCollider2D>().enabled = true;

    }
    private void DieSound(object sender, EventArgs e) {SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.hitSoundPrefab, SoundManager.instance.audiosGameObject, 2);
        
        die -= DieSound;
    } 

    private void HealthFunc()
    {

        switch (currenthealth)
        {
            case (int)health_Sys.P4:
                for (int i = 0; i < healthObjects.Length; i++)
                {
                    healthObjects[i].SetActive(true);

                }
                break;
            case (int)health_Sys.P3:
                for (int i = 0; i < healthObjects.Length; i++)
                {
                    healthObjects[i].SetActive(true);
                    healthObjects[2].SetActive(false);
                }
                break;
            case (int)health_Sys.P2:
                for (int i = 0; i < healthObjects.Length; i++)
                {
                    healthObjects[i].SetActive(true);
                    healthObjects[2].SetActive(false);
                    healthObjects[1].SetActive(false);
                }
                break;
            case (int)health_Sys.P1:
                for (int i = 0; i < healthObjects.Length; i++)
                {
                    healthObjects[i].SetActive(false);
                }
                break;
            default:
                Debug.LogError("Fix it");
                break;
        }


    }
    private void Hit()
    {
        if (currenthealth > 0)
        {
            currenthealth--;
            if (currenthealth <= 0)
            {
                canMove = false;
                Die();
            }
            else
            SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.hitSoundPrefab, SoundManager.instance.audiosGameObject, 1);        
        }
        HealthFunc();
    }
    private void Die()
    {
       
        isDie = true;
        anim.SetTrigger("Die");
        speed = 0;
        diePanel.SetActive(true);
        healthPanel.SetActive(false);
        coinsPanel.SetActive(false);
        LevelManager.instance.canGenerate = false;
        SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.hitSoundPrefab, SoundManager.instance.audiosGameObject, 1);
        PlayFabManager.Instance.UploadHighScore(GameManager.instance.scoreAmount);
    }
    private void AnimationVoid()
    {
        //Jump
        if (rb.velocity.y <= 0 && rb.velocity.y > -0.5f)
        {
            anim.SetBool(JUMPANIM, false);
            anim.SetBool(FALLANIM, false);
        }
        if (rb.velocity.y > 0.01f)
        {
            anim.SetBool(JUMPANIM, true);
        }

        if (rb.velocity.y < -1.8)
        {
            anim.SetBool(JUMPANIM, false);
            anim.SetBool(FALLANIM, true);
        }
        if (rb.velocity.y >= 0)
            anim.SetBool(FALLANIM, false);
    }
    private void MovePlatforms()=>transformsFollowers.transform.position = new Vector3(transform.position.x, transformsFollowers.transform.position.y, -10);
    private void OnDrawGizmos() 
    {
        test = new Vector2(groundDetection.transform.position.x, -7);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 5);
        Debug.DrawLine(groundDetection.transform.position, test, Color.green, .1f);
    } 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(COINTAG))
        {
            SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.coinsSoundPrefab, SoundManager.instance.audiosGameObject, 4);

            GameManager.instance.coinsAmount++;

            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag(HEADTAG) && !ground.isGrounded && rb.velocity.y < 0)
        {
            SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.coinsSoundPrefab, SoundManager.instance.audiosGameObject, 3);

            rb.velocity = new Vector2(rb.velocity.x, jumpEnemyForce);

            Destroy(collision.transform.parent.gameObject);

            isEnemyDie = true;

            StartCoroutine(EnemyTag());

            if (collision.gameObject.transform.parent.name == BIRDENEMY)
                GameManager.instance.coinsAmount += GameManager.instance.birdEnemyBonus;
            else if (collision.gameObject.transform.parent.name == GREENENEMY)
                GameManager.instance.coinsAmount += GameManager.instance.greenEnemyBonus;

        }
        else if (collision.CompareTag(BODYTAG) && collision.gameObject != null && isEnemyDie == false&&!isDamage&& rb.velocity.y >= 0)
        {
            //sound
            Hit();
            StartCoroutine(Damage1());
        }
        else if (collision.CompareTag(DIETAG))
        {
            die?.Invoke(this, EventArgs.Empty);
            Invoke(nameof(Die), 1.2f);
        }
    }
   

    IEnumerator EnemyTag()
    {
        yield return new WaitForSeconds(0.01f);
        isEnemyDie = false;
    }
    
    IEnumerator Damage1()
    {
        isDamage = true;
        yield return new WaitForSeconds(damageCooldown);
        isDamage = false;
    }

}
