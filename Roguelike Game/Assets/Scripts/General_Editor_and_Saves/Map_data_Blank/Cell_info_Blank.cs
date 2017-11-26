using System.Collections.Generic;
using System;

[Serializable]
public class Cell_info_Blank
{
    public int id; // уникальный индентификатор клетка, который равен ее порядковому номеру
    public string address; // адрес клетки который состоит из позиции по оси Х и У (у_х) 

    public int position_x; // адрес клетки по оси Х
    public int position_y; // адрес клетки по оси У

    public int cell_type;

    public int cell_base_id; // coverage - покрытие (земля)

    public int cell_decoration_id;
    //public float cell_decoration_x;
    //public float cell_decoration_y;

    public int cell_stuffing_active; // активное ли это клетка значения 1 и 0 ( true / false )
    public Cell_stuffing_Blank cell_stuffing;

    public int default_active; // активное ли это клетка по умолчанию. значения 1 и 0 ( true / false )
    public Cell_stuffing_Blank default_bestiary;
}
