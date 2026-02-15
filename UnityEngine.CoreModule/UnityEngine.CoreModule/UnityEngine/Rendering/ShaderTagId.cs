using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C7 RID: 967
	public struct ShaderTagId : IEquatable<ShaderTagId>
	{
		// Token: 0x06001A76 RID: 6774 RVA: 0x00039B42 File Offset: 0x00037D42
		public ShaderTagId(string name)
		{
			this.m_Id = Shader.TagToID(name);
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00039B54 File Offset: 0x00037D54
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x00039B6C File Offset: 0x00037D6C
		internal int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00039B78 File Offset: 0x00037D78
		public override bool Equals(object obj)
		{
			return obj is ShaderTagId && this.Equals((ShaderTagId)obj);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00039BA4 File Offset: 0x00037DA4
		public bool Equals(ShaderTagId other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public override int GetHashCode()
		{
			int hashCode = 2079669542;
			return hashCode * -1521134295 + this.m_Id.GetHashCode();
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00039BF4 File Offset: 0x00037DF4
		public static bool operator ==(ShaderTagId tag1, ShaderTagId tag2)
		{
			return tag1.Equals(tag2);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00039C10 File Offset: 0x00037E10
		public static bool operator !=(ShaderTagId tag1, ShaderTagId tag2)
		{
			return !(tag1 == tag2);
		}

		// Token: 0x04000C72 RID: 3186
		public static readonly ShaderTagId none;

		// Token: 0x04000C73 RID: 3187
		private int m_Id;
	}
}
