using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int dashDirection;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashDirection == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    dashDirection = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    dashDirection = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                dashDirection = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (dashDirection == 1)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else if (dashDirection == 2)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }
}
