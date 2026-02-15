using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200035F RID: 863
	[AttributeUsage(AttributeTargets.Field, Inherited = true)]
	public abstract class ResourcePathsBaseAttribute : Attribute
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x00002059 File Offset: 0x00000259
		protected ResourcePathsBaseAttribute(string[] paths, bool isField, SearchType location)
		{
		}
	}
}
