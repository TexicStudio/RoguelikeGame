using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_row_stuffing_layer_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    // Use this for initialization
    void Start () {
		
	}

    public void Destroy_object()
    {
        Destroy(main_object);
    }
}
