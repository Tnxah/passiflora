using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector3 touchPosition;
    Vector2 direction;

    bool detecting = false;
    public Touch currentTouch;
    //List<Touch> currentTouch = new List<Touch>();

    public bool free = true;
    

    public static List<Touch> touches;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (touches == null)
        {
            print("Init " + gameObject.name);
            touches = new List<Touch>();
        }
    }



    void Update()
    {


        int i = 0;
        while (free && i < Input.touchCount)
        {
            
            currentTouch = Input.GetTouch(i);
            //if (touches.Contains(t))
            //{
            //    i++;
            //    continue;
            //}
             if (currentTouch.phase == TouchPhase.Began)
            {
                free = false;
                touches.Add(currentTouch);
                //currentTouch = Input.GetTouch(i);
                //print(t.fingerId + " " + gameObject.name);
                //print(gameObject.name + " " + currentTouch.phase);
                
            }

            i++;
        }

        if (currentTouch.phase == TouchPhase.Ended)
        {
            print("ended" + gameObject.name);
            touches.Remove(currentTouch);
            free = true;
        }


        //print(gameObject.name);

        //touchPosition = Camera.main.ScreenToWorldPoint(currentTouch.position);

        //RaycastHit2D hit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 5000);
        //Debug.DrawRay(touchPosition, Camera.main.transform.forward * 50, Color.green, 10);


        ////////////--------------------------------------------------

        //if (hit.collider != null && hit.collider.GetComponent<FingerControl>() == this)
        //{
        //    detecting = true;
        //}

        //if (detecting)
        //{
        //    direction = (touchPosition - transform.position);
        //    rb.velocity = direction * 50f;
        //}


        //if (currentTouch.phase == TouchPhase.Ended)
        //{
        //    rb.velocity = Vector3.zero;
        //    detecting = false;
        //    print("End pressing");
        //}

    }



    //private void Update()
    //{
    //    int i = 0;
    //    while (i < Input.touchCount)
    //    {
    //        Touch t = Input.GetTouch(i);
    //        if (touches.Contains(t))
    //        {
    //            i++;
    //            continue;
    //        }
    //        if (t.phase == TouchPhase.Began)
    //        {
    //            touches.Add(t);
    //            print(t.fingerId + " " + gameObject.name);
    //        }
    //        if (t.phase == TouchPhase.Ended)
    //        {
    //            touches.Remove(t);
    //        }
    //        i++;
    //    }
    //}



}
