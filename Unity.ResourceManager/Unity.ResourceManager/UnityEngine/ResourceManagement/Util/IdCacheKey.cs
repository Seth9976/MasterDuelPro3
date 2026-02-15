using System;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200002D RID: 45
	internal sealed class IdCacheKey : IOperationCacheKey, IEquatable<IOperationCacheKey>
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000061C8 File Offset: 0x000043C8
		public IdCacheKey(string id)
		{
			this.ID = id;
			this.locationType = typeof(object);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000061E7 File Offset: 0x000043E7
		public IdCacheKey(Type locType, string id)
		{
			this.ID = id;
			this.locationType = locType;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000061FD File Offset: 0x000043FD
		private bool Equals(IdCacheKey other)
		{
			return this == other || (other != null && other.ID == this.ID && this.locationType == other.locationType);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006230 File Offset: 0x00004430
		public override int GetHashCode()
		{
			return (527 + this.ID.GetHashCode()) * 31 + this.locationType.GetHashCode();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006252 File Offset: 0x00004452
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IdCacheKey);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006252 File Offset: 0x00004452
		public bool Equals(IOperationCacheKey other)
		{
			return this.Equals(other as IdCacheKey);
		}

		// Token: 0x0400007E RID: 126
		public string ID;

		// Token: 0x0400007F RID: 127
		public Type locationType;
	}
}
