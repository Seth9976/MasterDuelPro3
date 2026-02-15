using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200007F RID: 127
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class CollectionAccessAttribute : Attribute
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000048A4 File Offset: 0x00002AA4
		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.<CollectionAccessType>k__BackingField = collectionAccessType;
		}
	}
}
