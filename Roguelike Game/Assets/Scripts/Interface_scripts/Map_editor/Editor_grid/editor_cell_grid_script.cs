using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editor_cell_grid_script : MonoBehaviour
{
    #region editor_cell_x_y
    [Header("   editor_cell_x_y")]
    public editor_cell_script editor_cell_x1_y1;
    public editor_cell_script editor_cell_x2_y1;
    public editor_cell_script editor_cell_x3_y1;
    public editor_cell_script editor_cell_x4_y1;
    public editor_cell_script editor_cell_x5_y1;
    public editor_cell_script editor_cell_x6_y1;
    public editor_cell_script editor_cell_x7_y1;
    public editor_cell_script editor_cell_x8_y1;
    public editor_cell_script editor_cell_x9_y1;
    [Space]
    public editor_cell_script editor_cell_x1_y2;
    public editor_cell_script editor_cell_x2_y2;
    public editor_cell_script editor_cell_x3_y2;
    public editor_cell_script editor_cell_x4_y2;
    public editor_cell_script editor_cell_x5_y2;
    public editor_cell_script editor_cell_x6_y2;
    public editor_cell_script editor_cell_x7_y2;
    public editor_cell_script editor_cell_x8_y2;
    public editor_cell_script editor_cell_x9_y2;
    [Space]
    public editor_cell_script editor_cell_x1_y3;
    public editor_cell_script editor_cell_x2_y3;
    public editor_cell_script editor_cell_x3_y3;
    public editor_cell_script editor_cell_x4_y3;
    public editor_cell_script editor_cell_x5_y3;
    public editor_cell_script editor_cell_x6_y3;
    public editor_cell_script editor_cell_x7_y3;
    public editor_cell_script editor_cell_x8_y3;
    public editor_cell_script editor_cell_x9_y3;
    [Space]
    public editor_cell_script editor_cell_x1_y4;
    public editor_cell_script editor_cell_x2_y4;
    public editor_cell_script editor_cell_x3_y4;
    public editor_cell_script editor_cell_x4_y4;
    public editor_cell_script editor_cell_x5_y4;
    public editor_cell_script editor_cell_x6_y4;
    public editor_cell_script editor_cell_x7_y4;
    public editor_cell_script editor_cell_x8_y4;
    public editor_cell_script editor_cell_x9_y4;
    [Space]
    public editor_cell_script editor_cell_x1_y5;
    public editor_cell_script editor_cell_x2_y5;
    public editor_cell_script editor_cell_x3_y5;
    public editor_cell_script editor_cell_x4_y5;
    public editor_cell_script editor_cell_x5_y5;
    public editor_cell_script editor_cell_x6_y5;
    public editor_cell_script editor_cell_x7_y5;
    public editor_cell_script editor_cell_x8_y5;
    public editor_cell_script editor_cell_x9_y5;
    [Space]
    public editor_cell_script editor_cell_x1_y6;
    public editor_cell_script editor_cell_x2_y6;
    public editor_cell_script editor_cell_x3_y6;
    public editor_cell_script editor_cell_x4_y6;
    public editor_cell_script editor_cell_x5_y6;
    public editor_cell_script editor_cell_x6_y6;
    public editor_cell_script editor_cell_x7_y6;
    public editor_cell_script editor_cell_x8_y6;
    public editor_cell_script editor_cell_x9_y6;
    #endregion

    // Use this for initialization
    void Start()
    {
        #region creat editor_cell_x_y
        editor_cell_x1_y1.Creat_cell(1, 1);
        editor_cell_x2_y1.Creat_cell(2, 1);
        editor_cell_x3_y1.Creat_cell(3, 1);
        editor_cell_x4_y1.Creat_cell(4, 1);
        editor_cell_x5_y1.Creat_cell(5, 1);
        editor_cell_x6_y1.Creat_cell(6, 1);
        editor_cell_x7_y1.Creat_cell(7, 1);
        editor_cell_x8_y1.Creat_cell(8, 1);
        editor_cell_x9_y1.Creat_cell(9, 1);

        editor_cell_x1_y2.Creat_cell(1, 2);
        editor_cell_x2_y2.Creat_cell(2, 2);
        editor_cell_x3_y2.Creat_cell(3, 2);
        editor_cell_x4_y2.Creat_cell(4, 2);
        editor_cell_x5_y2.Creat_cell(5, 2);
        editor_cell_x6_y2.Creat_cell(6, 2);
        editor_cell_x7_y2.Creat_cell(7, 2);
        editor_cell_x8_y2.Creat_cell(8, 2);
        editor_cell_x9_y2.Creat_cell(9, 2);

        editor_cell_x1_y3.Creat_cell(1, 3);
        editor_cell_x2_y3.Creat_cell(2, 3);
        editor_cell_x3_y3.Creat_cell(3, 3);
        editor_cell_x4_y3.Creat_cell(4, 3);
        editor_cell_x5_y3.Creat_cell(5, 3);
        editor_cell_x6_y3.Creat_cell(6, 3);
        editor_cell_x7_y3.Creat_cell(7, 3);
        editor_cell_x8_y3.Creat_cell(8, 3);
        editor_cell_x9_y3.Creat_cell(9, 3);

        editor_cell_x1_y4.Creat_cell(1, 4);
        editor_cell_x2_y4.Creat_cell(2, 4);
        editor_cell_x3_y4.Creat_cell(3, 4);
        editor_cell_x4_y4.Creat_cell(4, 4);
        editor_cell_x5_y4.Creat_cell(5, 4);
        editor_cell_x6_y4.Creat_cell(6, 4);
        editor_cell_x7_y4.Creat_cell(7, 4);
        editor_cell_x8_y4.Creat_cell(8, 4);
        editor_cell_x9_y4.Creat_cell(9, 4);

        editor_cell_x1_y5.Creat_cell(1, 5);
        editor_cell_x2_y5.Creat_cell(2, 5);
        editor_cell_x3_y5.Creat_cell(3, 5);
        editor_cell_x4_y5.Creat_cell(4, 5);
        editor_cell_x5_y5.Creat_cell(5, 5);
        editor_cell_x6_y5.Creat_cell(6, 5);
        editor_cell_x7_y5.Creat_cell(7, 5);
        editor_cell_x8_y5.Creat_cell(8, 5);
        editor_cell_x9_y5.Creat_cell(9, 5);

        editor_cell_x1_y6.Creat_cell(1, 6);
        editor_cell_x2_y6.Creat_cell(2, 6);
        editor_cell_x3_y6.Creat_cell(3, 6);
        editor_cell_x4_y6.Creat_cell(4, 6);
        editor_cell_x5_y6.Creat_cell(5, 6);
        editor_cell_x6_y6.Creat_cell(6, 6);
        editor_cell_x7_y6.Creat_cell(7, 6);
        editor_cell_x8_y6.Creat_cell(8, 6);
        editor_cell_x9_y6.Creat_cell(9, 6);
        #endregion
    }

    // Update is called once per frame
    void Update() {

    }

    public void Display_grid(int position_x, int position_y)
    {
        editor_cell_x1_y1.Display_address_x(position_x, position_y);
        editor_cell_x2_y1.Display_address_x(position_x, position_y);
        editor_cell_x3_y1.Display_address_x(position_x, position_y);
        editor_cell_x4_y1.Display_address_x(position_x, position_y);
        editor_cell_x5_y1.Display_address_x(position_x, position_y);
        editor_cell_x6_y1.Display_address_x(position_x, position_y);
        editor_cell_x7_y1.Display_address_x(position_x, position_y);
        editor_cell_x8_y1.Display_address_x(position_x, position_y);
        editor_cell_x9_y1.Display_address_x(position_x, position_y);

        editor_cell_x1_y2.Display_address_x(position_x, position_y);
        editor_cell_x2_y2.Display_address_x(position_x, position_y);
        editor_cell_x3_y2.Display_address_x(position_x, position_y);
        editor_cell_x4_y2.Display_address_x(position_x, position_y);
        editor_cell_x5_y2.Display_address_x(position_x, position_y);
        editor_cell_x6_y2.Display_address_x(position_x, position_y);
        editor_cell_x7_y2.Display_address_x(position_x, position_y);
        editor_cell_x8_y2.Display_address_x(position_x, position_y);
        editor_cell_x9_y2.Display_address_x(position_x, position_y);

        editor_cell_x1_y3.Display_address_x(position_x, position_y);
        editor_cell_x2_y3.Display_address_x(position_x, position_y);
        editor_cell_x3_y3.Display_address_x(position_x, position_y);
        editor_cell_x4_y3.Display_address_x(position_x, position_y);
        editor_cell_x5_y3.Display_address_x(position_x, position_y);
        editor_cell_x6_y3.Display_address_x(position_x, position_y);
        editor_cell_x7_y3.Display_address_x(position_x, position_y);
        editor_cell_x8_y3.Display_address_x(position_x, position_y);
        editor_cell_x9_y3.Display_address_x(position_x, position_y);

        editor_cell_x1_y4.Display_address_x(position_x, position_y);
        editor_cell_x2_y4.Display_address_x(position_x, position_y);
        editor_cell_x3_y4.Display_address_x(position_x, position_y);
        editor_cell_x4_y4.Display_address_x(position_x, position_y);
        editor_cell_x5_y4.Display_address_x(position_x, position_y);
        editor_cell_x6_y4.Display_address_x(position_x, position_y);
        editor_cell_x7_y4.Display_address_x(position_x, position_y);
        editor_cell_x8_y4.Display_address_x(position_x, position_y);
        editor_cell_x9_y4.Display_address_x(position_x, position_y);

        editor_cell_x1_y5.Display_address_x(position_x, position_y);
        editor_cell_x2_y5.Display_address_x(position_x, position_y);
        editor_cell_x3_y5.Display_address_x(position_x, position_y);
        editor_cell_x4_y5.Display_address_x(position_x, position_y);
        editor_cell_x5_y5.Display_address_x(position_x, position_y);
        editor_cell_x6_y5.Display_address_x(position_x, position_y);
        editor_cell_x7_y5.Display_address_x(position_x, position_y);
        editor_cell_x8_y5.Display_address_x(position_x, position_y);
        editor_cell_x9_y5.Display_address_x(position_x, position_y);

        editor_cell_x1_y6.Display_address_x(position_x, position_y);
        editor_cell_x2_y6.Display_address_x(position_x, position_y);
        editor_cell_x3_y6.Display_address_x(position_x, position_y);
        editor_cell_x4_y6.Display_address_x(position_x, position_y);
        editor_cell_x5_y6.Display_address_x(position_x, position_y);
        editor_cell_x6_y6.Display_address_x(position_x, position_y);
        editor_cell_x7_y6.Display_address_x(position_x, position_y);
        editor_cell_x8_y6.Display_address_x(position_x, position_y);
        editor_cell_x9_y6.Display_address_x(position_x, position_y);




        editor_cell_x1_y1.Display_address_y(position_x, position_y);
        editor_cell_x2_y1.Display_address_y(position_x, position_y);
        editor_cell_x3_y1.Display_address_y(position_x, position_y);
        editor_cell_x4_y1.Display_address_y(position_x, position_y);
        editor_cell_x5_y1.Display_address_y(position_x, position_y);
        editor_cell_x6_y1.Display_address_y(position_x, position_y);
        editor_cell_x7_y1.Display_address_y(position_x, position_y);
        editor_cell_x8_y1.Display_address_y(position_x, position_y);
        editor_cell_x9_y1.Display_address_y(position_x, position_y);

        editor_cell_x1_y2.Display_address_y(position_x, position_y);
        editor_cell_x2_y2.Display_address_y(position_x, position_y);
        editor_cell_x3_y2.Display_address_y(position_x, position_y);
        editor_cell_x4_y2.Display_address_y(position_x, position_y);
        editor_cell_x5_y2.Display_address_y(position_x, position_y);
        editor_cell_x6_y2.Display_address_y(position_x, position_y);
        editor_cell_x7_y2.Display_address_y(position_x, position_y);
        editor_cell_x8_y2.Display_address_y(position_x, position_y);
        editor_cell_x9_y2.Display_address_y(position_x, position_y);

        editor_cell_x1_y3.Display_address_y(position_x, position_y);
        editor_cell_x2_y3.Display_address_y(position_x, position_y);
        editor_cell_x3_y3.Display_address_y(position_x, position_y);
        editor_cell_x4_y3.Display_address_y(position_x, position_y);
        editor_cell_x5_y3.Display_address_y(position_x, position_y);
        editor_cell_x6_y3.Display_address_y(position_x, position_y);
        editor_cell_x7_y3.Display_address_y(position_x, position_y);
        editor_cell_x8_y3.Display_address_y(position_x, position_y);
        editor_cell_x9_y3.Display_address_y(position_x, position_y);

        editor_cell_x1_y4.Display_address_y(position_x, position_y);
        editor_cell_x2_y4.Display_address_y(position_x, position_y);
        editor_cell_x3_y4.Display_address_y(position_x, position_y);
        editor_cell_x4_y4.Display_address_y(position_x, position_y);
        editor_cell_x5_y4.Display_address_y(position_x, position_y);
        editor_cell_x6_y4.Display_address_y(position_x, position_y);
        editor_cell_x7_y4.Display_address_y(position_x, position_y);
        editor_cell_x8_y4.Display_address_y(position_x, position_y);
        editor_cell_x9_y4.Display_address_y(position_x, position_y);

        editor_cell_x1_y5.Display_address_y(position_x, position_y);
        editor_cell_x2_y5.Display_address_y(position_x, position_y);
        editor_cell_x3_y5.Display_address_y(position_x, position_y);
        editor_cell_x4_y5.Display_address_y(position_x, position_y);
        editor_cell_x5_y5.Display_address_y(position_x, position_y);
        editor_cell_x6_y5.Display_address_y(position_x, position_y);
        editor_cell_x7_y5.Display_address_y(position_x, position_y);
        editor_cell_x8_y5.Display_address_y(position_x, position_y);
        editor_cell_x9_y5.Display_address_y(position_x, position_y);

        editor_cell_x1_y6.Display_address_y(position_x, position_y);
        editor_cell_x2_y6.Display_address_y(position_x, position_y);
        editor_cell_x3_y6.Display_address_y(position_x, position_y);
        editor_cell_x4_y6.Display_address_y(position_x, position_y);
        editor_cell_x5_y6.Display_address_y(position_x, position_y);
        editor_cell_x6_y6.Display_address_y(position_x, position_y);
        editor_cell_x7_y6.Display_address_y(position_x, position_y);
        editor_cell_x8_y6.Display_address_y(position_x, position_y);
        editor_cell_x9_y6.Display_address_y(position_x, position_y);
    }
}
