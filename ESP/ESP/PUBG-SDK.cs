using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CPUZ.Model;

namespace CPUZ
{

    public class OffsetAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public OffsetAttribute(ulong offset)
        {
            Offset = offset;
        }
        public ulong Offset;
    }

    public class BaseEntry
    {
        protected BaseEntry(ulong address)
        {
            BasePoint = address;
            DoProperty();
        }

        private void DoProperty()
        {
            var t = GetType().GetProperties();
            foreach (var propertyInfo in t)
            {
                var attributes = propertyInfo.GetCustomAttributes(false);
                if (attributes.Length == 0) continue;
                if (attributes[0].GetType() != typeof(OffsetAttribute)) continue;
                var methodInfo =
                    typeof(KReader)
                        .GetMethod("Reader")
                        .MakeGenericMethod(
                            propertyInfo.PropertyType.BaseType == typeof(BaseEntry) ? typeof(ulong) : propertyInfo.PropertyType);
                //执行KReader泛型方法取得值
                var value = methodInfo.Invoke(null, new object[] { (BasePoint + ((OffsetAttribute)attributes[0]).Offset)});
                if (propertyInfo.PropertyType.BaseType == typeof(BaseEntry))
                {
                    var instance = Activator.CreateInstance(propertyInfo.PropertyType, value);
                    propertyInfo.SetValue(this, Convert.ChangeType(instance, propertyInfo.PropertyType), null);
                }
                else if(propertyInfo.PropertyType.IsValueType)
                {
                    propertyInfo.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
            }
        }

        protected ulong BasePoint;
    }

    public class Pubg : BaseEntry
    {
        public Pubg(ulong address) : base(address)
        {
        }
        public UWorld UWorld => new UWorld(KReader.readUlong(BasePoint));
    }

    public class UWorld : BaseEntry
    {
        public UWorld(ulong address) : base(address)
        {
        }

        [Offset(0x0140)]
        public UGameInstance
            OwningGameInstance
        { get; set; }
    }

    //class UGameInstance* OwningGameInstance; // 0x0140(0x0008) 
    public class UGameInstance : BaseEntry
    {

        public UGameInstance(ulong address) : base(address)
        {
        }
        [Offset(0x0038)]
        public ULocalPlayers LocalPlayers { get; set; }
    }

    //TArray<class ULocalPlayer*> LocalPlayers; // 0x0038(0x0010)
    public class ULocalPlayers : BaseEntry
    {
        private readonly int 占位 = 0x00;

        public ULocalPlayers(ulong address) : base(address)
        {
        }

        public ULocalPlayer this[int index] =>
            new ULocalPlayer(KReader.readUlong(BasePoint + (ulong)(index * 占位)));
    }

    public class ULocalPlayer : BaseEntry
    {
        public ULocalPlayer(ulong address) : base(address)
        {

        }
        [Offset(0x0058)]
        public UGameViewportClient ViewportClient { get; protected set; }
        [Offset(0x03A8)]
        public APawn Pawn { get; set; }
        [Offset(0x0030)]
        public APlayerController PlayerController { get; set; }
    }

    //class UGameViewportClient* ViewportClient; // 0x0058(0x0008) 
    public class UGameViewportClient : BaseEntry
    {
        public UGameViewportClient(ulong address) : base(address)
        {
        }
        [Offset(0x0080)]
        public PWorld UWorld { get; set; }
    }

    //class APawn* Pawn; // 0x03A8(0x0008)
    public class APawn : BaseEntry
    {
        public APawn(ulong address) : base(address)
        {
        }
        [Offset(0x03C0)]
        public APlayerState PlayerState { get; set; }

    }

    //class APlayerState* PlayerState; // 0x03C0(0x0008)
    public class APlayerState : BaseEntry
    {

        public APlayerState(ulong address) : base(address)
        {
        }
        [Offset(0x0080)]
        public PWorld World { get; set; }

        //int TeamNumber; // 0x0444(0x0004)
        [Offset(0x0444)]
        public int TeamNumber { get; set; }

        //int PlayerId; // 0x03C8(0x0004)
        [Offset(0x03C8)]
        public int PlayerId { get; set; }

        //unsigned char bIsInactive : 1; // 0x03CC(0x0001)
        [Offset(0x03CC)]
        public char bIsInactive { get; set; }

        //struct FString PlayerName; // 0x03A8(0x0010)
        [Offset(0x03A8)]
        public ulong PlayerNameAddress { get; set; }
        [Offset(0x03A8 + 0x8)]
        public int PlayerNameLengthPlus1 { get; set; }

        public string PlayerName
        {
            get
            {
                if (PlayerNameLengthPlus1 <= 0) return "";
                var v = KReader.ReadStringUnicode(PlayerNameAddress, PlayerNameLengthPlus1);
                return v.Length > 0 ? v.Substring(0, v.Length - 1) : string.Empty;
            }
        }
    }

    //class UWorld* World; // 0x0080(0x0008)
    public class PWorld : BaseEntry
    {
        public PWorld(ulong address) : base(address)
        {
        }

        [Offset(0x0030)]
        public ULevel PersistentLevel { get; set; }
        [Offset(0x00F8)]
        public AGameStateBase GameState { get; set; }

        public Vector3 WorldLocation => new Vector3
        {
            X = KReader.readInt32(BasePoint + 0x918),
            Y = KReader.readInt32(BasePoint + 0x91C),
            Z = KReader.readInt32(BasePoint + 0x920)
        };
    }

    //class ULevel* PersistentLevel; // 0x0030(0x0008)
    public class ULevel : BaseEntry
    {
        public ULevel(ulong address) : base(address)
        {
        }

        [Offset(0xA8)]
        public int PlayerCount{ get; set; }
        [Offset(0x00A0)]
        public AActors AActors { get; set; }
    }

    //TArray<class AActor*>							   AActors;													 // 0x00A0(0x0010)
    public class AActors : BaseEntry
    {
        private readonly int 位移 = 0x8;

        public AActors(ulong address) : base(address)
        {
        }

        public AActor this[int index] => new AActor(KReader.readUlong(BasePoint + (ulong)(index * 位移)));
    }

    public class AActor : BaseEntry
    {
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


        public AActor(ulong address) : base(address)
        {
        }
        [Offset(0x0018)]
        public int ActorID { get; set; }

        public string ActorName => KReader.getGNameFromId(ActorID);

        [Offset(0x0180)]
        public USceneComponent RootComponent { get; set; }
        [Offset(0x03C0)]
        public APlayerState PlayerState { get; set; }
        [Offset(0x107C)]
        public float Health { get; set; }
        [Offset(0x0400)]
        public Mesh Mesh { get; set; }

        //DropItems
        [Offset(0x02D8)]
        public DropItems DropItems { get; set; }
        [Offset(0x02E0)]
        public int DropItemsCount { get; set; }
        public bool isDropItems => DropName.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);

        //空投和死亡盒子
        [Offset(0x04D0)]
        public DropItems AItemPackages { get; set; }
        [Offset(0x04D4)]
        public int AItemPackagesCount { get; set; }

        public bool isAirdropOrDeathdrop =>
            AirdropOrDeathdrop.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
        public bool isPlayer => playerGName.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
        public bool isVehicle => vehicleGNameVec.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
    }

    public class AItemPackages : BaseEntry
    {
        private readonly int 位移 = 0x08;

        public AItemPackages(ulong address) : base(address)
        {
        }

        public AItemPackage this[int index] =>
            new AItemPackage(KReader.readUlong(BasePoint + (ulong)(index * 位移)));
    }

    public class AItemPackage : BaseEntry
    {
        public AItemPackage(ulong address) : base(address)
        {
        }
    }


    public class DropItems : BaseEntry
    {
        private readonly int 位移 = 0x10;

        public DropItems(ulong address) : base(address)
        {
        }

        public UDropItem this[int index] =>
            new UDropItem(KReader.readUlong(BasePoint + (ulong)(index * 位移)));
    }

    public class UDropItem : BaseEntry
    {
        public Vector3 RelativeLocation;

        public UDropItem(ulong address) : base(address)
        {
        }
        [Offset(0x0448)]
        public UItem UItem { get; set; }

        [Offset(0x01E0)]
        public Vector3 Location { get; set; }
    }

    //		class UItem* Item; // 0x0448(0x0008)
    public class UItem : BaseEntry
    {
        public UItem(ulong address) : base(address)
        {
        }

        [Offset(0x0018)]
        public int ItemId { get; set; }

        public string ItemName => KReader.getGNameFromId(ItemId);
    }

    public class Mesh : BaseEntry
    {
        public Mesh(ulong address) : base(address)
        {
        }

        public ulong mesh => BasePoint;
    }

    //class USceneComponent* RootComponent; // 0x0180(0x0008)
    public class USceneComponent : BaseEntry
    {
        public USceneComponent(ulong address) : base(address)
        {
        }

        //struct FVector Location; // 0x01A0(0x000C)
        [Offset(0x01A0)]
        public Vector3 Location { get; set; }

        //struct FVector RelativeLocation; // 0x01E0(0x000C)
        [Offset(0x01E0)]
        public Vector3 RelativeLocation { get; set; }

        //struct FRotator RelativeRotation; // 0x01EC(0x000C) 
        [Offset(0x01EC)]
        public Vector3 RelativeRotation { get; set; }
    }

    //class APlayerController* PlayerController; // 0x0030(0x0008) 
    public class APlayerController : BaseEntry
    {
        [Offset(0x0438)]
        public APlayerCameraManager PlayerCameraManager { get; set; }

        public APlayerController(ulong address) : base(address)
        {
        }
    }

    //class APlayerCameraManager* PlayerCameraManager; // 0x0438(0x0008)
    public class APlayerCameraManager : BaseEntry
    {
        public APlayerCameraManager(ulong address) : base(address)
        {
        }
        [Offset(0x42C)]
        public Vector3 povRotation { get; set; }
        [Offset(0x420)]
        public Vector3 povLocation { get; set; }
        [Offset(0x438)]
        public float Fov { get; set; }
    }

    //class AGameStateBase* GameState; // 0x00F8(0x0008)
    public class AGameStateBase : BaseEntry
    {
        public AGameStateBase(ulong address) : base(address)
        {
        }

        [Offset(0x440)]
        public Vector3 blue { get; set; }
        [Offset(0x44C)]
        public float blueR { get; set; }

        [Offset(0x450)]
        public Vector3 white { get; set; }
        [Offset(0x45C)]
        public float whiteR { get; set; }
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Pov
    {
        public Vector3 Rotation;
    }
}