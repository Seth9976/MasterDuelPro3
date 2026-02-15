using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace UnityEngine.AddressableAssets.Utility
{
	// Token: 0x02000043 RID: 67
	internal static class SerializationUtilities
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00007238 File Offset: 0x00005438
		internal static int ReadInt32FromByteArray(byte[] data, int offset)
		{
			return (int)data[offset] | ((int)data[offset + 1] << 8) | ((int)data[offset + 2] << 16) | ((int)data[offset + 3] << 24);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007257 File Offset: 0x00005457
		internal static int WriteInt32ToByteArray(byte[] data, int val, int offset)
		{
			data[offset] = (byte)(val & 255);
			data[offset + 1] = (byte)((val >> 8) & 255);
			data[offset + 2] = (byte)((val >> 16) & 255);
			data[offset + 3] = (byte)((val >> 24) & 255);
			return offset + 4;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007298 File Offset: 0x00005498
		internal static object ReadObjectFromByteArray(byte[] keyData, int dataIndex)
		{
			try
			{
				SerializationUtilities.ObjectType keyType = (SerializationUtilities.ObjectType)keyData[dataIndex];
				dataIndex++;
				switch (keyType)
				{
				case SerializationUtilities.ObjectType.AsciiString:
				{
					int dataLength = BitConverter.ToInt32(keyData, dataIndex);
					return Encoding.ASCII.GetString(keyData, dataIndex + 4, dataLength);
				}
				case SerializationUtilities.ObjectType.UnicodeString:
				{
					int dataLength2 = BitConverter.ToInt32(keyData, dataIndex);
					return Encoding.Unicode.GetString(keyData, dataIndex + 4, dataLength2);
				}
				case SerializationUtilities.ObjectType.UInt16:
					return BitConverter.ToUInt16(keyData, dataIndex);
				case SerializationUtilities.ObjectType.UInt32:
					return BitConverter.ToUInt32(keyData, dataIndex);
				case SerializationUtilities.ObjectType.Int32:
					return BitConverter.ToInt32(keyData, dataIndex);
				case SerializationUtilities.ObjectType.Hash128:
					return Hash128.Parse(Encoding.ASCII.GetString(keyData, dataIndex + 1, (int)keyData[dataIndex]));
				case SerializationUtilities.ObjectType.Type:
					return Type.GetTypeFromCLSID(new Guid(Encoding.ASCII.GetString(keyData, dataIndex + 1, (int)keyData[dataIndex])));
				case SerializationUtilities.ObjectType.JsonObject:
				{
					int assemblyNameLength = (int)keyData[dataIndex];
					dataIndex++;
					string assemblyName = Encoding.ASCII.GetString(keyData, dataIndex, assemblyNameLength);
					dataIndex += assemblyNameLength;
					int classNameLength = (int)keyData[dataIndex];
					dataIndex++;
					string className = Encoding.ASCII.GetString(keyData, dataIndex, classNameLength);
					dataIndex += classNameLength;
					int jsonLength = BitConverter.ToInt32(keyData, dataIndex);
					dataIndex += 4;
					string @string = Encoding.Unicode.GetString(keyData, dataIndex, jsonLength);
					Type t = Assembly.Load(assemblyName).GetType(className);
					return JsonUtility.FromJson(@string, t);
				}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			return null;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000742C File Offset: 0x0000562C
		internal static int WriteObjectToByteList(object obj, List<byte> buffer)
		{
			Type objectType = obj.GetType();
			if (objectType == typeof(string))
			{
				string str = obj as string;
				if (str == null)
				{
					str = string.Empty;
				}
				byte[] tmp = Encoding.Unicode.GetBytes(str);
				byte[] tmp2 = Encoding.ASCII.GetBytes(str);
				if (Encoding.Unicode.GetString(tmp) == Encoding.ASCII.GetString(tmp2))
				{
					buffer.Add(0);
					buffer.AddRange(BitConverter.GetBytes(tmp2.Length));
					buffer.AddRange(tmp2);
					return tmp2.Length + 5;
				}
				buffer.Add(1);
				buffer.AddRange(BitConverter.GetBytes(tmp.Length));
				buffer.AddRange(tmp);
				return tmp.Length + 5;
			}
			else
			{
				if (objectType == typeof(uint))
				{
					byte[] tmp3 = BitConverter.GetBytes((uint)obj);
					buffer.Add(3);
					buffer.AddRange(tmp3);
					return tmp3.Length + 1;
				}
				if (objectType == typeof(ushort))
				{
					byte[] tmp4 = BitConverter.GetBytes((ushort)obj);
					buffer.Add(2);
					buffer.AddRange(tmp4);
					return tmp4.Length + 1;
				}
				if (objectType == typeof(int))
				{
					byte[] tmp5 = BitConverter.GetBytes((int)obj);
					buffer.Add(4);
					buffer.AddRange(tmp5);
					return tmp5.Length + 1;
				}
				if (objectType == typeof(Hash128))
				{
					Hash128 guid = (Hash128)obj;
					byte[] tmp6 = Encoding.ASCII.GetBytes(guid.ToString());
					buffer.Add(5);
					buffer.Add((byte)tmp6.Length);
					buffer.AddRange(tmp6);
					return tmp6.Length + 2;
				}
				if (objectType == typeof(Type))
				{
					byte[] tmp7 = objectType.GUID.ToByteArray();
					buffer.Add(6);
					buffer.Add((byte)tmp7.Length);
					buffer.AddRange(tmp7);
					return tmp7.Length + 2;
				}
				if (objectType.GetCustomAttributes(typeof(SerializableAttribute), true).Length == 0)
				{
					return 0;
				}
				int num = 0;
				buffer.Add(7);
				int num2 = num + 1;
				byte[] tmpAssemblyName = Encoding.ASCII.GetBytes(objectType.Assembly.FullName);
				buffer.Add((byte)tmpAssemblyName.Length);
				int num3 = num2 + 1;
				buffer.AddRange(tmpAssemblyName);
				int num4 = num3 + tmpAssemblyName.Length;
				string objName = objectType.FullName;
				if (objName == null)
				{
					objName = string.Empty;
				}
				byte[] tmpClassName = Encoding.ASCII.GetBytes(objName);
				buffer.Add((byte)tmpClassName.Length);
				int num5 = num4 + 1;
				buffer.AddRange(tmpClassName);
				int num6 = num5 + tmpClassName.Length;
				byte[] tmpJson = Encoding.Unicode.GetBytes(JsonUtility.ToJson(obj));
				buffer.AddRange(BitConverter.GetBytes(tmpJson.Length));
				int num7 = num6 + 4;
				buffer.AddRange(tmpJson);
				return num7 + tmpJson.Length;
			}
		}

		// Token: 0x02000044 RID: 68
		internal enum ObjectType
		{
			// Token: 0x040000DA RID: 218
			AsciiString,
			// Token: 0x040000DB RID: 219
			UnicodeString,
			// Token: 0x040000DC RID: 220
			UInt16,
			// Token: 0x040000DD RID: 221
			UInt32,
			// Token: 0x040000DE RID: 222
			Int32,
			// Token: 0x040000DF RID: 223
			Hash128,
			// Token: 0x040000E0 RID: 224
			Type,
			// Token: 0x040000E1 RID: 225
			JsonObject
		}
	}
}
