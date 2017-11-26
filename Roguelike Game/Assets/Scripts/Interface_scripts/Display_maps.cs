using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Display_maps : MonoBehaviour
{
    public int map_width; // высота карты
    public int map_height; // ширина карты

    public int display_min_i; // начальное значение по оси Х у карты которое отображаеться . для редактора
    public int display_max_i; // конечное значение по оси Х у карты которое отображаеться . для редактора

    public int display_min_j; // начальное значение по оси У у карты которое отображаеться . для редактора
    public int display_max_j; // конечное значение по оси У у карты которое отображаеться . для редактора

    public GameObject mask_map;
    public GameObject map;

    public Dictionary<string, GameObject> cell = new Dictionary<string, GameObject>();
    // Use this for initialization
    void Start ()
    {

    }
}
