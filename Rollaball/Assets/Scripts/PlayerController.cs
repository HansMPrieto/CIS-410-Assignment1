using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    //public float jumpForce = 5;
    //private float maxJumpCount = 2;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    //private bool onGround = true;
    //private int count;
    //private float jumpCount;
    private float movementX;
    private float movementY;

    private float jumpCount;
    private float isJump = 0;
    private bool onGround = true;
    private int count;
    public float jumpForce = 5;
    private float maxJumpCount = 2;

    void DoJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        jumpCount = jumpCount + 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        jumpCount = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue val)
    {
        Debug.Log(val.Get<float>());
        isJump = val.Get<float>();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        if(isJump == 1 && onGround == true)
        {
            DoJump();
            onGround = false;
        }

        else if((isJump == 1) && (jumpCount == 1) && (jumpCount < maxJumpCount))
        {
            DoJump();
        }
        isJump = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

}
