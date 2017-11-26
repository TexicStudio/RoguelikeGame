using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class map_cell_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public Transform main_transform;
    public map_cell_script main_script;


    [Space]
    [Header("   основа клетки")]
    public GameObject cell_base_go;
    public SpriteRenderer cell_base;


    [Space]
    [Header("   декор клетки")]
    public GameObject cell_decoration_go;
    public Transform cell_decoration_transform;
    public SpriteRenderer cell_decoration;


    [Space]
    [Header("   начинка клетки")]
    public GameObject cell_stuffing_go;
    public Transform cell_stuffing_transform;
    public SpriteRenderer cell_stuffing;

    [Space]
    [Header("   уровень начинка клетки")]
    public GameObject cell_stuffing_level_go;
    public SpriteRenderer cell_stuffing_level;

    [Header("   дополнительный элементы начинки клетки")]
    public GameObject cell_stuffing_additional_gameObject;
    public Transform cell_stuffing_additional_background_transform;
    public SpriteRenderer cell_stuffing_additional_background;
    public Transform cell_stuffing_additional_transform;
    public SpriteRenderer cell_stuffing_additional;


    [Space]
    [Header("   по умолчанию начинка клетки")]
    public GameObject cell_default_stuffing_go;
    public Transform cell_default_stuffing_transform;
    public SpriteRenderer cell_default_stuffing;

    [Space]
    [Header("   уровень по умолчанию начинка клетки")]
    public GameObject cell_default_stuffing_level_go;
    public SpriteRenderer cell_default_stuffing_level;

    [Header("   дополнительный элементы начинки клетки")]
    public GameObject cell_default_stuffing_additional_gameObject;
    public Transform cell_default_stuffing_additional_background_transform;
    public SpriteRenderer cell_default_stuffing_additional_background;
    public Transform cell_default_stuffing_additional_transform;
    public SpriteRenderer cell_default_stuffing_additional;



    [Space]
    [Header("   данные")]
    public int _x = 0;
    public int _y = 0;

    [Space]
    public string cell_address;

    [Space]
    public bool create_cell = false;
    public bool delete_cell = false;

    //public Cell_info_Blank cell_info;

    public map_object_script map_frame;

    public bool new_editor = false;

    [Space]
    [Header("   дополнительный элемент макс размер")]
    public float stuffing_additional_width;
    public float stuffing_additional_height;

    [Space]
    [Header("   элемент по умолчанию макс размер")]
    public float default_stuffing_width;
    public float default_stuffing_height;
    [Header("   элемент по умолчанию дополнительный элементы макс размер")]
    public float default_stuffing_additional_width;
    public float default_stuffing_additional_height;

    [Space]
    private float image_scale;

    // Use this for initialization
    void Start ()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       //Debug.Log(cell_address + "     ТРИГЕР enter :     " + collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       //Debug.Log(cell_address + "     ТРИГЕР exit :     " + collision.name);
    }


    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        Hide_info();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Show_info();
    }
    */



    public void Display_cell(Cell_info_Blank temp_cell_info)
    {
        //Debug.Log("map_cell_script - " + temp_cell_info.address);

        Display_base(temp_cell_info.cell_base_id);

        Display_decoration(temp_cell_info.cell_decoration_id);

        Display_stuffing(temp_cell_info.cell_stuffing);

        Display_additional_stuffing(temp_cell_info.cell_stuffing);

        Display_default_bestiary(temp_cell_info.default_bestiary);

        Display_default_bestiary_additional_stuffing(temp_cell_info.default_bestiary);

        cell_address = "" + temp_cell_info.address;

        main_object.name = "" + temp_cell_info.id + " " + temp_cell_info.address;

        temp_cell_info = null;
    }


    public void Display_temp_cell()
    {
        Display_base(1);
    }



    #region Coverage // Base            основа клетки
    public void Display_base(int object_id)
    {
        Coverage_Data base_info = Main.db_data.coverage_data[object_id];

        //Debug.Log("Base_id = " + object_id);

        if (base_info.image_id != 0)
        {
            if (cell_base_go.activeSelf == false)
            {
                cell_base_go.SetActive(true);
            }

            Image_Data image_info = Main.db_data.image_data[base_info.image_id];

            cell_base.sprite = Main.Image_list[image_info.image_name];
            cell_base_go.name = "" + base_info.coverage_name;
        }
        else
        {
            cell_base.sprite = null;

            //cell_base_go.SetActive(false);
        }
    }
    #endregion




    #region Decoration          декор клетки
    public void Display_decoration(int object_id)
    {
        if (object_id != 0)
        {
            if (cell_decoration_go.activeSelf == false)
            {
                cell_decoration_go.SetActive(true);
            }

            Cell_decoration_Data decoration_info = Main.db_data.cell_decoration[object_id];

            Image_Data image_info = Main.db_data.image_data[decoration_info.image_id];

            cell_decoration.sprite = Main.Image_list[image_info.image_name];
            cell_decoration_go.name = "" + decoration_info.decoration_name;
        }
        else
        {
            cell_decoration.sprite = null;

            cell_decoration_go.SetActive(false);
        }
    }
    #endregion




    # region Stuffing           начинка клетки
    public void Display_stuffing(Cell_stuffing_Blank object_info)
    {
        if (object_info.id != 0)
        {
            if (cell_stuffing_go.activeSelf == false)
            {
                cell_stuffing_go.SetActive(true);
            }

            Image_Data image_info = Main.db_data.image_data[object_info.image_id];

            cell_stuffing.sprite = Main.Image_list[image_info.image_name];
            cell_stuffing_go.name = "" + object_info.editor_type + " _ " + object_info.editor_id + " _ " + image_info.image_name;
        }
        else
        {
            cell_stuffing.sprite = null;

            cell_stuffing_go.SetActive(false);

            Display_additional_stuffing(object_info);
        }

        Display_stuffing_level(object_info.level);
    }
    #endregion


    # region Stuffing LEVEL           уровень клетки
    public void Display_stuffing_level(int level_num)
    {
        if (level_num > 0)
        {
            if (cell_stuffing_level_go.activeSelf == false)
            {
                cell_stuffing_level_go.SetActive(true);
            }

            cell_stuffing_level.sprite = Main.level_image_list[level_num];
            cell_stuffing_go.name = "" + level_num;
        }
        else
        {
            cell_stuffing_level.sprite = null;

            cell_stuffing_level_go.SetActive(false);
        }
    }
    #endregion


    #region Additional Stuffing           дополнительный элементы начинки клетки
    public void Display_additional_stuffing(Cell_stuffing_Blank object_info)
    {
        if (object_info.object_active != 0)
        {
            if (cell_stuffing_additional_gameObject.activeSelf == false)
            {
                cell_stuffing_additional_gameObject.SetActive(true);
            }

            Image_Data image_info = Main.db_data.image_data[object_info.object_image_id];

            cell_stuffing_additional.sprite = Main.Image_list[image_info.image_name];
            cell_stuffing_additional_gameObject.name = "" + object_info.object_editor_type + " _ " + object_info.object_editor_id + " _ " + image_info.image_name;

            image_scale = Calculate_image_size(image_info._width, image_info._height, stuffing_additional_width, stuffing_additional_height);

            cell_stuffing_additional_transform.localScale = new Vector3(image_scale, image_scale, 1);
        }
        else
        {
            cell_stuffing_additional.sprite = null;

            cell_stuffing_additional_gameObject.SetActive(false);
        }
    }
    #endregion




    #region Default Bestiary            по умолчанию начинка клетки
    public void Display_default_bestiary(Cell_stuffing_Blank object_info)
    {
        if (object_info.id != 0)
        {
            if (cell_default_stuffing_go.activeSelf == false)
            {
                cell_default_stuffing_go.SetActive(true);
            }

            Image_Data image_info = Main.db_data.image_data[object_info.image_id];

            cell_default_stuffing.sprite = Main.Image_list[image_info.image_name];
            cell_default_stuffing_go.name = "" + object_info.editor_type + " _ " + object_info.editor_id + " _ " + image_info.image_name;

            image_scale = Calculate_image_size(image_info._width, image_info._height, default_stuffing_width, default_stuffing_height);

            cell_default_stuffing_transform.localScale = new Vector3(image_scale, image_scale, 1);
        }
        else
        {
            cell_default_stuffing.sprite = null;

            cell_default_stuffing_go.SetActive(false);

            Display_default_bestiary_additional_stuffing(object_info);
        }

        Display_default_bestiary_level(object_info.level);
    }
    #endregion


    #region Default Bestiary LEVEL           уровень клетки по умолчанию
    public void Display_default_bestiary_level(int level_num)
    {
        if (level_num > 0)
        {
            if (cell_default_stuffing_level_go.activeSelf == false)
            {
                cell_default_stuffing_level_go.SetActive(true);
            }

            cell_default_stuffing_level.sprite = Main.level_image_list[level_num];
            cell_default_stuffing_go.name = "" + level_num;
        }
        else
        {
            cell_default_stuffing_level.sprite = null;

            cell_default_stuffing_level_go.SetActive(false);
        }
    }
    #endregion


    #region Default Bestiary Additional Stuffing          дополнительный элементы у обьекта по умолчанию
    public void Display_default_bestiary_additional_stuffing(Cell_stuffing_Blank object_info)
    {
        if (object_info.object_active != 0)
        {
            if (cell_default_stuffing_additional_gameObject.activeSelf == false)
            {
                cell_default_stuffing_additional_gameObject.SetActive(true);
            }

            Image_Data image_info = Main.db_data.image_data[object_info.object_image_id];

            cell_default_stuffing_additional.sprite = Main.Image_list[image_info.image_name];
            cell_default_stuffing_additional_gameObject.name = "" + object_info.object_editor_type + " _ " + object_info.object_editor_id + " _ " + image_info.image_name;

            image_scale = Calculate_image_size(image_info._width, image_info._height, default_stuffing_additional_width, default_stuffing_additional_height);

            cell_default_stuffing_additional_transform.localScale = new Vector3(image_scale, image_scale, 1);
        }
        else
        {
            cell_default_stuffing_additional.sprite = null;

            cell_default_stuffing_additional_gameObject.SetActive(false);
        }
    }
    #endregion




    private float Calculate_image_size(float _w, float _h, float frame_w, float frame_h)
    {
        float zoom;
        float size_width;
        float size_height;

        if (_w <= _h)
        {
            size_width = _h;
            size_height = frame_h;
        }
        else
        {
            size_width = _w;
            size_height = frame_w;
        }

        zoom = (size_width - size_height) / size_width;

        if (zoom <= 0)
        {
            zoom = 1;
        }

        /*
        size_width = image_info._width - image_info._width * zoom;
        size_height = image_info._height - image_info._height * zoom;
        */
        return zoom;
    }






    public void Cell_SetActive()
    {
        cell_base_go.SetActive(true);
        cell_decoration_go.SetActive(true);

        cell_stuffing_go.SetActive(true);

        cell_default_stuffing_go.SetActive(true);
    }

    public void Cell_Deactivate()
    {
        cell_base_go.SetActive(false);
        cell_decoration_go.SetActive(false);

        cell_stuffing_go.SetActive(false);

        cell_default_stuffing_go.SetActive(false);
    }
}
