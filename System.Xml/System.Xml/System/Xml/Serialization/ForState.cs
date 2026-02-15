using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x02000148 RID: 328
	internal class ForState
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00050A06 File Offset: 0x0004EC06
		internal ForState(LocalBuilder indexVar, Label beginLabel, Label testLabel, object end)
		{
			this.indexVar = indexVar;
			this.beginLabel = beginLabel;
			this.testLabel = testLabel;
			this.end = end;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00050A2B File Offset: 0x0004EC2B
		internal LocalBuilder Index
		{
			get
			{
				return this.indexVar;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00050A33 File Offset: 0x0004EC33
		internal Label BeginLabel
		{
			get
			{
				return this.beginLabel;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00050A3B File Offset: 0x0004EC3B
		internal Label TestLabel
		{
			get
			{
				return this.testLabel;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00050A43 File Offset: 0x0004EC43
		internal object End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x040007EC RID: 2028
		private LocalBuilder indexVar;

		// Token: 0x040007ED RID: 2029
		private Label beginLabel;

		// Token: 0x040007EE RID: 2030
		private Label testLabel;

		// Token: 0x040007EF RID: 2031
		private object end;
	}
}
