using System;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006F5 RID: 1781
	public class StreamingFileChecker : LocalFileChecker
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override Resource.Type TypeAssetBundle
		{
			get
			{
				return Resource.Type.None;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override Resource.Type TypeBinary
		{
			get
			{
				return Resource.Type.None;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override Resource.Type TypeLocalFile
		{
			get
			{
				return Resource.Type.None;
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isExistFile(string path)
		{
			return false;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool isAssetBundleData(string path)
		{
			return false;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetCheckExistFilePath(string path)
		{
			return null;
		}
	}
}
