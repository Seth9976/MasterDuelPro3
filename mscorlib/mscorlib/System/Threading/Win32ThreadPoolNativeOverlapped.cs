using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x02000243 RID: 579
	internal struct Win32ThreadPoolNativeOverlapped
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x000107A8 File Offset: 0x0000E9A8
		static Win32ThreadPoolNativeOverlapped()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000558A4 File Offset: 0x00053AA4
		internal Win32ThreadPoolNativeOverlapped.OverlappedData Data
		{
			get
			{
				return Win32ThreadPoolNativeOverlapped.s_dataArray[this._dataIndex];
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000558B4 File Offset: 0x00053AB4
		internal unsafe static Win32ThreadPoolNativeOverlapped* Allocate(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			Win32ThreadPoolNativeOverlapped* ptr = Win32ThreadPoolNativeOverlapped.AllocateNew();
			try
			{
				ptr->SetData(callback, state, pinData, preAllocated);
			}
			catch
			{
				Win32ThreadPoolNativeOverlapped.Free(ptr);
				throw;
			}
			return ptr;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000558F0 File Offset: 0x00053AF0
		private unsafe static Win32ThreadPoolNativeOverlapped* AllocateNew()
		{
			IntPtr intPtr;
			Win32ThreadPoolNativeOverlapped* ptr;
			while ((intPtr = Volatile.Read(ref Win32ThreadPoolNativeOverlapped.s_freeList)) != IntPtr.Zero)
			{
				ptr = (Win32ThreadPoolNativeOverlapped*)(void*)intPtr;
				if (!(Interlocked.CompareExchange(ref Win32ThreadPoolNativeOverlapped.s_freeList, ptr->_nextFree, intPtr) != intPtr))
				{
					ptr->_nextFree = IntPtr.Zero;
					return ptr;
				}
			}
			ptr = (Win32ThreadPoolNativeOverlapped*)(void*)Interop.MemAlloc((UIntPtr)((ulong)((long)sizeof(Win32ThreadPoolNativeOverlapped))));
			*ptr = default(Win32ThreadPoolNativeOverlapped);
			Win32ThreadPoolNativeOverlapped.OverlappedData overlappedData = new Win32ThreadPoolNativeOverlapped.OverlappedData();
			int num = Interlocked.Increment(ref Win32ThreadPoolNativeOverlapped.s_dataCount) - 1;
			if (num < 0)
			{
				Environment.FailFast("Too many outstanding Win32ThreadPoolNativeOverlapped instances");
			}
			for (;;)
			{
				Win32ThreadPoolNativeOverlapped.OverlappedData[] array = Volatile.Read<Win32ThreadPoolNativeOverlapped.OverlappedData[]>(ref Win32ThreadPoolNativeOverlapped.s_dataArray);
				int num2 = ((array == null) ? 0 : array.Length);
				if (num2 <= num)
				{
					int i = num2;
					if (i == 0)
					{
						i = 128;
					}
					while (i <= num)
					{
						i = i * 3 / 2;
					}
					Win32ThreadPoolNativeOverlapped.OverlappedData[] array2 = array;
					Array.Resize<Win32ThreadPoolNativeOverlapped.OverlappedData>(ref array2, i);
					if (Interlocked.CompareExchange<Win32ThreadPoolNativeOverlapped.OverlappedData[]>(ref Win32ThreadPoolNativeOverlapped.s_dataArray, array2, array) != array)
					{
						continue;
					}
					array = array2;
				}
				if (Win32ThreadPoolNativeOverlapped.s_dataArray[num] != null)
				{
					break;
				}
				Interlocked.Exchange<Win32ThreadPoolNativeOverlapped.OverlappedData>(ref array[num], overlappedData);
			}
			ptr->_dataIndex = num;
			return ptr;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00055A04 File Offset: 0x00053C04
		private void SetData(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			Win32ThreadPoolNativeOverlapped.OverlappedData data = this.Data;
			data._callback = callback;
			data._state = state;
			data._executionContext = ExecutionContext.Capture();
			data._preAllocated = preAllocated;
			if (pinData != null)
			{
				object[] array = pinData as object[];
				if (array != null && array.GetType() == typeof(object[]))
				{
					if (data._pinnedData == null || data._pinnedData.Length < array.Length)
					{
						Array.Resize<GCHandle>(ref data._pinnedData, array.Length);
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (!data._pinnedData[i].IsAllocated)
						{
							data._pinnedData[i] = GCHandle.Alloc(array[i], GCHandleType.Pinned);
						}
						else
						{
							data._pinnedData[i].Target = array[i];
						}
					}
					return;
				}
				if (data._pinnedData == null)
				{
					data._pinnedData = new GCHandle[1];
				}
				if (!data._pinnedData[0].IsAllocated)
				{
					data._pinnedData[0] = GCHandle.Alloc(pinData, GCHandleType.Pinned);
					return;
				}
				data._pinnedData[0].Target = pinData;
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00055B20 File Offset: 0x00053D20
		internal unsafe static void Free(Win32ThreadPoolNativeOverlapped* overlapped)
		{
			overlapped->Data.Reset();
			overlapped->_overlapped = default(NativeOverlapped);
			IntPtr intPtr;
			do
			{
				intPtr = Volatile.Read(ref Win32ThreadPoolNativeOverlapped.s_freeList);
				overlapped->_nextFree = intPtr;
			}
			while (!(Interlocked.CompareExchange(ref Win32ThreadPoolNativeOverlapped.s_freeList, (IntPtr)((void*)overlapped), intPtr) == intPtr));
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00002645 File Offset: 0x00000845
		internal unsafe static NativeOverlapped* ToNativeOverlapped(Win32ThreadPoolNativeOverlapped* overlapped)
		{
			return (NativeOverlapped*)overlapped;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00002645 File Offset: 0x00000845
		internal unsafe static Win32ThreadPoolNativeOverlapped* FromNativeOverlapped(NativeOverlapped* overlapped)
		{
			return (Win32ThreadPoolNativeOverlapped*)overlapped;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00055B70 File Offset: 0x00053D70
		internal unsafe static void CompleteWithCallback(uint errorCode, uint bytesWritten, Win32ThreadPoolNativeOverlapped* overlapped)
		{
			Win32ThreadPoolNativeOverlapped.OverlappedData data = overlapped->Data;
			data._completed = true;
			if (data._executionContext == null)
			{
				data._callback(errorCode, bytesWritten, Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(overlapped));
				return;
			}
			ContextCallback contextCallback = Win32ThreadPoolNativeOverlapped.s_executionContextCallback;
			if (contextCallback == null)
			{
				contextCallback = (Win32ThreadPoolNativeOverlapped.s_executionContextCallback = new ContextCallback(Win32ThreadPoolNativeOverlapped.OnExecutionContextCallback));
			}
			Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs executionContextCallbackArgs = Win32ThreadPoolNativeOverlapped.t_executionContextCallbackArgs;
			if (executionContextCallbackArgs == null)
			{
				executionContextCallbackArgs = new Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs();
			}
			Win32ThreadPoolNativeOverlapped.t_executionContextCallbackArgs = null;
			executionContextCallbackArgs._errorCode = errorCode;
			executionContextCallbackArgs._bytesWritten = bytesWritten;
			executionContextCallbackArgs._overlapped = overlapped;
			executionContextCallbackArgs._data = data;
			ExecutionContext.Run(data._executionContext, contextCallback, executionContextCallbackArgs);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00055C04 File Offset: 0x00053E04
		private unsafe static void OnExecutionContextCallback(object state)
		{
			Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs executionContextCallbackArgs = (Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs)state;
			uint errorCode = executionContextCallbackArgs._errorCode;
			uint bytesWritten = executionContextCallbackArgs._bytesWritten;
			Win32ThreadPoolNativeOverlapped* overlapped = executionContextCallbackArgs._overlapped;
			Win32ThreadPoolNativeOverlapped.OverlappedData data = executionContextCallbackArgs._data;
			executionContextCallbackArgs._data = null;
			Win32ThreadPoolNativeOverlapped.t_executionContextCallbackArgs = executionContextCallbackArgs;
			data._callback(errorCode, bytesWritten, Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(overlapped));
		}

		// Token: 0x04000A6D RID: 2669
		[ThreadStatic]
		private static Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs t_executionContextCallbackArgs;

		// Token: 0x04000A6E RID: 2670
		private static ContextCallback s_executionContextCallback;

		// Token: 0x04000A6F RID: 2671
		private static Win32ThreadPoolNativeOverlapped.OverlappedData[] s_dataArray;

		// Token: 0x04000A70 RID: 2672
		private static int s_dataCount;

		// Token: 0x04000A71 RID: 2673
		private static IntPtr s_freeList;

		// Token: 0x04000A72 RID: 2674
		private NativeOverlapped _overlapped;

		// Token: 0x04000A73 RID: 2675
		private IntPtr _nextFree;

		// Token: 0x04000A74 RID: 2676
		private int _dataIndex;

		// Token: 0x02000244 RID: 580
		private class ExecutionContextCallbackArgs
		{
			// Token: 0x04000A75 RID: 2677
			internal uint _errorCode;

			// Token: 0x04000A76 RID: 2678
			internal uint _bytesWritten;

			// Token: 0x04000A77 RID: 2679
			internal unsafe Win32ThreadPoolNativeOverlapped* _overlapped;

			// Token: 0x04000A78 RID: 2680
			internal Win32ThreadPoolNativeOverlapped.OverlappedData _data;
		}

		// Token: 0x02000245 RID: 581
		internal class OverlappedData
		{
			// Token: 0x06001553 RID: 5459 RVA: 0x00055C54 File Offset: 0x00053E54
			internal void Reset()
			{
				if (this._pinnedData != null)
				{
					for (int i = 0; i < this._pinnedData.Length; i++)
					{
						if (this._pinnedData[i].IsAllocated && this._pinnedData[i].Target != null)
						{
							this._pinnedData[i].Target = null;
						}
					}
				}
				this._callback = null;
				this._state = null;
				this._executionContext = null;
				this._completed = false;
				this._preAllocated = null;
			}

			// Token: 0x04000A79 RID: 2681
			internal GCHandle[] _pinnedData;

			// Token: 0x04000A7A RID: 2682
			internal IOCompletionCallback _callback;

			// Token: 0x04000A7B RID: 2683
			internal object _state;

			// Token: 0x04000A7C RID: 2684
			internal ExecutionContext _executionContext;

			// Token: 0x04000A7D RID: 2685
			internal ThreadPoolBoundHandle _boundHandle;

			// Token: 0x04000A7E RID: 2686
			internal PreAllocatedOverlapped _preAllocated;

			// Token: 0x04000A7F RID: 2687
			internal bool _completed;
		}
	}
}
