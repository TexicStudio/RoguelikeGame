using System.Collections.Generic;

public class Info_Data
{
    public Dictionary<string, Info_Relief> relief = new Dictionary<string, Info_Relief>();

    public Dictionary<string, Info_Pack_Stuff> stuff_pack = new Dictionary<string, Info_Pack_Stuff>();


    public Dictionary<string, Info_Loot> loot = new Dictionary<string, Info_Loot>();

    public Dictionary<string, Info_Pack_Loot> loot_pack = new Dictionary<string, Info_Pack_Loot>();

    public Dictionary<string, Info_Pack_Bestiary> bestiary_pack = new Dictionary<string, Info_Pack_Bestiary>();
}
