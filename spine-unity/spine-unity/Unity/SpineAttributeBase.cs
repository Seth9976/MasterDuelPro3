using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public abstract class SpineAttributeBase : PropertyAttribute
	{
		// Token: 0x04000215 RID: 533
		public string dataField = "";

		// Token: 0x04000216 RID: 534
		public string startsWith = "";

		// Token: 0x04000217 RID: 535
		public bool includeNone = true;

		// Token: 0x04000218 RID: 536
		public bool fallbackToTextField;

		// Token: 0x04000219 RID: 537
		public bool avoidGenericMenu;
	}
}
