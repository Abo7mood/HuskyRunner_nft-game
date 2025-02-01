#if UNITY_STANDALONE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class VoiceInput : MonoBehaviour
{
   
    [SerializeField] AudioMixerGroup MixerGroupMaster, MixerGroupMicrophone;
    [Space(15)]
    [SerializeField] int playerY; // the player velocity in genereal
    [Space(15)]
    [SerializeField] float loudness = 0; // the sound amount
    [SerializeField] int loudnessMax; // the sound to make the event
    [Space(15)]
    [SerializeField] float _sensitivity = 100; // sensitvity
    [SerializeField] AudioSource _audio; // audio source

    [Space(35)]
   [SerializeField] Rigidbody2D rb;
    [SerializeField] GroundCheck ground;
    [Space(15)]
    [SerializeField] Image micSensitivityVisuality;
    private void Start()
    {
        Getter();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0))
        {
            _audio.Play();
        }
    }
    private void Update()
    {
        if (loudness > 0)
                    _audio.outputAudioMixerGroup = MixerGroupMicrophone;
        else
        _audio.outputAudioMixerGroup = MixerGroupMaster;
        if (isScene(0))
        {
            micSensitivityVisuality.fillAmount = loudness;
            GetAverageSensetivity();
        }
       
        loudness = GetAveragedVolume() * _sensitivity;
        

        if (isScene(0)) return;
        if (PlayerController.instance.canMove)
            InputCall();
    }
    private void FixedUpdate()
    {
        if(isScene(1))
        jump();
    }
    void jump()
    {

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (PlayerController.instance.fallMulitplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !isTalking())
            rb.velocity += Vector2.up * Physics2D.gravity.y * (PlayerController.instance.lowJumpMulitplier - 1) * Time.deltaTime;
    }
    private void InputCall()
    {
        if (isTalking() && ground.isGrounded == true)
        {
            SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.coinsSoundPrefab, SoundManager.instance.audiosGameObject, 0);
            rb.velocity = new Vector2(rb.velocity.x, PlayerController.instance.jumpForce);
        }

    }
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
    float GetAverageSensetivity()
    {
        micSensitivityVisuality.fillAmount = loudness/70;
        return micSensitivityVisuality.fillAmount;
    }
    public void SetSensitivity(float sensitivity) => _sensitivity = sensitivity;
    public void SetMaxLoudness(float maxLoudness) => loudnessMax = (int)maxLoudness;

    public void Setter()
    {
        PlayerPrefs.SetFloat("Sensitivity", _sensitivity);
        PlayerPrefs.SetInt("loudnessMax", loudnessMax);

    }
    public void Getter()
    {
        loudnessMax = PlayerPrefs.GetInt("loudnessMax");
        _sensitivity = PlayerPrefs.GetFloat("Sensitivity");

    }

    bool isTalking() => loudness > loudnessMax;
    bool isScene(int index) => SceneManager.GetActiveScene().buildIndex == index;

}
#endif