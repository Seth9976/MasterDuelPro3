using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A4 RID: 420
	public class DropdownMenuSeparator : DropdownMenuItem
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0003B426 File Offset: 0x00039626
		public string subMenuPath { get; }

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003B42E File Offset: 0x0003962E
		public DropdownMenuSeparator(string subMenuPath)
		{
			this.subMenuPath = subMenuPath;
		}
	}
}
