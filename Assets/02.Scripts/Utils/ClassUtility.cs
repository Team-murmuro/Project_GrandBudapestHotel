using System;
using System.Collections.Generic;

namespace Utils.ClassUtility
{
    public class CustomerDataList
    {
        public List<CustomerData> Customers;
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

    [Serializable]
    public class RoomData
    {
        public int id;
        public string name;
        public int cleanliness;
        public bool isOccupied;
    }
}
