﻿using System;
using System.Runtime.InteropServices;

namespace CPUZ.Model
{
    public enum Bones : int
    {
        Root = 0,
        pelvis = 1,
        spine_01 = 2,
        spine_02 = 3,
        spine_03 = 4,
        neck_01 = 5,
        Head = 6,
        face_root = 7,
        eyebrows_pos_root = 8,
        eyebrows_root = 9,
        eyebrows_r = 10,
        eyebrows_l = 11,
        eyebrow_l = 12,
        eyebrow_r = 13,
        forehead_root = 14,
        forehead = 15,
        jaw_pos_root = 16,
        jaw_root = 17,
        jaw = 18,
        mouth_down_pos_root = 19,
        mouth_down_root = 20,
        lip_bm_01 = 21,
        lip_bm_02 = 22,
        lip_br = 23,
        lip_bl = 24,
        jaw_01 = 25,
        jaw_02 = 26,
        cheek_pos_root = 27,
        cheek_root = 28,
        cheek_r = 29,
        cheek_l = 30,
        nose_side_root = 31,
        nose_side_r_01 = 32,
        nose_side_r_02 = 33,
        nose_side_l_01 = 34,
        nose_side_l_02 = 35,
        eye_pos_r_root = 36,
        eye_r_root = 37,
        eye_rot_r_root = 38,
        eye_lid_u_r = 39,
        eye_r = 40,
        eye_lid_b_r = 41,
        eye_pos_l_root = 42,
        eye_l_root = 43,
        eye_rot_l_root = 44,
        eye_lid_u_l = 45,
        eye_l = 46,
        eye_lid_b_l = 47,
        nose_pos_root = 48,
        nose = 49,
        mouth_up_pos_root = 50,
        mouth_up_root = 51,
        lip_ul = 52,
        lip_um_01 = 53,
        lip_um_02 = 54,
        lip_ur = 55,
        lip_l = 56,
        lip_r = 57,
        hair_root = 58,
        hair_b_01 = 59,
        hair_b_02 = 60,
        hair_l_01 = 61,
        hair_l_02 = 62,
        hair_r_01 = 63,
        hair_r_02 = 64,
        hair_f_02 = 65,
        hair_f_01 = 66,
        hair_b_pt_01 = 67,
        hair_b_pt_02 = 68,
        hair_b_pt_03 = 69,
        hair_b_pt_04 = 70,
        hair_b_pt_05 = 71,
        camera_fpp = 72,
        GunReferencePoint = 73,
        GunRef = 74,
        breast_l = 75,
        breast_r = 76,
        clavicle_l = 77,
        upperarm_l = 78,
        lowerarm_l = 79,
        hand_l = 80,
        thumb_01_l = 81,
        thumb_02_l = 82,
        thumb_03_l = 83,
        thumb_04_l_MBONLY = 84,
        index_01_l = 85,
        index_02_l = 86,
        index_03_l = 87,
        index_04_l_MBONLY = 88,
        middle_01_l = 89,
        middle_02_l = 90,
        middle_03_l = 91,
        middle_04_l_MBONLY = 92,
        ring_01_l = 93,
        ring_02_l = 94,
        ring_03_l = 95,
        ring_04_l_MBONLY = 96,
        pinky_01_l = 97,
        pinky_02_l = 98,
        pinky_03_l = 99,
        pinky_04_l_MBONLY = 100,
        item_l = 101,
        lowerarm_twist_01_l = 102,
        upperarm_twist_01_l = 103,
        clavicle_r = 104,
        upperarm_r = 105,
        lowerarm_r = 106,
        hand_r = 107,
        thumb_01_r = 108,
        thumb_02_r = 109,
        thumb_03_r = 110,
        thumb_04_r_MBONLY = 111,
        index_01_r = 112,
        index_02_r = 113,
        index_03_r = 114,
        index_04_r_MBONLY = 115,
        middle_01_r = 116,
        middle_02_r = 117,
        middle_03_r = 118,
        middle_04_r_MBONLY = 119,
        ring_01_r = 120,
        ring_02_r = 121,
        ring_03_r = 122,
        ring_04_r_MBONLY = 123,
        pinky_01_r = 124,
        pinky_02_r = 125,
        pinky_03_r = 126,
        pinky_04_r_MBONLY = 127,
        item_r = 128,
        lowerarm_twist_01_r = 129,
        upperarm_twist_01_r = 130,
        BackPack = 131,
        backpack_01 = 132,
        backpack_02 = 133,
        Slot_Primary = 134,
        Slot_Secondary = 135,
        Slot_Melee = 136,
        slot_throwable = 137,
        coat_l_01 = 138,
        coat_l_02 = 139,
        coat_l_03 = 140,
        coat_l_04 = 141,
        coat_fl_01 = 142,
        coat_fl_02 = 143,
        coat_fl_03 = 144,
        coat_fl_04 = 145,
        coat_b_01 = 146,
        coat_b_02 = 147,
        coat_b_03 = 148,
        coat_b_04 = 149,
        coat_r_01 = 150,
        coat_r_02 = 151,
        coat_r_03 = 152,
        coat_r_04 = 153,
        coat_fr_01 = 154,
        coat_fr_02 = 155,
        coat_fr_03 = 156,
        coat_fr_04 = 157,
        thigh_l = 158,
        calf_l = 159,
        foot_l = 160,
        ball_l = 161,
        calf_twist_01_l = 162,
        thigh_twist_01_l = 163,
        thigh_r = 164,
        calf_r = 165,
        foot_r = 166,
        ball_r = 167,
        calf_twist_01_r = 168,
        thigh_twist_01_r = 169,
        Slot_SideArm = 170,
        skirt_l_01 = 171,
        skirt_l_02 = 172,
        skirt_l_03 = 173,
        skirt_f_01 = 174,
        skirt_f_02 = 175,
        skirt_f_03 = 176,
        skirt_b_01 = 177,
        skirt_b_02 = 178,
        skirt_b_03 = 179,
        skirt_r_01 = 180,
        skirt_r_02 = 181,
        skirt_r_03 = 182,
        ik_hand_root = 183,
        ik_hand_gun = 184,
        ik_hand_r = 185,
        ik_hand_l = 186,
        ik_aim_root = 187,
        ik_aim_l = 188,
        ik_aim_r = 189,
        ik_foot_root = 190,
        ik_foot_l = 191,
        ik_foot_r = 192,
        camera_tpp = 193,
        ik_target_root = 194,
        ik_target_l = 195,
        ik_target_r = 196,
        VB_spine_03_spine_03 = 197,
        VB_upperarm_r_lowerarm_r = 198
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FRotator
    {
        public float Pitch;
        public float Yaw;
        public float Roll;

        public FRotator(float flPitch, float flYaw, float flRoll)
        {
            Pitch = flPitch;
            Yaw = flYaw;
            Roll = flRoll;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.Pitch * this.Pitch + this.Yaw * this.Yaw + this.Roll * this.Roll);
            }
        }

        public FRotator Clamp()
        {
            var result = this;

            if (result.Pitch > 180)
                result.Pitch -= 360;

            else if (result.Pitch < -180)
                result.Pitch += 360;

            if (result.Yaw > 180)
                result.Yaw -= 360;

            else if (result.Yaw < -180)
                result.Yaw += 360;

            if (result.Pitch < -89)
                result.Pitch = -89;

            if (result.Pitch > 89)
                result.Pitch = 89;

            while (result.Yaw < -180.0f)
                result.Yaw += 360.0f;

            while (result.Yaw > 180.0f)
                result.Yaw -= 360.0f;

            result.Roll = 0;

            return result;
        }

        public void GetAxes(out Vector3 x, out Vector3 y, out Vector3 z)
        {
            var m = ToMatrix();

            x = new Vector3(m.M11, m.M12, m.M13);
            y = new Vector3(m.M21, m.M22, m.M23);
            z = new Vector3(m.M31, m.M32, m.M33);
        }

        public Vector3 ToVector()
        {
            float radPitch = (float)(this.Pitch * Math.PI / 180f);
            float radYaw = (float)(this.Yaw * Math.PI / 180f);

            float SP = (float)Math.Sin(radPitch);
            float CP = (float)Math.Cos(radPitch);
            float SY = (float)Math.Sin(radYaw);
            float CY = (float)Math.Cos(radYaw);

            return new Vector3(CP * CY, CP * SY, SP);
        }

        public SharpDX.Matrix ToMatrix(Vector3 origin = default(Vector3))
        {

            float radPitch = (float)(this.Pitch * Math.PI / 180f);
            float radYaw = (float)(this.Yaw * Math.PI / 180f);
            float radRoll = (float)(this.Roll * Math.PI / 180f);

            float SP = (float)Math.Sin(radPitch);
            float CP = (float)Math.Cos(radPitch);
            float SY = (float)Math.Sin(radYaw);
            float CY = (float)Math.Cos(radYaw);
            float SR = (float)Math.Sin(radRoll);
            float CR = (float)Math.Cos(radRoll);

            SharpDX.Matrix m = new SharpDX.Matrix();
            m[0, 0] = CP * CY;
            m[0, 1] = CP * SY;
            m[0, 2] = SP;
            m[0, 3] = 0f;

            m[1, 0] = SR * SP * CY - CR * SY;
            m[1, 1] = SR * SP * SY + CR * CY;
            m[1, 2] = -SR * CP;
            m[1, 3] = 0f;

            m[2, 0] = -(CR * SP * CY + SR * SY);
            m[2, 1] = CY * SR - CR * SP * SY;
            m[2, 2] = CR * CP;
            m[2, 3] = 0f;

            m[3, 0] = origin.X;
            m[3, 1] = origin.Y;
            m[3, 2] = origin.Z;
            m[3, 3] = 1f;
            return m;
        }

        public static FRotator operator +(FRotator angA, FRotator angB) => new FRotator(angA.Pitch + angB.Pitch, angA.Yaw + angB.Yaw, angA.Roll + angB.Roll);
        public static FRotator operator -(FRotator angA, FRotator angB) => new FRotator(angA.Pitch - angB.Pitch, angA.Yaw - angB.Yaw, angA.Roll - angB.Roll);
        public static FRotator operator /(FRotator angA, float flNum) => new FRotator(angA.Pitch / flNum, angA.Yaw / flNum, angA.Roll / flNum);
        public static FRotator operator *(FRotator angA, float flNum) => new FRotator(angA.Pitch * flNum, angA.Yaw * flNum, angA.Roll * flNum);
        public static bool operator ==(FRotator angA, FRotator angB) => angA.Pitch == angB.Pitch && angA.Yaw == angB.Yaw && angA.Yaw == angB.Yaw;
        public static bool operator !=(FRotator angA, FRotator angB) => angA.Pitch != angB.Pitch || angA.Yaw != angB.Yaw || angA.Yaw != angB.Yaw;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            }
        }
        public Vector2 To2D()
        {
            return new Vector2(this.X, this.Y);
        }
        public FRotator ToFRotator()
        {
            FRotator rot = new FRotator();

            float RADPI = (float)(180 / Math.PI);
            rot.Yaw = (float)Math.Atan2(this.Y, this.X) * RADPI;
            rot.Pitch = (float)Math.Atan2(this.Z, Math.Sqrt((this.X * this.X) + (this.Y * this.Y))) * RADPI;
            rot.Roll = 0;

            return rot;
        }

        public static float DotProduct(Vector3 vecA, Vector3 vecB) => vecA.X * vecB.X + vecA.Y * vecB.Y + vecA.Z * vecB.Z;

        #region Overrides
        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
        #endregion
        #region Operators
        public static Vector3 operator +(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X + vecB.X, vecA.Y + vecB.Y, vecA.Z + vecB.Z);
        public static Vector3 operator -(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X - vecB.X, vecA.Y - vecB.Y, vecA.Z - vecB.Z);
        public static Vector3 operator *(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X * vecB.X, vecA.Y * vecB.Y, vecA.Z * vecB.Z);
        public static Vector3 operator *(Vector3 vecA, int n) => new Vector3(vecA.X * n, vecA.Y * n, vecA.Z * n);
        public static Vector3 operator /(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X / vecB.X, vecA.Y / vecB.Y, vecA.Z / vecB.Z);

        public static bool operator ==(Vector3 vecA, Vector3 vecB) => vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Y == vecB.Y;
        public static bool operator !=(Vector3 vecA, Vector3 vecB) => vecA.X != vecB.X || vecA.Y != vecB.Y || vecA.Y != vecB.Y;
        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y);
            }
        }
        #region Functions
        public Vector2 Rotate(Vector2 centerpoint, double angle, bool bAngleInRadians = false)
        {
            if (!bAngleInRadians)
                angle = Math.PI * angle / 180.0;

            return new Vector2(this.X * Math.Cos(angle) - this.Y * Math.Sin(angle), this.X * Math.Sin(angle) + this.Y * Math.Cos(angle));
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"{X},{Y}";
        }
        #endregion
        #region Operators
        public static Vector2 operator +(Vector2 vecA, Vector2 vecB) => new Vector2(vecA.X + vecB.X, vecA.Y + vecB.Y);
        public static Vector2 operator -(Vector2 vecA, Vector2 vecB) => new Vector2(vecA.X - vecB.X, vecA.Y - vecB.Y);
        public static Vector2 operator *(Vector2 vecA, int n) => new Vector2(vecA.X * n, vecA.Y * n);
        public static Vector2 operator /(Vector2 vecA, Vector2 vecB) => new Vector2(vecA.X / vecB.X, vecA.Y / vecB.Y);

        public static Vector2 operator +(Vector2 vecA, float val) => new Vector2(vecA.X + val, vecA.Y + val);
        public static Vector2 operator -(Vector2 vecA, float val) => new Vector2(vecA.X - val, vecA.Y - val);
        public static Vector2 operator *(Vector2 vecA, float val) => new Vector2(vecA.X * val, vecA.Y * val);
        public static Vector2 operator /(Vector2 vecA, float val) => new Vector2(vecA.X / val, vecA.Y / val);


        public static bool operator ==(Vector2 vecA, Vector2 vecB) => vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Y == vecB.Y;
        public static bool operator !=(Vector2 vecA, Vector2 vecB) => vecA.X != vecB.X || vecA.Y != vecB.Y || vecA.Y != vecB.Y;
        #endregion
    }

}