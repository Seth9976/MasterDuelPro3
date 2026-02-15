using System;

// Token: 0x02000003 RID: 3
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Delegate)]
internal class MapAttribute : Attribute
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
	public MapAttribute()
	{
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000205B File Offset: 0x0000025B
	public MapAttribute(string nativeType)
	{
		this.nativeType = nativeType;
	}

	// Token: 0x04000001 RID: 1
	private string nativeType;
}
