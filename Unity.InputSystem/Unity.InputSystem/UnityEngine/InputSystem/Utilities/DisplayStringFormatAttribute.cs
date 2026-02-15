using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200023A RID: 570
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class DisplayStringFormatAttribute : Attribute
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0005EE1C File Offset: 0x0005D01C
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x0005EE24 File Offset: 0x0005D024
		public string formatString { get; set; }

		// Token: 0x060014D0 RID: 5328 RVA: 0x0005EE2D File Offset: 0x0005D02D
		public DisplayStringFormatAttribute(string formatString)
		{
			this.formatString = formatString;
		}
	}
}
