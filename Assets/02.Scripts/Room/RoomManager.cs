using UnityEngine;
using System.Linq;
using Utils.EnumType;
using Utils.ClassUtility;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    private static RoomManager instance;
    public static RoomManager Instance {  get { return instance; } }

    public List<RoomData> roomData;
    public List<Room> parlors = new List<Room>();   // 맵에 있는 모든 객실
    public List<Room> facilitys = new List<Room>(); // 맵에 있는 모든 시설

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        roomData = DataManager.Instance.LoadJson<RoomList>(DataManager.Instance.roomDataFileName).Rooms;
        List<Room> rooms = GameObject.FindGameObjectsWithTag("Room").Select(obj => obj.GetComponent<Room>()).ToList();

        int parlorsNum = 0;
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].roomType == ZoneType.Parlor)
            {
                parlors.Add(rooms[i]);
                parlors[parlorsNum].roomData = roomData[parlorsNum];
                parlorsNum++;
            }
            else if (rooms[i].roomType == ZoneType.Facility)
            {
                facilitys.Add(rooms[i]);
            }
        }
    }

    // 방 할당
    public void AssignRoom(CustomerController _customer)
    {
        for(int i = 0; i < parlors.Count; i++)
        {
            if (!parlors[i].roomData.isOccupied)
            {
                CustomerManager.Instance.customers.Add(_customer);
                CustomerManager.Instance.InfomationLineMove();
                parlors[i].roomData.isOccupied = true;
                _customer.SetRoom(parlors[i]);
                break;
            }
        }
    }
}