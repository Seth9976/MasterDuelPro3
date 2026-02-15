using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BE RID: 190
	[Conditional("UNITY_EDITOR")]
	internal class URPHelpURLAttribute : CoreRPHelpURLAttribute
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00012473 File Offset: 0x00010673
		public URPHelpURLAttribute(string pageName, string pageHash = "")
			: base(pageName, pageHash, "com.unity.render-pipelines.universal")
		{
		}
	}
}
