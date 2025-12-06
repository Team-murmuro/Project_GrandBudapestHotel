using System;
using System.Collections.Generic;

namespace Utils.ClassUtility
{
    // 플레이어 데이터 구조
    [Serializable]
    public class PlayerList
    {
        public List<PlayerData> Players;
    }

    [Serializable]
    public class PlayerData
    {
        public int id;
        public int level;
        public string name;
        public string gender;
        public float moveSpeed;
    }

    // 손님 데이터 구조
    [Serializable]
    public class CustomerList
    {
        public List<CustomerData> Customers;
    }

    [Serializable]
    public class CustomerData
    {
        public int id;
        public string name;
        public string gender;
        public float moveSpeed;
        public string personality;
        public int stress;
        public int satisfaction;
        public string roomID;
        public string checkIn_Time;
    }

    // 방 데이터 구조
    [Serializable]
    public class RoomList
    {
        public List<RoomData> Rooms;
    }

    [Serializable]
    public class RoomData
    {
        public int id;
        public string name;
        public int cleanliness;
        public bool isOccupied;
    }
}
