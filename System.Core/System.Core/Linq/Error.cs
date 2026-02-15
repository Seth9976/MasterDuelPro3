using System;

namespace System.Linq
{
	// Token: 0x02000018 RID: 24
	internal static class Error
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00004350 File Offset: 0x00002550
		internal static Exception ArgumentNull(string s)
		{
			return new ArgumentNullException(s);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004358 File Offset: 0x00002558
		internal static Exception ArgumentOutOfRange(string s)
		{
			return new ArgumentOutOfRangeException(s);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004360 File Offset: 0x00002560
		internal static Exception MoreThanOneElement()
		{
			return new InvalidOperationException("Sequence contains more than one element");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000436C File Offset: 0x0000256C
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException("Sequence contains more than one matching element");
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004378 File Offset: 0x00002578
		internal static Exception NoElements()
		{
			return new InvalidOperationException("Sequence contains no elements");
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004384 File Offset: 0x00002584
		internal static Exception NoMatch()
		{
			return new InvalidOperationException("Sequence contains no matching element");
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004390 File Offset: 0x00002590
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
