using System;
using System.Globalization;

namespace System.Xml
{
	// Token: 0x02000013 RID: 19
	internal struct BinXmlSqlMoney
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000365A File Offset: 0x0000185A
		public BinXmlSqlMoney(int v)
		{
			this.data = (long)v;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003664 File Offset: 0x00001864
		public BinXmlSqlMoney(long v)
		{
			this.data = v;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003670 File Offset: 0x00001870
		public decimal ToDecimal()
		{
			bool flag;
			ulong num;
			if (this.data < 0L)
			{
				flag = true;
				num = (ulong)(-(ulong)this.data);
			}
			else
			{
				flag = false;
				num = (ulong)this.data;
			}
			return new decimal((int)num, (int)(num >> 32), 0, flag, 4);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000036AC File Offset: 0x000018AC
		public override string ToString()
		{
			return this.ToDecimal().ToString("#0.00##", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000088 RID: 136
		private long data;
	}
}
