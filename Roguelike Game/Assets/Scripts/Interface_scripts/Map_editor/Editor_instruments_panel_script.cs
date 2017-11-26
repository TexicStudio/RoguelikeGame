using UnityEngine;
using UnityEngine.UI;

public class Editor_instruments_panel_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public RectTransform main_rectTransform;

    [Space]
    [Header("   инструменты рисования")]
    public Toggle ordinary_mouse;
    public Toggle single_drawing;
    public Toggle drawing;

    [Space]
    [Header("   маштаб карты")]
    public Toggle scale_100;
    public Toggle scale_70;
    public Toggle scale_50;
    public Toggle scale_30;

    [Space]
    [Header("   дополнительный предмет")]
    public Toggle cell_additional_stuffing;

    [Space]
    [Header("   по умолчанию")]
    public Toggle cell_default_stuffing;

    // Use this for initialization
    void Start () {
		
	}

    public void Choose_the_drawing_tool()
    {
        if(ordinary_mouse.isOn == true)
        {
            Main.action_name = "";
        }
        else if (single_drawing.isOn == true)
        {
            Main.action_name = "cell_draw";
        }
        else if (drawing.isOn == true)
        {
            Main.action_name = "drawing_cells";
        }
    }

    public void Choose_cell_additional_stuffing()
    {
        if (cell_additional_stuffing.isOn == true)
        {
            Main.additional_stuffing_layer = true;
        }
        else
        {
            Main.additional_stuffing_layer = false;
        }
    }

    public void Choose_cell_default_stuffing()
    {
        if (cell_default_stuffing.isOn == true)
        {
            Main.default_stuffing_layer = true;
        }
        else
        {
            Main.default_stuffing_layer = false;
        }
    }
}
