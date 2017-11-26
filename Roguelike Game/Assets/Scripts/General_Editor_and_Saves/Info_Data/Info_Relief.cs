public class Info_Relief
{
    public string editor_id;
    public string editor_type;

    public string editor_name = "";

    public int info_active = 0; // значение 1 или 0 ( true // false )/ служит для того чтоб указывать нужно ли прописывать ресурс.

    public int default_id;

    public int production_place_id; // ID картинки которая отображаеться при наличии ресурса

    public int place_after_extraction_id; // ID картинки которая отображаеться при 0 едениц ресурса

    public Cell_stuffing_Blank resource;  // ID ресура / придмета или пака придметов

    public int resource_value;// количество рессурса
    public int resource_value_min;
    public int resource_value_max;

    public int cooldown_value; // время отката возможности добычи ресурса
    public int cooldown_min;
    public int cooldown_max;
}
