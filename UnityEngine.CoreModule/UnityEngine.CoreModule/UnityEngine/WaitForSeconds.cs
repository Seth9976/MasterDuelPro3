using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C9 RID: 457
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x00026155 File Offset: 0x00024355
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x040006A9 RID: 1705
		internal float m_Seconds;
	}
}
