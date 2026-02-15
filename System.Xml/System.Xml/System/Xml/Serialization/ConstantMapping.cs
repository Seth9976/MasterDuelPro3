using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000169 RID: 361
	internal class ConstantMapping : Mapping
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x000541DD File Offset: 0x000523DD
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x000541F3 File Offset: 0x000523F3
		internal string XmlName
		{
			get
			{
				if (this.xmlName != null)
				{
					return this.xmlName;
				}
				return string.Empty;
			}
			set
			{
				this.xmlName = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x000541FC File Offset: 0x000523FC
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x00054212 File Offset: 0x00052412
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0005421B File Offset: 0x0005241B
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00054223 File Offset: 0x00052423
		internal long Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400084F RID: 2127
		private string xmlName;

		// Token: 0x04000850 RID: 2128
		private string name;

		// Token: 0x04000851 RID: 2129
		private long value;
	}
}
