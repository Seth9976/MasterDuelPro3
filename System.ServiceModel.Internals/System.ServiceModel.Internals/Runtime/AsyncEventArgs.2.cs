using System;

namespace System.Runtime
{
	// Token: 0x02000009 RID: 9
	internal class AsyncEventArgs<TArgument> : AsyncEventArgs
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000236F File Offset: 0x0000056F
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002377 File Offset: 0x00000577
		public TArgument Arguments { get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002380 File Offset: 0x00000580
		public virtual void Set(AsyncEventArgsCallback callback, TArgument arguments, object state)
		{
			base.SetAsyncState(callback, state);
			this.Arguments = arguments;
		}
	}
}
