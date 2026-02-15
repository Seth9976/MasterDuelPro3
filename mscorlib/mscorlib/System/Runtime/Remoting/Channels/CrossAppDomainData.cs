using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000453 RID: 1107
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x06002470 RID: 9328 RVA: 0x00095C44 File Offset: 0x00093E44
		internal CrossAppDomainData(int domainId)
		{
			this._ContextID = 0;
			this._DomainID = domainId;
			this._processGuid = RemotingConfiguration.ProcessId;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x00095C6A File Offset: 0x00093E6A
		internal int DomainID
		{
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x00095C72 File Offset: 0x00093E72
		internal string ProcessID
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x0400118E RID: 4494
		private object _ContextID;

		// Token: 0x0400118F RID: 4495
		private int _DomainID;

		// Token: 0x04001190 RID: 4496
		private string _processGuid;
	}
}
