using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000182 RID: 386
	[NativeHeader("Runtime/Mono/DelayedCallAwaitable.h")]
	[AsyncMethodBuilder(typeof(Awaitable.AwaitableAsyncMethodBuilder))]
	[NativeHeader("Runtime/Mono/Awaitable.h")]
	[NativeHeader("Runtime/Mono/AsyncOperationAwaitable.h")]
	public class Awaitable : IEnumerator
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable FromAsyncOperation(AsyncOperation op, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			IntPtr ptr = Awaitable.FromAsyncOperationInternal(op.m_Ptr);
			return Awaitable.FromNativeAwaitableHandle(ptr, cancellationToken);
		}

		// Token: 0x06000FA0 RID: 4000
		[FreeFunction("Scripting::Awaitables::FromAsyncOperation", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FromAsyncOperationInternal(IntPtr asyncOperation);

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00020E05 File Offset: 0x0001F005
		[ExcludeFromDocs]
		public Awaitable.Awaiter GetAwaiter()
		{
			return new Awaitable.Awaiter(this);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00020E10 File Offset: 0x0001F010
		[RequiredByNativeCode(GenerateProxy = true)]
		private void SetExceptionFromNative(Exception ex)
		{
			bool lockTaken = false;
			try
			{
				this._spinLock.Enter(ref lockTaken);
				this._exceptionToRethrow = ExceptionDispatchInfo.Capture(ex);
			}
			finally
			{
				bool flag = lockTaken;
				if (flag)
				{
					this._spinLock.Exit();
				}
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00020E64 File Offset: 0x0001F064
		[RequiredByNativeCode(GenerateProxy = true)]
		private void RunContinuation()
		{
			Action continuation = null;
			bool lockTaken = false;
			try
			{
				this._spinLock.Enter(ref lockTaken);
				continuation = this._continuation;
				this._continuation = null;
			}
			finally
			{
				bool flag = lockTaken;
				if (flag)
				{
					this._spinLock.Exit();
				}
			}
			if (continuation != null)
			{
				continuation();
			}
		}

		// Token: 0x06000FA4 RID: 4004
		[FreeFunction("Scripting::Awaitables::AttachManagedWrapper", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AttachManagedGCHandleToNativeAwaitable(IntPtr nativeAwaitable, UIntPtr gcHandle);

		// Token: 0x06000FA5 RID: 4005
		[FreeFunction("Scripting::Awaitables::Release", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseNativeAwaitable(IntPtr nativeAwaitable);

		// Token: 0x06000FA6 RID: 4006
		[FreeFunction("Scripting::Awaitables::Cancel", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelNativeAwaitable(IntPtr nativeAwaitable);

		// Token: 0x06000FA7 RID: 4007
		[FreeFunction("Scripting::Awaitables::IsCompleted", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IsNativeAwaitableCompleted(IntPtr nativeAwaitable);

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		private Awaitable()
		{
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		internal static Awaitable NewManagedAwaitable()
		{
			Awaitable awaitable = Awaitable._pool.Value.Get();
			awaitable._handle = Awaitable.AwaitableHandle.ManagedHandle;
			return awaitable;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00020F10 File Offset: 0x0001F110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static Awaitable FromNativeAwaitableHandle(IntPtr nativeHandle, CancellationToken cancellationToken)
		{
			Awaitable awaitable = Awaitable._pool.Value.Get();
			awaitable._handle = nativeHandle;
			Awaitable.AttachManagedGCHandleToNativeAwaitable(nativeHandle, (UIntPtr)((void*)GCHandle.ToIntPtr(GCHandle.Alloc(awaitable))));
			bool canBeCanceled = cancellationToken.CanBeCanceled;
			if (canBeCanceled)
			{
				Awaitable.WireupCancellation(awaitable, cancellationToken);
			}
			return awaitable;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00020F74 File Offset: 0x0001F174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void WireupCancellation(Awaitable awaitable, CancellationToken cancellationToken)
		{
			bool flag = awaitable == null;
			if (flag)
			{
				throw new ArgumentNullException("awaitable");
			}
			bool lockTaken = false;
			try
			{
				awaitable._spinLock.Enter(ref lockTaken);
				awaitable._cancelTokenRegistration = new CancellationTokenRegistration?(cancellationToken.Register(delegate(object coroutine)
				{
					((Awaitable)coroutine).Cancel();
				}, awaitable));
			}
			finally
			{
				bool flag2 = lockTaken;
				if (flag2)
				{
					awaitable._spinLock.Exit();
				}
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00021004 File Offset: 0x0001F204
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void RaiseManagedCompletion(Exception exception)
		{
			Action continuation = null;
			bool lockTaken = false;
			try
			{
				this._spinLock.Enter(ref lockTaken);
				bool flag = exception != null;
				if (flag)
				{
					this._exceptionToRethrow = ExceptionDispatchInfo.Capture(exception);
				}
				this._managedAwaitableDone = true;
				continuation = this._continuation;
				this._continuation = null;
			}
			finally
			{
				bool flag2 = lockTaken;
				if (flag2)
				{
					this._spinLock.Exit();
				}
			}
			if (continuation != null)
			{
				continuation();
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00021088 File Offset: 0x0001F288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void RaiseManagedCompletion()
		{
			Action continuation = null;
			bool lockTaken = false;
			try
			{
				this._spinLock.Enter(ref lockTaken);
				this._managedAwaitableDone = true;
				continuation = this._continuation;
				this._continuation = null;
				this._managedCompletionQueue = null;
			}
			finally
			{
				bool flag = lockTaken;
				if (flag)
				{
					this._spinLock.Exit();
				}
			}
			if (continuation != null)
			{
				continuation();
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000210FC File Offset: 0x0001F2FC
		internal void PropagateExceptionAndRelease()
		{
			bool lockTaken = false;
			try
			{
				this._spinLock.Enter(ref lockTaken);
				this.CheckPointerValidity();
				bool flag = this._cancelTokenRegistration != null;
				if (flag)
				{
					this._cancelTokenRegistration.Value.Dispose();
					this._cancelTokenRegistration = null;
				}
				this._managedAwaitableDone = false;
				Awaitable.AwaitableHandle ptr = this._handle;
				this._handle = Awaitable.AwaitableHandle.NullHandle;
				ExceptionDispatchInfo toRethrow = this._exceptionToRethrow;
				this._exceptionToRethrow = null;
				this._managedCompletionQueue = null;
				this._continuation = null;
				bool flag2 = !ptr.IsManaged && !ptr.IsNull;
				if (flag2)
				{
					Awaitable.ReleaseNativeAwaitable(ptr);
				}
				Awaitable._pool.Value.Release(this);
				if (toRethrow != null)
				{
					toRethrow.Throw();
				}
			}
			finally
			{
				bool flag3 = lockTaken;
				if (flag3)
				{
					this._spinLock.Exit();
				}
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000211FC File Offset: 0x0001F3FC
		public void Cancel()
		{
			Awaitable.AwaitableHandle handle = this.CheckPointerValidity();
			bool isManaged = handle.IsManaged;
			if (isManaged)
			{
				Awaitable.DoubleBufferedAwaitableList managedCompletionQueue = this._managedCompletionQueue;
				if (managedCompletionQueue != null)
				{
					managedCompletionQueue.Remove(this);
				}
				this.RaiseManagedCompletion(new OperationCanceledException());
			}
			else
			{
				Awaitable.CancelNativeAwaitable(handle);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00021250 File Offset: 0x0001F450
		private bool IsCompletedNoLock
		{
			get
			{
				this.CheckPointerValidity();
				bool isManaged = this._handle.IsManaged;
				bool flag;
				if (isManaged)
				{
					flag = this._managedAwaitableDone;
				}
				else
				{
					flag = Awaitable.IsNativeAwaitableCompleted(this._handle) != 0;
				}
				return flag;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00021298 File Offset: 0x0001F498
		public bool IsCompleted
		{
			get
			{
				bool lockTaken = false;
				bool isCompletedNoLock;
				try
				{
					this._spinLock.Enter(ref lockTaken);
					isCompletedNoLock = this.IsCompletedNoLock;
				}
				finally
				{
					bool flag = lockTaken;
					if (flag)
					{
						this._spinLock.Exit();
					}
				}
				return isCompletedNoLock;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000212E8 File Offset: 0x0001F4E8
		internal bool IsDettachedOrCompleted
		{
			get
			{
				bool lockTaken = false;
				bool flag;
				try
				{
					this._spinLock.Enter(ref lockTaken);
					bool isNull = this._handle.IsNull;
					if (isNull)
					{
						flag = true;
					}
					else
					{
						this.CheckPointerValidity();
						bool isManaged = this._handle.IsManaged;
						if (isManaged)
						{
							flag = this._managedAwaitableDone;
						}
						else
						{
							flag = Awaitable.IsNativeAwaitableCompleted(this._handle) != 0;
						}
					}
				}
				finally
				{
					bool flag2 = lockTaken;
					if (flag2)
					{
						this._spinLock.Exit();
					}
				}
				return flag;
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002137C File Offset: 0x0001F57C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Awaitable.AwaitableHandle CheckPointerValidity()
		{
			Awaitable.AwaitableHandle handle = this._handle;
			bool isNull = handle.IsNull;
			if (isNull)
			{
				throw new InvalidOperationException("Awaitable is in detached state");
			}
			return handle;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000213B0 File Offset: 0x0001F5B0
		internal void SetContinuation(Action continuation)
		{
			bool done = false;
			bool lockTaken = false;
			try
			{
				this._spinLock.Enter(ref lockTaken);
				bool isCompletedNoLock = this.IsCompletedNoLock;
				if (isCompletedNoLock)
				{
					done = true;
				}
				else
				{
					this._continuation = continuation;
				}
			}
			finally
			{
				bool flag = lockTaken;
				if (flag)
				{
					this._spinLock.Exit();
				}
			}
			bool flag2 = done;
			if (flag2)
			{
				continuation();
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00021424 File Offset: 0x0001F624
		bool IEnumerator.MoveNext()
		{
			bool isCompleted = this.IsCompleted;
			bool flag;
			if (isCompleted)
			{
				this.PropagateExceptionAndRelease();
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00003D56 File Offset: 0x00001F56
		void IEnumerator.Reset()
		{
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0002144D File Offset: 0x0001F64D
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00021450 File Offset: 0x0001F650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable EndOfFrameAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			Awaitable.EnsureDelayedCallWiredUp();
			Awaitable awaitable = Awaitable.NewManagedAwaitable();
			Awaitable._endOfFrameAwaitables.Add(awaitable, -1);
			awaitable._managedCompletionQueue = Awaitable._endOfFrameAwaitables;
			bool canBeCanceled = cancellationToken.CanBeCanceled;
			if (canBeCanceled)
			{
				Awaitable.WireupCancellation(awaitable, cancellationToken);
			}
			return awaitable;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000214A4 File Offset: 0x0001F6A4
		private static void EnsureDelayedCallWiredUp()
		{
			bool nextFrameAndEndOfFrameWiredUp = Awaitable._nextFrameAndEndOfFrameWiredUp;
			if (!nextFrameAndEndOfFrameWiredUp)
			{
				Awaitable._nextFrameAndEndOfFrameWiredUp = true;
				Awaitable.WireupNextFrameAndEndOfFrameCallbacks();
				Awaitable._nextFrameAndEndOfFrameWiredUpCTRegistration = Application.exitCancellationToken.Register(new Action(Awaitable.OnDelayedCallManagerCleared));
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000214E8 File Offset: 0x0001F6E8
		[RequiredByNativeCode]
		private static void OnDelayedCallManagerCleared()
		{
			Awaitable._nextFrameAndEndOfFrameWiredUp = false;
			Awaitable._nextFrameAwaitables.Clear();
			Awaitable._endOfFrameAwaitables.Clear();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00021507 File Offset: 0x0001F707
		[RequiredByNativeCode]
		private static void OnUpdate()
		{
			Awaitable._nextFrameAwaitables.SwapAndComplete();
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00021515 File Offset: 0x0001F715
		[RequiredByNativeCode]
		private static void OnEndOfFrame()
		{
			Awaitable._endOfFrameAwaitables.SwapAndComplete();
		}

		// Token: 0x06000FBD RID: 4029
		[FreeFunction("Scripting::Awaitables::WireupNextFrameAndEndOfFrameCallbacks")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WireupNextFrameAndEndOfFrameCallbacks();

		// Token: 0x06000FBE RID: 4030 RVA: 0x00021523 File Offset: 0x0001F723
		internal static void SetSynchronizationContext(SynchronizationContext synchronizationContext)
		{
			Awaitable._synchronizationContext = synchronizationContext;
		}

		// Token: 0x04000625 RID: 1573
		private SpinLock _spinLock = default(SpinLock);

		// Token: 0x04000626 RID: 1574
		private static readonly ThreadLocal<ObjectPool<Awaitable>> _pool = new ThreadLocal<ObjectPool<Awaitable>>(() => new ObjectPool<Awaitable>(() => new Awaitable(), null, null, null, false, 10, 10000));

		// Token: 0x04000627 RID: 1575
		private Awaitable.AwaitableHandle _handle;

		// Token: 0x04000628 RID: 1576
		private ExceptionDispatchInfo _exceptionToRethrow;

		// Token: 0x04000629 RID: 1577
		private bool _managedAwaitableDone;

		// Token: 0x0400062A RID: 1578
		private Action _continuation;

		// Token: 0x0400062B RID: 1579
		private CancellationTokenRegistration? _cancelTokenRegistration;

		// Token: 0x0400062C RID: 1580
		private Awaitable.DoubleBufferedAwaitableList _managedCompletionQueue;

		// Token: 0x0400062D RID: 1581
		private static bool _nextFrameAndEndOfFrameWiredUp = false;

		// Token: 0x0400062E RID: 1582
		private static CancellationTokenRegistration _nextFrameAndEndOfFrameWiredUpCTRegistration = default(CancellationTokenRegistration);

		// Token: 0x0400062F RID: 1583
		private static readonly Awaitable.DoubleBufferedAwaitableList _nextFrameAwaitables = new Awaitable.DoubleBufferedAwaitableList();

		// Token: 0x04000630 RID: 1584
		private static readonly Awaitable.DoubleBufferedAwaitableList _endOfFrameAwaitables = new Awaitable.DoubleBufferedAwaitableList();

		// Token: 0x04000631 RID: 1585
		private static SynchronizationContext _synchronizationContext;

		// Token: 0x02000183 RID: 387
		[ExcludeFromDocs]
		public struct AwaitableAsyncMethodBuilder
		{
			// Token: 0x04000632 RID: 1586
			private Awaitable.AwaitableAsyncMethodBuilder.IStateMachineBox _stateMachineBox;

			// Token: 0x04000633 RID: 1587
			private Awaitable _resultingCoroutine;

			// Token: 0x02000184 RID: 388
			private interface IStateMachineBox : IDisposable
			{
			}
		}

		// Token: 0x02000185 RID: 389
		[ExcludeFromDocs]
		public struct AwaitableAsyncMethodBuilder<T>
		{
			// Token: 0x04000634 RID: 1588
			private Awaitable.AwaitableAsyncMethodBuilder<T>.IStateMachineBox _stateMachineBox;

			// Token: 0x04000635 RID: 1589
			private Awaitable<T> _resultingCoroutine;

			// Token: 0x02000186 RID: 390
			private interface IStateMachineBox : IDisposable
			{
			}
		}

		// Token: 0x02000187 RID: 391
		[ExcludeFromDocs]
		public struct Awaiter : INotifyCompletion
		{
			// Token: 0x06000FC0 RID: 4032 RVA: 0x00021578 File Offset: 0x0001F778
			internal Awaiter(Awaitable awaited)
			{
				this._awaited = awaited;
			}

			// Token: 0x06000FC1 RID: 4033 RVA: 0x00021581 File Offset: 0x0001F781
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				this._awaited.SetContinuation(continuation);
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00021591 File Offset: 0x0001F791
			public bool IsCompleted
			{
				get
				{
					return this._awaited.IsCompleted;
				}
			}

			// Token: 0x06000FC3 RID: 4035 RVA: 0x0002159E File Offset: 0x0001F79E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void GetResult()
			{
				this._awaited.PropagateExceptionAndRelease();
			}

			// Token: 0x04000636 RID: 1590
			private readonly Awaitable _awaited;
		}

		// Token: 0x02000188 RID: 392
		private readonly struct AwaitableHandle
		{
			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000215AC File Offset: 0x0001F7AC
			public bool IsNull
			{
				get
				{
					return this._handle == IntPtr.Zero;
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000215BE File Offset: 0x0001F7BE
			public bool IsManaged
			{
				get
				{
					return this._handle == Awaitable.AwaitableHandle.ManagedHandle._handle;
				}
			}

			// Token: 0x06000FC6 RID: 4038 RVA: 0x000215D5 File Offset: 0x0001F7D5
			public AwaitableHandle(IntPtr handle)
			{
				this._handle = handle;
			}

			// Token: 0x06000FC7 RID: 4039 RVA: 0x000215DE File Offset: 0x0001F7DE
			public static implicit operator IntPtr(Awaitable.AwaitableHandle handle)
			{
				return handle._handle;
			}

			// Token: 0x06000FC8 RID: 4040 RVA: 0x000215E6 File Offset: 0x0001F7E6
			public static implicit operator Awaitable.AwaitableHandle(IntPtr handle)
			{
				return new Awaitable.AwaitableHandle(handle);
			}

			// Token: 0x04000637 RID: 1591
			private readonly IntPtr _handle;

			// Token: 0x04000638 RID: 1592
			public static Awaitable.AwaitableHandle ManagedHandle = new Awaitable.AwaitableHandle(new IntPtr(-1));

			// Token: 0x04000639 RID: 1593
			public static Awaitable.AwaitableHandle NullHandle = new Awaitable.AwaitableHandle(IntPtr.Zero);
		}

		// Token: 0x02000189 RID: 393
		private struct AwaitableAndFrameIndex
		{
			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0002160F File Offset: 0x0001F80F
			public readonly Awaitable Awaitable { get; }

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00021617 File Offset: 0x0001F817
			public readonly int FrameIndex { get; }

			// Token: 0x06000FCC RID: 4044 RVA: 0x0002161F File Offset: 0x0001F81F
			public AwaitableAndFrameIndex(Awaitable awaitable, int frameIndex)
			{
				this.Awaitable = awaitable;
				this.FrameIndex = frameIndex;
			}
		}

		// Token: 0x0200018A RID: 394
		private class DoubleBufferedAwaitableList
		{
			// Token: 0x06000FCD RID: 4045 RVA: 0x00021630 File Offset: 0x0001F830
			public void SwapAndComplete()
			{
				List<Awaitable.AwaitableAndFrameIndex> oldScratch = this._scratch;
				List<Awaitable.AwaitableAndFrameIndex> toIterate = this._awaitables;
				this._awaitables = oldScratch;
				this._scratch = toIterate;
				try
				{
					foreach (Awaitable.AwaitableAndFrameIndex item in toIterate)
					{
						bool flag = !item.Awaitable.IsDettachedOrCompleted;
						if (flag)
						{
							bool flag2 = Time.frameCount >= item.FrameIndex || item.FrameIndex == -1;
							if (flag2)
							{
								item.Awaitable.RaiseManagedCompletion();
							}
							else
							{
								oldScratch.Add(item);
							}
						}
					}
				}
				finally
				{
					toIterate.Clear();
				}
			}

			// Token: 0x06000FCE RID: 4046 RVA: 0x00021708 File Offset: 0x0001F908
			public void Add(Awaitable item, int frameIndex)
			{
				this._awaitables.Add(new Awaitable.AwaitableAndFrameIndex(item, frameIndex));
			}

			// Token: 0x06000FCF RID: 4047 RVA: 0x00021720 File Offset: 0x0001F920
			public void Remove(Awaitable item)
			{
				this._awaitables.RemoveAll((Awaitable.AwaitableAndFrameIndex x) => x.Awaitable == item);
			}

			// Token: 0x06000FD0 RID: 4048 RVA: 0x00021753 File Offset: 0x0001F953
			public void Clear()
			{
				this._awaitables.Clear();
			}

			// Token: 0x0400063C RID: 1596
			private List<Awaitable.AwaitableAndFrameIndex> _awaitables = new List<Awaitable.AwaitableAndFrameIndex>();

			// Token: 0x0400063D RID: 1597
			private List<Awaitable.AwaitableAndFrameIndex> _scratch = new List<Awaitable.AwaitableAndFrameIndex>();
		}
	}
}
