using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000EC RID: 236
	public class DocumentationInfo
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00012824 File Offset: 0x00010A24
		public static string version
		{
			get
			{
				return "13.1";
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001282B File Offset: 0x00010A2B
		public static string GetPackageLink(string packageName, string packageVersion, string pageName)
		{
			return string.Format("https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html#{3}", new object[] { packageName, packageVersion, pageName, "" });
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00012851 File Offset: 0x00010A51
		public static string GetPageLink(string packageName, string pageName)
		{
			return string.Format("https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html#{3}", new object[]
			{
				packageName,
				DocumentationInfo.version,
				pageName,
				""
			});
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001287B File Offset: 0x00010A7B
		public static string GetPageLink(string packageName, string pageName, string pageHash)
		{
			return string.Format("https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html#{3}", new object[]
			{
				packageName,
				DocumentationInfo.version,
				pageName,
				pageHash
			});
		}

		// Token: 0x040002E7 RID: 743
		private const string fallbackVersion = "13.1";

		// Token: 0x040002E8 RID: 744
		private const string url = "https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html#{3}";
	}
}
