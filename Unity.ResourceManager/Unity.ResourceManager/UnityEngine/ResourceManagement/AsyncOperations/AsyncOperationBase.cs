using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000078 RID: 120
	public abstract class AsyncOperationBase<TObject> : IAsyncOperation
	{
		// Token: 0x060002BA RID: 698
		protected abstract void Execute();

		// Token: 0x060002BB RID: 699 RVA: 0x00006444 File Offset: 0x00004644
		protected virtual void Destroy()
		{
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000AD83 File Offset: 0x00008F83
		protected virtual float Progress
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00004448 File Offset: 0x00002648
		protected virtual string DebugName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00006444 File Offset: 0x00004644
		public virtual void GetDependencies(List<AsyncOperationHandle> dependencies)
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000AD8A File Offset: 0x00008F8A
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000AD92 File Offset: 0x00008F92
		public TObject Result { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000AD9B File Offset: 0x00008F9B
		internal int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000ADA3 File Offset: 0x00008FA3
		internal bool CompletedEventHasListeners
		{
			get
			{
				return this.m_CompletedActionT != null && this.m_CompletedActionT.Count > 0;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000ADBD File Offset: 0x00008FBD
		internal bool DestroyedEventHasListeners
		{
			get
			{
				return this.m_DestroyedAction != null && this.m_DestroyedAction.Count > 0;
			}
		}

		// Token: 0x17000092 RID: 146
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000ADD7 File Offset: 0x00008FD7
		internal Action<IAsyncOperation> OnDestroy
		{
			set
			{
				this.m_OnDestroyAction = value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002C5 RID: 709 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		// (remove) Token: 0x060002C6 RID: 710 RVA: 0x0000AE18 File Offset: 0x00009018
		internal event Action Executed;

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000AE4D File Offset: 0x0000904D
		protected internal int ReferenceCount
		{
			get
			{
				return this.m_referenceCount;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000AE55 File Offset: 0x00009055
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000AE5D File Offset: 0x0000905D
		public bool IsRunning { get; internal set; }

		// Token: 0x060002CA RID: 714 RVA: 0x0000AE66 File Offset: 0x00009066
		protected AsyncOperationBase()
		{
			this.m_UpdateCallback = new Action<float>(this.UpdateCallback);
			this.m_dependencyCompleteAction = delegate(AsyncOperationHandle o)
			{
				this.InvokeExecute();
			};
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AE9C File Offset: 0x0000909C
		internal static string ShortenPath(string p, bool keepExtension)
		{
			int slashIndex = p.LastIndexOf('/');
			if (slashIndex > 0)
			{
				p = p.Substring(slashIndex + 1);
			}
			if (!keepExtension)
			{
				slashIndex = p.LastIndexOf('.');
				if (slashIndex > 0)
				{
					p = p.Substring(0, slashIndex);
				}
			}
			return p;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AEDC File Offset: 0x000090DC
		public void WaitForCompletion()
		{
			if (PlatformUtilities.PlatformUsesMultiThreading(Application.platform))
			{
				while (!this.InvokeWaitForCompletion())
				{
				}
				return;
			}
			throw new Exception(string.Format("{0} does not support synchronous Addressable loading.  Please do not use WaitForCompletion on the {1} platform.", Application.platform, Application.platform));
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000AF16 File Offset: 0x00009116
		protected virtual bool InvokeWaitForCompletion()
		{
			return true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000AF19 File Offset: 0x00009119
		protected internal void IncrementReferenceCount()
		{
			if (this.m_referenceCount == 0)
			{
				throw new Exception(string.Format("Cannot increment reference count on operation {0} because it has already been destroyed", this));
			}
			this.m_referenceCount++;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000AF44 File Offset: 0x00009144
		protected internal void DecrementReferenceCount()
		{
			if (this.m_referenceCount <= 0)
			{
				throw new Exception(string.Format("Cannot decrement reference count for operation {0} because it is already 0", this));
			}
			this.m_referenceCount--;
			if (this.m_referenceCount == 0)
			{
				if (this.m_DestroyedAction != null)
				{
					this.m_DestroyedAction.Invoke(this.Handle);
					this.m_DestroyedAction.Clear();
				}
				this.Destroy();
				this.Result = default(TObject);
				this.m_referenceCount = 1;
				this.m_Status = AsyncOperationStatus.None;
				this.m_taskCompletionSource = null;
				this.m_taskCompletionSourceTypeless = null;
				this.m_Error = null;
				this.m_Version++;
				this.m_RM = null;
				if (this.m_OnDestroyAction != null)
				{
					this.m_OnDestroyAction(this);
					this.m_OnDestroyAction = null;
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B018 File Offset: 0x00009218
		internal Task<TObject> Task
		{
			get
			{
				if (this.m_taskCompletionSource == null)
				{
					this.m_taskCompletionSource = new TaskCompletionSource<TObject>(TaskCreationOptions.RunContinuationsAsynchronously);
					if (this.IsDone && !this.CompletedEventHasListeners)
					{
						this.m_taskCompletionSource.SetResult(this.Result);
					}
				}
				return this.m_taskCompletionSource.Task;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B068 File Offset: 0x00009268
		Task<object> IAsyncOperation.Task
		{
			get
			{
				if (this.m_taskCompletionSourceTypeless == null)
				{
					this.m_taskCompletionSourceTypeless = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
					if (this.IsDone && !this.CompletedEventHasListeners)
					{
						this.m_taskCompletionSourceTypeless.SetResult(this.Result);
					}
				}
				return this.m_taskCompletionSourceTypeless.Task;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B0BC File Offset: 0x000092BC
		public override string ToString()
		{
			string instId = "";
			Object or = this.Result as Object;
			if (or != null)
			{
				instId = "(" + or.GetInstanceID().ToString() + ")";
			}
			string text = "{0}, result='{1}', status='{2}'";
			object obj = base.ToString();
			Object @object = or;
			return string.Format(text, obj, ((@object != null) ? @object.ToString() : null) + instId, this.m_Status);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B135 File Offset: 0x00009335
		private void RegisterForDeferredCallbackEvent(bool incrementReferenceCount = true)
		{
			if (this.IsDone && !this.m_InDeferredCallbackQueue)
			{
				this.m_InDeferredCallbackQueue = true;
				ResourceManager rm = this.m_RM;
				if (rm == null)
				{
					return;
				}
				rm.RegisterForDeferredCallback(this, incrementReferenceCount);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060002D4 RID: 724 RVA: 0x0000B160 File Offset: 0x00009360
		// (remove) Token: 0x060002D5 RID: 725 RVA: 0x0000B188 File Offset: 0x00009388
		internal event Action<AsyncOperationHandle<TObject>> Completed
		{
			add
			{
				if (this.m_CompletedActionT == null)
				{
					this.m_CompletedActionT = DelegateList<AsyncOperationHandle<TObject>>.CreateWithGlobalCache();
				}
				this.m_CompletedActionT.Add(value);
				this.RegisterForDeferredCallbackEvent(true);
			}
			remove
			{
				DelegateList<AsyncOperationHandle<TObject>> completedActionT = this.m_CompletedActionT;
				if (completedActionT == null)
				{
					return;
				}
				completedActionT.Remove(value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060002D6 RID: 726 RVA: 0x0000B19B File Offset: 0x0000939B
		// (remove) Token: 0x060002D7 RID: 727 RVA: 0x0000B1BC File Offset: 0x000093BC
		internal event Action<AsyncOperationHandle> Destroyed
		{
			add
			{
				if (this.m_DestroyedAction == null)
				{
					this.m_DestroyedAction = DelegateList<AsyncOperationHandle>.CreateWithGlobalCache();
				}
				this.m_DestroyedAction.Add(value);
			}
			remove
			{
				DelegateList<AsyncOperationHandle> destroyedAction = this.m_DestroyedAction;
				if (destroyedAction == null)
				{
					return;
				}
				destroyedAction.Remove(value);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002D8 RID: 728 RVA: 0x0000B1D0 File Offset: 0x000093D0
		// (remove) Token: 0x060002D9 RID: 729 RVA: 0x0000B1FC File Offset: 0x000093FC
		internal event Action<AsyncOperationHandle> CompletedTypeless
		{
			add
			{
				this.Completed += delegate(AsyncOperationHandle<TObject> s)
				{
					value(s);
				};
			}
			remove
			{
				this.Completed -= delegate(AsyncOperationHandle<TObject> s)
				{
					value(s);
				};
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B228 File Offset: 0x00009428
		internal AsyncOperationStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B230 File Offset: 0x00009430
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000B238 File Offset: 0x00009438
		internal Exception OperationException
		{
			get
			{
				return this.m_Error;
			}
			private set
			{
				this.m_Error = value;
				if (this.m_Error != null && ResourceManager.ExceptionHandler != null)
				{
					ResourceManager.ExceptionHandler(new AsyncOperationHandle(this), value);
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B261 File Offset: 0x00009461
		internal bool MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006444 File Offset: 0x00004644
		internal void Reset()
		{
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000466C File Offset: 0x0000286C
		internal object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B26C File Offset: 0x0000946C
		internal bool IsDone
		{
			get
			{
				return this.Status == AsyncOperationStatus.Failed || this.Status == AsyncOperationStatus.Succeeded;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000B284 File Offset: 0x00009484
		internal float PercentComplete
		{
			get
			{
				if (this.m_Status == AsyncOperationStatus.None)
				{
					try
					{
						return this.Progress;
					}
					catch
					{
						return 0f;
					}
				}
				return 1f;
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B2C4 File Offset: 0x000094C4
		internal void InvokeCompletionEvent()
		{
			if (this.m_CompletedActionT != null)
			{
				this.m_CompletedActionT.Invoke(this.Handle);
				this.m_CompletedActionT.Clear();
			}
			if (this.m_taskCompletionSource != null)
			{
				this.m_taskCompletionSource.TrySetResult(this.Result);
			}
			if (this.m_taskCompletionSourceTypeless != null)
			{
				this.m_taskCompletionSourceTypeless.TrySetResult(this.Result);
			}
			this.m_InDeferredCallbackQueue = false;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B335 File Offset: 0x00009535
		internal AsyncOperationHandle<TObject> Handle
		{
			get
			{
				return new AsyncOperationHandle<TObject>(this);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B33D File Offset: 0x0000953D
		private void UpdateCallback(float unscaledDeltaTime)
		{
			(this as IUpdateReceiver).Update(unscaledDeltaTime);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B34B File Offset: 0x0000954B
		public void Complete(TObject result, bool success, string errorMsg)
		{
			this.Complete(result, success, errorMsg, true);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B357 File Offset: 0x00009557
		public void Complete(TObject result, bool success, string errorMsg, bool releaseDependenciesOnFailure)
		{
			this.Complete(result, success, (!string.IsNullOrEmpty(errorMsg)) ? new OperationException(errorMsg, null) : null, releaseDependenciesOnFailure);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B378 File Offset: 0x00009578
		public void Complete(TObject result, bool success, Exception exception, bool releaseDependenciesOnFailure = true)
		{
			if (this.IsDone)
			{
				return;
			}
			IUpdateReceiver upOp = this as IUpdateReceiver;
			if (this.m_UpdateCallbacks != null && upOp != null)
			{
				this.m_UpdateCallbacks.Remove(this.m_UpdateCallback);
			}
			this.Result = result;
			this.m_Status = (success ? AsyncOperationStatus.Succeeded : AsyncOperationStatus.Failed);
			if (this.m_Status == AsyncOperationStatus.Failed || exception != null)
			{
				if (exception == null || string.IsNullOrEmpty(exception.Message))
				{
					this.OperationException = new OperationException("Unknown error in AsyncOperation : " + this.DebugName, null);
				}
				else
				{
					this.OperationException = exception;
				}
			}
			if (this.m_Status == AsyncOperationStatus.Failed)
			{
				if (releaseDependenciesOnFailure)
				{
					this.ReleaseDependencies();
				}
				ICachable cachedOperation = this as ICachable;
				if (((cachedOperation != null) ? cachedOperation.Key : null) != null)
				{
					ResourceManager rm = this.m_RM;
					if (rm != null)
					{
						rm.RemoveOperationFromCache(cachedOperation.Key);
					}
				}
				this.RegisterForDeferredCallbackEvent(false);
			}
			else
			{
				this.InvokeCompletionEvent();
				this.DecrementReferenceCount();
			}
			this.IsRunning = false;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B464 File Offset: 0x00009664
		internal void Start(ResourceManager rm, AsyncOperationHandle dependency, DelegateList<float> updateCallbacks)
		{
			this.m_RM = rm;
			this.IsRunning = true;
			this.HasExecuted = false;
			this.IncrementReferenceCount();
			this.m_UpdateCallbacks = updateCallbacks;
			if (dependency.IsValid() && !dependency.IsDone)
			{
				dependency.Completed += this.m_dependencyCompleteAction;
				return;
			}
			this.InvokeExecute();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B4B9 File Offset: 0x000096B9
		internal void InvokeExecute()
		{
			this.Execute();
			this.HasExecuted = true;
			if (this is IUpdateReceiver && !this.IsDone)
			{
				this.m_UpdateCallbacks.Add(this.m_UpdateCallback);
			}
			Action executed = this.Executed;
			if (executed == null)
			{
				return;
			}
			executed();
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002EA RID: 746 RVA: 0x0000B4F9 File Offset: 0x000096F9
		// (remove) Token: 0x060002EB RID: 747 RVA: 0x0000B502 File Offset: 0x00009702
		event Action<AsyncOperationHandle> IAsyncOperation.CompletedTypeless
		{
			add
			{
				this.CompletedTypeless += value;
			}
			remove
			{
				this.CompletedTypeless -= value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002EC RID: 748 RVA: 0x0000B50B File Offset: 0x0000970B
		// (remove) Token: 0x060002ED RID: 749 RVA: 0x0000B514 File Offset: 0x00009714
		event Action<AsyncOperationHandle> IAsyncOperation.Destroyed
		{
			add
			{
				this.Destroyed += value;
			}
			remove
			{
				this.Destroyed -= value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B51D File Offset: 0x0000971D
		int IAsyncOperation.Version
		{
			get
			{
				return this.Version;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B525 File Offset: 0x00009725
		int IAsyncOperation.ReferenceCount
		{
			get
			{
				return this.ReferenceCount;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B52D File Offset: 0x0000972D
		float IAsyncOperation.PercentComplete
		{
			get
			{
				return this.PercentComplete;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B535 File Offset: 0x00009735
		AsyncOperationStatus IAsyncOperation.Status
		{
			get
			{
				return this.Status;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000B53D File Offset: 0x0000973D
		Exception IAsyncOperation.OperationException
		{
			get
			{
				return this.OperationException;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B545 File Offset: 0x00009745
		bool IAsyncOperation.IsDone
		{
			get
			{
				return this.IsDone;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B54D File Offset: 0x0000974D
		AsyncOperationHandle IAsyncOperation.Handle
		{
			get
			{
				return this.Handle;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000B55A File Offset: 0x0000975A
		Action<IAsyncOperation> IAsyncOperation.OnDestroy
		{
			set
			{
				this.OnDestroy = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000B563 File Offset: 0x00009763
		string IAsyncOperation.DebugName
		{
			get
			{
				return this.DebugName;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B56B File Offset: 0x0000976B
		object IAsyncOperation.GetResultAsObject()
		{
			return this.Result;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000B578 File Offset: 0x00009778
		Type IAsyncOperation.ResultType
		{
			get
			{
				return typeof(TObject);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B584 File Offset: 0x00009784
		void IAsyncOperation.GetDependencies(List<AsyncOperationHandle> deps)
		{
			this.GetDependencies(deps);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B58D File Offset: 0x0000978D
		void IAsyncOperation.DecrementReferenceCount()
		{
			this.DecrementReferenceCount();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B595 File Offset: 0x00009795
		void IAsyncOperation.IncrementReferenceCount()
		{
			this.IncrementReferenceCount();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B59D File Offset: 0x0000979D
		void IAsyncOperation.InvokeCompletionEvent()
		{
			this.InvokeCompletionEvent();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B5A5 File Offset: 0x000097A5
		void IAsyncOperation.Start(ResourceManager rm, AsyncOperationHandle dependency, DelegateList<float> updateCallbacks)
		{
			this.Start(rm, dependency, updateCallbacks);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00006444 File Offset: 0x00004644
		internal virtual void ReleaseDependencies()
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B5B0 File Offset: 0x000097B0
		DownloadStatus IAsyncOperation.GetDownloadStatus(HashSet<object> visited)
		{
			return this.GetDownloadStatus(visited);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B5BC File Offset: 0x000097BC
		internal virtual DownloadStatus GetDownloadStatus(HashSet<object> visited)
		{
			visited.Add(this);
			return new DownloadStatus
			{
				IsDone = this.IsDone
			};
		}

		// Token: 0x04000149 RID: 329
		private int m_referenceCount = 1;

		// Token: 0x0400014A RID: 330
		internal AsyncOperationStatus m_Status;

		// Token: 0x0400014B RID: 331
		internal Exception m_Error;

		// Token: 0x0400014C RID: 332
		internal ResourceManager m_RM;

		// Token: 0x0400014D RID: 333
		internal int m_Version;

		// Token: 0x0400014E RID: 334
		private DelegateList<AsyncOperationHandle> m_DestroyedAction;

		// Token: 0x0400014F RID: 335
		private DelegateList<AsyncOperationHandle<TObject>> m_CompletedActionT;

		// Token: 0x04000150 RID: 336
		private Action<IAsyncOperation> m_OnDestroyAction;

		// Token: 0x04000151 RID: 337
		private Action<AsyncOperationHandle> m_dependencyCompleteAction;

		// Token: 0x04000152 RID: 338
		protected internal bool HasExecuted;

		// Token: 0x04000155 RID: 341
		private TaskCompletionSource<TObject> m_taskCompletionSource;

		// Token: 0x04000156 RID: 342
		private TaskCompletionSource<object> m_taskCompletionSourceTypeless;

		// Token: 0x04000157 RID: 343
		private bool m_InDeferredCallbackQueue;

		// Token: 0x04000158 RID: 344
		private DelegateList<float> m_UpdateCallbacks;

		// Token: 0x04000159 RID: 345
		private Action<float> m_UpdateCallback;
	}
}
