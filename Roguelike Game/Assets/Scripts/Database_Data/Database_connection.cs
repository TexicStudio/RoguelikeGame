using UnityEngine;
using System.Collections;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database_connection
{
    public string database_name = "Roguelike.db";
    public string database_address = "/Database/";

    private string connection_address;

    private string sql_text;

    private IDbConnection dbconnection;
    private IDbCommand dbcommand;
    private IDataReader sql_data;

    private bool dbcommand_exist = false;

    public void Connection()
    {
        connection_address = "URI=file:" + Application.dataPath + database_address + database_name;

        dbconnection = (IDbConnection)new SqliteConnection(connection_address);
        dbconnection.Open(); //Open connection to the database.
    }

    public IDataReader SQL_Query(string text)
    {
        if (dbcommand_exist == true)
        {
            dbcommand_exist = false;

            dbcommand.Dispose();
            dbcommand = null;

            sql_data.Close();
            sql_data = null;
        }

        dbcommand_exist = true;

        dbcommand = dbconnection.CreateCommand();

        sql_text = text;

        dbcommand.CommandText = sql_text;

        sql_data = dbcommand.ExecuteReader();
        return sql_data;
    }

    public void SQL_Insert_map(Map_list_Data map_data)
    {
        if (dbcommand_exist == true)
        {
            dbcommand_exist = false;

            dbcommand.Dispose();
            dbcommand = null;

            sql_data.Close();
            sql_data = null;
        }

        dbcommand_exist = true;

        dbcommand = dbconnection.CreateCommand();

        sql_text = "INSERT INTO map (id, type_id, map_name, file_name) VALUES (@param1 , @param2, @param3, @param4)";

        dbcommand.CommandText = sql_text;

        SqliteParameter param1 = new SqliteParameter("@param1", map_data.id);
        dbcommand.Parameters.Add(param1);

        SqliteParameter param2 = new SqliteParameter("@param2", map_data.type_id);
        dbcommand.Parameters.Add(param2);

        SqliteParameter param3 = new SqliteParameter("@param3", map_data.map_name);
        dbcommand.Parameters.Add(param3);

        SqliteParameter param4 = new SqliteParameter("@param4", map_data.file_name);
        dbcommand.Parameters.Add(param4);

        dbcommand.ExecuteNonQuery();


        dbcommand_exist = false;

        dbcommand.Dispose();
        dbcommand = null;

        dbconnection.Close();
        dbconnection = null;
    }

    public void Connection_close()
    {
        if (dbcommand_exist == true)
        {
            dbcommand_exist = false;

            sql_data.Close();
            sql_data = null;

            dbcommand.Dispose();
            dbcommand = null;
        }

        dbconnection.Close();
        dbconnection = null;
    }
}
