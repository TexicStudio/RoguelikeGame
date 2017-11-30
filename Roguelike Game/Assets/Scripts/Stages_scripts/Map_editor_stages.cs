using UnityEngine;
using System.Collections.Generic;

using System.IO;
// LayoutRebuilder
public class Map_editor_stages : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject Main_Canvas;
    public RectTransform Main_Canvas_RectTransform;


    [Space]
    public GameObject maps_container_object;
    public maps_container_script maps_container;

    [Space]
    public GameObject instruments_panel_gameObject;
    public Editor_instruments_panel_script instruments_panel;

    [Space]
    public GameObject object_library_gameObject;
    public object_library_script object_library;

    public Dictionary<string, custom_object_library_panel_script> object_library_list = new Dictionary<string, custom_object_library_panel_script>();


    [Space]
    [Header("   Главный герой")]
    public GameObject protagonist_gameObject;
    public Transform protagonist_Transform;
    

    [Space]
    [Header("   данные")]
    public int cell_row_min = 1;
    public int cell_row_max;

    public int cell_column_min = 1;
    public int cell_column_max;

    public Dictionary<string, map_object_script> maps = new Dictionary<string, map_object_script>();

    public bool new_item_button_exist;

    private string old_library_name = "";

    private float movment_scale;
    private float movment_pos_x;
    private float movment_pos_y;



    private bool start_create = false;
    private string start_map_name = "";


    private bool change_action = false;
    private List<string> map_name_list = new List<string>();

    // Use this for initialization
    void Start()
    {
        Debug.Log("----------------------------------------------------------------");
        Debug.Log("5_Map_Editor");
        Debug.Log("----------------------------------------------------------------");

        Main.action_name = "";

        Create_scene();
    }



    void Update()
    {
        if (change_action == true)
        {
            change_action = false;
            Start_create_map();
        }
    }



    public void Create_scene()
    {
        Main.selected_object.id = 0;
        Main.selected_object.editor_id = "delete";
        Main.selected_object.editor_type = "bestiary";
        Main.selected_object.editor_name = "";
        /*
        instruments_panel.scale_100.onValueChanged.AddListener((bool on) => Map_scale_Click(on));
        instruments_panel.scale_70.onValueChanged.AddListener((bool on) => Map_scale_Click(on));
        instruments_panel.scale_50.onValueChanged.AddListener((bool on) => Map_scale_Click(on));
        instruments_panel.scale_30.onValueChanged.AddListener((bool on) => Map_scale_Click(on));
        */
        //Debug.Log("5_Map_Editor - 1");

        Begin_display_map();
    }




    #region Создание карты
    public void Begin_display_map()
    {

        Debug.Log("5_Map_Editor - Begin_display_map");

        start_create = true;

        
        foreach (KeyValuePair<string, Story_map_Data> story_map_info in Main.Story_info["0"].Map_data)
        {
            //Create_map(story_map_info.Key, story_map_info.Value);
            map_name_list.Add(story_map_info.Key);
        }

        //Main.selected_map = "" + start_map_name;

        //maps[Main.selected_map].gameObject.SetActive(true);

        Start_create_map();
    }

    private void Start_create_map()
    {
        if (map_name_list.Count > 0)
        {
            Create_map(map_name_list[0], Main.Story_info["0"].Map_data[map_name_list[0]]);
        }
        else
        {
            Main.selected_map = "" + start_map_name;

            maps[Main.selected_map].gameObject.SetActive(true);
        }
    }

    public void Create_map(string temp_map_name, Story_map_Data new_map_data)
    {
        if(start_create == true)
        {
            start_create = false;

            start_map_name = "" + new_map_data.id;
        }

        Debug.Log("5_Map_Editor - Create_map - name = " + temp_map_name + " != " + new_map_data.id);

        Main.selected_map = "" + new_map_data.id;

        GameObject new_map_object = Instantiate(Resources.Load("Prefab/Map/map_object", typeof(GameObject))) as GameObject;
        new_map_object.transform.SetParent(maps_container.map_frame.transform);
        new_map_object.name = "" + new_map_data.id + "_(" + new_map_data.test_name + ")"; //new_map_data.name

        map_object_script new_map = new_map_object.GetComponent<map_object_script>();

        new_map.main_transform.localPosition = new Vector3(0, 0, 0);


        cell_row_min = 1;
        cell_row_max = new_map_data.map_height;

        cell_column_min = 1;
        cell_column_max = new_map_data.map_width;

        new_map.Create_map(Main.selected_map, cell_row_min, cell_row_max, cell_column_min, cell_column_max, maps_container.cell_row_max, maps_container.cell_column_max);

        maps.Add(Main.selected_map, new_map);

        maps[Main.selected_map].gameObject.SetActive(false);


        protagonist_Transform.position = new Vector2((Main.Story_info["0"].protagonist_position_x - 1) * 240, (Main.Story_info["0"].protagonist_position_y - 1) * 240);


        map_name_list.RemoveAt(0);
        change_action = true;
    }
    #endregion


    #region Маштаб карты
    public void Map_scale_Click(bool temp_bool)
    {
        if (instruments_panel.scale_100.isOn == true)
        {
            if(Main.scale_cell != 1.0f)
            {
                movment_scale = Main.scale_cell - 1.0f;
                movment_pos_x = Main_Canvas_RectTransform.position.x * movment_scale;
                movment_pos_y = Main_Canvas_RectTransform.position.y * movment_scale;

                Main_Canvas_RectTransform.position = new Vector3(Main_Canvas_RectTransform.position.x + movment_pos_x, Main_Canvas_RectTransform.position.y + movment_pos_y, 0);
            }

            Main.scale_cell = 1.0f;
            maps[Main.selected_map].main_transform.localScale = new Vector3(Main.scale_cell, Main.scale_cell, 1.0f);
        }
        else if (instruments_panel.scale_70.isOn == true)
        {
            if (Main.scale_cell != 0.70f)
            {
                movment_scale = Main.scale_cell - 0.70f;
                movment_pos_x = Main_Canvas_RectTransform.position.x * movment_scale;
                movment_pos_y = Main_Canvas_RectTransform.position.y * movment_scale;

                Main_Canvas_RectTransform.position = new Vector3(Main_Canvas_RectTransform.position.x + movment_pos_x, Main_Canvas_RectTransform.position.y + movment_pos_y, 0);
            }

            Main.scale_cell = 0.70f;
            maps[Main.selected_map].main_transform.localScale = new Vector3(Main.scale_cell, Main.scale_cell, 1.0f);
        }
        else if (instruments_panel.scale_50.isOn == true)
        {
            if (Main.scale_cell != 0.50f)
            {
                movment_scale = Main.scale_cell - 0.50f;
                movment_pos_x = Main_Canvas_RectTransform.position.x * movment_scale;
                movment_pos_y = Main_Canvas_RectTransform.position.y * movment_scale;

                Main_Canvas_RectTransform.position = new Vector3(Main_Canvas_RectTransform.position.x + movment_pos_x, Main_Canvas_RectTransform.position.y + movment_pos_y, 0);
            }

            Main.scale_cell = 0.50f;
            maps[Main.selected_map].main_transform.localScale = new Vector3(Main.scale_cell, Main.scale_cell, 1.0f);
        }
        else if (instruments_panel.scale_30.isOn == true)
        {
            if (Main.scale_cell != 0.30f)
            {
                movment_scale = Main.scale_cell - 0.30f;
                movment_pos_x = Main_Canvas_RectTransform.position.x * movment_scale;
                movment_pos_y = Main_Canvas_RectTransform.position.y * movment_scale;

                Main_Canvas_RectTransform.position = new Vector3(Main_Canvas_RectTransform.position.x + movment_pos_x, Main_Canvas_RectTransform.position.y + movment_pos_y, 0);
            }

            Main.scale_cell = 0.30f;
            maps[Main.selected_map].main_transform.localScale = new Vector3(Main.scale_cell, Main.scale_cell, 1.0f);
        }

        //Debug.Log("меняем маштаб карты");
    }
    #endregion




    #region Переход по данжам
    public void Map_cell_Click(string new_map_name, string new_cell_adress)
    {
        maps[Main.selected_map].gameObject.SetActive(false);

        Main.selected_map = "" + new_map_name;

        maps[Main.selected_map].gameObject.SetActive(true);
    }
    #endregion
}