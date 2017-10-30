using System;
using System.Runtime.InteropServices;
using CPUZ.Model;

namespace CPUZ
{

    /// <summary>
    /// 数据包内容的头信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ReadStruct
    {
        public static readonly int Size = Marshal.SizeOf(typeof(ReadStruct));

        [MarshalAs(UnmanagedType.I8)]
        public ulong UserBufferAdress;

        [MarshalAs(UnmanagedType.I8)]
        public ulong GameAddressOffset;
        [MarshalAs(UnmanagedType.I8)]
        public ulong ReadSize;
        [MarshalAs(UnmanagedType.I4)]
        public uint UserPID;
        [MarshalAs(UnmanagedType.I4)]
        public uint GamePID;
        [MarshalAs(UnmanagedType.Bool)]
        public bool WriteOrRead;
        [MarshalAs(UnmanagedType.I4)]
        public uint ProtocolMsg;
        [MarshalAs(UnmanagedType.Bool)]
        public bool WriteOrRead2;
        [MarshalAs(UnmanagedType.I4)]
        public uint ProtocolMsg2;
    }



    public unsafe static class KReader
    {
        public static ulong m_PUBase = 0;

        const uint GENERIC_READ = 0x80000000;
        const int GENERIC_WRITE = 0x40000000;
        const uint OPEN_EXISTING = 3;
        const int FILE_SHARE_READ = 1;
        const int FILE_SHARE_WRITE = 2;
        static IntPtr handle;



        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        static extern unsafe System.IntPtr CreateFile
        (
            string FileName,          // file name
            uint DesiredAccess,       // access mode
            uint ShareMode,           // share mode
            uint SecurityAttributes,  // Security Attributes
            uint CreationDisposition, // how to create
            uint FlagsAndAttributes,  // file attributes
            int hTemplateFile         // handle to template file
        );

        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true)]
        static extern unsafe bool ReadFile
        (
            System.IntPtr hFile,      // handle to file
            void* pBuffer,            // data buffer
            uint NumberOfBytesToRead,  // number of bytes to read
            uint* pNumberOfBytesRead,  // number of bytes read
            uint Overlapped            // overlapped buffer
        );

        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true)]
        public static extern unsafe bool WriteFile
        (
            System.IntPtr hFile,      // handle to file
            void* pBuffer,            // data buffer
            int NumberOfBytesToRead,  // number of bytes to read
            int* pNumberOfBytesRead,  // number of bytes read
            uint Overlapped            // overlapped buffer
        );

        [System.Runtime.InteropServices.DllImport("kernel32", SetLastError = true)]
        static extern unsafe bool CloseHandle
        (
            System.IntPtr hObject // handle to object
        );


        public static bool Close()
        {
            return CloseHandle(handle);
        }

        public static ulong readPuBase()
        {
            ulong baseAdd = 0;
            void* read = null;
            ReadStruct rStruct = new ReadStruct()
            {
                UserBufferAdress = (ulong)&baseAdd,
                GameAddressOffset = (ulong)read,
                ReadSize = (ulong)Marshal.SizeOf(typeof(ulong)),
                UserPID = CommonUtil.GetCurrentProcessId(),
                GamePID = 0,
                WriteOrRead = true,
                ProtocolMsg = 1,
                WriteOrRead2 = true,
                ProtocolMsg2 = 0
            };

            // open the existing file for reading       
            handle = CreateFile
            (
                "\\\\.\\lolClientBase",
                GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                0,
                OPEN_EXISTING,
                0,
                0
            );

            if (handle != System.IntPtr.Zero)
            {
                if (WriteFile(handle, &rStruct, ReadStruct.Size, null, 0))
                {
                    m_PUBase = baseAdd;
                }
            }
            return baseAdd;
        }

        public static Vector3 readVec(ulong w_read, int sdf = 0)
        {
            Vector3 writeMe;
            ReadStruct rStruct = new ReadStruct()
            {
                UserBufferAdress = (ulong)&writeMe,
                GameAddressOffset = (ulong)w_read,
                ReadSize = (ulong)Marshal.SizeOf(typeof(Vector3)),
                UserPID = CommonUtil.GetCurrentProcessId(),
                GamePID = 0,
                WriteOrRead = true,
                ProtocolMsg = 0,
                WriteOrRead2 = true,
                ProtocolMsg2 = 0
            };
            WriteFile(handle, &rStruct, ReadStruct.Size, null, 0);

            return writeMe;
        }

        public static ulong readUlong(ulong w_read, int sdf = 0)
        {
            ulong writeMe;

            ReadStruct rStruct = new ReadStruct()
            {
                UserBufferAdress = (ulong)&writeMe,
                GameAddressOffset = (ulong)w_read,
                ReadSize = (ulong)Marshal.SizeOf(typeof(ulong)),
                UserPID = CommonUtil.GetCurrentProcessId(),
                GamePID = 0,
                WriteOrRead = true,
                ProtocolMsg = 0,
                WriteOrRead2 = true,
                ProtocolMsg2 = 0
            };

            WriteFile(handle, &rStruct, ReadStruct.Size, null, 0);
            return writeMe;
        }


        public static Int32 readInt32(ulong w_read, int sdf = 0)
        {
            Int32 writeMe;

            ReadStruct rStruct = new ReadStruct()
            {
                UserBufferAdress = (ulong)&writeMe,
                GameAddressOffset = (ulong)w_read,
                ReadSize = (ulong)Marshal.SizeOf(typeof(Int32)),
                UserPID = CommonUtil.GetCurrentProcessId(),
                GamePID = 0,
                WriteOrRead = true,
                ProtocolMsg = 0,
                WriteOrRead2 = true,
                ProtocolMsg2 = 0
            };

            WriteFile(handle, &rStruct, ReadStruct.Size, null, 0);
            return writeMe;
        }
        public static float readFloat(ulong w_read, int sdf = 0)
        {
            float writeMe;

            ReadStruct rStruct = new ReadStruct()
            {
                UserBufferAdress = (ulong)&writeMe,
                GameAddressOffset = (ulong)w_read,
                ReadSize = (ulong)Marshal.SizeOf(typeof(float)),
                UserPID = CommonUtil.GetCurrentProcessId(),
                GamePID = 0,
                WriteOrRead = true,
                ProtocolMsg = 0,
                WriteOrRead2 = true,
                ProtocolMsg2 = 0
            };

            WriteFile(handle, &rStruct, ReadStruct.Size, null, 0);
            return writeMe;
        }

        public static char readChar(ulong w_read, int sdf = 0)
        {
            char writeMe;

            ReadStruct rStruct = new ReadStruct()
            {
                UserBufferAdress = (ulong)&writeMe,
                GameAddressOffset = (ulong)w_read,
                ReadSize = (ulong)Marshal.SizeOf(typeof(char)),
                UserPID = CommonUtil.GetCurrentProcessId(),
                GamePID = 0,
                WriteOrRead = true,
                ProtocolMsg = 0,
                WriteOrRead2 = true,
                ProtocolMsg2 = 0
            };

            WriteFile(handle, &rStruct, ReadStruct.Size, null, 0);
            return writeMe;
        }


        public static byte[] readSize(ulong w_read, Int32 w_readSize, int asd = 0)
        {
            byte[] writeMe = new byte[w_readSize];
            fixed (byte* n = writeMe)
            {

                ReadStruct rStruct = new ReadStruct()
                {
                    UserBufferAdress = (ulong)n,
                    GameAddressOffset = (ulong)w_read,
                    ReadSize = (ulong)w_readSize - 2,
                    UserPID = CommonUtil.GetCurrentProcessId(),
                    GamePID = 0,
                    WriteOrRead = true,
                    ProtocolMsg = 0,
                    WriteOrRead2 = true,
                    ProtocolMsg2 = 0
                };
                WriteFile(handle, &rStruct, ReadStruct.Size, null, 0);

                for (int i = 0; i < w_readSize; i++)
                {
                    if (n[i] != 0)
                        writeMe[i] = n[i];
                }
                return writeMe;
            }
        }

        public static string getGNameFromId(Int32 w_id)
        {
            var GNames = readUlong(m_PUBase + 0x36E8790, PROTO_NORMAL_READ);
            var singleNameChunk = readUlong(GNames + (ulong)(w_id / 0x4000) * 8, PROTO_NORMAL_READ);
            var singleNamePtr = readUlong(singleNameChunk + (ulong)(8 * (w_id % 0x4000)), PROTO_NORMAL_READ);

            var name = System.Text.Encoding.Default.GetString(readSize(singleNamePtr + 16, 64, PROTO_NORMAL_READ));
            return name;
        }

        private const int PROTO_NORMAL_READ = 0;
    }

}