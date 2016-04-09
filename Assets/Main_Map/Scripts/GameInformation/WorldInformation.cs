using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using LitJson;

//Stores all the world information -- This is what is going to get loaded from online
public class WorldInformation : MonoBehaviour {
    public static string CurrentArea { get; set; }
    //paths
    //public static int[,] Edges = new int[10, 5] { { 2, 9, 0, 0, 0 }, { 1, 4, 0, 0, 0 }, { 4, 0, 0, 0, 0 }, { 2, 3, 5, 9, 0 }, { 4, 6, 7, 8, 0 }, { 5, 7, 0, 0, 0 }, { 5, 6, 0, 0, 0 }, { 5, 10, 0, 0, 0 }, { 1, 4, 10, 0, 0 }, { 8, 9, 0, 0, 0 } };
    public static int[,] Edges = new int[34, 5] { { 2, 3, 24, 34, 0 }, { 1, 3, 0, 0, 0 }, { 1, 2, 4, 0, 0 }, { 3, 5, 27, 0, 0 }, { 4, 6, 7, 0, 0 }, { 5, 7, 32, 33, 0 }, { 5, 6, 9, 10, 0 }, { 9, 11, 26, 0, 0 }, { 7, 6, 10, 11, 0 }, { 7, 9, 12, 13, 14 }, { 8, 9, 12, 29, 0 }, { 10, 11, 13, 0, 0 }, { 10, 12, 14, 0, 0 }, { 10, 13, 15, 0, 0 }, { 14, 16, 30, 0, 0 }, { 15, 17, 30, 31, 0 }, { 16, 18, 31, 0, 0 }, { 17, 19, 31, 0, 0 }, { 18, 20, 33, 0, 0 }, { 19, 21, 0, 0, 0 }, { 20, 22, 23, 0, 0 }, { 21, 23, 32, 33, 0 }, { 21, 22, 24, 25, 0 }, { 1, 23, 25, 34, 0 }, { 23, 24, 34, 0, 0 }, { 8, 27, 28, 0, 0 }, { 4,26, 28, 0, 0 }, { 26, 27, 0, 0, 0 }, { 11, 0, 0, 0, 0 }, { 15, 16, 0, 0, 0 }, { 16, 17, 18, 0, 0 }, { 6, 22, 33, 0, 0 }, { 6,19, 22, 32, 0 }, { 1, 24, 25, 0, 0 } };
    //total of 24 Areas


    private static string[] AreaNames = new string[34] { "Alnerwick", "Bardford", "Holden", "Ashborne", "Aramore", "Gilramore", "Tarmsworth", "Lancaster", "Shadowfen", "Jueht Fields", "Kiahs Grassland", "Cloud Prairie", "Strotyl Plateau", "Boa Valley", "Giant's Expanse", "Grasshopper Plains", "Mutolm Meadow", "Kicalt Fields", "Great Meadow", "Great Plains", "Sacred Grasslands", "Abandoned Fields", "Ruehn's Expanse", "Lazy Foot Gardens", "Gilivore Prairie", "Knife Range", "The Parched Fields", "The Angry Wilds", "The Red Sea", "Desert of Akrid", "Unresting Barrens", "Desolated Savanna", "The Sea of Fire", "The Wasteland" };
    //0-8 are cities, 9-25 are plains, 26-33 are deserts
    private static string[] AreaIDs = new string[34] { "000001", "000032", "000019", "000029", "000014", "000016", "000023", "000007", "000028", "000002", "000003", "000004", "000005", "000011", "000012", "000015", "000017", "000018", "000020", "000022", "000024", "000027", "000030", "000031", "000033", "000034", "000021", "000006", "000008", "000009", "000010", "000013", "000025", "000026" };

    private static string[] KingNames = new string[10] { "Sir", "Mr test a log", "DO", "Go harder", "LEGgoMYEggo", "More king names", "Beat dat ass", "That was wierd ", "keep[ing on", "hello?" };
    private string url = "http://localhost/361/SetWorldInformation.php";
    public static JsonData AreaData;

   

    public static List<Area> Areas { get; set; }
    public static Inventory shopInv = new Inventory();
    public static List<Quest> AvailableQuests { get; set; }
    public static int currentWorldID { get; set; }
    public static List<Kingdom> Kingdoms { get; set; }

    public static int DayCounter { get; set; }

    private static CreateNewWeapon WeaponCreator = new CreateNewWeapon();
    private static CreateNewEquipment EquipmentCreator = new CreateNewEquipment();
    private static CreateNewPotion PotionCreator = new CreateNewPotion();
    private static CreateNewQuest questCreator = new CreateNewQuest();
    private int areaNamer = 0;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void initAreas()
    {
        Areas = new List<Area>();
        for (int i = 0; i < 34; i++)
        {
            Areas.Add(initArea());
        }
        
    }

    private Area initArea()
    {
        //Gets this info from db
        //if it's a city then give it a name
        Area newArea = new Area();
        newArea.AreaID = AreaIDs[areaNamer];
        newArea.AreaName = AreaNames[areaNamer];
        if (areaNamer <= 8) newArea.AreaType = Area.AreaTypes.City;
        else if (areaNamer <= 25) newArea.AreaType = Area.AreaTypes.Plains;
        else newArea.AreaType = Area.AreaTypes.Desert;
        newArea.IconNumber = Int32.Parse(AreaIDs[areaNamer]);
        areaNamer++;
        return newArea;
    }

    private void printAreaInfo(Area a)
    {
        Debug.Log(a.AreaID + ". ------ . " + a.AreaName + ".---------." + a.AreaType.ToString());
    }

    void Start()
    {
        //AvailableQuests = new List<Quest>();
        //RenewShopInv();
        //LoadNewQuests();
        LoadInformation.LoadAllInformation();
        initShopsAndQuests();
        initAreas();
        Areas.ForEach(printAreaInfo);
        

        CurrentArea = "1";
        WWW www = new WWW(url);
        StartCoroutine(goDoIt(www));

    }

    IEnumerator goDoIt(WWW www)
    {
        yield return www;
        Debug.Log(www.text);
        AreaData = JsonMapper.ToObject(www.text);
        Debug.Log(AreaData[0]["name"]);
        //Debug.Log(AreaData[1]["name"]);
        //Debug.Log(AreaData[2]["name"]);
        //tester = www.text;
    }

    private void initShopsAndQuests()
    {
       AvailableQuests = new List<Quest>();
       LoadNewQuests();
       RenewShopInv();

    }


    //Day counter! 

    public static void LoadNewQuests()
    {
        //Loads new quests for the quest keep
        
        
        for (int i = 0; i<3; i++)
        {
            Debug.Log("Initiating quests in world info!");
            Debug.Log("Is there a problem with this? " + questCreator.returnQuest().QuestName);
            AvailableQuests.Add(questCreator.returnQuest());
        }
    }

    // Use this for initialization
    public static void RenewShopInv()
    {
       
        //add to database!!!!!! -------------------------------------------------->>>>>>>>>>>>>>

        List<BaseWeapon> WArr = new List<BaseWeapon>();
        List<BasePotion> PArr = new List<BasePotion>();
        List<BaseEquipment> EArr = new List<BaseEquipment>();

        for(int i = 0; i<5; i++)
        {
            WArr.Add( WeaponCreator.returnWeapon());
            PArr.Add(PotionCreator.returnPotion());
            EArr.Add(EquipmentCreator.returnEquipment());
        }

        shopInv.Equipment = EArr;
        shopInv.Potions = PArr;
        shopInv.Weapons = WArr;

    }

    

    

    // Update is called once per frame
    void Update () {
	
	}
}
