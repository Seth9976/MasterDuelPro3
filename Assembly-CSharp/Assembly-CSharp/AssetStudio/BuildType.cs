using System;

namespace AssetStudio
{
	// Token: 0x0200008F RID: 143
	public class BuildType
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000BC1F File Offset: 0x00009E1F
		public BuildType(string type)
		{
			this.buildType = type;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BC2E File Offset: 0x00009E2E
		public bool IsAlpha
		{
			get
			{
				return this.buildType == "a";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000BC40 File Offset: 0x00009E40
		public bool IsPatch
		{
			get
			{
				return this.buildType == "p";
			}
		}

		// Token: 0x040003A6 RID: 934
		private string buildType;
	}
}
