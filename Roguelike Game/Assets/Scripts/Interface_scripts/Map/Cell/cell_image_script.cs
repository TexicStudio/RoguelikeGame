using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cell_image_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;
    public Image _image;

    [Space]

    [Header("   характеристики")]
    public float _width = 0;
    public float _height = 0;
    public float _x = 0;
    public float _y = 0;

    [Space]

    [Header("   данные")]
    public string _name;
    public int address_x = 0;
    public int address_y = 0;

    // Use this for initialization
    void Start () {
		
	}

    public void Object_resize(float _w, float _h)
    {
        _width = _w;
        _height = _h;
        main_rectTransform.sizeDelta = new Vector2(_width, _height);
    }

    public void Object_position(float pos_x, float pos_y)
    {
        _x = pos_x;
        _y = pos_y;
        main_rectTransform.localPosition = new Vector3(_x, _y, main_rectTransform.position.z);
    }

    public void Destroy_object()
    {
        Destroy(main_object);
    }
}
