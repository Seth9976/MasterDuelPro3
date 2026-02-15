using System;

namespace YgomSystem
{
	// Token: 0x020004B1 RID: 1201
	public class PooledThreadBase
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool Run(object parameter = null)
		{
			return false;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Terminate()
		{
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsRun()
		{
			return false;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Join()
		{
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Sleep(int msec)
		{
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool InitThread(object parameter)
		{
			return false;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool ExecThread(object parameter)
		{
			return false;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ExitThread(object parameter)
		{
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0000216D File Offset: 0x0000036D
		private void ThreadProc(object parameter = null)
		{
		}

		// Token: 0x040027A9 RID: 10153
		private const int WaitOneFrame = 16;

		// Token: 0x040027AA RID: 10154
		private bool IsExec;
	}
}
