using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000EA RID: 234
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
	public class CoreRPHelpURLAttribute : HelpURLAttribute
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x000127E8 File Offset: 0x000109E8
		public CoreRPHelpURLAttribute(string pageName, string packageName = "com.unity.render-pipelines.core")
			: base(DocumentationInfo.GetPageLink(packageName, pageName, ""))
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000127FC File Offset: 0x000109FC
		public CoreRPHelpURLAttribute(string pageName, string pageHash, string packageName = "com.unity.render-pipelines.core")
			: base(DocumentationInfo.GetPageLink(packageName, pageName, pageHash))
		{
		}
	}
}
