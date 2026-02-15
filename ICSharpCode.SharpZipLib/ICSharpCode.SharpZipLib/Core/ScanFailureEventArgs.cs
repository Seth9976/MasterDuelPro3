using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000AE RID: 174
	public class ScanFailureEventArgs : EventArgs
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0001A3AE File Offset: 0x000185AE
		public ScanFailureEventArgs(string name, Exception e)
		{
			this.name_ = name;
			this.exception_ = e;
			this.continueRunning_ = true;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001A3CB File Offset: 0x000185CB
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001A3D3 File Offset: 0x000185D3
		public Exception Exception
		{
			get
			{
				return this.exception_;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001A3DB File Offset: 0x000185DB
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001A3E3 File Offset: 0x000185E3
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

		// Token: 0x04000436 RID: 1078
		private string name_;

		// Token: 0x04000437 RID: 1079
		private Exception exception_;

		// Token: 0x04000438 RID: 1080
		private bool continueRunning_;
	}
}
