using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell_base_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    [Space]

    [Header("   характеристики")]
    public float _x = 0;
    public float _y = 0;

    [Space]
    
    public int object_num = 0;

    private cell_image_script object_main;

    // Use this for initialization
    void Start () {
		
	}

    public void Creat_cell(int object_id)
    {
        if (object_num == 0)
        {
            object_num = 1;
            Creat_object(object_id);
        }
        else
        {
            Redisplay_object(object_id);
        }
    }

    private void Creat_object(int object_id)
    {
        GameObject new_object_go = Instantiate(Resources.Load("Prefab/Map/Cell/cell_image", typeof(GameObject))) as GameObject;
        new_object_go.transform.SetParent(main_object.transform);
        object_main = new_object_go.GetComponent<cell_image_script>();

        Coverage_Data base_info = Main.db_data.coverage_data[object_id];

        if (base_info.image_id != 0)
        {
            Image_Data image_info = Main.db_data.image_data[base_info.image_id];

            object_main._image.sprite = Main.Image_list[image_info.image_name];
            object_main._name = "" + base_info.coverage_name;
            object_main.name = base_info.coverage_name;

            object_main.Object_resize(image_info._width, image_info._height);

            object_main.Object_position(0, 0);
        }
        else
        {
            object_main._image.sprite = null;

            object_main.Object_resize(0, 0);

            object_main.Object_position(0, 0);

            object_main.main_object.SetActive(false);
        }
    }

    private void Redisplay_object(int object_id)
    {
        Coverage_Data base_info = Main.db_data.coverage_data[object_id];

        if (base_info.image_id == 0)
        {
            object_main.main_object.SetActive(false);
        }
        else
        {
            if (object_main.main_object.activeSelf == false)
            {
                object_main.main_object.SetActive(true);
            }


            Image_Data image_info = Main.db_data.image_data[base_info.image_id];

            object_main._image.sprite = Main.Image_list[image_info.image_name];
            object_main._name = "" + base_info.coverage_name;
            object_main.name = base_info.coverage_name;

            object_main.Object_resize(image_info._width, image_info._height);

            object_main.Object_position(0, 0);
        }
    }

    public void Destroy_object()
    {
        Destroy(main_object);
    }
}
