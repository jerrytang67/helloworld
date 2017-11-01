using System.Collections.Generic;

namespace CPUZ.Model
{
    public class JSON_DATA
    {
        public List<Camera> camera { get; set; } = new List<Camera>();
        public List<Item> items { get; set; } = new List<Item>();
        public List<Players> players { get; set; } = new List<Players>();
        public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();
        public List<Zone> zone { get; set; } = new List<Zone>();
    }

    public class Camera
    {
        public string n { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    public class Item
    {
        public string n { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public float rx { get; set; }
        public float ry { get; set; }
        public float rz { get; set; }


    }

    public class Players
    {
        public string AName { get; set; }
        public int AID { get; set; }
        public int id { get; set; }
        public float health { get; set; }

        public int isInactive { get; set; }

        public float rotator { get; set; }

        public int t { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float rx { get; set; }
        public float ry { get; set; }
        public float rz { get; set; }

        public ulong mesh { get; set; }
    }

    public class Vehicle
    {
        public string v { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public float rx { get; set; }
        public float ry { get; set; }
        public float rz { get; set; }
    }

    public class Zone
    {
        public float r { get; set; }
        public float x { get; set; }
        public float y { get; set; }
    }

}