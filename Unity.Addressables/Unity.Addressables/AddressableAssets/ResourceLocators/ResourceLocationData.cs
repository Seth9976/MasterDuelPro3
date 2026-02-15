using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets.Utility;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.Serialization;

namespace UnityEngine.AddressableAssets.ResourceLocators
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class ResourceLocationData
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000091D4 File Offset: 0x000073D4
		public string[] Keys
		{
			get
			{
				return this.m_Keys;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000091DC File Offset: 0x000073DC
		public string InternalId
		{
			get
			{
				return this.m_InternalId;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000091E4 File Offset: 0x000073E4
		public string Provider
		{
			get
			{
				return this.m_Provider;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000091EC File Offset: 0x000073EC
		public string[] Dependencies
		{
			get
			{
				return this.m_Dependencies;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000091F4 File Offset: 0x000073F4
		public Type ResourceType
		{
			get
			{
				return this.m_ResourceType.Value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009201 File Offset: 0x00007401
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00009238 File Offset: 0x00007438
		public object Data
		{
			get
			{
				if (this._Data == null)
				{
					if (this.SerializedData == null || this.SerializedData.Length == 0)
					{
						return null;
					}
					this._Data = SerializationUtilities.ReadObjectFromByteArray(this.SerializedData, 0);
				}
				return this._Data;
			}
			set
			{
				List<byte> tmp = new List<byte>();
				SerializationUtilities.WriteObjectToByteList(value, tmp);
				this.SerializedData = tmp.ToArray();
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009260 File Offset: 0x00007460
		public ResourceLocationData(string[] keys, string id, Type provider, Type t, string[] dependencies = null)
		{
			this.m_Keys = keys;
			this.m_InternalId = id;
			this.m_Provider = ((provider == null) ? "" : provider.FullName);
			this.m_Dependencies = ((dependencies == null) ? new string[0] : dependencies);
			this.m_ResourceType = new SerializedType
			{
				Value = t
			};
		}

		// Token: 0x0400013E RID: 318
		[FormerlySerializedAs("m_keys")]
		[SerializeField]
		private string[] m_Keys;

		// Token: 0x0400013F RID: 319
		[FormerlySerializedAs("m_internalId")]
		[SerializeField]
		private string m_InternalId;

		// Token: 0x04000140 RID: 320
		[FormerlySerializedAs("m_provider")]
		[SerializeField]
		private string m_Provider;

		// Token: 0x04000141 RID: 321
		[FormerlySerializedAs("m_dependencies")]
		[SerializeField]
		private string[] m_Dependencies;

		// Token: 0x04000142 RID: 322
		[SerializeField]
		private SerializedType m_ResourceType;

		// Token: 0x04000143 RID: 323
		[SerializeField]
		private byte[] SerializedData;

		// Token: 0x04000144 RID: 324
		private object _Data;
	}
}
