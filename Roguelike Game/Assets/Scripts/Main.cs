using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using System.Xml;

public class Main : MonoBehaviour
{
    static Main Instance;

    public static Font my_font; // шрифт который используеться во всем проекте

    public static int stage_w = 1920; // public static int stage_w = 1360; // разрешение экрана ( ширина )
    public static int stage_h = 1080; // разрешение экрана ( высота )

    public static Color_Data _color = new Color_Data();

    public static Dictionary<string, Sprite> Image_list = new Dictionary<string, Sprite>();
    
    public static Dictionary<int, Sprite> level_image_list = new Dictionary<int, Sprite>();


    public static float scale_cell = 1.0f;

    
    public static string cell_x_label = "x.";
    public static string cell_y_label = "y.";


    public static Data_DB db_data = new Data_DB(); // хранилище для данных полученых с БД путем запросов. 

    public static Dictionary<string, Map_info_Blank> Map_info = new Dictionary<string, Map_info_Blank>(); // Хранит все данные о каждой редактируемой карте.


    public static Dictionary<string, Story_data_script> Story_info = new Dictionary<string, Story_data_script>(); // Хранит все данные о каждой истории.


    public static Info_Data item_data = new Info_Data(); // хранит в себе все данные о придметах, и тд.

    public static Selected_library_object_info_Data selected_object = new Selected_library_object_info_Data();

    public static string selected_map;
    public static string selected_cell;

    public static int selected_cell_layer = 0;

    public static bool default_stuffing_layer = false;
    public static bool additional_stuffing_layer = false;


    public static string action_edit;
    public static string action_name = ""; // тип действия которое сейчас выполняеться . для редактора

    /*
                action_name
    -   cell_draw   // рисуем на однок клетке
     */


    public static bool mouse_down = false;

    public static float move_x;
    public static float move_y;

    public static float mouse_begin_pos_x;
    public static float mouse_begin_pos_y;

    public static string move_column_type = "";
    public static string move_row_type = "";

    public static bool movement_allowed = false;
    public static Vector3 movement;
    
    // Use this for initialization
    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}
/*
Создание библиотеки Coverage - id
new_custom_object_button.Creat_object("coverage", "", item_info.Value.id, item_info.Value.image_id);
            

Создание библиотеки Cell_decoration - id
new_custom_object_button.Creat_object("cell_decoration", "", item_info.Value.id, item_info.Value.image_id);


Создание библиотеки Relief - editor_id / editor_name
public Dictionary<string, Info_Relief> relief = new Dictionary<string, Info_Relief>();
new_custom_object_button.Creat_object("relief", item_info.Value.editor_name, 2, Main.db_data.relief_data[item_info.Value.default_id].image_id);


Создание библиотеки Dungeon - id
new_custom_object_button.Creat_object("dungeon", "", item_info.Value.id, item_info.Value.image_id);


Создание библиотеки Point_buffs - id
new_custom_object_button.Creat_object("point_buffs", "", item_info.Value.id, item_info.Value.image_id);


Создание библиотеки Region - id
new_custom_object_button.Creat_object("region", "", item_info.Value.id, item_info.Value.image_id);


Создание библиотеки Stuff - id
new_custom_object_button.Creat_object("stuff", item_info.Value.stuff_name, item_info.Value.id, Main.db_data.stuff_data[item_info.Value.id].image_id);


Создание библиотеки Loot
public Dictionary<string, Info_Loot> loot = new Dictionary<string, Info_Loot>();
object_library_list["loot"].object_num = 0;


Создание библиотеки Bestiary - id
new_custom_object_button.Creat_object("bestiary", item_info.Value.bestiary_name, item_info.Value.id, Main.db_data.bestiary_data[item_info.Value.id].image_id);
*/
