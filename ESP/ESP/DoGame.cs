using System;
using System.Collections.Generic;
using CPUZ.Model;

namespace CPUZ
{
    public static class DoGame
    {
        const int PROTO_NORMAL_READ = 0;

        public static ulong baseAdd;

        public static List<String> vehicleGNameVec = new List<string> { "Uaz", "Buggy", "Dacia", "ABP_Motorbike", "BP_Motorbike", "Boat_PG117" };
        public static List<String> DropName = new List<string> { "DroppedItemGroup", "DroppedItemInteractionComponent" };

        public static Dictionary<String, string> dropGNameMap = new Dictionary<string, string>
        {
            {"Item_Head_G_01_Lv3_C", "Helm3"},
            {"Item_Head_G_01_Lv3_", "Helm3"},
            {"Item_Armor_C_01_Lv3", "Vest3"},
            {"Item_Head_F_02_Lv2_C", "Helm2"},
            {"Item_Armor_C_01_Lv3_C", "Vest3"},
            {"Item_Armor_D_01_Lv2_C", "Vest2"},
            {"Item_Equip_Armor_Lv3_C", "Vest3"},
            {"Item_Equip_Armor_Lv3", "Vest3"},
            {"Item_Attach_Weapon_Muzzle_Suppressor_SniperRifle", "Supp(SR)"},
            {"Item_Attach_Weapon_Muzzle_Suppressor_Large", "Supp(AR)"},
            {"Item_Attach_Weapon_Muzzle_Suppressor_Large_C", "Supp(SR)"},
            {"Item_Heal_MedKit", "Meds"},
            {"Item_Heal_FirstAid", "Meds"},
            {"Item_Weapon_Kar98k", "kar98"},
            {"Item_Weapon_Mini14", "mini"},
            {"Item_Weapon_M16A4", "M16"},
            {"Item_Weapon_HK416", "m416"},
            {"Item_Weapon_SCAR-L", "SCAR"},
            {"Item_Weapon_SKS", "sks"},
            {"Item_Attach_Weapon_Upper_ACOG_01", "4x"},
            {"Item_Attach_Weapon_Upper_CQBSS", "8x"},
            {"Item_Attach_Weapon_Upper_CQBSS_C", "8x"},
            {"Item_Boost_PainKiller_C", "YAO"},
            {"Item_Boost_EnergyDrink_C", "YAO"},
            {"Item_Weapon_Pan_C","PAN" }
        };


        static DoGame()
        {
            baseAdd = KReader.readPuBase();
        }
        static bool IsDropGroup(string name)
        {
            foreach (var g in DropName)
            {
                if (name.IndexOf(g, StringComparison.Ordinal) > -1)
                {
                    return true;
                }
            }
            return false;
        }


        static string IsDropItem(string name)
        {
            foreach (var g in dropGNameMap)
            {
                if (name.IndexOf(g.Key, StringComparison.Ordinal) > -1)
                {
                    return g.Value;
                }
            }
            return "";
        }

        static string IsVehicle(string name)
        {
            foreach (var g in vehicleGNameVec)
            {
                if (name.IndexOf(g, StringComparison.Ordinal) > -1)
                {
                    return g;
                }
            }
            return "";
        }

        public static JSON_DATA getGameDate()
        {
            if (baseAdd == 0)
                return null;
            JSON_DATA json = new JSON_DATA
            {
                players = new List<Players>(),
                camera = new List<Camera>(),
                vehicles = new List<Vehicle>(),
                items = new List<Item>(),
                zone = new List<Zone>()
            };

            var m_UWorld = KReader.readUlong(KReader.m_PUBase + 0x37E5988);
            var m_gameInstance = KReader.readUlong(m_UWorld + 0x140);
            var m_ULocalPlayer = KReader.readUlong(m_gameInstance + 0x38);
            var m_localPlayer = KReader.readUlong(m_ULocalPlayer + 0x0);
            var m_viewportclient = KReader.readUlong(m_localPlayer + 0x58);
            var m_localPawn = KReader.readUlong(m_localPlayer + 0x3A8);
            var m_localPlayerState = KReader.readUlong(m_localPawn + 0x03C0);
            var m_PWorld = KReader.readUlong(m_viewportclient + 0x80);
            var m_ULevel = KReader.readUlong(m_PWorld + 0x30);
            var m_playerCount = KReader.readInt32(m_ULevel + 0xA8);

            var m_localPlayerControl = KReader.readUlong(m_localPlayer + 0x30, PROTO_NORMAL_READ);
            var m_localPlayerCamerManager = KReader.readUlong(m_localPlayerControl + 0x438, PROTO_NORMAL_READ);


            var ATslGameState = KReader.readUlong(m_PWorld + 0x00F8, PROTO_NORMAL_READ);
            var m_localPlayerPosition = KReader.readVec(m_localPlayer + 0x70, PROTO_NORMAL_READ);
            var m_localPlayerBasePointer = KReader.readUlong(m_localPlayer, PROTO_NORMAL_READ);
            var m_localTeam = KReader.readUlong(m_localPlayerState + 0x0444, PROTO_NORMAL_READ);
            var m_AActorPtr = KReader.readUlong(m_ULevel + 0xA0, PROTO_NORMAL_READ);


            var povRotation = KReader.readVec(m_localPlayerCamerManager + 0x42C, PROTO_NORMAL_READ);
            var povLocation = KReader.readVec(m_localPlayerCamerManager + 0x420, PROTO_NORMAL_READ);
            var Fov = KReader.readFloat(m_localPlayerCamerManager + 0x438, PROTO_NORMAL_READ);


            //get zone info
            Vector3 blue = KReader.readVec(ATslGameState + 0x440, PROTO_NORMAL_READ);
            float blueR = KReader.readFloat(ATslGameState + 0x44C, PROTO_NORMAL_READ);

            Vector3 white = KReader.readVec(ATslGameState + 0x450, PROTO_NORMAL_READ);
            float whiteR = KReader.readFloat(ATslGameState + 0x45C, PROTO_NORMAL_READ);

            json.camera.Add(new Camera { n = "Rotation", X = povRotation.X, Y = povRotation.Y, Z = povRotation.Z });
            json.camera.Add(new Camera { n = "Location", X = povLocation.X, Y = povLocation.Y, Z = povLocation.Z });
            json.camera.Add(new Camera { n = "Fov", X = Fov, Y = 0, Z = 0 });

            json.zone.Add(new Zone { r = blueR, x = blue.X, y = blue.Y });
            json.zone.Add(new Zone { r = whiteR, x = white.X, y = white.Y });


            List<string> a = new List<string>();

            for (int i = 0; i < m_playerCount; i++)
            {
                var curActor = KReader.readUlong(m_AActorPtr + (ulong)(i * 0x8), PROTO_NORMAL_READ);
                var curActorID = KReader.readInt32(curActor + 0x0018, PROTO_NORMAL_READ);

                string actorGName = KReader.getGNameFromId(curActorID);


                //is player
                if (curActorID == 66943 || curActorID == 66948)
                {
                    var rootCmpPtr = KReader.readUlong(curActor + 0x180, PROTO_NORMAL_READ);
                    var playerState = KReader.readUlong(curActor + 0x3C0, PROTO_NORMAL_READ);
                    Vector3 actorLocation = KReader.readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

                    var actorTeam = KReader.readInt32(playerState + 0x0444, PROTO_NORMAL_READ);

                    actorLocation.X += KReader.readInt32(m_PWorld + 0x918, PROTO_NORMAL_READ);
                    actorLocation.Y += KReader.readInt32(m_PWorld + 0x91C, PROTO_NORMAL_READ);
                    actorLocation.Z += KReader.readInt32(m_PWorld + 0x920, PROTO_NORMAL_READ);

                    var Health = Convert.ToInt32(KReader.readFloat(curActor + 0x107C, PROTO_NORMAL_READ));
                    var relativeLocation = KReader.readVec(rootCmpPtr + 0x01E0, PROTO_NORMAL_READ);
                    var RelativeRotation = KReader.readVec(rootCmpPtr + 0x01EC, PROTO_NORMAL_READ);
                    var playerId = KReader.readInt32(playerState + 0x03C8, PROTO_NORMAL_READ);
                    var isInactive = KReader.readChar(playerState + 0x03CC, PROTO_NORMAL_READ);

                    json.players.Add(new Players
                    {
                        //AName = actorGName,
                        //AID = curActorID,
                        x = actorLocation.X,
                        y = actorLocation.Y,
                        z = actorLocation.Z,
                        health = Health,
                        id = playerId,
                        rotator = RelativeRotation.Y,
                        t = actorTeam,
                        isInactive = isInactive,
                        rx = relativeLocation.X,
                        ry = relativeLocation.Y,
                        rz = relativeLocation.Z
                    });
                }
                else if (!string.IsNullOrEmpty(IsVehicle(actorGName)))
                {
                    var rootCmpPtr = KReader.readUlong(curActor + 0x180, PROTO_NORMAL_READ);
                    Vector3 actorLocation = KReader.readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

                    actorLocation.X += KReader.readInt32(m_PWorld + 0x918, PROTO_NORMAL_READ);
                    actorLocation.Y += KReader.readInt32(m_PWorld + 0x91C, PROTO_NORMAL_READ);
                    actorLocation.Z += KReader.readInt32(m_PWorld + 0x920, PROTO_NORMAL_READ);
                    Vector3 relativeLocation = KReader.readVec(rootCmpPtr + 0x01E0, PROTO_NORMAL_READ);

                    json.vehicles.Add(new Vehicle
                    {
                        v = actorGName.Substring(0, 4),
                        x = actorLocation.X,
                        y = actorLocation.Y,
                        z = actorLocation.Z,
                        rx = relativeLocation.X,
                        ry = relativeLocation.Y,
                        rz = relativeLocation.Z,
                    });
                }
                else if (IsDropGroup(actorGName))
                {
                    var rootCmpPtr = KReader.readUlong(curActor + 0x180, PROTO_NORMAL_READ);
                    Vector3 actorLocation = KReader.readVec(rootCmpPtr + 0x1A0, PROTO_NORMAL_READ);

                    var DroppedItemArray = KReader.readUlong(curActor + 0x2D8, PROTO_NORMAL_READ);
                    var DroppedItemCount = KReader.readInt32(curActor + 0x2E0, PROTO_NORMAL_READ);

                    for (int j = 0; j < DroppedItemCount; j++)
                    {
                        var ADroppedItem = KReader.readUlong(DroppedItemArray + (ulong)(j * 0x10), PROTO_NORMAL_READ);
                        Vector3 droppedLocation = KReader.readVec(ADroppedItem + 0x01E0, PROTO_NORMAL_READ);
                        droppedLocation.X = droppedLocation.X + actorLocation.X + KReader.readInt32(m_PWorld + 0x918, PROTO_NORMAL_READ);
                        droppedLocation.Y = droppedLocation.Y + actorLocation.Y + KReader.readInt32(m_PWorld + 0x91C, PROTO_NORMAL_READ);
                        droppedLocation.Z = droppedLocation.Z + actorLocation.Z + KReader.readInt32(m_PWorld + 0x920, PROTO_NORMAL_READ);

                        var UItem = KReader.readUlong(ADroppedItem + 0x0448, PROTO_NORMAL_READ);
                        var UItemID = KReader.readInt32(UItem + 0x18, PROTO_NORMAL_READ);
                        var itemName = KReader.getGNameFromId(UItemID);

                        if (!string.IsNullOrEmpty(IsDropItem(itemName)))
                        {
                            Vector3 relativeLocation = KReader.readVec(rootCmpPtr + 0x01E0, PROTO_NORMAL_READ);

                            a.Add(itemName);
                            json.items.Add(new Item
                            {
                                n = IsDropItem(itemName),
                                x = droppedLocation.X,
                                y = droppedLocation.Y,
                                z = droppedLocation.Z,
                                rx = relativeLocation.X,
                                ry = relativeLocation.Y,
                                rz = relativeLocation.Z
                            });
                        }




                    }


                }
            }
            return json;



            //80472,80468,80464,,Bug
            //81001,BP_
            //81523 UAZ
            //80376 BOA
            //80829 DAZ
        }

    }
}