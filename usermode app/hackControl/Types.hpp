#pragma once

#include <Windows.h>
#include "json.hpp"
#define CHECK_VALID( _v ) 0
using byte = BYTE;
using uint64_t = ULONGLONG;
using int64_t = LONGLONG;
using uint32_t = UINT32;
using int32_t = INT32;
using uint16_t = USHORT;
using int16_t = SHORT;

bool readLoop = false;

// ugly to define the wanted items / player gnames this way.
std::vector<std::string> playerGNameVec = { "PlayerMale", "PlayerFemale" };
std::vector<std::string> vehicleGNameVec = { "Uaz", "Buggy", "Dacia", "ABP_Motorbike", "BP_Motorbike", "Boat_PG117" };
std::map<std::string, std::string> dropGNameMap = { { "Item_Head_G_01_Lv3_C", "Helm3" },{ "Item_Head_G_01_Lv3_", "Helm3" },{ "Item_Armor_C_01_Lv3", "Vest3" },{"Item_Head_F_02_Lv2_C","Helm2"}, { "Item_Armor_C_01_Lv3_C", "Vest3" },{"Item_Armor_D_01_Lv2_C","Vest2"}, { "Item_Equip_Armor_Lv3_C", "Vest3" },{"Item_Equip_Armor_Lv3", "Vest3"},{"Item_Attach_Weapon_Muzzle_Suppressor_SniperRifle", "Supp(SR)"},{"Item_Attach_Weapon_Muzzle_Suppressor_Large", "Supp(AR)"},{"Item_Attach_Weapon_Muzzle_Suppressor_Large_C", "Supp(SR)"},{"Item_Heal_MedKit", "Meds"},{"Item_Heal_FirstAid", "Meds"},{"Item_Weapon_Kar98k", "kar98"},{"Item_Weapon_Mini14", "mini"},{"Item_Weapon_M16A4", "M16"},{"Item_Weapon_HK416", "m416"},{"Item_Weapon_SCAR-L", "SCAR"},{"Item_Weapon_SKS", "sks"},{"Item_Attach_Weapon_Upper_ACOG_01", "4x"},{"Item_Attach_Weapon_Upper_CQBSS", "8x"},{"Item_Attach_Weapon_Upper_CQBSS_C", "8x"},{"Item_Boost_PainKiller_C", "YAO"},{"Item_Boost_EnergyDrink_C", "YAO"} };

using json = nlohmann::json;

enum PROTO_MESSAGE {
	PROTO_NORMAL_READ = 0,
	PROTO_GET_BASEADDR = 1
};





struct Vector3
{
	float X;
	float Y;
	float Z;
};

typedef struct readStruct
{
	ULONGLONG UserBufferAdress;
	ULONGLONG GameAddressOffset;
	ULONGLONG ReadSize;
	ULONG     UserPID;
	ULONG     GamePID;
	BOOLEAN   WriteOrRead;
	UINT32	  ProtocolMsg;
	BOOLEAN   WriteOrRead2;
	UINT32	  ProtocolMsg2;
} ReadStruct, *pReadStruct;

// ScriptStruct CoreUObject.Vector
// 0x000C
struct FVector {
	float                                              X;                                                        // 0x0000(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_SaveGame, CPF_IsPlainOldData)
	float                                              Y;                                                        // 0x0004(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_SaveGame, CPF_IsPlainOldData)
	float                                              Z;                                                        // 0x0008(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_SaveGame, CPF_IsPlainOldData)

	inline FVector()
		: X(0), Y(0), Z(0) {
	}

	inline FVector(float x, float y, float z)
		: X(x),
		Y(y),
		Z(z) {
	}

	__forceinline FVector FVector::operator-(const FVector& V) {
		return FVector(X - V.X, Y - V.Y, Z - V.Z);
	}

	__forceinline FVector FVector::operator+(const FVector& V) {
		return FVector(X + V.X, Y + V.Y, Z + V.Z);
	}

	__forceinline FVector FVector::operator*(float Scale) const {
		return FVector(X * Scale, Y * Scale, Z * Scale);
	}

	__forceinline FVector FVector::operator/(float Scale) const {
		const float RScale = 1.f / Scale;
		return FVector(X * RScale, Y * RScale, Z * RScale);
	}

	__forceinline FVector FVector::operator+(float A) const {
		return FVector(X + A, Y + A, Z + A);
	}

	__forceinline FVector FVector::operator-(float A) const {
		return FVector(X - A, Y - A, Z - A);
	}

	__forceinline FVector FVector::operator*(const FVector& V) const {
		return FVector(X * V.X, Y * V.Y, Z * V.Z);
	}

	__forceinline FVector FVector::operator/(const FVector& V) const {
		return FVector(X / V.X, Y / V.Y, Z / V.Z);
	}

	__forceinline float FVector::operator|(const FVector& V) const {
		return X*V.X + Y*V.Y + Z*V.Z;
	}

	__forceinline float FVector::operator^(const FVector& V) const {
		return X*V.Y - Y*V.X - Z*V.Z;
	}

	__forceinline FVector& FVector::operator+=(const FVector& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		X += v.X;
		Y += v.Y;
		Z += v.Z;
		return *this;
	}

	__forceinline FVector& FVector::operator-=(const FVector& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		X -= v.X;
		Y -= v.Y;
		Z -= v.Z;
		return *this;
	}

	__forceinline FVector& FVector::operator*=(const FVector& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		X *= v.X;
		Y *= v.Y;
		Z *= v.Z;
		return *this;
	}

	__forceinline FVector& FVector::operator/=(const FVector& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		X /= v.X;
		Y /= v.Y;
		Z /= v.Z;
		return *this;
	}

	__forceinline bool FVector::operator==(const FVector& src) const {
		CHECK_VALID(src);
		CHECK_VALID(*this);
		return (src.X == X) && (src.Y == Y) && (src.Z == Z);
	}

	__forceinline bool FVector::operator!=(const FVector& src) const {
		CHECK_VALID(src);
		CHECK_VALID(*this);
		return (src.X != X) || (src.Y != Y) || (src.Z != Z);
	}

	__forceinline float FVector::Size() const {
		return sqrt(X*X + Y*Y + Z*Z);
	}

	__forceinline float FVector::Size2D() const {
		return sqrt(X*X + Y*Y);
	}

	__forceinline float FVector::SizeSquared() const {
		return X*X + Y*Y + Z*Z;
	}

	__forceinline float FVector::SizeSquared2D() const {
		return X*X + Y*Y;
	}

	__forceinline float FVector::Dot(const FVector& vOther) const {
		const FVector& a = *this;

		return (a.X * vOther.X + a.Y * vOther.Y + a.Z * vOther.Z);
	}

	__forceinline FVector FVector::Normalize() {
		FVector vector;
		float length = this->Size();

		if (length != 0) {
			vector.X = X / length;
			vector.Y = Y / length;
			vector.Z = Z / length;
		}
		else
			vector.X = vector.Y = 0.0f;
		vector.Z = 1.0f;

		return vector;
	}
};


template<class T>
struct TArray {
	friend struct FString;

public:
	inline TArray() {
		Data = nullptr;
		Count = Max = 0;
	};

	inline int Num() const {
		return Count;
	};

	inline T& operator[](int i) {
		return Data[i];
	};

	inline const T& operator[](int i) const {
		return Data[i];
	};

	inline bool IsValidIndex(int i) const {
		return i < Num();
	}

private:
	T* Data;
	int32_t Count;
	int32_t Max;
};

struct FString : private TArray<wchar_t> {
	inline FString() {
	};

	FString(const wchar_t* other) {
		Max = Count = *other ? (int32_t)std::wcslen(other) + 1 : 0;

		if (Count) {
			Data = const_cast<wchar_t*>(other);
		}
	};

	inline bool IsValid() const {
		return Data != nullptr;
	}

	inline const wchar_t* c_str() const {
		return Data;
	}

	std::string ToString() const {
		auto length = std::wcslen(Data);

		std::string str(length, '\0');

		std::use_facet<std::ctype<wchar_t>>(std::locale()).narrow(Data, Data + length, '?', &str[0]);

		return str;
	}
};

// ScriptStruct CoreUObject.Rotator
// 0x000C
struct FRotator {
	float                                              Pitch;                                                    // 0x0000(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_SaveGame, CPF_IsPlainOldData)
	float                                              Yaw;                                                      // 0x0004(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_SaveGame, CPF_IsPlainOldData)
	float                                              Roll;                                                     // 0x0008(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_SaveGame, CPF_IsPlainOldData)

	inline FRotator()
		: Pitch(0), Yaw(0), Roll(0) {
	}

	inline FRotator(float x, float y, float z)
		: Pitch(x),
		Yaw(y),
		Roll(z) {
	}

	__forceinline FRotator FRotator::operator+(const FRotator& V) {
		return FRotator(Pitch + V.Pitch, Yaw + V.Yaw, Roll + V.Roll);
	}

	__forceinline FRotator FRotator::operator-(const FRotator& V) {
		return FRotator(Pitch - V.Pitch, Yaw - V.Yaw, Roll - V.Roll);
	}

	__forceinline FRotator FRotator::operator*(float Scale) const {
		return FRotator(Pitch * Scale, Yaw * Scale, Roll * Scale);
	}

	__forceinline FRotator FRotator::operator/(float Scale) const {
		const float RScale = 1.f / Scale;
		return FRotator(Pitch * RScale, Yaw * RScale, Roll * RScale);
	}

	__forceinline FRotator FRotator::operator+(float A) const {
		return FRotator(Pitch + A, Yaw + A, Roll + A);
	}

	__forceinline FRotator FRotator::operator-(float A) const {
		return FRotator(Pitch - A, Yaw - A, Roll - A);
	}

	__forceinline FRotator FRotator::operator*(const FRotator& V) const {
		return FRotator(Pitch * V.Pitch, Yaw * V.Yaw, Roll * V.Roll);
	}

	__forceinline FRotator FRotator::operator/(const FRotator& V) const {
		return FRotator(Pitch / V.Pitch, Yaw / V.Yaw, Roll / V.Roll);
	}

	__forceinline float FRotator::operator|(const FRotator& V) const {
		return Pitch*V.Pitch + Yaw*V.Yaw + Roll*V.Roll;
	}

	__forceinline FRotator& FRotator::operator+=(const FRotator& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		Pitch += v.Pitch;
		Yaw += v.Yaw;
		Roll += v.Roll;
		return *this;
	}

	__forceinline FRotator& FRotator::operator-=(const FRotator& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		Pitch -= v.Pitch;
		Yaw -= v.Yaw;
		Roll -= v.Roll;
		return *this;
	}

	__forceinline FRotator& FRotator::operator*=(const FRotator& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		Pitch *= v.Pitch;
		Yaw *= v.Yaw;
		Roll *= v.Roll;
		return *this;
	}

	__forceinline FRotator& FRotator::operator/=(const FRotator& v) {
		CHECK_VALID(*this);
		CHECK_VALID(v);
		Pitch /= v.Pitch;
		Yaw /= v.Yaw;
		Roll /= v.Roll;
		return *this;
	}

	__forceinline float FRotator::operator^(const FRotator& V) const {
		return Pitch*V.Yaw - Yaw*V.Pitch - Roll*V.Roll;
	}

	__forceinline bool FRotator::operator==(const FRotator& src) const {
		CHECK_VALID(src);
		CHECK_VALID(*this);
		return (src.Pitch == Pitch) && (src.Yaw == Yaw) && (src.Roll == Roll);
	}

	__forceinline bool FRotator::operator!=(const FRotator& src) const {
		CHECK_VALID(src);
		CHECK_VALID(*this);
		return (src.Pitch != Pitch) || (src.Yaw != Yaw) || (src.Roll != Roll);
	}

	__forceinline float FRotator::Size() const {
		return sqrt(Pitch*Pitch + Yaw* Yaw + Roll*Roll);
	}


	__forceinline float FRotator::SizeSquared() const {
		return Pitch*Pitch + Yaw* Yaw + Roll*Roll;
	}

	__forceinline float FRotator::Dot(const FRotator& vOther) const {
		const FRotator& a = *this;

		return (a.Pitch * vOther.Pitch + a.Yaw * vOther.Yaw + a.Roll * vOther.Roll);
	}

	__forceinline float FRotator::ClampAxis(float Angle) {
		// returns Angle in the range (-360,360)
		Angle = fmod(Angle, 360.f);

		if (Angle < 0.f) {
			// shift to [0,360) range
			Angle += 360.f;
		}

		return Angle;
	}

	__forceinline float FRotator::NormalizeAxis(float Angle) {
		// returns Angle in the range [0,360)
		Angle = ClampAxis(Angle);

		if (Angle > 180.f) {
			// shift to (-180,180]
			Angle -= 360.f;
		}

		return Angle;
	}

	__forceinline void FRotator::Normalize() {
		Pitch = NormalizeAxis(Pitch);
		Yaw = NormalizeAxis(Yaw);
		Roll = NormalizeAxis(Roll);
	}

	__forceinline FRotator FRotator::GetNormalized() const {
		FRotator Rot = *this;
		Rot.Normalize();
		return Rot;
	}
};
