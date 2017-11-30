using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_data_script
{
    public string name;

    public string main_map_name;

    public int protagonist_position_x; // позиция главного героя на карте по Оси х
    public int protagonist_position_y; // позиция главного героя на карте по Оси y

    public Dictionary<string, Story_map_Data> Map_data = new Dictionary<string, Story_map_Data>(); // Хранит все данные о каждой редактируемой карте.
}
