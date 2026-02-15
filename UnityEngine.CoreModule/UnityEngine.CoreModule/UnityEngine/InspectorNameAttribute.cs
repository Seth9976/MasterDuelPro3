using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200015D RID: 349
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class InspectorNameAttribute : PropertyAttribute
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x00020160 File Offset: 0x0001E360
		public InspectorNameAttribute(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x040005F6 RID: 1526
		public readonly string displayName;
	}
}
