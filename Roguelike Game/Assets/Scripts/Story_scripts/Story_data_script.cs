using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_data_script
{
    public string name;

    public string main_map_name;

    public Dictionary<string, Story_map_Data> Map_data = new Dictionary<string, Story_map_Data>(); // Хранит все данные о каждой редактируемой карте.
}
