using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000548 RID: 1352
	internal class ManagedErrorInfo : IErrorInfo
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x000A896B File Offset: 0x000A6B6B
		public ManagedErrorInfo(Exception e)
		{
			this.m_Exception = e;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000A897A File Offset: 0x000A6B7A
		public Exception Exception
		{
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000A8982 File Offset: 0x000A6B82
		public int GetGUID(out Guid guid)
		{
			guid = Guid.Empty;
			return 0;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000A8990 File Offset: 0x000A6B90
		public int GetSource(out string source)
		{
			source = this.m_Exception.Source;
			return 0;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000A89A0 File Offset: 0x000A6BA0
		public int GetDescription(out string description)
		{
			description = this.m_Exception.Message;
			return 0;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000A89B0 File Offset: 0x000A6BB0
		public int GetHelpFile(out string helpFile)
		{
			helpFile = this.m_Exception.HelpLink;
			return 0;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000A89C0 File Offset: 0x000A6BC0
		public int GetHelpContext(out uint helpContext)
		{
			helpContext = 0U;
			return 0;
		}

		// Token: 0x04001592 RID: 5522
		private Exception m_Exception;
	}
}
