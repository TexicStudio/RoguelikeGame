using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class maps_container_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public Transform main_transform;


    [Space]
    public GameObject map_frame;
    

    [Space]
    [Header("   данные")]
    public int cell_row_max; // y
    public int cell_column_max; // x

    [Space]
    public float _width_max;
    public float _height_max;

    [Space]
    public float _width = 0;
    public float _height = 0;

    [Space]
    public float cell_size = 0;

    private float map_name_size;

    // Use this for initialization
    void Start () {
	
	}
    
    public void Display_frame_resize(int cell_column, int cell_row)
    {
        _width = cell_column * cell_size;
        _height = cell_row * cell_size;

        if(_width_max < _width)
        {
            _width = _width_max;
        }

        if (_height_max < _height)
        {
            _height = _height_max;
        }
    }
}
