using System.Collections.Generic;
using System;

[Serializable]
public class Cell_stuffing_Blank
{
    public int id;

    public string editor_id;
    public string editor_type;

    public int level;

    public int image_id;

    // дополнительный лут на клетке с обеьктами опеределенного типа
    public int object_active; // активное ли это клетка значения 1 и 0 ( true / false )

    public int object_id;

    public string object_editor_id;
    public string object_editor_type;

    public int object_level;

    public int object_image_id;
}