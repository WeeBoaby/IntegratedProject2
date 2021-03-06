﻿// resources used;  http://answers.unity3d.com/questions/323195/how-can-i-have-a-static-class-i-can-access-from-an.html
// 					http://unitygems.com/saving-data-1-remember-me/
//					http://www.dotnetperls.com/list
//					http://www.dotnetperls.com/enum
//					http://support.microsoft.com/kb/816149
//					http://stackoverflow.com/questions/1968328/read-numbers-from-a-text-file-in-c-sharp
//					http://stackoverflow.com/questions/29482/cast-int-to-enum-in-c-sharp

//You must include these namespaces
//to use BinaryFormatter
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class DataStore : MonoBehaviour
{
    //core stuff, don't reset these
    public static DataStore DT;
    public GUISkin skin;
    public static Dictionary<string, string> NPCText = new Dictionary<string, string>();
    public static Dictionary<string, bool> DoorsOpened = new Dictionary<string, bool>();
    public enum Gender { male, female };

    //duno' what's this
    public static Dictionary<string, int> HUBBuildings = new Dictionary<string, int>();
    public int NPCCount;

    //player relevant stuff, needs to be reseted when starting new game
    public int score;
    public string PlayerProgress = "";
    public string checkpoint = "ClickSign";
    public string PlayerName = "";
    public Gender PlayerGender = Gender.male; //defaults to male
    public static Dictionary<string, short> PlayerInventory = new Dictionary<string, short>();




    void Awake()
    {
        if (DT != null)
            GameObject.Destroy(DT);
        else
            DT = this;

        DontDestroyOnLoad(this);
    }



    //save all stuff to file, line by line

    public void SaveToFile()
    {

        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "//" + "SaveGame_2" + ".save");


        sw.WriteLine(score);
        sw.WriteLine(PlayerProgress);
        sw.WriteLine(checkpoint);
        sw.WriteLine(PlayerName);
        sw.WriteLine(PlayerGender);
        sw.WriteLine(DataStore.PlayerInventory["can"]);
        sw.WriteLine(DataStore.PlayerInventory["bottle"]);
        sw.WriteLine(DataStore.PlayerInventory["paper"]);
        sw.WriteLine(DataStore.PlayerInventory["keys"]);
        sw.WriteLine(DataStore.PlayerInventory["hammer"]);
        sw.WriteLine(DataStore.PlayerInventory["paintbrush"]);
        sw.WriteLine(DataStore.PlayerInventory["trowel"]);
        sw.WriteLine(DataStore.HUBBuildings["gardens"]);
        sw.WriteLine(DataStore.HUBBuildings["decorations"]);
        sw.WriteLine(DataStore.HUBBuildings["houses"]);
        sw.WriteLine(DataStore.DoorsOpened["Door1"]);
        sw.WriteLine(DataStore.DoorsOpened["Door2"]);
        sw.WriteLine(DataStore.DoorsOpened["Door3"]);
        sw.WriteLine(DataStore.DoorsOpened["Door4"]);
        sw.WriteLine(DataStore.DoorsOpened["Door5"]);
        sw.WriteLine(DataStore.DoorsOpened["Door6"]);
        //Close the file
        sw.Close();
    }

    //load all data back from file	
    public void LoadFromFile()
    {
        using (TextReader reader = File.OpenText(Application.persistentDataPath + "//" + "SaveGame_2" + ".save"))
        {
            DataStore.DT.score = int.Parse(reader.ReadLine());
            DataStore.DT.PlayerProgress = reader.ReadLine();
            DataStore.DT.checkpoint = reader.ReadLine();
            DataStore.DT.PlayerName = reader.ReadLine();
            DataStore.DT.PlayerGender = (Gender)Enum.Parse(typeof(Gender), reader.ReadLine());
            DataStore.PlayerInventory["can"] = short.Parse(reader.ReadLine());
            DataStore.PlayerInventory["bottle"] = short.Parse(reader.ReadLine());
            DataStore.PlayerInventory["paper"] = short.Parse(reader.ReadLine());
            DataStore.PlayerInventory["keys"] = short.Parse(reader.ReadLine());
            DataStore.PlayerInventory["hammer"] = short.Parse(reader.ReadLine());
            DataStore.PlayerInventory["paintbrush"] = short.Parse(reader.ReadLine());
            DataStore.PlayerInventory["trowel"] = short.Parse(reader.ReadLine());
            DataStore.HUBBuildings["gardens"] = short.Parse(reader.ReadLine());
            DataStore.HUBBuildings["decorations"] = short.Parse(reader.ReadLine());
            DataStore.HUBBuildings["houses"] = short.Parse(reader.ReadLine());
            DataStore.DoorsOpened["Door1"] = bool.Parse(reader.ReadLine());
            DataStore.DoorsOpened["Door2"] = bool.Parse(reader.ReadLine());
            DataStore.DoorsOpened["Door3"] = bool.Parse(reader.ReadLine());
            DataStore.DoorsOpened["Door4"] = bool.Parse(reader.ReadLine());
            DataStore.DoorsOpened["Door5"] = bool.Parse(reader.ReadLine());
            DataStore.DoorsOpened["Door6"] = bool.Parse(reader.ReadLine());
        }

        Inventory.can = DataStore.PlayerInventory["can"];
        Inventory.bottle = DataStore.PlayerInventory["bottle"];
        Inventory.paper = DataStore.PlayerInventory["paper"];
        Inventory.keys = DataStore.PlayerInventory["keys"];
        Debug.Log(DataStore.PlayerInventory["paper"]);

    }

    public void ResetPlayer()
    {

        //reset player relevant values in DT

        DataStore.DT.score = 0;
        DataStore.DT.PlayerProgress = "";
        DataStore.DT.checkpoint = "ClickSign"; //THIS SHOULD BE CHANGED TO THE VERY FIRST, DEFAULT CHECKPOINT
        DataStore.DT.PlayerName = "";
        DataStore.DT.PlayerGender = DataStore.Gender.male; //defaults to male

        DataStore.PlayerInventory["can"] = 0;
        DataStore.PlayerInventory["bottle"] = 0;
        DataStore.PlayerInventory["paper"] = 0;
        DataStore.PlayerInventory["keys"] = 0;
        DataStore.PlayerInventory["hammer"] = 0;
        DataStore.PlayerInventory["trowel"] = 0;
        DataStore.PlayerInventory["paintbrush"] = 0;


        DataStore.DoorsOpened["Door1"] = true;
        DataStore.DoorsOpened["Door2"] = true;
        DataStore.DoorsOpened["Door3"] = true;
        DataStore.DoorsOpened["Door4"] = true;
        DataStore.DoorsOpened["Door5"] = true;
        DataStore.DoorsOpened["Door6"] = true;

        DataStore.HUBBuildings["gardens"] = 0;
        DataStore.HUBBuildings["houses"] = 0;
        DataStore.HUBBuildings["decorations"] = 0;

        //sync this with the current inventory

        Inventory.can = DataStore.PlayerInventory["can"];
        Inventory.bottle = DataStore.PlayerInventory["bottle"];
        Inventory.paper = DataStore.PlayerInventory["paper"];
        Inventory.keys = DataStore.PlayerInventory["keys"];
        Inventory.hammer = DataStore.PlayerInventory["hammer"];
        Inventory.trowel = DataStore.PlayerInventory["trowel"];
        Inventory.paintbrush = DataStore.PlayerInventory["paintbrush"];



    }
    //method used to sync the dt data with the current game state data
   public void SyncDataWithDT()
    {
        DataStore.PlayerInventory["can"] = Inventory.can;
        DataStore.PlayerInventory["bottle"] = Inventory.bottle;
        DataStore.PlayerInventory["paper"] = Inventory.paper;
        DataStore.PlayerInventory["keys"] = Inventory.keys;
        DataStore.PlayerInventory["hammer"] = Inventory.hammer;
        DataStore.PlayerInventory["trowel"] = Inventory.trowel;
        DataStore.PlayerInventory["paintbrush"] = Inventory.paintbrush;

        DataStore.DT.SaveToFile();
    }

    /* NOT IN USE any longer
        public void SavaToFile0(string fileName)
            {
            Debug.Log("save methood begins");
                //Get a binary formatter
                var b = new BinaryFormatter();
                //Create a file
                var f = File.Create(Application.persistentDataPath + "/" + fileName + ".dat");
                //Save player relevant date			
                //b.Serialize(f, source);

                        b.Serialize(f,score);
                        b.Serialize(f,PlayerProgress);
                        b.Serialize(f,checkpoint);
                        b.Serialize(f,PlayerName);
                        b.Serialize (f, PlayerGender);
                        b.Serialize (f, PlayerInventory);
			

                //close file
                f.Close();
            Debug.Log("save method ended");
            }




        //load data back from file
        public object LoadData(string objectName)
        {
            var output = new object();

            Debug.Log("load methood begins");

            //Binary formatter for loading back
                var b = new BinaryFormatter();
            //Get the file
                var f = File.Open(Application.persistentDataPath + "/" + objectName + ".dat", FileMode.Open);
            //Load back the scores
            output = b.Deserialize(f);
                f.Close();
            Debug.Log("load methood ends");
            return output;		
        }

    */
}