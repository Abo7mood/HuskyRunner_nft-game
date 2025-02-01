using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcInput : MonoBehaviour
{
    [SerializeField] GroundCheck ground;
    [SerializeField] Rigidbody2D rb;
    private void Update()
    { if(PlayerController.instance.canMove)        
        InputCall();
    }
    private void FixedUpdate()
    {
        jump();
    }
    private void InputCall()
    {

        if (Input.GetKeyDown(KeyCode.Space) ||Input.GetMouseButtonDown(0))
        {
            if (ground.isGrounded)
            {
                SoundManager.instance.SoundPlayer(SoundManager.instance.soundObjecter.coinsSoundPrefab, SoundManager.instance.audiosGameObject, 0);
                rb.velocity = new Vector2(rb.velocity.x, PlayerController.instance.jumpForce);
            }
            
        }


    }
    void jump()
    {

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (PlayerController.instance.fallMulitplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0)
        {
            if(!Input.GetKey(KeyCode.Space)&&!Input.GetMouseButton(0))
                rb.velocity += Vector2.up * Physics2D.gravity.y * (PlayerController.instance.lowJumpMulitplier - 1) * Time.deltaTime;
        }
    }
}
