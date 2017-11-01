using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CPUZ;
using CPUZ.Model;

namespace CPUZ
{
    public class BaseEntry
    {
        public BaseEntry(ulong address)
        {
            this.BasePoint = address;
        }
        protected ulong BasePoint { get; set; }
    }


    public class UWorld : BaseEntry
    {
        private ulong getval;
        public UWorld(ulong address) : base(address)
        {
            this.BasePoint = KReader.readUlong(address);
            getval = KReader.readUlong(BasePoint + 0x0140);
        }
        public UGameInstance OwningGameInstance => new UGameInstance(getval);

    }

    //class UGameInstance*                               OwningGameInstance;                                       // 0x0140(0x0008) (CPF_ZeroConstructor, CPF_Transient, CPF_IsPlainOldData)
    public class UGameInstance : BaseEntry
    {
        public UGameInstance(ulong address) : base(address)
        {
            this.BasePoint = address;
            LocalPlayers = new ULocalPlayers(KReader.readUlong(this.BasePoint + 0x0038));

        }
        public ULocalPlayers LocalPlayers;
    }

    //TArray<class ULocalPlayer*>                        LocalPlayers;                                             // 0x0038(0x0010) (CPF_ZeroConstructor)
    public class ULocalPlayers : BaseEntry
    {
        private int 占位 = 0x00;
        public ULocalPlayers(ulong address) : base(address)
        {
        }
        public ULocalPlayer this[int index] =>
            new ULocalPlayer(KReader.readUlong(this.BasePoint + (ulong)(index * 占位)));
    }

    public class ULocalPlayer : BaseEntry
    {

        public ULocalPlayer(ulong address) : base(address)
        {
        }

        public UGameViewportClient ViewportClient => new UGameViewportClient(KReader.readUlong(this.BasePoint + 0x0058));
        public APawn Pawn => new APawn(KReader.readUlong(this.BasePoint + 0x03A8));
        public APlayerController PlayerController => new APlayerController(KReader.readUlong(this.BasePoint + 0x0030));
    }

    //class UGameViewportClient*                         ViewportClient;                                           // 0x0058(0x0008) (CPF_ZeroConstructor, CPF_IsPlainOldData)
    public class UGameViewportClient : BaseEntry
    {
        public UGameViewportClient(ulong address) : base(address)
        {
        }
        public PWorld UWorld => new PWorld(KReader.readUlong(this.BasePoint + 0x0080));
    }

    //class APawn* Pawn;                                                     // 0x03A8(0x0008) (CPF_Net, CPF_ZeroConstructor, CPF_IsPlainOldData)
    public class APawn : BaseEntry
    {
        public APawn(ulong address) : base(address)
        {
            PlayerState = new APlayerState(KReader.readUlong(this.BasePoint + 0x03C0));
        }

        public APlayerState PlayerState;
    }
    //class APlayerState*                                PlayerState;                                              // 0x03C0(0x0008) (CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_Net, CPF_ZeroConstructor, CPF_IsPlainOldData)
    public class APlayerState : BaseEntry
    {
        public APlayerState(ulong address) : base(address)
        {
            World = new PWorld(KReader.readUlong(this.BasePoint + 0x0080));
        }
        public PWorld World;

        //int                                                TeamNumber;                                               // 0x0444(0x0004) (CPF_Net, CPF_ZeroConstructor, CPF_Transient, CPF_IsPlainOldData)
        public int TeamNumber => KReader.readInt32(this.BasePoint + 0x0444);
        //int                                                PlayerId;                                                 // 0x03C8(0x0004) (CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_Net, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public int PlayerId => KReader.readInt32(this.BasePoint + 0x03C8);
        //unsigned char                                      bIsInactive : 1;                                          // 0x03CC(0x0001) (CPF_Net)
        public char bIsInactive => KReader.readChar(this.BasePoint + 0x03CC);

        //struct FString                                     PlayerName;                                               // 0x03A8(0x0010) (CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_Net, CPF_ZeroConstructor)
        public string PlayerName => System.Text.Encoding.Default.GetString(KReader.readSize(this.BasePoint + 0x03A8, 10));


    }

    //class UWorld*                                      World;                                                    // 0x0080(0x0008) (CPF_ZeroConstructor, CPF_IsPlainOldData)
    public class PWorld : BaseEntry
    {
        public PWorld(ulong address) : base(address)
        {
            PersistentLevel = new ULevel(KReader.readUlong(this.BasePoint + 0x0030));
            GameState = new AGameStateBase(KReader.readUlong(this.BasePoint + 0x00F8));
        }
        public ULevel PersistentLevel;
        public AGameStateBase GameState;


        public Vector3 WorldLocation => new Vector3
        {
            X = KReader.readInt32(this.BasePoint + 0x918),
            Y = KReader.readInt32(this.BasePoint + 0x91C),
            Z = KReader.readInt32(this.BasePoint + 0x920)
        };
    }

    //class ULevel*                                      PersistentLevel;                                          // 0x0030(0x0008) (CPF_ZeroConstructor, CPF_Transient, CPF_IsPlainOldData)
    public class ULevel : BaseEntry
    {
        public ULevel(ulong address) : base(address)
        {
            PlayerCount = KReader.readInt32(this.BasePoint + 0xA8);
            AActors = new AActors(KReader.readUlong(this.BasePoint + 0x00A0));
        }
        public int PlayerCount;
        public AActors AActors;
    }

    //TArray<class AActor*>							   AActors;													 // 0x00A0(0x0010)
    public class AActors : BaseEntry
    {
        private int 位移 = 0x8;
        public AActors(ulong address) : base(address)
        {

        }
        public AActor this[int index] => new AActor(KReader.readUlong(this.BasePoint + (ulong)(index * 位移)));
    }

    public class AActor : BaseEntry
    {
        private static List<string> vehicleGNameVec = new List<string> { "Uaz", "Buggy", "Dacia", "ABP_Motorbike", "BP_Motorbike", "Boat_PG117" };
        private static List<string> DropName = new List<string> { "DroppedItemGroup", "DroppedItemInteractionComponent" };

        public AActor(ulong address) : base(address)
        {
        }
        public int ActorID => KReader.readInt32(this.BasePoint + 0x0018);
        public string ActorName => KReader.getGNameFromId(ActorID);
        public USceneComponent RootComponent => new USceneComponent(KReader.readUlong(this.BasePoint + 0x0180));
        public APlayerState PlayerState => new APlayerState(KReader.readUlong(this.BasePoint + 0x03C0));
        public float Health => KReader.readFloat(this.BasePoint + 0x107C);
        public Mesh Mesh => new Mesh(KReader.readUlong(this.BasePoint + 0x0400));


        //DropItems
        public DropItems DropItems => new DropItems(KReader.readUlong(this.BasePoint + 0x02D8));
        public int DropItemsCount => KReader.readInt32(this.BasePoint + 0x02E0);
        public bool isDropItems => DropName.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);


        public bool isPlayer => (this.ActorID == 66943 || this.ActorID == 66948);
        public bool isVehicle => vehicleGNameVec.Any(g => ActorName.IndexOf(g, StringComparison.Ordinal) > -1);
    }

    public class DropItems : BaseEntry
    {
        private int 位移 = 0x10;
        public DropItems(ulong address) : base(address)
        {
        }

        public UDropItem this[int index] =>
            new UDropItem(KReader.readUlong(this.BasePoint + (ulong)(index * 位移)));
    }

    public class UDropItem : BaseEntry
    {
        private Vector3 BaseLocation;

        public UDropItem(ulong address) : base(address)
        {
        }
        public UItem UItem =>
            new UItem(KReader.readUlong(this.BasePoint + 0x0448));


        private Vector3 droppedLocationbase => KReader.readVec(this.BasePoint + 0x01E0);

        public Vector3 Loction => droppedLocationbase;
        public Vector3 RelativeLocation;
    }

    //		class UItem*                                       Item;                                                     // 0x0448(0x0008) (CPF_Net, CPF_ZeroConstructor, CPF_IsPlainOldData)
    public class UItem : BaseEntry
    {
        private Vector3 BaseLocation;
        public UItem(ulong address) : base(address)
        {
        }
        public int ItemId => KReader.readInt32(this.BasePoint + 0x0018);

        //struct FText                                       ItemName;                                                 // 0x0040(0x0018)
        //public string ItemName => System.Text.Encoding.Default.GetString(KReader.readSize(this.BasePoint + 0x0040, 24));
        public string ItemName => KReader.getGNameFromId(ItemId);



    }

    public class Mesh : BaseEntry
    {
        public Mesh(ulong address) : base(address)
        {
        }

        public ulong mesh => this.BasePoint;

    }

    //class USceneComponent*                             RootComponent;                                            // 0x0180(0x0008) (CPF_ExportObject, CPF_ZeroConstructor, CPF_InstancedReference, CPF_IsPlainOldData)
    public class USceneComponent : BaseEntry
    {
        public USceneComponent(ulong address) : base(address)
        {
        }
        //struct FVector                                     Location;												 // 0x01A0(0x000C)
        public Vector3 Location => KReader.readVec(this.BasePoint + 0x01A0);
        //struct FVector                                     RelativeLocation;                                         // 0x01E0(0x000C) (CPF_Edit, CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_Net, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public Vector3 RelativeLocation => KReader.readVec(this.BasePoint + 0x01E0);
        //struct FRotator                                    RelativeRotation;                                         // 0x01EC(0x000C) (CPF_Edit, CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_Net, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public Vector3 RelativeRotation => KReader.readVec(this.BasePoint + 0x01EC);

    }

    //class APlayerController*                           PlayerController;                                         // 0x0030(0x0008) (CPF_ZeroConstructor, CPF_Transient, CPF_IsPlainOldData)
    public class APlayerController : BaseEntry
    {
        public APlayerController(ulong address) : base(address)
        {
            PlayerCameraManager = new APlayerCameraManager(KReader.readUlong(this.BasePoint + 0x0438));
        }
        public APlayerCameraManager PlayerCameraManager;
    }

    //class APlayerCameraManager*                        PlayerCameraManager;                                      // 0x0438(0x0008) (CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_ZeroConstructor, CPF_IsPlainOldData)
    public class APlayerCameraManager : BaseEntry
    {
        public APlayerCameraManager(ulong address) : base(address)
        {
        }

        public Vector3 povRotation => KReader.readVec(this.BasePoint + 0x42C);
        public Vector3 povLocation => KReader.readVec(this.BasePoint + 0x420);
        public float Fov => KReader.readFloat(this.BasePoint + 0x438);
    }

    //class AGameStateBase*                              GameState;                                                // 0x00F8(0x0008) (CPF_ZeroConstructor, CPF_Transient, CPF_IsPlainOldData)
    public class AGameStateBase : BaseEntry
    {
        public AGameStateBase(ulong address) : base(address)
        {
        }
        public Vector3 blue => KReader.readVec(this.BasePoint + 0x440);
        public float blueR => KReader.readFloat(this.BasePoint + 0x44C);

        public Vector3 white => KReader.readVec(this.BasePoint + 0x450);
        public float whiteR => KReader.readFloat(this.BasePoint + 0x45C);

    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Pov
    {
        public Vector3 Rotation;
    }


}