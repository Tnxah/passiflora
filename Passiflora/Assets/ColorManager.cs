using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    private Color player;
    [SerializeField]
    private Color background;
    [SerializeField]
    private Color panels;
    [SerializeField]
    private List<Color> obstacles;

    private void Awake()
    {
        Settings.playerColor = player;
        Settings.obstacleColor = obstacles;
        Settings.panelsColor = panels;
        Settings.backgroundColor = background;
    }
}
