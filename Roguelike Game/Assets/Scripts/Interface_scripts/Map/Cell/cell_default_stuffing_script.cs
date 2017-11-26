using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cell_default_stuffing_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    [Space]
    public GameObject main_frame;
    public RectTransform main_frame_rectTransform;

    [Space]
    [Header("   дополнительный элементы")]
    public GameObject additional_gameObject;
    public cell_additional_stuffing_script additional_object;

    [Space]
    [Header("   уровень клетки")]
    public GameObject level_gameObject;
    public RectTransform level_rectTransform;
    public Text level;

    [Space]

    [Header("   характеристики")]
    public float _width = 0;
    public float _height = 0;
    public float _x = 0;
    public float _y = 0;

    [Space]

    public int object_num = 0;

    private cell_image_script object_main;

    [Space]

    public float zoom;
    public float size_width;
    public float size_height;


    // Use this for initialization
    void Start()
    {

    }

    public void Creat_cell(Cell_stuffing_Blank object_info)
    {
        if (object_num == 0)
        {
            object_num = 1;
            Creat_object(object_info);
        }
        else
        {
            Redisplay_object(object_info);
        }
    }

    private void Creat_object(Cell_stuffing_Blank object_info)
    {
        GameObject new_object_go = Instantiate(Resources.Load("Prefab/Map/Cell/cell_image", typeof(GameObject))) as GameObject;
        new_object_go.transform.SetParent(main_frame.transform);
        object_main = new_object_go.GetComponent<cell_image_script>();

        Display_level(object_info.level);

        if (object_info.id != 0)
        {
            Display_image(object_info.image_id);
            object_main._name = "" + object_info.editor_id;
            object_main.name = object_info.editor_id;
        }
        else
        {
            object_main._image.sprite = null;

            object_main.Object_resize(0, 0);

            object_main.Object_position(0, 0);

            object_main.main_object.SetActive(false);
        }

        if (object_info.object_active != 0)
        {
            additional_object.Display_image(object_info.object_image_id);
            additional_gameObject.SetActive(true);
        }
        else
        {
            additional_gameObject.SetActive(false);
        }
    }

    private void Redisplay_object(Cell_stuffing_Blank object_info)
    {
        Display_level(object_info.level);

        if (object_info.image_id == 0)
        {
            object_main.main_object.SetActive(false);
        }
        else
        {
            if (object_main.main_object.activeSelf == false)
            {
                object_main.main_object.SetActive(true);
            }

            if (object_info.id != 0)
            {
                object_main._name = "" + object_info.editor_id;
                object_main.name = object_info.editor_id;
                Display_image(object_info.image_id);
            }
            else
            {
                object_main._image.sprite = null;

                object_main.Object_resize(0, 0);

                object_main.Object_position(0, 0);

                object_main.main_object.SetActive(false);
            }
        }

        if (object_info.object_active != 0)
        {
            additional_object.Display_image(object_info.object_image_id);
            additional_gameObject.SetActive(true);
        }
        else
        {
            additional_gameObject.SetActive(false);
        }
    }

    private void Display_image(int new_image_id)
    {
        if (new_image_id != 0)
        {
            Image_Data image_info = Main.db_data.image_data[new_image_id];

            object_main._image.sprite = Main.Image_list[image_info.image_name];

            if ((main_rectTransform.sizeDelta.x < image_info._width) || (main_rectTransform.sizeDelta.y < image_info._height))
            {
                if (image_info._width <= image_info._height)
                {
                    size_width = image_info._height;
                    size_height = main_rectTransform.sizeDelta.y;
                }
                else
                {
                    size_width = image_info._width;
                    size_height = main_rectTransform.sizeDelta.x;
                }

                zoom = (size_width - size_height) / size_width;


                size_width = image_info._width - image_info._width * zoom;
                size_height = image_info._height - image_info._height * zoom;

                object_main.Object_resize(size_width, size_height);
            }
            else
            {
                object_main.Object_resize(image_info._width, image_info._height);
            }

            object_main.Object_position(0, 0);
        }
        else
        {
            object_main._image.sprite = null;
            object_main.main_object.SetActive(false);
        }
    }
    
    public void Display_level(int level_id)
    {
        if (level_id != 0)
        {
            level_gameObject.SetActive(true);

            level.text = "" + level_id;

            if (level.preferredWidth > level_rectTransform.sizeDelta.x)
            {
                level_rectTransform.sizeDelta = new Vector2(main_rectTransform.sizeDelta.x, level_rectTransform.sizeDelta.y);
            }
            else
            {
                level_rectTransform.sizeDelta = new Vector2(level.preferredWidth, level_rectTransform.sizeDelta.y);
            }
        }
        else
        {
            level_gameObject.SetActive(false);
        }
    }
}
