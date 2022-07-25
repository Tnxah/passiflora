using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : MonoBehaviour
{

    public static FingerControl instance;

    Vector3 touchPosition;

    public GameObject first;
    public GameObject second;

    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 100);
            Debug.DrawRay(touchPosition, Camera.main.transform.forward * 50, Color.green, 5);

            if (touch.fingerId == 0)
            {

                if (hit && first == null && hit.collider.CompareTag("Player"))
                {
                    first = hit.collider.gameObject;
                }
                if (first)
                {
                    Vector2 direction = (touchPosition - first.transform.position).normalized;
                    first.GetComponent<Rigidbody2D>().velocity = direction * 10000f * Time.fixedDeltaTime;
                }
            }
            if (touch.fingerId == 1)
            {

                if (hit && second == null && hit.collider.CompareTag("Player"))
                {
                    second = hit.collider.gameObject;
                }
                if (second)
                {
                    Vector2 direction = (touchPosition - second.transform.position).normalized;
                    second.GetComponent<Rigidbody2D>().velocity = direction * 10000f * Time.fixedDeltaTime;
                }

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touch.fingerId == 0 && first)
                {
                    first.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    first = null;
                }
                else if (touch.fingerId == 1 && second)
                {
                    second.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    second = null;
                }

            }

        }
    }

    public void StopLights()
    {
        
            first.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            first = null;
       
       
            second.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            second = null;
        
    }

    public bool BouthTouched()
    {
        if (first && second)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 

}