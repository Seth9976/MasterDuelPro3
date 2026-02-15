using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200017A RID: 378
	internal class ConstantModel
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x00055DCF File Offset: 0x00053FCF
		internal ConstantModel(FieldInfo fieldInfo, long value)
		{
			this.fieldInfo = fieldInfo;
			this.value = value;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00055DE5 File Offset: 0x00053FE5
		internal string Name
		{
			get
			{
				return this.fieldInfo.Name;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00055DF2 File Offset: 0x00053FF2
		internal long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00055DFA File Offset: 0x00053FFA
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x04000895 RID: 2197
		private FieldInfo fieldInfo;

		// Token: 0x04000896 RID: 2198
		private long value;
	}
}
