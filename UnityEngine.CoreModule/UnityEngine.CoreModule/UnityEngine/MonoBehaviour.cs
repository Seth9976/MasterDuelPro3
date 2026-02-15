using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AB RID: 427
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[RequiredByNativeCode]
	[ExtensionOfNativeClass]
	[NativeHeader("Runtime/Scripting/DelayedCallUtility.h")]
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00023B8C File Offset: 0x00021D8C
		public CancellationToken destroyCancellationToken
		{
			get
			{
				bool flag = this == null;
				if (flag)
				{
					throw new MissingReferenceException("DestroyCancellation token should be called atleast once before destroying the monobehaviour object");
				}
				bool flag2 = this.m_CancellationTokenSource == null;
				if (flag2)
				{
					this.m_CancellationTokenSource = new CancellationTokenSource();
					this.OnCancellationTokenCreated();
				}
				return this.m_CancellationTokenSource.Token;
			}
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00023BE0 File Offset: 0x00021DE0
		[RequiredByNativeCode]
		private void RaiseCancellation()
		{
			CancellationTokenSource cancellationTokenSource = this.m_CancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00023BF8 File Offset: 0x00021DF8
		public bool IsInvoking()
		{
			return MonoBehaviour.Internal_IsInvokingAll(this);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00023C10 File Offset: 0x00021E10
		public void CancelInvoke()
		{
			MonoBehaviour.Internal_CancelInvokeAll(this);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00023C1A File Offset: 0x00021E1A
		public void Invoke(string methodName, float time)
		{
			MonoBehaviour.InvokeDelayed(this, methodName, time, 0f);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00023C2C File Offset: 0x00021E2C
		public void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			bool flag = repeatRate <= 1E-05f && repeatRate != 0f;
			if (flag)
			{
				throw new UnityException("Invoke repeat rate has to be larger than 0.00001F");
			}
			MonoBehaviour.InvokeDelayed(this, methodName, time, repeatRate);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00023C69 File Offset: 0x00021E69
		public void CancelInvoke(string methodName)
		{
			MonoBehaviour.CancelInvoke(this, methodName);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00023C74 File Offset: 0x00021E74
		public bool IsInvoking(string methodName)
		{
			return MonoBehaviour.IsInvoking(this, methodName);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00023C90 File Offset: 0x00021E90
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return this.StartCoroutine(methodName, value);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00023CAC File Offset: 0x00021EAC
		public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
		{
			bool flag = string.IsNullOrEmpty(methodName);
			if (flag)
			{
				throw new NullReferenceException("methodName is null or empty");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged(methodName, value);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00023CF4 File Offset: 0x00021EF4
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged2(routine);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00023D38 File Offset: 0x00021F38
		[Obsolete("StartCoroutine_Auto has been deprecated. Use StartCoroutine instead (UnityUpgradable) -> StartCoroutine([mscorlib] System.Collections.IEnumerator)", false)]
		public Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return this.StartCoroutine(routine);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00023D54 File Offset: 0x00021F54
		public void StopCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineFromEnumeratorManaged(routine);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00023D98 File Offset: 0x00021F98
		public void StopCoroutine(Coroutine routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineManaged(routine);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00023DDC File Offset: 0x00021FDC
		public unsafe void StopCoroutine(string methodName)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				MonoBehaviour.StopCoroutine_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00023E40 File Offset: 0x00022040
		public void StopAllCoroutines()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.StopAllCoroutines_Injected(intPtr);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00023E64 File Offset: 0x00022064
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00023E88 File Offset: 0x00022088
		public bool useGUILayout
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MonoBehaviour.get_useGUILayout_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MonoBehaviour.set_useGUILayout_Injected(intPtr, value);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00023EAC File Offset: 0x000220AC
		public bool didStart
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MonoBehaviour.get_didStart_Injected(intPtr);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00023ED0 File Offset: 0x000220D0
		public bool didAwake
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MonoBehaviour.get_didAwake_Injected(intPtr);
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00023EF2 File Offset: 0x000220F2
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00023EFC File Offset: 0x000220FC
		[FreeFunction("CancelInvoke")]
		private static void Internal_CancelInvokeAll([NotNull] MonoBehaviour self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			MonoBehaviour.Internal_CancelInvokeAll_Injected(intPtr);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00023F34 File Offset: 0x00022134
		[FreeFunction("IsInvoking")]
		private static bool Internal_IsInvokingAll([NotNull] MonoBehaviour self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return MonoBehaviour.Internal_IsInvokingAll_Injected(intPtr);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00023F6C File Offset: 0x0002216C
		[FreeFunction]
		private unsafe static void InvokeDelayed([NotNull] MonoBehaviour self, string methodName, float time, float repeatRate)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(self, "self");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				MonoBehaviour.InvokeDelayed_Injected(intPtr, ref managedSpanWrapper, time, repeatRate);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00023FE8 File Offset: 0x000221E8
		[FreeFunction]
		private unsafe static void CancelInvoke([NotNull] MonoBehaviour self, string methodName)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(self, "self");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				MonoBehaviour.CancelInvoke_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00024060 File Offset: 0x00022260
		[FreeFunction]
		private unsafe static bool IsInvoking([NotNull] MonoBehaviour self, string methodName)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(self);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(self, "self");
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = MonoBehaviour.IsInvoking_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000240DC File Offset: 0x000222DC
		[FreeFunction]
		private static bool IsObjectMonoBehaviour([NotNull] Object obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(obj);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return MonoBehaviour.IsObjectMonoBehaviour_Injected(intPtr);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00024114 File Offset: 0x00022314
		[return: Unmarshalled]
		private unsafe Coroutine StartCoroutineManaged(string methodName, object value)
		{
			Coroutine coroutine;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(methodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = methodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				coroutine = MonoBehaviour.StartCoroutineManaged_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
			return coroutine;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0002417C File Offset: 0x0002237C
		[return: Unmarshalled]
		private Coroutine StartCoroutineManaged2(IEnumerator enumerator)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MonoBehaviour.StartCoroutineManaged2_Injected(intPtr, enumerator);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000241A0 File Offset: 0x000223A0
		private void StopCoroutineManaged(Coroutine routine)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.StopCoroutineManaged_Injected(intPtr, (routine == null) ? ((IntPtr)0) : Coroutine.BindingsMarshaller.ConvertToNative(routine));
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000241D4 File Offset: 0x000223D4
		private void StopCoroutineFromEnumeratorManaged(IEnumerator routine)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.StopCoroutineFromEnumeratorManaged_Injected(intPtr, routine);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000241F8 File Offset: 0x000223F8
		internal string GetScriptClassName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				MonoBehaviour.GetScriptClassName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00024238 File Offset: 0x00022438
		private void OnCancellationTokenCreated()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MonoBehaviour>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MonoBehaviour.OnCancellationTokenCreated_Injected(intPtr);
		}

		// Token: 0x060010ED RID: 4333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopCoroutine_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName);

		// Token: 0x060010EE RID: 4334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopAllCoroutines_Injected(IntPtr _unity_self);

		// Token: 0x060010EF RID: 4335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useGUILayout_Injected(IntPtr _unity_self);

		// Token: 0x060010F0 RID: 4336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useGUILayout_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060010F1 RID: 4337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_didStart_Injected(IntPtr _unity_self);

		// Token: 0x060010F2 RID: 4338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_didAwake_Injected(IntPtr _unity_self);

		// Token: 0x060010F3 RID: 4339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CancelInvokeAll_Injected(IntPtr self);

		// Token: 0x060010F4 RID: 4340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsInvokingAll_Injected(IntPtr self);

		// Token: 0x060010F5 RID: 4341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeDelayed_Injected(IntPtr self, ref ManagedSpanWrapper methodName, float time, float repeatRate);

		// Token: 0x060010F6 RID: 4342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInvoke_Injected(IntPtr self, ref ManagedSpanWrapper methodName);

		// Token: 0x060010F7 RID: 4343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInvoking_Injected(IntPtr self, ref ManagedSpanWrapper methodName);

		// Token: 0x060010F8 RID: 4344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsObjectMonoBehaviour_Injected(IntPtr obj);

		// Token: 0x060010F9 RID: 4345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Coroutine StartCoroutineManaged_Injected(IntPtr _unity_self, ref ManagedSpanWrapper methodName, object value);

		// Token: 0x060010FA RID: 4346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Coroutine StartCoroutineManaged2_Injected(IntPtr _unity_self, IEnumerator enumerator);

		// Token: 0x060010FB RID: 4347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopCoroutineManaged_Injected(IntPtr _unity_self, IntPtr routine);

		// Token: 0x060010FC RID: 4348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopCoroutineFromEnumeratorManaged_Injected(IntPtr _unity_self, IEnumerator routine);

		// Token: 0x060010FD RID: 4349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetScriptClassName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060010FE RID: 4350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnCancellationTokenCreated_Injected(IntPtr _unity_self);

		// Token: 0x04000675 RID: 1653
		private CancellationTokenSource m_CancellationTokenSource;
	}
}
