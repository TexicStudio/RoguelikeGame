using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cell_additional_stuffing_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    [Space]
    [Header("   изображение")]
    public GameObject object_image_gameObject;
    public Image object_image;
    public RectTransform object_image_rectTransform;

    [Space]

    [Header("   характеристики")]
    public int object_num = 0;

    [Space]
    public float zoom;
    public float size_width;
    public float size_height;

    // Use this for initialization
    void Start () {
		
	}

    public void Display_image(int new_image_id)
    {
        if (new_image_id != 0)
        {
            object_image_gameObject.SetActive(true);

            Image_Data image_info = Main.db_data.image_data[new_image_id];

            object_image.sprite = Main.Image_list[image_info.image_name];

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

                object_image_rectTransform.sizeDelta = new Vector2(size_width, size_height);
            }
            else
            {
                object_image_rectTransform.sizeDelta = new Vector2(image_info._width, image_info._height);
            }
        }
        else
        {
            object_image_gameObject.SetActive(false);
        }
    }
}
