using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BF RID: 191
	internal class Documentation : DocumentationInfo
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00012482 File Offset: 0x00010682
		public static string GetPageLink(string pageName)
		{
			return DocumentationInfo.GetPageLink("com.unity.render-pipelines.universal", pageName);
		}

		// Token: 0x040003EA RID: 1002
		public const string packageName = "com.unity.render-pipelines.universal";
	}
}
