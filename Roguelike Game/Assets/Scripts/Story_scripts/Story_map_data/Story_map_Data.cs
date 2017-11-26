using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class Story_map_Data
{
    public string id;

    public string name;
    public string test_name;

    public Story_Summary_Map_info_Data short_info = new Story_Summary_Map_info_Data();

    public int map_width; // высота карты
    public int map_height; // ширина карты
    public int map_cell_number; // количество клеток в карте

    public int protagonist_position_x; // позиция главного героя на карте по Оси х
    public int protagonist_position_y; // позиция главного героя на карте по Оси y

    public Dictionary<string, Cell_info_Blank> cell_info = new Dictionary<string, Cell_info_Blank>(); // Инфорация о клетках
}
