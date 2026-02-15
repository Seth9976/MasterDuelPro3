using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Android
{
	// Token: 0x02000010 RID: 16
	[StaticAccessor("AndroidApplication", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidApplication.bindings.h")]
	public static class AndroidApplication
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000100 RID: 256
		internal static extern IntPtr UnityPlayerRaw
		{
			[ThreadSafe]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006B58 File Offset: 0x00004D58
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void AcquireMainThreadSynchronizationContext()
		{
			AndroidApplication.m_MainThreadSynchronizationContext = SynchronizationContext.Current;
			bool flag = AndroidApplication.m_MainThreadSynchronizationContext == null;
			if (flag)
			{
				throw new Exception("Failed to acquire main thread synchronization context");
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006B87 File Offset: 0x00004D87
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void SetCurrentConfiguration(AndroidConfiguration config)
		{
			AndroidApplication.m_CurrentConfiguration = config;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006B90 File Offset: 0x00004D90
		[RequiredByNativeCode(GenerateProxy = true)]
		private static AndroidConfiguration GetCurrentConfiguration()
		{
			return AndroidApplication.m_CurrentConfiguration;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006BA8 File Offset: 0x00004DA8
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void DispatchConfigurationChanged(bool notifySubscribers)
		{
			if (notifySubscribers)
			{
				Action<AndroidConfiguration> action = AndroidApplication.onConfigurationChanged;
				if (action != null)
				{
					action(AndroidApplication.m_CurrentConfiguration);
				}
			}
		}

		// Token: 0x04000022 RID: 34
		private static SynchronizationContext m_MainThreadSynchronizationContext;

		// Token: 0x04000023 RID: 35
		private static AndroidConfiguration m_CurrentConfiguration;

		// Token: 0x04000024 RID: 36
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<AndroidConfiguration> onConfigurationChanged;
	}
}
