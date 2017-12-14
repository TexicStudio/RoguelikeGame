using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    struct XYCellCoord
    {
        public int x;
        public int y;
    }

    bool goUp, goDown, goLeft, goRight; //команда на движение в заданном направлении
    public float playerSpeed; //скорость игрока
    float endPosX; //конечная позиция игрока при передвижении на 1 клетку для Х
    float endPosY; //конечная позиция игрока при передвижении на 1 клетку для Y
    Vector2 touchOrigin; //для тачскрина
    private XYCellCoord _playerCell = new XYCellCoord { x = 3, y = 4 }; //текущие координаты игрока
    //private int _playerCellPosX = 3;
    //private int _playerCellPosY = 4; 
    private XYCellCoord _endCell = new XYCellCoord { x = 3, y = 4 }; //конечные координаты игрока

    public GameObject SCENESCRIPT;
    private Map_editor_stages _mapEditorStages;
    public int MaxCellHeight = 0;
    public int MaxCellWidth = 0;


    void Start()
    {
        _mapEditorStages = SCENESCRIPT.GetComponent<Map_editor_stages>();
        playerSpeed = 600f;
        touchOrigin = -Vector2.one;
    }


    void Update()
    {
        if (goUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (playerSpeed * Time.deltaTime));
            //взял формулу передвижения с инета. до этого была другая формула, но она не подходила
            if (transform.position.y >= endPosY) //когда дошли до конечной точки, то выравниваем позицию игрока. обычно, игрока сдвигается немного дальше
            {
                transform.position = new Vector3(transform.position.x, endPosY);
                goUp = false;
                _playerCell.y = _endCell.y;
            }
            return;
        }

        if (goDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (playerSpeed * Time.deltaTime));
            if (transform.position.y <= endPosY)
            {
                transform.position = new Vector3(transform.position.x, endPosY);
                goDown = false;
                _playerCell.y = _endCell.y;
            }
            return;
        }

        if (goLeft)
        {
            transform.position = new Vector3(transform.position.x - (playerSpeed * Time.deltaTime), transform.position.y);
            if (transform.position.x <= endPosX)
            {
                transform.position = new Vector3(endPosX, transform.position.y);
                goLeft = false;
                _playerCell.x = _endCell.x;
            }
            return;
        }

        if (goRight)
        {
            transform.position = new Vector3(transform.position.x + (playerSpeed * Time.deltaTime), transform.position.y);
            if (transform.position.x >= endPosX)
            {
                transform.position = new Vector3(endPosX, transform.position.y);
                goRight = false;
                _playerCell.x = _endCell.x;
            }
            return;
        }

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKey(KeyCode.W))
        {
            if (_endCell.y + 1 > MaxCellHeight)
                return;

            _endCell.y = _playerCell.y + 1;


            string cellName = "x." + _endCell.x + "_y." + _endCell.y;
            GetCellCoordAndSetEndPosition(cellName);
            //map_cell_script skyCell = _mapEditorStages.maps[Main.selected_map].cell[cellName];
            //endPosX = skyCell.transform.position.x;
            //endPosY = skyCell.transform.position.y;
            //transform.GetChild(0).GetComponent<Animator>().SetBool("up", true);//включение анимации
            goUp = true; //разрешение на взлёт 
            return;
        }


        if (Input.GetKey(KeyCode.S))
        {

            if (_endCell.y - 1 < 0)
                return;

            _endCell.y = _playerCell.y - 1;
            string cellName = "x." + _playerCell.x + "_y." + _endCell.y;
            GetCellCoordAndSetEndPosition(cellName);
            //map_cell_script bottomCell = _mapEditorStages.maps[Main.selected_map].cell[cellName];
            //endPosX = bottomCell.transform.position.x;
            //endPosY = bottomCell.transform.position.y;
            goDown = true;
            return;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_endCell.x - 1 < 0)
                return;
            _endCell.x = _playerCell.x - 1;
            string cellName = "x." + _endCell.x + "_y." + _playerCell.y;
            GetCellCoordAndSetEndPosition(cellName);
            //map_cell_script leftCell = _mapEditorStages.maps[Main.selected_map].cell[cellName];
            //endPosX = leftCell.transform.position.x;
            //endPosY = leftCell.transform.position.y;
            goLeft = true;
            return;
        }


        if (Input.GetKey(KeyCode.D))
        //&& (transform.position.x + step) < border)
        {
            if (_endCell.x + 1 > MaxCellWidth)
                return;
            _endCell.x = _playerCell.x + 1;
            string cellName = "x." + _endCell.x + "_y." + _playerCell.y;
            GetCellCoordAndSetEndPosition(cellName);
            //map_cell_script rightCell = _mapEditorStages.maps[Main.selected_map].cell[cellName];
            //endPosX = rightCell.transform.position.x;
            //endPosY = rightCell.transform.position.y;
            //transform.GetChild(0).GetComponent<Animator>().SetBool("right", true);
            goRight = true;
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else
                if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    Vector2 touchEnd = myTouch.position;
                    float x = touchEnd.x - touchOrigin.x;
                    float y = touchEnd.y - touchOrigin.y;
                    touchOrigin.x = -1;
                    if (Mathf.Abs(x) < Mathf.Abs(y))
                    {
                        if (y > 0 && (transform.position.y + step < border) && !gameOverPrefab)
                        {
                            goUp = true;
                            endPosX = transform.position.y + step;
                            return;
                        }
                        if (y < 0 && (transform.position.y - step > -border) && !gameOverPrefab)
                        {
                            goDown = true;
                            endPosX = transform.position.y - step;
                            return;
                        }
                    }
                    else
                    {
                        if (x > 0 && (transform.position.x + step < border) && !gameOverPrefab)
                        {
                            goRight = true;
                            endPosX = transform.position.x + step;
                            return;
                        }
                        if (x < 0 && (transform.position.x - step > -border) && !gameOverPrefab)
                        {
                            goLeft = true;
                            endPosX = transform.position.x - step;
                        }
                    }
                }
        }
#endif
    }


    private void GetCellCoordAndSetEndPosition(string cellName)
    {
        map_cell_script cell = _mapEditorStages.maps[Main.selected_map].cell[cellName];
        endPosX = cell.transform.position.x;
        endPosY = cell.transform.position.y;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gatherer")
            return;
        if (other.tag == "Monster Cell")
        {
            string cellName = "x." + _endCell.x + "_y." + _endCell.y;
            Bestiary_Data monterInfo = Main.db_data.bestiary_data[Main.Story_info["0"].Map_data[Main.selected_map].
                cell_info[cellName].cell_stuffing.id];
            Debug.Log(monterInfo.bestiary_name);
        }
        if (other.tag == "Not Passable Obj" || other.tag == "Resources")
        {
            _endCell = _playerCell;
            string cellName = "x." + _endCell.x + "_y." + _endCell.y;
            GetCellCoordAndSetEndPosition(cellName);
            if (goDown)
            {
                goDown = false;
                goUp = true;
            }
            else if (goUp)
            {
                goUp = false;
                goDown = true;
            }
            else if (goLeft)
            {
                goLeft = false;
                goRight = true;
            }
            else if (goRight)
            {
                goRight = false;
                goLeft = true;
            }
        }
        //Debug.Log("parent goes ouch!");
    }
}
