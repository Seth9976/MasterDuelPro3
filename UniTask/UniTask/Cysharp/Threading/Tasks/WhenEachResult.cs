using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200010D RID: 269
	public readonly struct WhenEachResult<T>
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001FCC1 File Offset: 0x0001DEC1
		public T Result { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001FCC9 File Offset: 0x0001DEC9
		public Exception Exception { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001FCD1 File Offset: 0x0001DED1
		public bool IsCompletedSuccessfully
		{
			get
			{
				return this.Exception == null;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001FCDC File Offset: 0x0001DEDC
		public bool IsFaulted
		{
			get
			{
				return this.Exception != null;
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001FCE7 File Offset: 0x0001DEE7
		public WhenEachResult(T result)
		{
			this.Result = result;
			this.Exception = null;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		public WhenEachResult(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Result = default(T);
			this.Exception = exception;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001FD1A File Offset: 0x0001DF1A
		public void TryThrow()
		{
			if (this.IsFaulted)
			{
				ExceptionDispatchInfo.Capture(this.Exception).Throw();
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001FD34 File Offset: 0x0001DF34
		public T GetResult()
		{
			if (this.IsFaulted)
			{
				ExceptionDispatchInfo.Capture(this.Exception).Throw();
			}
			return this.Result;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001FD54 File Offset: 0x0001DF54
		public override string ToString()
		{
			if (this.IsCompletedSuccessfully)
			{
				T result = this.Result;
				return ((result != null) ? result.ToString() : null) ?? "";
			}
			return "Exception{" + this.Exception.Message + "}";
		}
	}
}
