using System;
using System.Collections.Generic;

[Serializable]
public class Summary_Map_info_Blank
{
    //public Dictionary<string, string> cell_info = new Dictionary<string, string>();
    //public Dictionary<int, int> coverage = new Dictionary<int, int>();

    public Dictionary<string, string> relief = new Dictionary<string, string>(); //<cell_adress , editor_id>

    //public Dictionary<int, int> cell_decoration = new Dictionary<int, int>();

    public Dictionary<string, int> dungeon = new Dictionary<string, int>(); //<cell_adress , id>
    public Dictionary<string, int> dungeon_transition = new Dictionary<string, int>(); //<cell_adress , id>

    public Dictionary<string, int> region = new Dictionary<string, int>(); //<cell_adress , id>
    public Dictionary<string, int> region_teleportation = new Dictionary<string, int>(); //<cell_adress , id>
    public Dictionary<string, int> region_dungeon_exit = new Dictionary<string, int>(); //<cell_adress , id>
    public Dictionary<string, int> region_protagonist_start_point = new Dictionary<string, int>(); //<cell_adress , id>

    //public Dictionary<int, int> point_buffs = new Dictionary<int, int>();
    //public Dictionary<string, string> loot = new Dictionary<string, string>();
    //public Dictionary<int, int> stuff = new Dictionary<int, int>();
    //public Dictionary<int, int> bestiary = new Dictionary<int, int>();
    //public Dictionary<string, string> pack_loot = new Dictionary<string, string>();
    //public Dictionary<string, string> pack_stuff = new Dictionary<string, string>();
    //public Dictionary<string, string> pack_bestiary = new Dictionary<string, string>();
    //public Dictionary<int, int> level = new Dictionary<int, int>();
    //public Dictionary<int, int> default_level = new Dictionary<int, int>();
}
