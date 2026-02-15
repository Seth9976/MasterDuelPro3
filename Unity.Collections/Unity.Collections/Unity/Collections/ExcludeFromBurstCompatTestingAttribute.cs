using System;

namespace Unity.Collections
{
	// Token: 0x02000081 RID: 129
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public class ExcludeFromBurstCompatTestingAttribute : Attribute
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00016B9E File Offset: 0x00014D9E
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00016BA6 File Offset: 0x00014DA6
		public string Reason { get; set; }

		// Token: 0x060006CD RID: 1741 RVA: 0x00016BAF File Offset: 0x00014DAF
		public ExcludeFromBurstCompatTestingAttribute(string _reason)
		{
			this.Reason = _reason;
		}
	}
}
