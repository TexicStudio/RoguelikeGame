using System.Collections.Generic;

public class Data_DB
{
    public int map_list_count;
    public Dictionary<int, Map_list_Data> map_list = new Dictionary<int, Map_list_Data>(); // Таблица "map". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> map_type = new List<Info_Type_name_Data>();

    public int image_count;
    public Dictionary<int, Image_Data> image_data = new Dictionary<int, Image_Data>(); // Таблица "image". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> image_type = new List<Info_Type_name_Data>(); // Таблица "image_type". Данные которые загружаютсья с базы данных.


    public Dictionary<int, Info_Type_name_Data> point_type = new Dictionary<int, Info_Type_name_Data>(); // Таблица "point_type". Данные которые загружаютсья с базы данных.


    public Dictionary<int, Coverage_Data> coverage_data = new Dictionary<int, Coverage_Data>(); // Таблица "coverage". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> coverage_type = new List<Info_Type_name_Data>(); // Таблица "coverage_type". Данные которые загружаютсья с базы данных.

    public Dictionary<int, Cell_decoration_Data> cell_decoration = new Dictionary<int, Cell_decoration_Data>();
    public List<Info_Type_name_Data> cell_decoration_type = new List<Info_Type_name_Data>();

    public Dictionary<int, Relief_Data> relief_data = new Dictionary<int, Relief_Data>(); // Таблица "relief". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> relief_type = new List<Info_Type_name_Data>(); // Таблица "relief_type". Данные которые загружаютсья с базы данных.

    public Dictionary<int, Dungeon_Data> dungeon_data = new Dictionary<int, Dungeon_Data>();
    public List<Info_Type_name_Data> dungeon_type = new List<Info_Type_name_Data>();

    public Dictionary<int, Point_buffs_Data> point_buffs_data = new Dictionary<int, Point_buffs_Data>();
    public List<Info_Type_name_Data> point_buffs_type = new List<Info_Type_name_Data>();

    public Dictionary<int, Region_Data> region_data = new Dictionary<int, Region_Data>();
    public List<Info_Type_name_Data> region_type = new List<Info_Type_name_Data>();


    public Dictionary<int, Stuff_Data> stuff_data = new Dictionary<int, Stuff_Data>(); // Таблица "stuff". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> stuff_type = new List<Info_Type_name_Data>(); // Таблица "stuff_type". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> stuff_class = new List<Info_Type_name_Data>(); // Таблица "stuff_class". Данные которые загружаютсья с базы данных.
    public Dictionary<int, Ability_info_Data> stuff_abilities = new Dictionary<int, Ability_info_Data>(); // Таблица "stuff_abilities". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> stuff_abilities_type = new List<Info_Type_name_Data>(); // Таблица "stuff_abilities_type". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> stuff_abilities_class = new List<Info_Type_name_Data>(); // Таблица "stuff_abilities_class". Данные которые загружаютсья с базы данных.

    public Dictionary<int, Bestiary_Data> bestiary_data = new Dictionary<int, Bestiary_Data>(); // Таблица "bestiary". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> bestiary_fight_type = new List<Info_Type_name_Data>(); // Таблица "bestiary_fight_type". Данные которые загружаютсья с базы данных.
    public Dictionary<int, Ability_info_Data> bestiary_abilities = new Dictionary<int, Ability_info_Data>(); // Таблица "bestiary_abilities". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> bestiary_abilities_type = new List<Info_Type_name_Data>(); // Таблица "bestiary_abilities_type". Данные которые загружаютсья с базы данных.
    public List<Info_Type_name_Data> bestiary_abilities_class = new List<Info_Type_name_Data>(); // Таблица "bestiary_abilities_class". Данные которые загружаютсья с базы данных.
}
