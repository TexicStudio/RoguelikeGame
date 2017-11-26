using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_row_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    [Space]
    [Header("   слой для частей клетки")]
    public GameObject cell_frame;
    public GameObject base_frame;
    public GameObject decoration_frame;
    public GameObject stuffing_frame;
    public GameObject default_stuffing_frame;
    public GameObject editor_frame;


    // Use this for initialization
    void Start () {
		
	}

    public void Destroy_object()
    {
        Destroy(main_object);
    }
}
