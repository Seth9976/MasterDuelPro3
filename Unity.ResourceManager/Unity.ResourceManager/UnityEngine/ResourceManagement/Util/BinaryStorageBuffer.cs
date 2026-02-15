using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000019 RID: 25
	internal class BinaryStorageBuffer
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000044AD File Offset: 0x000026AD
		private unsafe static void ComputeHash(void* pData, ulong size, Hash128* hash)
		{
			if (pData == null || size == 0UL)
			{
				*hash = default(Hash128);
				return;
			}
			HashUnsafeUtilities.ComputeHash128(pData, size, hash);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000044C8 File Offset: 0x000026C8
		private static void AddSerializationAdapter(Dictionary<Type, BinaryStorageBuffer.ISerializationAdapter> serializationAdapters, BinaryStorageBuffer.ISerializationAdapter adapter, bool forceOverride = false)
		{
			bool added = false;
			foreach (Type i in adapter.GetType().GetInterfaces())
			{
				if (i.IsGenericType && typeof(BinaryStorageBuffer.ISerializationAdapter).IsAssignableFrom(i))
				{
					Type aType = i.GenericTypeArguments[0];
					if (serializationAdapters.ContainsKey(aType))
					{
						if (forceOverride)
						{
							BinaryStorageBuffer.ISerializationAdapter prevAdapter = serializationAdapters[aType];
							serializationAdapters.Remove(aType);
							serializationAdapters[aType] = adapter;
							added = true;
							Debug.Log(string.Format("Replacing adapter for type {0}: {1} -> {2}", aType, prevAdapter, adapter));
						}
						else
						{
							Debug.Log(string.Format("Failed to register adapter for type {0}: {1}, {2} is already registered.", aType, adapter, serializationAdapters[aType]));
						}
					}
					else
					{
						serializationAdapters[aType] = adapter;
						added = true;
					}
				}
			}
			if (added)
			{
				IEnumerable<BinaryStorageBuffer.ISerializationAdapter> deps = adapter.Dependencies;
				if (deps != null)
				{
					foreach (BinaryStorageBuffer.ISerializationAdapter d in deps)
					{
						BinaryStorageBuffer.AddSerializationAdapter(serializationAdapters, d, false);
					}
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000045E0 File Offset: 0x000027E0
		private static bool GetSerializationAdapter(Dictionary<Type, BinaryStorageBuffer.ISerializationAdapter> serializationAdapters, Type t, out BinaryStorageBuffer.ISerializationAdapter adapter)
		{
			if (!serializationAdapters.TryGetValue(t, out adapter))
			{
				foreach (KeyValuePair<Type, BinaryStorageBuffer.ISerializationAdapter> i in serializationAdapters)
				{
					if (i.Key.IsAssignableFrom(t))
					{
						BinaryStorageBuffer.ISerializationAdapter value;
						adapter = (value = i.Value);
						return value != null;
					}
				}
				Debug.LogError(string.Format("Unable to find serialization adapter for type {0}.", t));
			}
			return adapter != null;
		}

		// Token: 0x04000054 RID: 84
		private const uint kUnicodeStringFlag = 2147483648U;

		// Token: 0x04000055 RID: 85
		private const uint kDynamicStringFlag = 1073741824U;

		// Token: 0x04000056 RID: 86
		private const uint kClearFlagsMask = 1073741823U;

		// Token: 0x0200001A RID: 26
		private class BuiltinTypesSerializer : BinaryStorageBuffer.ISerializationAdapter<int>, BinaryStorageBuffer.ISerializationAdapter, BinaryStorageBuffer.ISerializationAdapter<bool>, BinaryStorageBuffer.ISerializationAdapter<long>, BinaryStorageBuffer.ISerializationAdapter<string>, BinaryStorageBuffer.ISerializationAdapter<Hash128>
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000466C File Offset: 0x0000286C
			public IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00004670 File Offset: 0x00002870
			public object Deserialize(BinaryStorageBuffer.Reader reader, Type t, uint offset)
			{
				if (offset == 4294967295U)
				{
					return null;
				}
				if (t == typeof(int))
				{
					return reader.ReadValue<int>(offset);
				}
				if (t == typeof(bool))
				{
					return reader.ReadValue<bool>(offset);
				}
				if (t == typeof(long))
				{
					return reader.ReadValue<long>(offset);
				}
				if (t == typeof(Hash128))
				{
					return reader.ReadValue<Hash128>(offset);
				}
				if (t == typeof(string))
				{
					BinaryStorageBuffer.BuiltinTypesSerializer.ObjectToStringRemap remap = reader.ReadValue<BinaryStorageBuffer.BuiltinTypesSerializer.ObjectToStringRemap>(offset);
					return reader.ReadString(remap.stringId, remap.separator, false);
				}
				return null;
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00004730 File Offset: 0x00002930
			private char FindBestSeparator(string str, params char[] seps)
			{
				int bestCount = 0;
				char bestSep = '\0';
				for (int i = 0; i < seps.Length; i++)
				{
					char s = seps[i];
					int sepCount = str.Count((char c) => c == s);
					if (sepCount > bestCount)
					{
						bestCount = sepCount;
						bestSep = s;
					}
				}
				if (bestCount == 0)
				{
					return '\0';
				}
				string[] array = str.Split(bestSep, StringSplitOptions.None);
				int validParts = 0;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i].Length > 4)
					{
						validParts++;
					}
				}
				if (validParts <= 1)
				{
					return '\0';
				}
				return bestSep;
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x000047C8 File Offset: 0x000029C8
			public uint Serialize(BinaryStorageBuffer.Writer writer, object val)
			{
				if (val == null)
				{
					return uint.MaxValue;
				}
				Type t = val.GetType();
				if (t == typeof(int))
				{
					return writer.Write<int>((int)val);
				}
				if (t == typeof(bool))
				{
					return writer.Write<bool>((bool)val);
				}
				if (t == typeof(long))
				{
					return writer.Write<long>((long)val);
				}
				if (t == typeof(Hash128))
				{
					return writer.Write<Hash128>((Hash128)val);
				}
				if (!(t == typeof(string)))
				{
					return uint.MaxValue;
				}
				string str = val as string;
				if (string.IsNullOrEmpty(str))
				{
					return uint.MaxValue;
				}
				char bestSep = this.FindBestSeparator(str, new char[] { '/', '\\', '.', '-', '_', ',' });
				return writer.Write<BinaryStorageBuffer.BuiltinTypesSerializer.ObjectToStringRemap>(new BinaryStorageBuffer.BuiltinTypesSerializer.ObjectToStringRemap
				{
					stringId = writer.WriteString((string)val, bestSep),
					separator = bestSep
				});
			}

			// Token: 0x0200001B RID: 27
			private struct ObjectToStringRemap
			{
				// Token: 0x04000057 RID: 87
				public uint stringId;

				// Token: 0x04000058 RID: 88
				public char separator;
			}
		}

		// Token: 0x0200001D RID: 29
		private class TypeSerializer : BinaryStorageBuffer.ISerializationAdapter<Type>, BinaryStorageBuffer.ISerializationAdapter
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000466C File Offset: 0x0000286C
			public IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060000CA RID: 202 RVA: 0x000048D4 File Offset: 0x00002AD4
			public object Deserialize(BinaryStorageBuffer.Reader reader, Type type, uint offset)
			{
				object obj;
				try
				{
					BinaryStorageBuffer.TypeSerializer.Data d = reader.ReadValue<BinaryStorageBuffer.TypeSerializer.Data>(offset);
					string text = reader.ReadString(d.assemblyId, '.', true);
					string className = reader.ReadString(d.classId, '.', true);
					Assembly assembly = Assembly.Load(text);
					obj = ((assembly == null) ? null : assembly.GetType(className));
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					obj = null;
				}
				return obj;
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00004940 File Offset: 0x00002B40
			public uint Serialize(BinaryStorageBuffer.Writer writer, object val)
			{
				if (val == null)
				{
					return uint.MaxValue;
				}
				Type t = val as Type;
				return writer.Write<BinaryStorageBuffer.TypeSerializer.Data>(new BinaryStorageBuffer.TypeSerializer.Data
				{
					assemblyId = writer.WriteString(t.Assembly.FullName, '.'),
					classId = writer.WriteString(t.FullName, '.')
				});
			}

			// Token: 0x0200001E RID: 30
			private struct Data
			{
				// Token: 0x0400005A RID: 90
				public uint assemblyId;

				// Token: 0x0400005B RID: 91
				public uint classId;
			}
		}

		// Token: 0x0200001F RID: 31
		private struct DynamicString
		{
			// Token: 0x0400005C RID: 92
			public uint stringId;

			// Token: 0x0400005D RID: 93
			public uint nextId;
		}

		// Token: 0x02000020 RID: 32
		private struct ObjectTypeData
		{
			// Token: 0x0400005E RID: 94
			public uint typeId;

			// Token: 0x0400005F RID: 95
			public uint objectId;
		}

		// Token: 0x02000021 RID: 33
		public interface ISerializationAdapter
		{
			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000CD RID: 205
			IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies { get; }

			// Token: 0x060000CE RID: 206
			uint Serialize(BinaryStorageBuffer.Writer writer, object val);

			// Token: 0x060000CF RID: 207
			object Deserialize(BinaryStorageBuffer.Reader reader, Type t, uint offset);
		}

		// Token: 0x02000022 RID: 34
		public interface ISerializationAdapter<T> : BinaryStorageBuffer.ISerializationAdapter
		{
		}

		// Token: 0x02000023 RID: 35
		public class Reader
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x00004998 File Offset: 0x00002B98
			private void Init(byte[] data, int maxCachedObjects, params BinaryStorageBuffer.ISerializationAdapter[] adapters)
			{
				this.m_Buffer = data;
				this.stringBuilder = new StringBuilder(1024);
				this.m_Cache = new LRUCache<uint, object>(maxCachedObjects);
				this.m_Adapters = new Dictionary<Type, BinaryStorageBuffer.ISerializationAdapter>();
				foreach (BinaryStorageBuffer.ISerializationAdapter a in adapters)
				{
					BinaryStorageBuffer.AddSerializationAdapter(this.m_Adapters, a, false);
				}
				BinaryStorageBuffer.AddSerializationAdapter(this.m_Adapters, new BinaryStorageBuffer.TypeSerializer(), false);
				BinaryStorageBuffer.AddSerializationAdapter(this.m_Adapters, new BinaryStorageBuffer.BuiltinTypesSerializer(), false);
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00004A16 File Offset: 0x00002C16
			public void AddSerializationAdapter(BinaryStorageBuffer.ISerializationAdapter a)
			{
				BinaryStorageBuffer.AddSerializationAdapter(this.m_Adapters, a, false);
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00004A25 File Offset: 0x00002C25
			public Reader(byte[] data, int maxCachedObjects = 1024, params BinaryStorageBuffer.ISerializationAdapter[] adapters)
			{
				this.Init(data, maxCachedObjects, adapters);
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00004A36 File Offset: 0x00002C36
			internal byte[] GetBuffer()
			{
				return this.m_Buffer;
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00004A40 File Offset: 0x00002C40
			public Reader(Stream inputStream, uint bufferSize, int maxCachedObjects, params BinaryStorageBuffer.ISerializationAdapter[] adapters)
			{
				byte[] data = new byte[(bufferSize == 0U) ? inputStream.Length : ((long)((ulong)bufferSize))];
				inputStream.Read(data, 0, data.Length);
				this.Init(data, maxCachedObjects, adapters);
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00004A80 File Offset: 0x00002C80
			private bool TryGetCachedValue<T>(uint offset, out T val)
			{
				object obj;
				if (this.m_Cache.TryGet(offset, out obj))
				{
					val = (T)((object)obj);
					return true;
				}
				val = default(T);
				return false;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00004AB4 File Offset: 0x00002CB4
			public object[] ReadObjectArray(uint id, bool cacheValues = true)
			{
				if (id == 4294967295U)
				{
					return null;
				}
				uint[] ids = this.ReadValueArray<uint>(id, cacheValues);
				object[] objs = new object[ids.Length];
				for (int i = 0; i < ids.Length; i++)
				{
					objs[i] = this.ReadObject(ids[i], cacheValues);
				}
				return objs;
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x00004AF8 File Offset: 0x00002CF8
			public object[] ReadObjectArray(Type t, uint id, bool cacheValues = true)
			{
				if (id == 4294967295U)
				{
					return null;
				}
				uint[] ids = this.ReadValueArray<uint>(id, cacheValues);
				object[] objs = new object[ids.Length];
				for (int i = 0; i < ids.Length; i++)
				{
					objs[i] = this.ReadObject(t, ids[i], cacheValues);
				}
				return objs;
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x00004B3C File Offset: 0x00002D3C
			public T[] ReadObjectArray<T>(uint id, bool cacheValues = true)
			{
				if (id == 4294967295U)
				{
					return null;
				}
				uint[] ids = this.ReadValueArray<uint>(id, cacheValues);
				T[] objs = new T[ids.Length];
				for (int i = 0; i < ids.Length; i++)
				{
					objs[i] = this.ReadObject<T>(ids[i], cacheValues);
				}
				return objs;
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x00004B84 File Offset: 0x00002D84
			public object ReadObject(uint id, bool cacheValue = true)
			{
				if (id == 4294967295U)
				{
					return null;
				}
				BinaryStorageBuffer.ObjectTypeData td = this.ReadValue<BinaryStorageBuffer.ObjectTypeData>(id);
				Type type = this.ReadObject<Type>(td.typeId, true);
				return this.ReadObject(type, td.objectId, cacheValue);
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00004BBB File Offset: 0x00002DBB
			public T ReadObject<T>(uint offset, bool cacheValue = true)
			{
				return (T)((object)this.ReadObject(typeof(T), offset, cacheValue));
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00004BD4 File Offset: 0x00002DD4
			public object ReadObject(Type t, uint id, bool cacheValue = true)
			{
				if (id == 4294967295U)
				{
					return null;
				}
				object val;
				if (this.TryGetCachedValue<object>(id, out val))
				{
					return val;
				}
				BinaryStorageBuffer.ISerializationAdapter adapter;
				if (!BinaryStorageBuffer.GetSerializationAdapter(this.m_Adapters, t, out adapter))
				{
					return null;
				}
				object res = null;
				try
				{
					res = adapter.Deserialize(this, t, id);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					return null;
				}
				if (cacheValue && res != null)
				{
					this.m_Cache.TryAdd(id, res);
				}
				return res;
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00004C44 File Offset: 0x00002E44
			public unsafe T[] ReadValueArray<[IsUnmanaged] T>(uint id, bool cacheValue = true) where T : struct, ValueType
			{
				if (id == 4294967295U)
				{
					return null;
				}
				if ((ulong)(id - 4U) >= (ulong)((long)this.m_Buffer.Length))
				{
					throw new Exception(string.Format("Data offset {0} is out of bounds of buffer with length of {1}.", id, this.m_Buffer.Length));
				}
				fixed (byte* ptr = &this.m_Buffer[(int)(id - 4U)])
				{
					byte* pData = ptr;
					T[] vals;
					if (this.TryGetCachedValue<T[]>(id, out vals))
					{
						return vals;
					}
					uint size = 0U;
					UnsafeUtility.MemCpy((void*)(&size), (void*)pData, 4L);
					if ((ulong)(id + size) > (ulong)((long)this.m_Buffer.Length))
					{
						throw new Exception(string.Format("Data size {0} is out of bounds of buffer with length of {1}.", size, this.m_Buffer.Length));
					}
					T[] valsT = new T[(ulong)size / (ulong)((long)sizeof(T))];
					T[] array;
					T* pVals;
					if ((array = valsT) == null || array.Length == 0)
					{
						pVals = null;
					}
					else
					{
						pVals = &array[0];
					}
					UnsafeUtility.MemCpy((void*)pVals, (void*)(pData + 4), (long)((ulong)size));
					array = null;
					if (cacheValue)
					{
						this.m_Cache.TryAdd(id, valsT);
					}
					return valsT;
				}
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00004D3C File Offset: 0x00002F3C
			public unsafe T ReadValue<[IsUnmanaged] T>(uint id) where T : struct, ValueType
			{
				if (id == 4294967295U)
				{
					return default(T);
				}
				if ((ulong)id >= (ulong)((long)this.m_Buffer.Length))
				{
					throw new Exception(string.Format("Data offset {0} is out of bounds of buffer with length of {1}.", id, this.m_Buffer.Length));
				}
				byte[] buffer;
				byte* pData;
				if ((buffer = this.m_Buffer) == null || buffer.Length == 0)
				{
					pData = null;
				}
				else
				{
					pData = &buffer[0];
				}
				T val;
				UnsafeUtility.MemCpy((void*)(&val), (void*)(pData + id), (long)sizeof(T));
				return val;
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00004DB8 File Offset: 0x00002FB8
			public string ReadString(uint id, char sep = '\0', bool cacheValue = true)
			{
				if (id == 4294967295U)
				{
					return null;
				}
				if (sep == '\0')
				{
					return this.ReadAutoEncodedString(id, cacheValue);
				}
				return this.ReadDynamicString(id, sep, cacheValue);
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00004DD8 File Offset: 0x00002FD8
			private unsafe string ReadStringInternal(uint offset, Encoding enc, bool cacheValue = true)
			{
				if ((ulong)(offset - 4U) >= (ulong)((long)this.m_Buffer.Length))
				{
					throw new Exception(string.Format("Data offset {0} is out of bounds of buffer with length of {1}.", offset, this.m_Buffer.Length));
				}
				string val;
				if (this.TryGetCachedValue<string>(offset, out val))
				{
					return val;
				}
				byte[] buffer;
				byte* pData;
				if ((buffer = this.m_Buffer) == null || buffer.Length == 0)
				{
					pData = null;
				}
				else
				{
					pData = &buffer[0];
				}
				uint strDataLength = *(uint*)(pData + (offset - 4U));
				if ((ulong)(offset + strDataLength) > (ulong)((long)this.m_Buffer.Length))
				{
					throw new Exception(string.Format("Data offset {0}, len {1} is out of bounds of buffer with length of {2}.", offset, strDataLength, this.m_Buffer.Length));
				}
				string valStr = enc.GetString(pData + offset, (int)strDataLength);
				if (cacheValue)
				{
					this.m_Cache.TryAdd(offset, valStr);
				}
				return valStr;
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00004EA4 File Offset: 0x000030A4
			private string ReadAutoEncodedString(uint id, bool cacheValue)
			{
				if ((id & 2147483648U) == 2147483648U)
				{
					return this.ReadStringInternal(id & 1073741823U, Encoding.Unicode, cacheValue);
				}
				return this.ReadStringInternal(id, Encoding.ASCII, cacheValue);
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x00004ED8 File Offset: 0x000030D8
			private string ReadDynamicString(uint id, char sep, bool cacheValue)
			{
				if ((id & 1073741824U) == 1073741824U)
				{
					string str;
					if (!this.TryGetCachedValue<string>(id, out str))
					{
						Stack<BinaryStorageBuffer.DynamicString> partStack = new Stack<BinaryStorageBuffer.DynamicString>();
						BinaryStorageBuffer.DynamicString ds;
						for (uint nextID = id; nextID != 4294967295U; nextID = ds.nextId)
						{
							ds = this.ReadValue<BinaryStorageBuffer.DynamicString>(nextID & 1073741823U);
							partStack.Push(ds);
						}
						BinaryStorageBuffer.DynamicString ds2;
						while (partStack.TryPop(out ds2))
						{
							this.stringBuilder.Append(this.ReadAutoEncodedString(ds2.stringId, cacheValue));
							if (partStack.Count != 0)
							{
								this.stringBuilder.Append(sep);
							}
						}
						str = this.stringBuilder.ToString();
						this.stringBuilder.Clear();
						if (cacheValue)
						{
							this.m_Cache.TryAdd(id, str);
						}
					}
					return str;
				}
				return this.ReadAutoEncodedString(id, cacheValue);
			}

			// Token: 0x04000060 RID: 96
			private byte[] m_Buffer;

			// Token: 0x04000061 RID: 97
			private Dictionary<Type, BinaryStorageBuffer.ISerializationAdapter> m_Adapters;

			// Token: 0x04000062 RID: 98
			private LRUCache<uint, object> m_Cache;

			// Token: 0x04000063 RID: 99
			private StringBuilder stringBuilder;
		}

		// Token: 0x02000024 RID: 36
		public class Writer
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004F9C File Offset: 0x0000319C
			public uint Length
			{
				get
				{
					return this.totalBytes;
				}
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x00004FA4 File Offset: 0x000031A4
			public Writer(int chunkSize = 1048576, params BinaryStorageBuffer.ISerializationAdapter[] adapters)
			{
				this.defaulChunkSize = (uint)((chunkSize > 0) ? chunkSize : 1048576);
				this.existingValues = new Dictionary<Hash128, uint>();
				this.chunks = new List<BinaryStorageBuffer.Writer.Chunk>(10);
				this.chunks.Add(new BinaryStorageBuffer.Writer.Chunk
				{
					position = 0U
				});
				this.serializationAdapters = new Dictionary<Type, BinaryStorageBuffer.ISerializationAdapter>();
				BinaryStorageBuffer.AddSerializationAdapter(this.serializationAdapters, new BinaryStorageBuffer.TypeSerializer(), false);
				BinaryStorageBuffer.AddSerializationAdapter(this.serializationAdapters, new BinaryStorageBuffer.BuiltinTypesSerializer(), false);
				foreach (BinaryStorageBuffer.ISerializationAdapter a in adapters)
				{
					BinaryStorageBuffer.AddSerializationAdapter(this.serializationAdapters, a, true);
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00005048 File Offset: 0x00003248
			private BinaryStorageBuffer.Writer.Chunk FindChunkWithSpace(uint length)
			{
				BinaryStorageBuffer.Writer.Chunk chunk = this.chunks[this.chunks.Count - 1];
				if (chunk.data == null)
				{
					chunk.data = new byte[(length > this.defaulChunkSize) ? length : this.defaulChunkSize];
				}
				if ((ulong)length > (ulong)((long)chunk.data.Length - (long)((ulong)chunk.position)))
				{
					chunk = new BinaryStorageBuffer.Writer.Chunk
					{
						position = 0U,
						data = new byte[(length > this.defaulChunkSize) ? length : this.defaulChunkSize]
					};
					this.chunks.Add(chunk);
				}
				return chunk;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x000050E0 File Offset: 0x000032E0
			private unsafe uint WriteInternal(void* pData, uint dataSize, bool prefixSize)
			{
				Hash128 hash;
				BinaryStorageBuffer.ComputeHash(pData, (ulong)dataSize, &hash);
				uint existingOffset;
				if (this.existingValues.TryGetValue(hash, out existingOffset))
				{
					return existingOffset;
				}
				uint addedBytes = (prefixSize ? (dataSize + 4U) : dataSize);
				BinaryStorageBuffer.Writer.Chunk chunk = this.FindChunkWithSpace(addedBytes);
				fixed (byte* ptr = &chunk.data[(int)chunk.position])
				{
					byte* pChunkData = ptr;
					uint id = this.totalBytes;
					if (prefixSize)
					{
						UnsafeUtility.MemCpy((void*)pChunkData, (void*)(&dataSize), 4L);
						if (dataSize > 0U)
						{
							UnsafeUtility.MemCpy((void*)(pChunkData + 4), pData, (long)((ulong)dataSize));
						}
						id += 4U;
					}
					else
					{
						if (dataSize == 0U)
						{
							return uint.MaxValue;
						}
						UnsafeUtility.MemCpy((void*)pChunkData, pData, (long)((ulong)dataSize));
					}
					this.totalBytes += addedBytes;
					chunk.position += addedBytes;
					this.existingValues[hash] = id;
					return id;
				}
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x000051A0 File Offset: 0x000033A0
			private uint ReserveInternal(uint dataSize, bool prefixSize)
			{
				uint addedBytes = (prefixSize ? (dataSize + 4U) : dataSize);
				BinaryStorageBuffer.Writer.Chunk chunk = this.FindChunkWithSpace(addedBytes);
				this.totalBytes += addedBytes;
				chunk.position += addedBytes;
				return this.totalBytes - dataSize;
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x000051E4 File Offset: 0x000033E4
			private unsafe void WriteInternal(uint id, void* pData, uint dataSize, bool prefixSize)
			{
				Hash128 hash;
				BinaryStorageBuffer.ComputeHash(pData, (ulong)dataSize, &hash);
				this.existingValues[hash] = id;
				uint chunkOffset = id;
				foreach (BinaryStorageBuffer.Writer.Chunk c in this.chunks)
				{
					if (chunkOffset < c.position)
					{
						try
						{
							byte[] array;
							byte* pChunkData;
							if ((array = c.data) == null || array.Length == 0)
							{
								pChunkData = null;
							}
							else
							{
								pChunkData = &array[0];
							}
							if (prefixSize)
							{
								UnsafeUtility.MemCpy((void*)(pChunkData + (chunkOffset - 4U)), (void*)(&dataSize), 4L);
							}
							UnsafeUtility.MemCpy((void*)(pChunkData + chunkOffset), pData, (long)((ulong)dataSize));
							break;
						}
						finally
						{
							byte[] array = null;
						}
					}
					chunkOffset -= c.position;
				}
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x000052B8 File Offset: 0x000034B8
			public uint Reserve<[IsUnmanaged] T>() where T : struct, ValueType
			{
				return this.ReserveInternal((uint)sizeof(T), false);
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x000052C8 File Offset: 0x000034C8
			public unsafe uint Write<[IsUnmanaged] T>(in T val) where T : struct, ValueType
			{
				fixed (T* ptr = &val)
				{
					T* pData = ptr;
					return this.WriteInternal((void*)pData, (uint)sizeof(T), false);
				}
			}

			// Token: 0x060000EA RID: 234 RVA: 0x000052E8 File Offset: 0x000034E8
			public unsafe uint Write<[IsUnmanaged] T>(T val) where T : struct, ValueType
			{
				return this.WriteInternal((void*)(&val), (uint)sizeof(T), false);
			}

			// Token: 0x060000EB RID: 235 RVA: 0x000052FC File Offset: 0x000034FC
			public unsafe uint Write<[IsUnmanaged] T>(uint offset, in T val) where T : struct, ValueType
			{
				fixed (T* ptr = &val)
				{
					T* pData = ptr;
					this.WriteInternal(offset, (void*)pData, (uint)sizeof(T), false);
				}
				return offset;
			}

			// Token: 0x060000EC RID: 236 RVA: 0x00005321 File Offset: 0x00003521
			public unsafe uint Write<[IsUnmanaged] T>(uint offset, T val) where T : struct, ValueType
			{
				this.WriteInternal(offset, (void*)(&val), (uint)sizeof(T), false);
				return offset;
			}

			// Token: 0x060000ED RID: 237 RVA: 0x00005335 File Offset: 0x00003535
			public uint Reserve<[IsUnmanaged] T>(uint count) where T : struct, ValueType
			{
				return this.ReserveInternal((uint)(sizeof(T) * (int)count), true);
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00005348 File Offset: 0x00003548
			public unsafe uint Write<[IsUnmanaged] T>(T[] values, bool hashElements = true) where T : struct, ValueType
			{
				T* pData;
				if (values == null || values.Length == 0)
				{
					pData = null;
				}
				else
				{
					pData = &values[0];
				}
				uint size = (uint)(values.Length * sizeof(T));
				Hash128 hash;
				BinaryStorageBuffer.ComputeHash((void*)pData, (ulong)size, &hash);
				uint existingOffset;
				if (this.existingValues.TryGetValue(hash, out existingOffset))
				{
					return existingOffset;
				}
				BinaryStorageBuffer.Writer.Chunk chunk = this.FindChunkWithSpace(size + 4U);
				fixed (byte* ptr = &chunk.data[(int)chunk.position])
				{
					byte* ptr2 = ptr;
					uint id = this.totalBytes + 4U;
					UnsafeUtility.MemCpy((void*)ptr2, (void*)(&size), 4L);
					UnsafeUtility.MemCpy((void*)(ptr2 + 4), (void*)pData, (long)((ulong)size));
					uint addedBytes = size + 4U;
					this.totalBytes += addedBytes;
					chunk.position += addedBytes;
					this.existingValues[hash] = id;
					if (hashElements && sizeof(T) > 4)
					{
						for (int i = 0; i < values.Length; i++)
						{
							hash = default(Hash128);
							BinaryStorageBuffer.ComputeHash((void*)(pData + (IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (ulong)((long)sizeof(T)), &hash);
							this.existingValues[hash] = id + (uint)(i * sizeof(T));
						}
					}
					return id;
				}
			}

			// Token: 0x060000EF RID: 239 RVA: 0x00005468 File Offset: 0x00003668
			public unsafe uint Write<[IsUnmanaged] T>(uint offset, T[] values, bool hashElements = true) where T : struct, ValueType
			{
				uint dataSize = (uint)(values.Length * sizeof(T));
				uint chunkOffset = offset;
				fixed (T[] array = values)
				{
					T* pValues;
					if (values == null || array.Length == 0)
					{
						pValues = null;
					}
					else
					{
						pValues = &array[0];
					}
					foreach (BinaryStorageBuffer.Writer.Chunk c in this.chunks)
					{
						if (chunkOffset < c.position)
						{
							try
							{
								byte[] array2;
								byte* pChunkData;
								if ((array2 = c.data) == null || array2.Length == 0)
								{
									pChunkData = null;
								}
								else
								{
									pChunkData = &array2[0];
								}
								UnsafeUtility.MemCpy((void*)(pChunkData + (chunkOffset - 4U)), (void*)(&dataSize), 4L);
								UnsafeUtility.MemCpy((void*)(pChunkData + chunkOffset), (void*)pValues, (long)((ulong)dataSize));
								if (hashElements && sizeof(T) > 4)
								{
									for (int i = 0; i < values.Length; i++)
									{
										T v = values[i];
										Hash128 hash;
										BinaryStorageBuffer.ComputeHash((void*)(&v), (ulong)((long)sizeof(T)), &hash);
										this.existingValues[hash] = offset + (uint)(i * sizeof(T));
									}
								}
								return offset;
							}
							finally
							{
								byte[] array2 = null;
							}
						}
						chunkOffset -= c.position;
					}
				}
				return uint.MaxValue;
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x000055A8 File Offset: 0x000037A8
			public uint WriteObjects<T>(IEnumerable<T> objs, bool serizalizeTypeData)
			{
				if (objs == null)
				{
					return uint.MaxValue;
				}
				uint[] ids = new uint[objs.Count<T>()];
				int index = 0;
				foreach (T o in objs)
				{
					ids[index++] = this.WriteObject(o, serizalizeTypeData);
				}
				return this.Write<uint>(ids, true);
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x00005618 File Offset: 0x00003818
			public uint WriteObject(object obj, bool serializeTypeData)
			{
				if (obj == null)
				{
					return uint.MaxValue;
				}
				Type objType = obj.GetType();
				BinaryStorageBuffer.ISerializationAdapter adapter;
				if (!BinaryStorageBuffer.GetSerializationAdapter(this.serializationAdapters, objType, out adapter))
				{
					return uint.MaxValue;
				}
				uint id = adapter.Serialize(this, obj);
				if (serializeTypeData)
				{
					id = this.Write<BinaryStorageBuffer.ObjectTypeData>(new BinaryStorageBuffer.ObjectTypeData
					{
						typeId = this.WriteObject(objType, false),
						objectId = id
					});
				}
				return id;
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x00005677 File Offset: 0x00003877
			public uint WriteString(string str, char sep = '\0')
			{
				if (str == null)
				{
					return uint.MaxValue;
				}
				if (sep != '\0')
				{
					return this.WriteDynamicString(str, sep);
				}
				return this.WriteAutoEncodedString(str);
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x00005694 File Offset: 0x00003894
			private unsafe uint WriteStringInternal(string val, Encoding enc)
			{
				if (val == null)
				{
					return uint.MaxValue;
				}
				byte[] tmp = enc.GetBytes(val);
				byte[] array;
				byte* pBytes;
				if ((array = tmp) == null || array.Length == 0)
				{
					pBytes = null;
				}
				else
				{
					pBytes = &array[0];
				}
				return this.WriteInternal((void*)pBytes, (uint)tmp.Length, true);
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x000056D4 File Offset: 0x000038D4
			public unsafe byte[] SerializeToByteArray()
			{
				byte[] data = new byte[this.totalBytes];
				byte[] array;
				byte* pData;
				if ((array = data) == null || array.Length == 0)
				{
					pData = null;
				}
				else
				{
					pData = &array[0];
				}
				uint offset = 0U;
				foreach (BinaryStorageBuffer.Writer.Chunk c in this.chunks)
				{
					try
					{
						byte[] array2;
						byte* pChunk;
						if ((array2 = c.data) == null || array2.Length == 0)
						{
							pChunk = null;
						}
						else
						{
							pChunk = &array2[0];
						}
						UnsafeUtility.MemCpy((void*)(pData + offset), (void*)pChunk, (long)((ulong)c.position));
					}
					finally
					{
						byte[] array2 = null;
					}
					offset += c.position;
				}
				array = null;
				return data;
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x000057A0 File Offset: 0x000039A0
			public uint SerializeToStream(Stream str)
			{
				foreach (BinaryStorageBuffer.Writer.Chunk c in this.chunks)
				{
					str.Write(c.data, 0, (int)c.position);
				}
				return this.totalBytes;
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x00005808 File Offset: 0x00003A08
			private static bool IsUnicode(string str)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i] > 'ÿ')
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x00005837 File Offset: 0x00003A37
			private uint WriteAutoEncodedString(string str)
			{
				if (str == null)
				{
					return uint.MaxValue;
				}
				if (BinaryStorageBuffer.Writer.IsUnicode(str))
				{
					return this.WriteUnicodeString(str);
				}
				return this.WriteStringInternal(str, Encoding.ASCII);
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x0000585C File Offset: 0x00003A5C
			private uint WriteUnicodeString(string str)
			{
				uint id = this.WriteStringInternal(str, Encoding.Unicode);
				return 2147483648U | id;
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00005880 File Offset: 0x00003A80
			private static uint ComputeStringSize(string str, out bool isUnicode)
			{
				if (isUnicode = BinaryStorageBuffer.Writer.IsUnicode(str))
				{
					return (uint)Encoding.Unicode.GetByteCount(str);
				}
				return (uint)Encoding.ASCII.GetByteCount(str);
			}

			// Token: 0x060000FA RID: 250 RVA: 0x000058B4 File Offset: 0x00003AB4
			private unsafe uint WriteDynamicString(string str, char sep)
			{
				if (str == null)
				{
					return uint.MaxValue;
				}
				string[] split = str.Split(sep, StringSplitOptions.None);
				uint minSize = (uint)sizeof(BinaryStorageBuffer.DynamicString);
				BinaryStorageBuffer.Writer.StringParts[] parts = new BinaryStorageBuffer.Writer.StringParts[split.Length];
				for (int i = 0; i < parts.Length; i++)
				{
					bool isUnicode;
					uint partSize = BinaryStorageBuffer.Writer.ComputeStringSize(split[i], out isUnicode);
					parts[i] = new BinaryStorageBuffer.Writer.StringParts
					{
						str = split[i],
						dataSize = partSize,
						isUnicode = isUnicode
					};
				}
				if (parts.Length < 2 || (parts.Length == 2 && parts[0].dataSize + parts[1].dataSize < minSize))
				{
					return this.WriteAutoEncodedString(str);
				}
				return 1073741824U | this.RecurseDynamicStringParts(parts, parts.Length - 1, sep, minSize);
			}

			// Token: 0x060000FB RID: 251 RVA: 0x0000596C File Offset: 0x00003B6C
			private uint RecurseDynamicStringParts(BinaryStorageBuffer.Writer.StringParts[] parts, int index, char sep, uint minSize)
			{
				while (index > 0)
				{
					uint currPartSize = parts[index].dataSize;
					if (currPartSize >= minSize)
					{
						break;
					}
					parts[index - 1].str = string.Format("{0}{1}{2}", parts[index - 1].str, sep, parts[index].str);
					int num = index - 1;
					parts[num].dataSize = parts[num].dataSize + (currPartSize + 1U);
					int num2 = index - 1;
					parts[num2].isUnicode = parts[num2].isUnicode | parts[index].isUnicode;
					index--;
				}
				uint strId = (parts[index].isUnicode ? this.WriteUnicodeString(parts[index].str) : this.WriteStringInternal(parts[index].str, Encoding.ASCII));
				uint nxtId = ((index > 0) ? this.RecurseDynamicStringParts(parts, index - 1, sep, minSize) : uint.MaxValue);
				return this.Write<BinaryStorageBuffer.DynamicString>(new BinaryStorageBuffer.DynamicString
				{
					stringId = strId,
					nextId = nxtId
				});
			}

			// Token: 0x04000064 RID: 100
			private uint totalBytes;

			// Token: 0x04000065 RID: 101
			private uint defaulChunkSize;

			// Token: 0x04000066 RID: 102
			private List<BinaryStorageBuffer.Writer.Chunk> chunks;

			// Token: 0x04000067 RID: 103
			private Dictionary<Hash128, uint> existingValues;

			// Token: 0x04000068 RID: 104
			private Dictionary<Type, BinaryStorageBuffer.ISerializationAdapter> serializationAdapters;

			// Token: 0x02000025 RID: 37
			private class Chunk
			{
				// Token: 0x04000069 RID: 105
				public uint position;

				// Token: 0x0400006A RID: 106
				public byte[] data;
			}

			// Token: 0x02000026 RID: 38
			private struct StringParts
			{
				// Token: 0x0400006B RID: 107
				public string str;

				// Token: 0x0400006C RID: 108
				public uint dataSize;

				// Token: 0x0400006D RID: 109
				public bool isUnicode;
			}
		}
	}
}
