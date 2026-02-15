using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000499 RID: 1177
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class UxmlElementAttribute : Attribute
	{
		// Token: 0x060021FA RID: 8698 RVA: 0x0007C7B9 File Offset: 0x0007A9B9
		public UxmlElementAttribute(string uxmlName)
		{
			this.name = uxmlName;
		}

		// Token: 0x04000F06 RID: 3846
		public readonly string name;
	}
}
