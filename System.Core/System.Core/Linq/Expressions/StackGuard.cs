using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
	// Token: 0x020000D3 RID: 211
	internal sealed class StackGuard
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00016D06 File Offset: 0x00014F06
		public bool TryEnterOnCurrentStack()
		{
			if (RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				return true;
			}
			if (this._executionStackCount < 1024)
			{
				return false;
			}
			throw new InsufficientExecutionStackException();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00016D25 File Offset: 0x00014F25
		public void RunOnEmptyStack<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2, T3>, T1, T2, T3> tuple = (Tuple<Action<T1, T2, T3>, T1, T2, T3>)s;
				tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
				return null;
			}, Tuple.Create<Action<T1, T2, T3>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00016D57 File Offset: 0x00014F57
		public R RunOnEmptyStack<T1, T2, R>(Func<T1, T2, R> action, T1 arg1, T2 arg2)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, R>, T1, T2> tuple = (Tuple<Func<T1, T2, R>, T1, T2>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3);
			}, Tuple.Create<Func<T1, T2, R>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00016D86 File Offset: 0x00014F86
		public R RunOnEmptyStack<T1, T2, T3, R>(Func<T1, T2, T3, R> action, T1 arg1, T2 arg2, T3 arg3)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, T3, R>, T1, T2, T3> tuple = (Tuple<Func<T1, T2, T3, R>, T1, T2, T3>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
			}, Tuple.Create<Func<T1, T2, T3, R>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00016DB8 File Offset: 0x00014FB8
		private R RunOnEmptyStackCore<R>(Func<object, R> action, object state)
		{
			this._executionStackCount++;
			R result;
			try
			{
				Task<R> task = Task.Factory.StartNew<R>(action, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
				TaskAwaiter<R> awaiter = task.GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					((IAsyncResult)task).AsyncWaitHandle.WaitOne();
				}
				result = awaiter.GetResult();
			}
			finally
			{
				this._executionStackCount--;
			}
			return result;
		}

		// Token: 0x04000200 RID: 512
		private int _executionStackCount;
	}
}
