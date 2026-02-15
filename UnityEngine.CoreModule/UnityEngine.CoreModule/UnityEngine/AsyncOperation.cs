using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000173 RID: 371
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Scripting/AsyncOperation.bindings.h")]
	[NativeHeader("Runtime/Misc/AsyncOperation.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x06000F72 RID: 3954
		[NativeMethod(IsThreadSafe = true)]
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x06000F73 RID: 3955
		[NativeMethod(IsThreadSafe = true)]
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetManagedObject(IntPtr ptr, [Unmarshalled] AsyncOperation self);

		// Token: 0x06000F74 RID: 3956 RVA: 0x00020857 File Offset: 0x0001EA57
		protected AsyncOperation(IntPtr ptr)
		{
			AsyncOperation.InternalSetManagedObject(ptr, this);
			this.m_Ptr = ptr;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00020870 File Offset: 0x0001EA70
		public bool isDone
		{
			[NativeMethod("IsDone")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_isDone_Injected(intPtr);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00020894 File Offset: 0x0001EA94
		public float progress
		{
			[NativeMethod("GetProgress")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_progress_Injected(intPtr);
			}
		}

		// Token: 0x1700027B RID: 635
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x000208B8 File Offset: 0x0001EAB8
		public int priority
		{
			[NativeMethod("SetPriority")]
			set
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AsyncOperation.set_priority_Injected(intPtr, value);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000208DC File Offset: 0x0001EADC
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00020900 File Offset: 0x0001EB00
		public bool allowSceneActivation
		{
			[NativeMethod("GetAllowSceneActivation")]
			get
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AsyncOperation.get_allowSceneActivation_Injected(intPtr);
			}
			[NativeMethod("SetAllowSceneActivation")]
			set
			{
				IntPtr intPtr = AsyncOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AsyncOperation.set_allowSceneActivation_Injected(intPtr, value);
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00020924 File Offset: 0x0001EB24
		~AsyncOperation()
		{
			AsyncOperation.InternalDestroy(this.m_Ptr);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002095C File Offset: 0x0001EB5C
		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			bool flag = this.m_completeCallback != null;
			if (flag)
			{
				this.m_completeCallback(this);
				this.m_completeCallback = null;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000F7C RID: 3964 RVA: 0x00020990 File Offset: 0x0001EB90
		// (remove) Token: 0x06000F7D RID: 3965 RVA: 0x000209CD File Offset: 0x0001EBCD
		public event Action<AsyncOperation> completed
		{
			add
			{
				bool isDone = this.isDone;
				if (isDone)
				{
					value(this);
				}
				else
				{
					this.m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(this.m_completeCallback, value);
				}
			}
			remove
			{
				this.m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(this.m_completeCallback, value);
			}
		}

		// Token: 0x06000F7E RID: 3966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isDone_Injected(IntPtr _unity_self);

		// Token: 0x06000F7F RID: 3967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_progress_Injected(IntPtr _unity_self);

		// Token: 0x06000F80 RID: 3968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_priority_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000F81 RID: 3969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowSceneActivation_Injected(IntPtr _unity_self);

		// Token: 0x06000F82 RID: 3970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowSceneActivation_Injected(IntPtr _unity_self, bool value);

		// Token: 0x04000614 RID: 1556
		[VisibleToOtherModules(new string[] { "UnityEngine.AssetBundleModule" })]
		internal IntPtr m_Ptr;

		// Token: 0x04000615 RID: 1557
		private Action<AsyncOperation> m_completeCallback;

		// Token: 0x02000174 RID: 372
		internal static class BindingsMarshaller
		{
			// Token: 0x06000F83 RID: 3971 RVA: 0x000209E7 File Offset: 0x0001EBE7
			public static AsyncOperation ConvertToManaged(IntPtr ptr)
			{
				return new AsyncOperation(ptr);
			}

			// Token: 0x06000F84 RID: 3972 RVA: 0x000209EF File Offset: 0x0001EBEF
			public static IntPtr ConvertToNative(AsyncOperation asyncOperation)
			{
				return asyncOperation.m_Ptr;
			}
		}
	}
}
