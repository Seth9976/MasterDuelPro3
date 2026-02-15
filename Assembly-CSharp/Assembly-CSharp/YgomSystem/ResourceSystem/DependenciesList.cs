using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006DD RID: 1757
	public class DependenciesList
	{
		// Token: 0x060036CD RID: 14029 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetFileNameForCommon()
		{
			return null;
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetFileNameForLanguage()
		{
			return null;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetFileNameForCardIllust()
		{
			return null;
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetFileNameForStreamingData()
		{
			return null;
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetFileName(string lang)
		{
			return null;
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0000216A File Offset: 0x0000036A
		public static DependenciesList Load()
		{
			return null;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadBaseData()
		{
			return null;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadLanguageData()
		{
			return null;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadCardIllustData()
		{
			return null;
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x0000216A File Offset: 0x0000036A
		public static DependenciesList LoadStreaming()
		{
			return null;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadStreamingBaseData()
		{
			return null;
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadStreamingCommonData()
		{
			return null;
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadStreamingLangData()
		{
			return null;
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList LoadStreamingCardIllustData()
		{
			return null;
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x0000216A File Offset: 0x0000036A
		private static DependenciesList EncryptFromBytes(byte[] bytes)
		{
			return null;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] GetDependencies(string assetName)
		{
			return null;
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Exist(string assetName)
		{
			return false;
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x0000216D File Offset: 0x0000036D
		public void Merge(DependenciesList target)
		{
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x04003140 RID: 12608
		private const string kManifestFileName = "manifest";

		// Token: 0x04003141 RID: 12609
		[SerializeField]
		private List<DependenciesList.DependenciesInfo> informations;

		// Token: 0x04003142 RID: 12610
		private Dictionary<string, DependenciesList.DependenciesInfo> dictionary;

		// Token: 0x020006DE RID: 1758
		[Serializable]
		public class DependenciesInfo
		{
			// Token: 0x04003143 RID: 12611
			public string assetName;

			// Token: 0x04003144 RID: 12612
			public string[] dependencies;
		}
	}
}
