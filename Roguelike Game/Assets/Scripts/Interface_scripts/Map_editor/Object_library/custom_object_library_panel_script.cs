using UnityEngine;
using UnityEngine.UI;
using System;

public class custom_object_library_panel_script : MonoBehaviour
{
    [Header("   основной обьект")]
    public GameObject main_object;
    public RectTransform main_object_rectTransform;
    public LayoutElement main_layoutElement;

    [Header("   кнопка, сокращенный вид")]
    public GameObject short_panel_button_gameObject;
    public LayoutElement short_panel_button_layoutElement;
    public Button short_panel_button;

    [Space]
    public Text short_panel_name;

    [Space]
    public GameObject default_frame;
    public GameObject selected_frame;

    [Header("   развернутый вид обьекта")]
    public GameObject main_panel;
    public RectTransform main_panel_rectTransform;
    public VerticalLayoutGroup main_panel_verticalLayoutGroup;

    [Header("   верхняя кнопка обьекта")]
    public Button button_top;
    public RectTransform button_top_rectTransform;
    public LayoutElement button_top_layoutElement;

    [Space]
    public Text button_top_name;

    [Header("   нижняя кнопка обьекта")]
    public Button button_down;
    public RectTransform button_down_rectTransform;
    public LayoutElement button_down_layoutElement;

    [Header("   центральная часть обьекта")]
    public GameObject object_center;
    public RectTransform object_center_rectTransform;
    public LayoutElement object_center_layoutElement;
    public GridLayoutGroup object_center_gridLayoutGroup;

    [Space]
    public GameObject new_object_frame_gameObject;
    public RectTransform new_object_frame_rectTransform;
    public LayoutElement new_object_frame_layoutElement;

    [Space]
    public Button new_object_button;

    [Space]

    [Header("   данные")]
    public int id;
    public string name_text;
    public int object_num = 0;
    public float new_object_size;
    public bool new_item_button_exist;
    public int new_item_button_exict_num;

    [Space]
    public float object_size;
    public int num_row;

    // Use this for initialization
    void Start () {
		
	}

    public void Creat_object(string temp_name_text, bool temp_new_item_button_exist)
    {
        name_text = "" + temp_name_text;

        short_panel_name.text = "" + name_text;
        button_top_name.text = "" + name_text;

        new_item_button_exist = temp_new_item_button_exist;

        switch (new_item_button_exist)
        {
            case true:
                new_item_button_exict_num = 1;
                new_object_frame_gameObject.SetActive(true);
                break;
            case false:
                new_item_button_exict_num = 0;
                new_object_frame_gameObject.SetActive(false);
                break;
        }

        Object_resize();
    }

    public void Object_resize()
    {
        object_size = object_num + 1 * new_item_button_exict_num;
        object_size = object_size / 3;
        num_row = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(object_size)));

        if (num_row == 0)
        {
            object_center_layoutElement.preferredHeight = 0;
        }
        else
        {
            object_center_layoutElement.preferredHeight = num_row * object_center_gridLayoutGroup.cellSize.y + (num_row - 1) * object_center_gridLayoutGroup.spacing.y;
            object_center_layoutElement.preferredHeight = object_center_layoutElement.preferredHeight + object_center_gridLayoutGroup.padding.top + object_center_gridLayoutGroup.padding.bottom;
        }

        object_center_rectTransform.sizeDelta = new Vector2(object_center_rectTransform.sizeDelta.x, object_center_layoutElement.preferredHeight);

        main_layoutElement.preferredHeight = button_top_layoutElement.preferredHeight + object_center_layoutElement.preferredHeight + button_down_layoutElement.preferredHeight;

        object_center_layoutElement.minHeight = object_center_layoutElement.preferredHeight;
        main_layoutElement.minHeight = main_layoutElement.preferredHeight;

        main_object_rectTransform.sizeDelta = new Vector2(main_object_rectTransform.sizeDelta.x, main_layoutElement.preferredHeight);

        object_size = main_layoutElement.preferredHeight;

        if (new_item_button_exict_num == 1)
        {
            new_object_frame_gameObject.transform.SetAsLastSibling();
        }
    }

    public void Show_full_data()
    {
        short_panel_button_gameObject.SetActive(false);

        main_panel.SetActive(true);

        main_layoutElement.preferredHeight = object_size;

        main_layoutElement.minHeight = main_layoutElement.preferredHeight;

        main_object_rectTransform.sizeDelta = new Vector2(main_object_rectTransform.sizeDelta.x, main_layoutElement.preferredHeight);
    }

    public void Hide_full_data()
    {
        main_panel.SetActive(false);
        short_panel_button_gameObject.SetActive(true);

        main_layoutElement.preferredHeight = short_panel_button_layoutElement.preferredHeight;
        
        main_layoutElement.minHeight = main_layoutElement.preferredHeight;

        main_object_rectTransform.sizeDelta = new Vector2(main_object_rectTransform.sizeDelta.x, main_layoutElement.preferredHeight);
    }

    public void Show_selected_frame()
    {
        selected_frame.SetActive(true);
        default_frame.SetActive(false);
    }

    public void Hide_selected_frame()
    {
        default_frame.SetActive(true);
        selected_frame.SetActive(false);
    }
}
