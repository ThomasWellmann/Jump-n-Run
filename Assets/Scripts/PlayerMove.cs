using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] Animator playerAnimator;
    Vector2 newPosition;

    void Start()
    {
        Debug.Log("Start Method in Player Move is being called");
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("attacking", false);
        playerAnimator.SetBool("running", false);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("running", true);
            playerSpriteRenderer.flipX = false;
            newPosition = transform.position;
            newPosition.x += speed * Time.deltaTime;
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerAnimator.SetBool("running", true);
            playerSpriteRenderer.flipX = true;
            newPosition = transform.position;
            newPosition.x -= speed * Time.deltaTime;
            transform.position = newPosition;
        }

        if (Input.GetMouseButton(2))
        {
            playerAnimator.SetBool("attacking", true);
        }
    }

}
