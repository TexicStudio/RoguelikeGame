using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Relief_page : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_windows;
    public ScrollRect main_scrollRect;
    [Space]
    public GameObject main_object;
    public Text main_object_titel;

    [Space]
    public Button ready_button;
    public Button back_button;

    [Space]
    [Header("   relief_short_info_panel")]
    public InputField relief_editor_name;
    public Text relief_type_name;

    [Space]
    public Toggle relief_active;
    public GameObject relief_active_blocked_panel;
    public GameObject relief_active_panel;

    [Space]
    [Header("   relief_production_info")]
    public Image relief_production_image;
    public Button relief_production_button;
    public Toggle relief_production_default;

    [Space]
    [Header("   relief_place_after_extraction_info")]
    public Image relief_place_after_extraction_image;
    public Button relief_place_after_extraction_button;
    public Toggle relief_place_after_extraction_default;

    [Space]
    [Header("   relief_resource_info")]
    public Image relief_resource_image;
    public Button relief_resource_button;

    [Space]
    [Header("   resource_data")]
    public InputField relief_info_resource_value_min;
    public InputField relief_info_resource_value_max;
    public Button random_resource_value;
    public InputField relief_info_resource_value;

    [Space]
    [Header("   cooldown_data")]
    public InputField relief_info_cooldown_value_min;
    public InputField relief_info_cooldown_value_max;
    public Button random_cooldown_value;
    public InputField relief_info_cooldown_value;


    private GameObject element_selection_windows;

    private int clone_num;
    private int num;



    public RectTransform new_object;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Windows_Move();
    }

    public void Windows_Move()
    {
       //Debug.Log("OnMove called.");

        if (new_object.position.x < 0)
        {
            new_object.position = new Vector2(0, new_object.position.y);
        }

        if (new_object.position.x > 1320)
        {
            new_object.position = new Vector2(1320, new_object.position.y);
        }

        if (new_object.position.y < 0)
        {
            new_object.position = new Vector2(new_object.position.x, 0);
        }

        if (new_object.position.y > 280)
        {
            new_object.position = new Vector2(new_object.position.x, 280);
        }
    }

    public void Create_new_relief_element()
    {

    }

    #region Название области в редакторе // Ввод данных
    public void Edit_relief_editor_name()
    {
        //Relief_data.editor_name = relief_editor_name.text;
    }
    public void End_edit_relief_editor_name()
    {
        //Relief_data.editor_name = relief_editor_name.text;
        /*
        while (Main.custom_relief_list.ContainsKey(relief_editor_name.text) == true)
        {
            relief_editor_name.text = "" + Relief_data.editor_name + " (" + clone_num + ")";
            clone_num = clone_num + 1;
        }
        */
       // Relief_data.editor_name = relief_editor_name.text;
    }
    #endregion

    #region Количество добываемого ресурса // Ввод данных
    /*
    public void Edit_resource_value_min()
    {
        if (Relief_data._info.resource_value_max < Convert.ToInt32(relief_info_resource_value_min.text))
        {
            relief_info_resource_value_min.text = "" + Relief_data._info.resource_value_min;
        }
    }
    public void Edit_resource_value_max()
    {
        if (Relief_data._info.resource_value_min > Convert.ToInt32(relief_info_resource_value_max.text))
        {
            relief_info_resource_value_max.text = "" + Relief_data._info.resource_value_max;
        }
    }
    public void Calculation_random_resource_value()
    {
        if (Relief_data._info.resource_value_max < Convert.ToInt32(relief_info_resource_value_min.text))
        {
            relief_info_resource_value_min.text = "" + Relief_data._info.resource_value_min;
        }
        else
        {
            Relief_data._info.resource_value_min = Convert.ToInt32(relief_info_resource_value_min.text);
        }

        if (Relief_data._info.resource_value_min > Convert.ToInt32(relief_info_resource_value_max.text))
        {
            relief_info_resource_value_max.text = "" + Relief_data._info.resource_value_max;
        }
        else
        {
            Relief_data._info.resource_value_max = Convert.ToInt32(relief_info_resource_value_max.text);
        }

        Relief_data._info.resource_value = UnityEngine.Random.Range(Convert.ToInt32(relief_info_resource_value_min.text), (Convert.ToInt32(relief_info_resource_value_max.text) + 1));
        relief_info_resource_value.text = "" + Relief_data._info.resource_value;
    }
    public void Edit_resource_value()
    {
        Relief_data._info.resource_value = Convert.ToInt32(relief_info_resource_value.text);
    }
    */
    #endregion

    #region Время восстановления места добычи // Ввод данных
    /*
     public void Edit_cooldown_value_min()
    {
        if (Relief_data._info.cooldown_max < Convert.ToInt32(relief_info_cooldown_value_min.text))
        {
            relief_info_cooldown_value_min.text = "" + Relief_data._info.cooldown_min;
        }
    }
    public void Edit_cooldown_value_max()
    {
        if (Relief_data._info.cooldown_min > Convert.ToInt32(relief_info_cooldown_value_max.text))
        {
            relief_info_cooldown_value_max.text = "" + Relief_data._info.cooldown_max;
        }
    }
    public void Calculation_random_cooldown_value()
    {
        if (Relief_data._info.cooldown_max < Convert.ToInt32(relief_info_cooldown_value_min.text))
        {
            relief_info_cooldown_value_min.text = "" + Relief_data._info.cooldown_min;
        }
        else
        {
            Relief_data._info.cooldown_min = Convert.ToInt32(relief_info_cooldown_value_min.text);
        }

        if (Relief_data._info.cooldown_min > Convert.ToInt32(relief_info_cooldown_value_max.text))
        {
            relief_info_cooldown_value_max.text = "" + Relief_data._info.cooldown_max;
        }
        else
        {
            Relief_data._info.cooldown_max = Convert.ToInt32(relief_info_cooldown_value_max.text);
        }

        Relief_data._info.cooldown_value = UnityEngine.Random.Range(Convert.ToInt32(relief_info_cooldown_value_min.text), (Convert.ToInt32(relief_info_cooldown_value_max.text) + 1));
        relief_info_cooldown_value.text = "" + Relief_data._info.cooldown_value;
    }
    public void Edit_cooldown_value()
    {
        Relief_data._info.cooldown_value = Convert.ToInt32(relief_info_cooldown_value.text);
    }
     */
    #endregion

    public void Back_button_Click()
    {
        main_object.SetActive(false);
        //Destroy(main_object);
    }
}
