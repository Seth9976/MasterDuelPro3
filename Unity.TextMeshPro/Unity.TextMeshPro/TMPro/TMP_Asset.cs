using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public abstract class TMP_Asset : ScriptableObject
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000291E File Offset: 0x00000B1E
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002926 File Offset: 0x00000B26
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000292F File Offset: 0x00000B2F
		public int instanceID
		{
			get
			{
				if (this.m_InstanceID == 0)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000294B File Offset: 0x00000B4B
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000296C File Offset: 0x00000B6C
		public int hashCode
		{
			get
			{
				if (this.m_HashCode == 0)
				{
					this.m_HashCode = TMP_TextUtilities.GetHashCode(base.name);
				}
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002975 File Offset: 0x00000B75
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000297D File Offset: 0x00000B7D
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002986 File Offset: 0x00000B86
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000298E File Offset: 0x00000B8E
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002997 File Offset: 0x00000B97
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000029CD File Offset: 0x00000BCD
		public int materialHashCode
		{
			get
			{
				if (this.m_MaterialHashCode == 0)
				{
					if (this.m_Material == null)
					{
						return 0;
					}
					this.m_MaterialHashCode = TMP_TextUtilities.GetSimpleHashCode(this.m_Material.name);
				}
				return this.m_MaterialHashCode;
			}
			set
			{
				this.m_MaterialHashCode = value;
			}
		}

		// Token: 0x04000033 RID: 51
		[SerializeField]
		internal string m_Version;

		// Token: 0x04000034 RID: 52
		internal int m_InstanceID;

		// Token: 0x04000035 RID: 53
		internal int m_HashCode;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		[FormerlySerializedAs("material")]
		internal Material m_Material;

		// Token: 0x04000038 RID: 56
		internal int m_MaterialHashCode;
	}
}
