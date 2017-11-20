using System;
using System.Collections.Generic;
using System.Linq;
using CPUZ.Model;
using SharpDX;
using Vector3 = CPUZ.Model.Vector3;

namespace CPUZ
{
    public static class DoGame
    {
        private static ulong baseAdd;

        private static List<string> playerGName = new List<string> { "PlayerMale", "PlayerFemale" };

        private static readonly List<string> vehicleGNameVec =
            new List<string> { "Uaz", "Buggy", "Dacia", "ABP_Motorbike", "BP_Motorbike", "Boat_PG117" };

        private static readonly List<string> DropName =
            new List<string> { "DroppedItemGroup", "DroppedItemInteractionComponent" };

        private static readonly List<string> AirdropOrDeathdrop = new List<string>
        {
            "DeathDropItemPackage",
            "Carapackage_RedBox_C",
            "CarePackage",
            "AircraftCarePackage"
        };


        //地图需要标记物品列表
        private static Dictionary<string, string> careItemList = new Dictionary<string, string>
        {
            {"Item_Head_G_01_Lv3_C", "三级头"},
            {"Item_Head_G_01_Lv3_", "三级头"},
            {"Item_Armor_C_01_Lv3", "Vest3"},
            {"Item_Armor_C_01_Lv3_C", "三级甲"},
            {"Item_Equip_Armor_Lv3", "三级甲"},
            {"Item_Head_F_01_Lv2_C","Helm2" },
            {"Item_Head_F_02_Lv2_C", "Helm2"},
            {"Item_Armor_D_01_Lv2_C", "Vest2"},
            {"Item_Equip_Armor_Lv2_C","Vest2" },
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
            {"Item_Back_C_02_Lv3","三级包" },
            {"Item_Attach_Weapon_Magazine_ExtendedQuickDraw_Large_C","快扩" }
        };

        static DoGame()
        {
            while (baseAdd == 0)
            {
                baseAdd = KReader.readPuBase();
            }
        }

        static string InCareList(string name)
        {
            foreach (var g in careItemList)
            {
                if (name.IndexOf(g.Key, StringComparison.Ordinal) > -1)
                {
                    return g.Value;
                }
            }
            return "";
        }

        public static bool isDropItems(string ActorName) => DropName.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
        public static bool isAirdropOrDeathdrop(string ActorName) => AirdropOrDeathdrop.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
        public static bool isPlayer(string ActorName) => playerGName.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
        public static bool isVehicle(string ActorName) => vehicleGNameVec.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);

        public static FTransform GetBoneWithIndex(ulong mesh, Bones bone)
        {
            var cachedBoneSpaceTransforms = KReader.readUlong(mesh + 0x0790);
            return KReader.readFtransform(cachedBoneSpaceTransforms + (ulong)((int)bone * 0x30));
        }

        public static Vector3 GetBoneWithRotation(ulong mesh, Bones bone)
        {
            var fbone = GetBoneWithIndex(mesh, bone);
            var componentToWorld = KReader.readFtransform(mesh + 0x0190);
            var matrix = Matrix.Multiply(fbone.ToMatrixWithScale(), componentToWorld.ToMatrixWithScale());
            return new Model.Vector3(matrix.M41, matrix.M42, matrix.M43);
        }

        public static JSON_DATA getGameDate()
        {
            if (baseAdd == 0)
                return null;
            var json = new JSON_DATA();

            if (Setting.模式 == 模式.模式一)
            {
                #region mode1
                var pubg = new Pubg(baseAdd + Setting.UWorldOffset);
                var uworld = pubg.UWorld;
                var gameInstance = uworld.OwningGameInstance;
                var localPlayers = gameInstance.LocalPlayers;
                var localPlayer = localPlayers[0];
                var viewportClient = localPlayer.ViewportClient;
                var pWorld = viewportClient.UWorld;

                for (int i = 0; i < pWorld.PersistentLevel.PlayerCount; i++)
                {
                    var actor = pWorld.PersistentLevel.AActors[i];
                    var actorId = actor.ActorID;
                    var actorGName = Cache.GetGName(actorId);// KReader.getGNameFromId(actorId);
                    var rootComponent = actor.RootComponent;
                    var actorLocation = rootComponent.Location;
                    //玩家
                    if (actor.isPlayer)
                    {
                        var playerState = actor.PlayerState;
                        var _actorLocation = actorLocation + pWorld.WorldLocation;

                        json.players.Add(new Players
                        {
                            //AName = actorGName,
                            AName = playerState.PlayerName,
                            AID = actorId,
                            x = _actorLocation.X,
                            y = _actorLocation.Y,
                            z = _actorLocation.Z,
                            health = actor.Health,
                            id = playerState.PlayerId,
                            rotator = rootComponent.RelativeRotation.Y,
                            t = playerState.TeamNumber,
                            isInactive = playerState.bIsInactive,
                            rx = rootComponent.RelativeLocation.X,
                            ry = rootComponent.RelativeLocation.Y,
                            rz = rootComponent.RelativeLocation.Z,
                            mesh = actor.Mesh.mesh
                        });
                    }
                    //车辆
                    else if (actor.isVehicle)
                    {
                        var _actorLocation = actorLocation + pWorld.WorldLocation;
                        json.vehicles.Add(new Vehicle
                        {
                            v = actorGName.Substring(0, 4),
                            x = _actorLocation.X,
                            y = _actorLocation.Y,
                            z = _actorLocation.Z,
                            rx = rootComponent.RelativeLocation.X,
                            ry = rootComponent.RelativeLocation.Y,
                            rz = rootComponent.RelativeLocation.Z,
                        });
                    }
                    //物品
                    else if (actor.isDropItems)
                    {
                        var droppedItemArray = actor.DropItems;
                        var droppedItemCount = actor.DropItemsCount;
                        for (var j = 0; j < droppedItemCount; j++)
                        {
                            var aDroppedItem = droppedItemArray[j];
                            var UItem = aDroppedItem.UItem;
                            //var UItemID = UItem.ItemId;
                            var itemName = UItem.ItemName;
                            if (string.IsNullOrEmpty(InCareList(itemName))) continue;
                            var droppedLocation = aDroppedItem.Location;
                            var relativeLocation = droppedLocation + actorLocation;
                            droppedLocation += actorLocation + pWorld.WorldLocation;
                            //a.Add(itemName);
                            json.items.Add(new Item
                            {
                                n = InCareList(itemName),
                                x = droppedLocation.X,
                                y = droppedLocation.Y,
                                z = droppedLocation.Z,
                                rx = relativeLocation.X,
                                ry = relativeLocation.Y,
                                rz = relativeLocation.Z
                            });
                        }
                    }
                    else if (actor.isAirdropOrDeathdrop)
                    {
                        //var droppedItemArray = actor.AItemPackages;
                        //var droppedItemCount = actor.AItemPackagesCount;
                        //for (var j = 0; j < droppedItemCount; j++)
                        //{
                        //    var dropItem = droppedItemArray[j];
                        var _actorLocation = actorLocation + pWorld.WorldLocation;

                        json.vehicles.Add(new Vehicle
                        {
                            v = actor.ActorName.Substring(0, 4),
                            x = _actorLocation.X,
                            y = _actorLocation.Y,
                            z = _actorLocation.Z,
                            rx = rootComponent.RelativeLocation.X,
                            ry = rootComponent.RelativeLocation.Y,
                            rz = rootComponent.RelativeLocation.Z
                        });
                        //}
                    }
                }

                var povRotation = localPlayer.PlayerController.PlayerCameraManager.povRotation;
                var povLocation = localPlayer.PlayerController.PlayerCameraManager.povLocation;
                //get zone info
                json.camera.Add(new Camera { n = "Rotation", X = povRotation.X, Y = povRotation.Y, Z = povRotation.Z });
                json.camera.Add(new Camera { n = "Location", X = povLocation.X, Y = povLocation.Y, Z = povLocation.Z });
                json.camera.Add(new Camera { n = "Fov", X = localPlayer.PlayerController.PlayerCameraManager.Fov, Y = 0, Z = 0 });
                json.zone.Add(new Zone { r = pWorld.GameState.blueR, x = pWorld.GameState.blue.X, y = pWorld.GameState.blue.Y });
                json.zone.Add(new Zone { r = pWorld.GameState.whiteR, x = pWorld.GameState.white.X, y = pWorld.GameState.white.Y });
                #endregion
            }
            else
            {
                var m_UWorld = KReader.readUlong(KReader.m_PUBase + Setting.UWorldOffset);
                var m_gameInstance = KReader.readUlong(m_UWorld + 0x140);
                var m_ULocalPlayer = KReader.readUlong(m_gameInstance + 0x38);
                var m_localPlayer = KReader.readUlong(m_ULocalPlayer + 0x0);
                var m_viewportclient = KReader.readUlong(m_localPlayer + 0x58);
                var m_localPawn = KReader.readUlong(m_localPlayer + 0x3A8);
                //var m_localPlayerState = KReader.readUlong(m_localPawn + 0x03C0);
                var m_PWorld = KReader.readUlong(m_viewportclient + 0x80);
                var m_ULevel = KReader.readUlong(m_PWorld + 0x30);
                var m_playerCount = KReader.readInt32(m_ULevel + 0xA8);

                var m_localPlayerControl = KReader.readUlong(m_localPlayer + 0x30);
                var m_localPlayerCamerManager = KReader.readUlong(m_localPlayerControl + 0x438);

                var ATslGameState = KReader.readUlong(m_PWorld + 0x00F8);
                var m_AActorPtr = KReader.readUlong(m_ULevel + 0xA0);


                var povRotation = KReader.readVec(m_localPlayerCamerManager + 0x42C);
                var povLocation = KReader.readVec(m_localPlayerCamerManager + 0x420);
                var Fov = KReader.readFloat(m_localPlayerCamerManager + 0x438);


                //get zone info
                Vector3 blue = KReader.readVec(ATslGameState + 0x440);
                float blueR = KReader.readFloat(ATslGameState + 0x44C);

                Vector3 white = KReader.readVec(ATslGameState + 0x450);
                float whiteR = KReader.readFloat(ATslGameState + 0x45C);


                json.camera.Add(new Camera { n = "Rotation", X = povRotation.X, Y = povRotation.Y, Z = povRotation.Z });
                json.camera.Add(new Camera { n = "Location", X = povLocation.X, Y = povLocation.Y, Z = povLocation.Z });
                json.camera.Add(new Camera { n = "Fov", X = Fov, Y = 0, Z = 0 });
                json.zone.Add(new Zone { r = blueR, x = blue.X, y = blue.Y });
                json.zone.Add(new Zone { r = whiteR, x = white.X, y = white.Y });

                var worldLocation = new Vector3
                {
                    X = KReader.readInt32(m_PWorld + 0x918),
                    Y = KReader.readInt32(m_PWorld + 0x91C),
                    Z = KReader.readInt32(m_PWorld + 0x920)
                };
                //this just for test
                //List<string> a = new List<string>();

                for (int i = 0; i < m_playerCount; i++)
                {
                    var curActor = KReader.readUlong(m_AActorPtr + (ulong)(i * 0x8));
                    var curActorID = KReader.readInt32(curActor + 0x0018);
                    var actorGName = Cache.GetGName(curActorID);// KReader.getGNameFromId(curActorID);
                    var rootCmpPtr = KReader.readUlong(curActor + 0x0180);
                    var actorLocation = KReader.readVec(rootCmpPtr + 0x1A0);
                    var relativeLocation = KReader.readVec(rootCmpPtr + 0x01E0);
                    //玩家
                    if (isPlayer(actorGName))
                    {
                        var playerState = KReader.readUlong(curActor + 0x03C0);
                        var actorTeam = KReader.readInt32(playerState + 0x0444);
                        var _actorLocation = actorLocation + worldLocation;
                        var Health = KReader.readFloat(curActor + 0x107C);
                        ulong mesh = KReader.readUlong(curActor + 0x0400);
                        var RelativeRotation = KReader.readVec(rootCmpPtr + 0x01EC);
                        var playerId = KReader.readInt32(playerState + 0x03C8);
                        var isInactive = KReader.readChar(playerState + 0x03CC);

                        json.players.Add(new Players
                        {
                            x = _actorLocation.X,
                            y = _actorLocation.Y,
                            z = _actorLocation.Z,
                            health = Health,
                            id = playerId,
                            rotator = RelativeRotation.Y,
                            t = actorTeam,
                            isInactive = isInactive,
                            rx = relativeLocation.X,
                            ry = relativeLocation.Y,
                            rz = relativeLocation.Z,
                            mesh = mesh
                        });
                    }
                    //车辆
                    else if (isVehicle(actorGName))
                    {
                        var _actorLocation = actorLocation + worldLocation;

                        json.vehicles.Add(new Vehicle
                        {
                            v = actorGName.Substring(0, 4),
                            x = _actorLocation.X,
                            y = _actorLocation.Y,
                            z = _actorLocation.Z,
                            rx = relativeLocation.X,
                            ry = relativeLocation.Y,
                            rz = relativeLocation.Z,
                        });
                    }
                    //物品
                    else if (isDropItems(actorGName))
                    {
                        var DroppedItemArray = KReader.readUlong(curActor + 0x02D8);
                        var DroppedItemCount = KReader.readInt32(curActor + 0x02E0);

                        for (var j = 0; j < DroppedItemCount; j++)
                        {
                            var ADroppedItem = KReader.readUlong(DroppedItemArray + (ulong)(j * 0x10));
                            var UItem = KReader.readUlong(ADroppedItem + 0x0448);
                            var UItemID = KReader.readInt32(UItem + 0x18);
                            var itemName = Cache.GetGName(UItemID);// KReader.getGNameFromId(UItemID);

                            if (string.IsNullOrEmpty(InCareList(itemName))) continue;
                            var droppedLocation = KReader.readVec(ADroppedItem + 0x01E0);
                            var _relativeLocation = droppedLocation + actorLocation;
                            droppedLocation = droppedLocation + actorLocation + worldLocation;

                            //a.Add(itemName);
                            json.items.Add(new Item
                            {
                                n = InCareList(itemName),
                                x = droppedLocation.X,
                                y = droppedLocation.Y,
                                z = droppedLocation.Z,
                                rx = _relativeLocation.X,
                                ry = _relativeLocation.Y,
                                rz = _relativeLocation.Z
                            });
                        }
                    }
                    else if (isAirdropOrDeathdrop(actorGName))
                    {
                        //var droppedItemArray = actor.AItemPackages;
                        //var droppedItemCount = actor.AItemPackagesCount;
                        //for (var j = 0; j < droppedItemCount; j++)
                        //{
                        //    var dropItem = droppedItemArray[j];
                        var _actorLocation = actorLocation+ worldLocation;
                        json.vehicles.Add(new Vehicle
                        {
                            v = actorGName.Substring(0, 3),
                            x = _actorLocation.X,
                            y = _actorLocation.Y,
                            z = _actorLocation.Z,
                            rx = relativeLocation.X,
                            ry = relativeLocation.Y,
                            rz = relativeLocation.Z
                        });
                        //}
                    }
                }
            }
            return json;

        }

    }
}