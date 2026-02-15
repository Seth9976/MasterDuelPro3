using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002C7 RID: 711
	internal class TaskExceptionHolder
	{
		// Token: 0x0600199D RID: 6557 RVA: 0x0006170B File Offset: 0x0005F90B
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00033991 File Offset: 0x00031B91
		private static bool ShouldFailFastOnUnobservedException()
		{
			return false;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0006171C File Offset: 0x0005F91C
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled && !Environment.HasShutdownStarted)
				{
					AggregateException ex = new AggregateException("A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread.", this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs = new UnobservedTaskExceptionEventArgs(ex);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, unobservedTaskExceptionEventArgs);
					if (TaskExceptionHolder.s_failFastOnUnobservedException && !unobservedTaskExceptionEventArgs.m_observed)
					{
						throw ex;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00061794 File Offset: 0x0005F994
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000617A1 File Offset: 0x0005F9A1
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000617B8 File Offset: 0x0005F9B8
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = exceptionDispatchInfo;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000617F4 File Offset: 0x0005F9F4
		private void AddFaultException(object exceptionObject)
		{
			LowLevelListWithIList<ExceptionDispatchInfo> lowLevelListWithIList = this.m_faultExceptions;
			if (lowLevelListWithIList == null)
			{
				lowLevelListWithIList = (this.m_faultExceptions = new LowLevelListWithIList<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				lowLevelListWithIList.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					lowLevelListWithIList.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception ex2 = enumerator.Current;
								lowLevelListWithIList.Add(ExceptionDispatchInfo.Capture(ex2));
							}
							goto IL_00AE;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException("(Internal)Expected an Exception or an IEnumerable<Exception>", "exceptionObject");
					}
					lowLevelListWithIList.AddRange(enumerable2);
				}
			}
			IL_00AE:
			if (lowLevelListWithIList.Count > 0)
			{
				this.MarkAsUnhandled();
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000618D0 File Offset: 0x0005FAD0
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000618EB File Offset: 0x0005FAEB
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006190C File Offset: 0x0005FB0C
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			LowLevelListWithIList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			array[array.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0006196E File Offset: 0x0005FB6E
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			IList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00061984 File Offset: 0x0005FB84
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x04000C25 RID: 3109
		private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();

		// Token: 0x04000C26 RID: 3110
		private readonly Task m_task;

		// Token: 0x04000C27 RID: 3111
		private volatile LowLevelListWithIList<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04000C28 RID: 3112
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04000C29 RID: 3113
		private volatile bool m_isHandled;
	}
}
