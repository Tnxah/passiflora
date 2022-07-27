using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle, IActivatable
{
    public void Activate()
    {
        GetComponent<Animator>().Play("Wake");
    }
}
