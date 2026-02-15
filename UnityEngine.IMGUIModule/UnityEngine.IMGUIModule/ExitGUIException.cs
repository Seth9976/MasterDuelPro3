using System;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	public sealed class ExitGUIException : Exception
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006866 File Offset: 0x00004A66
		public ExitGUIException()
		{
			GUIUtility.guiIsExiting = true;
		}
	}
}
