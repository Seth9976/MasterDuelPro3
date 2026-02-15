using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000AB RID: 171
	public class ScanEventArgs : EventArgs
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x0001A2E3 File Offset: 0x000184E3
		public ScanEventArgs(string name)
		{
			this.name_ = name;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001A2F9 File Offset: 0x000184F9
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001A301 File Offset: 0x00018501
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001A309 File Offset: 0x00018509
		public bool ContinueRunning
		{
			get
			{
				return this.continueRunning_;
			}
			set
			{
				this.continueRunning_ = value;
			}
		}

		// Token: 0x0400042F RID: 1071
		private string name_;

		// Token: 0x04000430 RID: 1072
		private bool continueRunning_ = true;
	}
}
