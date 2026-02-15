using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000069 RID: 105
	public static class Progress
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00005386 File Offset: 0x00003586
		public static IProgress<T> Create<T>(Action<T> handler)
		{
			if (handler == null)
			{
				return Progress.NullProgress<T>.Instance;
			}
			return new Progress.AnonymousProgress<T>(handler);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005397 File Offset: 0x00003597
		public static IProgress<T> CreateOnlyValueChanged<T>(Action<T> handler, IEqualityComparer<T> comparer = null)
		{
			if (handler == null)
			{
				return Progress.NullProgress<T>.Instance;
			}
			return new Progress.OnlyValueChangedProgress<T>(handler, comparer ?? UnityEqualityComparer.GetDefault<T>());
		}

		// Token: 0x0200006A RID: 106
		private sealed class NullProgress<T> : IProgress<T>
		{
			// Token: 0x06000168 RID: 360 RVA: 0x000020BB File Offset: 0x000002BB
			private NullProgress()
			{
			}

			// Token: 0x06000169 RID: 361 RVA: 0x000030EE File Offset: 0x000012EE
			public void Report(T value)
			{
			}

			// Token: 0x040000E0 RID: 224
			public static readonly IProgress<T> Instance = new Progress.NullProgress<T>();
		}

		// Token: 0x0200006B RID: 107
		private sealed class AnonymousProgress<T> : IProgress<T>
		{
			// Token: 0x0600016B RID: 363 RVA: 0x000053BE File Offset: 0x000035BE
			public AnonymousProgress(Action<T> action)
			{
				this.action = action;
			}

			// Token: 0x0600016C RID: 364 RVA: 0x000053CD File Offset: 0x000035CD
			public void Report(T value)
			{
				this.action(value);
			}

			// Token: 0x040000E1 RID: 225
			private readonly Action<T> action;
		}

		// Token: 0x0200006C RID: 108
		private sealed class OnlyValueChangedProgress<T> : IProgress<T>
		{
			// Token: 0x0600016D RID: 365 RVA: 0x000053DB File Offset: 0x000035DB
			public OnlyValueChangedProgress(Action<T> action, IEqualityComparer<T> comparer)
			{
				this.action = action;
				this.comparer = comparer;
				this.isFirstCall = true;
			}

			// Token: 0x0600016E RID: 366 RVA: 0x000053F8 File Offset: 0x000035F8
			public void Report(T value)
			{
				if (this.isFirstCall)
				{
					this.isFirstCall = false;
				}
				else if (this.comparer.Equals(value, this.latestValue))
				{
					return;
				}
				this.latestValue = value;
				this.action(value);
			}

			// Token: 0x040000E2 RID: 226
			private readonly Action<T> action;

			// Token: 0x040000E3 RID: 227
			private readonly IEqualityComparer<T> comparer;

			// Token: 0x040000E4 RID: 228
			private bool isFirstCall;

			// Token: 0x040000E5 RID: 229
			private T latestValue;
		}
	}
}
