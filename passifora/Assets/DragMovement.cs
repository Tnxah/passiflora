using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour
{

    public GameObject controlledObject;

    public Rigidbody2D rb;
    Vector3 touchPosition;
    Vector2 direction;

    bool detecting = false;

    //List<Touch> currentTouch = new List<Touch>();

    public bool free = true;

    public static List<int> alreadyUsed = new List<int>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            touchPosition = Camera.main.ScreenToWorldPoint(t.position);
            print(touchPosition);
            if (t.phase == TouchPhase.Began && controlledObject == null && !alreadyUsed.Contains(i))
            {
                Debug.Log("touch began");

                

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 100);
                Debug.DrawRay(touchPosition, Camera.main.transform.forward * 50, Color.green, 5);

                if (hit.collider != null && hit.collider.GetComponent<DragMovement>() == this)
                {
                    controlledObject = hit.collider.gameObject;
                    detecting = true;
                    alreadyUsed.Add(i);
                }

            }
            else if (t.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");

                controlledObject = null;
                detecting = false;
                rb.velocity = Vector3.zero;
                alreadyUsed.Remove(i);
            }
            else if (t.phase == TouchPhase.Moved && detecting)
            {
                Debug.Log("touch is moving");
                direction = (touchPosition - transform.position);
                rb.velocity = direction * 50f;
            }
            ++i;
        }


        //print(gameObject.name);

        


        ////////////--------------------------------------------------

        

        //if (detecting)
        //{
        //    
        //}


        //if (currentTouch.phase == TouchPhase.Ended)
        //{
        //    rb.velocity = Vector3.zero;
        //    detecting = false;
        //    print("End pressing");
        //}

    }
}
