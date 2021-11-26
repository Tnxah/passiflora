using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingShit : MonoBehaviour
{

    Vector3 touchPosition;

    public GameObject first;
    public GameObject second;
    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward, 100);
            Debug.DrawRay(touchPosition, Camera.main.transform.forward * 50, Color.green, 5);





            if (touch.fingerId == 0)
            {

                if (hit && first == null && hit.collider.gameObject.GetComponent<GameObject>() != null)
                {
                    first = hit.collider.gameObject;
                }
                if (first)
                {
                    Vector2 direction = (touchPosition - first.transform.position);
                    first.GetComponent<Rigidbody2D>().velocity = direction * 1000f * Time.deltaTime;
                }



            }
            else if (touch.fingerId == 1)
            {

                if (hit && second == null && hit.collider.gameObject.GetComponent<GameObject>() != null)
                {
                    second = hit.collider.gameObject;
                }
                if (second)
                {
                    Vector2 direction = (touchPosition - second.transform.position);
                    second.GetComponent<Rigidbody2D>().velocity = direction * 1000f * Time.deltaTime;
                }



            }
            if (touch.phase == TouchPhase.Ended)
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

}