using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AssetStudio
{
	// Token: 0x0200014A RID: 330
	public static class BinaryReaderExtensions
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x00014BA0 File Offset: 0x00012DA0
		public static void AlignStream(this BinaryReader reader)
		{
			reader.AlignStream(4);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00014BAC File Offset: 0x00012DAC
		public static void AlignStream(this BinaryReader reader, int alignment)
		{
			long mod = reader.BaseStream.Position % (long)alignment;
			if (mod != 0L)
			{
				reader.BaseStream.Position += (long)alignment - mod;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public static string ReadAlignedString(this BinaryReader reader)
		{
			int length = reader.ReadInt32();
			if (length > 0 && (long)length <= reader.BaseStream.Length - reader.BaseStream.Position)
			{
				byte[] stringData = reader.ReadBytes(length);
				string @string = Encoding.UTF8.GetString(stringData);
				reader.AlignStream(4);
				return @string;
			}
			return "";
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00014C38 File Offset: 0x00012E38
		public static string ReadStringToNull(this BinaryReader reader, int maxLength = 32767)
		{
			List<byte> bytes = new List<byte>();
			int count = 0;
			while (reader.BaseStream.Position != reader.BaseStream.Length && count < maxLength)
			{
				byte b = reader.ReadByte();
				if (b == 0)
				{
					break;
				}
				bytes.Add(b);
				count++;
			}
			return Encoding.UTF8.GetString(bytes.ToArray());
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014C90 File Offset: 0x00012E90
		public static Quaternion ReadQuaternion(this BinaryReader reader)
		{
			return new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00014CAF File Offset: 0x00012EAF
		public static Vector2 ReadVector2(this BinaryReader reader)
		{
			return new Vector2(reader.ReadSingle(), reader.ReadSingle());
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00014CC2 File Offset: 0x00012EC2
		public static Vector3 ReadVector3(this BinaryReader reader)
		{
			return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014CDB File Offset: 0x00012EDB
		public static Vector4 ReadVector4(this BinaryReader reader)
		{
			return new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00014CFA File Offset: 0x00012EFA
		public static Color ReadColor4(this BinaryReader reader)
		{
			return new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014D19 File Offset: 0x00012F19
		public static Matrix4x4 ReadMatrix(this BinaryReader reader)
		{
			return new Matrix4x4(reader.ReadSingleArray(16));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00014D28 File Offset: 0x00012F28
		private static T[] ReadArray<T>(Func<T> del, int length)
		{
			T[] array = new T[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = del();
			}
			return array;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00014D56 File Offset: 0x00012F56
		public static bool[] ReadBooleanArray(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<bool>(new Func<bool>(reader.ReadBoolean), reader.ReadInt32());
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00014D70 File Offset: 0x00012F70
		public static byte[] ReadUInt8Array(this BinaryReader reader)
		{
			return reader.ReadBytes(reader.ReadInt32());
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00014D7E File Offset: 0x00012F7E
		public static ushort[] ReadUInt16Array(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<ushort>(new Func<ushort>(reader.ReadUInt16), reader.ReadInt32());
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00014D98 File Offset: 0x00012F98
		public static int[] ReadInt32Array(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<int>(new Func<int>(reader.ReadInt32), reader.ReadInt32());
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014DB2 File Offset: 0x00012FB2
		public static int[] ReadInt32Array(this BinaryReader reader, int length)
		{
			return BinaryReaderExtensions.ReadArray<int>(new Func<int>(reader.ReadInt32), length);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00014DC7 File Offset: 0x00012FC7
		public static uint[] ReadUInt32Array(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<uint>(new Func<uint>(reader.ReadUInt32), reader.ReadInt32());
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00014DE1 File Offset: 0x00012FE1
		public static uint[][] ReadUInt32ArrayArray(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<uint[]>(new Func<uint[]>(reader.ReadUInt32Array), reader.ReadInt32());
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00014DFA File Offset: 0x00012FFA
		public static uint[] ReadUInt32Array(this BinaryReader reader, int length)
		{
			return BinaryReaderExtensions.ReadArray<uint>(new Func<uint>(reader.ReadUInt32), length);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00014E0F File Offset: 0x0001300F
		public static float[] ReadSingleArray(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<float>(new Func<float>(reader.ReadSingle), reader.ReadInt32());
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00014E29 File Offset: 0x00013029
		public static float[] ReadSingleArray(this BinaryReader reader, int length)
		{
			return BinaryReaderExtensions.ReadArray<float>(new Func<float>(reader.ReadSingle), length);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00014E3E File Offset: 0x0001303E
		public static string[] ReadStringArray(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<string>(new Func<string>(reader.ReadAlignedString), reader.ReadInt32());
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00014E57 File Offset: 0x00013057
		public static Vector2[] ReadVector2Array(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<Vector2>(new Func<Vector2>(reader.ReadVector2), reader.ReadInt32());
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00014E70 File Offset: 0x00013070
		public static Vector4[] ReadVector4Array(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<Vector4>(new Func<Vector4>(reader.ReadVector4), reader.ReadInt32());
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00014E89 File Offset: 0x00013089
		public static Matrix4x4[] ReadMatrixArray(this BinaryReader reader)
		{
			return BinaryReaderExtensions.ReadArray<Matrix4x4>(new Func<Matrix4x4>(reader.ReadMatrix), reader.ReadInt32());
		}
	}
}
