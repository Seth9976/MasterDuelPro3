using System;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000034 RID: 52
	[ExcludeFromObjectFactory]
	[Serializable]
	public abstract class TextAsset : ScriptableObject
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000AC70 File Offset: 0x00008E70
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000AC88 File Offset: 0x00008E88
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000AC94 File Offset: 0x00008E94
		public int instanceID
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				if (flag)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000ACFE File Offset: 0x00008EFE
		public int hashCode
		{
			get
			{
				bool flag = this.m_HashCode == 0;
				if (flag)
				{
					this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
				}
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000AD07 File Offset: 0x00008F07
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000AD0F File Offset: 0x00008F0F
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000AD18 File Offset: 0x00008F18
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000AD69 File Offset: 0x00008F69
		public int materialHashCode
		{
			get
			{
				bool flag = this.m_MaterialHashCode == 0;
				if (flag)
				{
					bool flag2 = this.m_Material == null;
					if (flag2)
					{
						return 0;
					}
					this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
				}
				return this.m_MaterialHashCode;
			}
			set
			{
				this.m_MaterialHashCode = value;
			}
		}

		// Token: 0x04000152 RID: 338
		[SerializeField]
		internal string m_Version;

		// Token: 0x04000153 RID: 339
		internal int m_InstanceID;

		// Token: 0x04000154 RID: 340
		internal int m_HashCode;

		// Token: 0x04000155 RID: 341
		[SerializeField]
		[FormerlySerializedAs("material")]
		internal Material m_Material;

		// Token: 0x04000156 RID: 342
		internal int m_MaterialHashCode;
	}
}
