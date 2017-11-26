using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class editor_cell_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    [Space]
    public Button _button;

    [Space]
    public GameObject address;

    [Space]
    public GameObject address_x;
    public RectTransform address_x_rectTransform;
    public Text address_x_text;

    [Space]
    public GameObject address_y;
    public RectTransform address_y_rectTransform;
    public Text address_y_text;

    [Space]
    public string cell_address;
    private int default_x;
    private int default_y;


    // Use this for initialization
    void Start()
    {

    }
    



    public void Creat_cell(int position_x, int position_y)
    {
        default_y = position_y;
        default_x = position_x;

        cell_address = "" + position_y + "_" + position_x;

        Address_x_resize(position_x);
        Address_y_resize(position_y);
    }



    public void Display_cell(int position_x, int position_y)
    {
        position_y = position_y + default_y;
        position_x = position_x + default_x;

        cell_address = "" + position_y + "_" + position_x;

        Address_x_resize(position_x);
        Address_y_resize(position_y);
    }




    public void Display_address_x(int position_x, int position_y)
    {
        position_y = position_y + default_y;
        position_x = position_x + default_x;

        cell_address = "" + position_y + "_" + position_x;

        main_object.name = "" + cell_address;

        Address_x_resize(position_x);
    }
    
    private void Address_x_resize(int position_x)
    {
        address_x_text.text = "" + position_x;
        /*
        if (address_x_text.preferredWidth > main_rectTransform.sizeDelta.x)
        {
            address_x_rectTransform.sizeDelta = new Vector2(main_rectTransform.sizeDelta.x, address_x_rectTransform.sizeDelta.y);
        }
        else if (address_x_text.preferredWidth < 20)
        {
            address_x_rectTransform.sizeDelta = new Vector2(20, address_x_rectTransform.sizeDelta.y);
        }
        else
        {
            address_x_rectTransform.sizeDelta = new Vector2(address_x_text.preferredWidth, address_x_rectTransform.sizeDelta.y);
        }*/
    }


    public void Display_address_y(int position_x, int position_y)
    {
        position_y = position_y + default_y;
        position_x = position_x + default_x;

        cell_address = "" + position_y + "_" + position_x;

        main_object.name = "" + cell_address;

        Address_y_resize(position_y);
    }

    private void Address_y_resize(int position_y)
    {
        address_y_text.text = "" + position_y;
        /*
        if (address_y_text.preferredWidth > main_rectTransform.sizeDelta.x)
        {
            address_y_rectTransform.sizeDelta = new Vector2(main_rectTransform.sizeDelta.x, address_y_rectTransform.sizeDelta.y);
        }
        else if (address_y_text.preferredWidth < 20)
        {
            address_y_rectTransform.sizeDelta = new Vector2(20, address_y_rectTransform.sizeDelta.y);
        }
        else
        {
            address_y_rectTransform.sizeDelta = new Vector2(address_y_text.preferredWidth, address_y_rectTransform.sizeDelta.y);
        }*/
    }
}
