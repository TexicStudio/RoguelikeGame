using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cell_editor_script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public map_cell_script main_cell;

    [Space]

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
    public GameObject address_x_label;

    [Space]

    public GameObject address_y;
    public RectTransform address_y_rectTransform;
    public Text address_y_text;
    public GameObject address_y_label;

    public string cell_address;

    [Space]
    private bool one_click = false;
    private float dclick_threshold = 0.25f;
    private float timerdclick = 0;

    // Use this for initialization
    void Start () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hide_info();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Show_info();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //  https://gamedev.stackexchange.com/questions/116455/how-to-properly-differentiate-single-clicks-and-double-click-in-unity3d-using-c
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (one_click != true)
            {
                timerdclick = eventData.clickTime;
                one_click = true;
            }
            else if (one_click && ((eventData.clickTime - timerdclick) < dclick_threshold))
            {
                //Debug.Log("double click");
                one_click = false;

                Click_button();
            }
            else
            {
                timerdclick = eventData.clickTime;
                one_click = true;
            }
        }
    }

    public void Click_button()
    {
       Debug.Log("Нажата клетка " + cell_address);
    }




    public void Address_resize(int position_x, int position_y)
    {
        address_x_text.text = "" + position_y;
        address_y_text.text = "" + position_x;

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
        }


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
        }
    }

    public void Show_info()
    {
        if (address.activeSelf == false)
        {
            address.SetActive(true);
        }
    }

    public void Hide_info()
    {
        Main.selected_cell = cell_address;

        if (address.activeSelf == true)
        {
            address.SetActive(false);
        }
    }

    public void Destroy_object()
    {
        Destroy(main_object);
    }
}
