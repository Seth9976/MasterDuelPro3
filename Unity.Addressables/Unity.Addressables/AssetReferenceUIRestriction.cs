using System;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class AssetReferenceUIRestriction : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public virtual bool ValidateAsset(Object obj)
		{
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F4 File Offset: 0x000002F4
		public virtual bool ValidateAsset(string path)
		{
			return true;
		}
	}
}
