using System.Collections.Generic;

namespace ESP.Model
{
    public class JSON_DATA
    {
        public List<Camera> camera { get; set; }
        public List<Item> items { get; set; }
        public List<Players> players { get; set; }
        public List<Vehicle> vehicles { get; set; }
        public List<Zone> zone { get; set; }
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