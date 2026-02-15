using System;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006E2 RID: 1762
	public class LocalFileChecker : BaseChecker
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual Resource.Type TypeLocalFile
		{
			get
			{
				return Resource.Type.None;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual Resource.Type TypeBinary
		{
			get
			{
				return Resource.Type.None;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual Resource.Type TypeAssetBundle
		{
			get
			{
				return Resource.Type.None;
			}
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x0000216A File Offset: 0x0000036A
		public override ResTypeData GetResType(string path)
		{
			return null;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x0000216A File Offset: 0x0000036A
		public override ResTypeData GetResTypeSimpleCheck(string path)
		{
			return null;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool isExistFile(string path)
		{
			return false;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool isAssetBundleData(string path)
		{
			return false;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual ResTypeData GetResTypeCheckType(string path, bool checkType)
		{
			return null;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetLocalFilePath(string path)
		{
			return null;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ClearCache()
		{
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual string GetCheckExistFilePath(string path)
		{
			return null;
		}
	}
}
