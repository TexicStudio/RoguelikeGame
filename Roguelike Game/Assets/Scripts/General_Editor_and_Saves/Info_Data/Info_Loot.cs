using System.Collections.Generic;

public class Info_Loot
{
    public string editor_id;
    public string editor_type;

    public string game_name;

    public int chance_min;
    public int chance_max;

    public int item_number;

    public List<Info_Stuff> stuff = new List<Info_Stuff>();
}
