using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ookii.Dialogs
{
	// Token: 0x02000038 RID: 56
	public class OkButtonClickedEventArgs : CancelEventArgs
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004E39 File Offset: 0x00003039
		public OkButtonClickedEventArgs(string input, IWin32Window inputBoxWindow)
		{
			this._input = input;
			this._inputBoxWindow = inputBoxWindow;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004E54 File Offset: 0x00003054
		public string Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004E6C File Offset: 0x0000306C
		public IWin32Window InputBoxWindow
		{
			get
			{
				return this._inputBoxWindow;
			}
		}

		// Token: 0x04000197 RID: 407
		private string _input;

		// Token: 0x04000198 RID: 408
		private IWin32Window _inputBoxWindow;
	}
}
