using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kid_movement : MonoBehaviour
{

    Vector3 characterScale;
    float characterScaleX;

    private Rigidbody2D rb;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;

        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    transform.Translate(Input.GetAxis("Horizontal") * 15f * Time.deltaTime, 0f, 0f);

    if(Input.GetAxis("Horizontal") < 0)
        characterScale.x = -characterScaleX;

    if(Input.GetAxis("Horizontal") > 0)
        characterScale.x = characterScaleX;

    transform.localScale = characterScale;

    if(Input.GetKeyDown(KeyCode.UpArrow))
        jumpAction();
    }
    private void jumpAction(){
        rb.AddForce(transform.up * 100f);
        ani.SetTrigger("jump");
    }
}
