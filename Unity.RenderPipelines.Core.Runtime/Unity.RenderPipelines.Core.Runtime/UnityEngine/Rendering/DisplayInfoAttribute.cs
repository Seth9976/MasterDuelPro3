using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000033 RID: 51
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false)]
	public class DisplayInfoAttribute : Attribute
	{
		// Token: 0x040000B4 RID: 180
		public string name;

		// Token: 0x040000B5 RID: 181
		public int order;
	}
}
