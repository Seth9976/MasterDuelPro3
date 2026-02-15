using System;
using System.Threading;

namespace YgomSystem
{
	// Token: 0x020004E2 RID: 1250
	public class ThreadBase
	{
		// Token: 0x060027D2 RID: 10194 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool Run(object parameter = null)
		{
			return false;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Terminate()
		{
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsRun()
		{
			return false;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Join()
		{
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Sleep(int msec)
		{
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool InitThread(object parameter)
		{
			return false;
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool ExecThread(object parameter)
		{
			return false;
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ExitThread(object parameter)
		{
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0000216D File Offset: 0x0000036D
		private void ThreadProc(object parameter)
		{
		}

		// Token: 0x040028AC RID: 10412
		private const int WaitOneFrame = 16;

		// Token: 0x040028AD RID: 10413
		private Thread ThreadInstance;

		// Token: 0x040028AE RID: 10414
		private bool IsExec;
	}
}
