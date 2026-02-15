using System;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006DB RID: 1755
	public class BuiltInChecker : BaseChecker
	{
		// Token: 0x060036C1 RID: 14017 RVA: 0x0000216A File Offset: 0x0000036A
		public override ResTypeData GetResType(string path)
		{
			return null;
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x0000216A File Offset: 0x0000036A
		public override ResTypeData GetResTypeSimpleCheck(string path)
		{
			return null;
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x0000216A File Offset: 0x0000036A
		protected ResTypeData GetData(string path)
		{
			return null;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetResourcePath(string path)
		{
			return null;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ExistsConvertFile(string path)
		{
			return false;
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool ExistsFile(string path)
		{
			return false;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize()
		{
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Destroy()
		{
		}

		// Token: 0x0400313F RID: 12607
		private ResourceExistsConvertData existsConvertData;
	}
}
