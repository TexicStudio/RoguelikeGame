using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class maps_list_windows_script : MonoBehaviour
{
    public GameObject main_windows;
    public GameObject map_list_windows;
    public Button map_list_background_windows;
    public GameObject map_list_windows_frame;
    public GameObject map_list_frame;
    public GameObject mask_map_list;
    public GameObject map_list;
    public GameObject background_selected;
    public GameObject map_list_grid;

    public GameObject map_list_button_frame;
    public Button map_list_button;
    public Text map_list_button_label;
    public Text map_list_button_text;
    public Button map_name_inactive;
    public Button map_name_active;
    public int map_list_button_frame_position = 335; // = 335;

    private float windows_size;

    // Use this for initialization
    void Start () {
	
	}

    public void Resize_windows(int num_row)
    {
        if (num_row == 0)
        {
            windows_size = 0;
            map_list_grid.GetComponent<RectTransform>().sizeDelta = new Vector2(map_list_grid.GetComponent<RectTransform>().sizeDelta.x, windows_size);
        }
        else
        {
            windows_size = 10 + 10 + 35 * num_row + 10 * (num_row - 1);
            map_list_grid.GetComponent<RectTransform>().sizeDelta = new Vector2(map_list_grid.GetComponent<RectTransform>().sizeDelta.x, windows_size);
        }
    }

    public void Hide_show_windows()
    {
        switch (map_list_windows.activeSelf)
        {
            case true:
                map_list_windows.SetActive(false);
                map_list_button_text.text = ">";
                map_list_button_frame.GetComponent<RectTransform>().localPosition = new Vector3(0, map_list_button_frame.GetComponent<RectTransform>().localPosition.y, 0);
                break;
            case false:
                map_list_windows.SetActive(true);
                map_list_button_text.text = "<";
                map_list_button_frame.GetComponent<RectTransform>().localPosition = new Vector3(map_list_button_frame_position, map_list_button_frame.GetComponent<RectTransform>().localPosition.y, 0);
                break;
        }
    }
}
