using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public struct ObjectInitializationData
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006849 File Offset: 0x00004A49
		public string Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006851 File Offset: 0x00004A51
		public SerializedType ObjectType
		{
			get
			{
				return this.m_ObjectType;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006859 File Offset: 0x00004A59
		public string Data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006861 File Offset: 0x00004A61
		public override string ToString()
		{
			return string.Format("ObjectInitializationData: id={0}, type={1}", this.m_Id, this.m_ObjectType);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006880 File Offset: 0x00004A80
		public TObject CreateInstance<TObject>(string idOverride = null)
		{
			TObject tobject;
			try
			{
				Type objType = this.m_ObjectType.Value;
				if (objType == null)
				{
					tobject = default(TObject);
					tobject = tobject;
				}
				else
				{
					object obj = Activator.CreateInstance(objType, true);
					IInitializableObject serObj = obj as IInitializableObject;
					if (serObj != null && !serObj.Initialize((idOverride == null) ? this.m_Id : idOverride, this.m_Data))
					{
						tobject = default(TObject);
					}
					else
					{
						tobject = (TObject)((object)obj);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				tobject = default(TObject);
			}
			return tobject;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006914 File Offset: 0x00004B14
		public AsyncOperationHandle GetAsyncInitHandle(ResourceManager rm, string idOverride = null)
		{
			AsyncOperationHandle asyncOperationHandle;
			try
			{
				Type objType = this.m_ObjectType.Value;
				if (objType == null)
				{
					asyncOperationHandle = default(AsyncOperationHandle);
					asyncOperationHandle = asyncOperationHandle;
				}
				else
				{
					IInitializableObject serObj = Activator.CreateInstance(objType, true) as IInitializableObject;
					if (serObj != null)
					{
						asyncOperationHandle = serObj.InitializeAsync(rm, (idOverride == null) ? this.m_Id : idOverride, this.m_Data);
					}
					else
					{
						asyncOperationHandle = default(AsyncOperationHandle);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				asyncOperationHandle = default(AsyncOperationHandle);
			}
			return asyncOperationHandle;
		}

		// Token: 0x04000092 RID: 146
		[FormerlySerializedAs("m_id")]
		[SerializeField]
		private string m_Id;

		// Token: 0x04000093 RID: 147
		[FormerlySerializedAs("m_objectType")]
		[SerializeField]
		private SerializedType m_ObjectType;

		// Token: 0x04000094 RID: 148
		[FormerlySerializedAs("m_data")]
		[SerializeField]
		private string m_Data;

		// Token: 0x0200003D RID: 61
		internal class Serializer : BinaryStorageBuffer.ISerializationAdapter<ObjectInitializationData>, BinaryStorageBuffer.ISerializationAdapter
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000159 RID: 345 RVA: 0x0000466C File Offset: 0x0000286C
			public IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600015A RID: 346 RVA: 0x000069A4 File Offset: 0x00004BA4
			public object Deserialize(BinaryStorageBuffer.Reader reader, Type t, uint offset)
			{
				ObjectInitializationData.Serializer.Data d = reader.ReadValue<ObjectInitializationData.Serializer.Data>(offset);
				return new ObjectInitializationData
				{
					m_Id = reader.ReadString(d.id, '\0', true),
					m_ObjectType = new SerializedType
					{
						Value = reader.ReadObject<Type>(d.type, true)
					},
					m_Data = reader.ReadString(d.data, '\0', true)
				};
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00006A18 File Offset: 0x00004C18
			public uint Serialize(BinaryStorageBuffer.Writer writer, object val)
			{
				ObjectInitializationData oid = (ObjectInitializationData)val;
				ObjectInitializationData.Serializer.Data d = new ObjectInitializationData.Serializer.Data
				{
					id = writer.WriteString(oid.m_Id, '\0'),
					type = writer.WriteObject(oid.ObjectType.Value, false),
					data = writer.WriteString(oid.m_Data, '\0')
				};
				return writer.Write<ObjectInitializationData.Serializer.Data>(d);
			}

			// Token: 0x0200003E RID: 62
			private struct Data
			{
				// Token: 0x04000095 RID: 149
				public uint id;

				// Token: 0x04000096 RID: 150
				public uint type;

				// Token: 0x04000097 RID: 151
				public uint data;
			}
		}
	}
}
