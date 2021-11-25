using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 touchPosition;
    Vector2 direction;

    bool detecting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 5000);
            Debug.DrawRay(touchPosition, Camera.main.transform.forward * 50, Color.green, 10);
            if (hit.collider != null && hit.collider.GetComponent<FingerControl>() != null)
            {
                detecting = true; 
            }
            
            if (detecting)
            {
                direction = (touchPosition - transform.position);
                rb.velocity = direction * 50f;
            }
                

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector3.zero;
                detecting = false;
                print("End pressing");
            }
        }
        

    }

    

}
