using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000091 RID: 145
	[NativeHeader("Runtime/Logging/LogSystem.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Misc/BuildSettings.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	[NativeHeader("Runtime/Network/NetworkUtility.h")]
	[NativeHeader("Runtime/PreloadManager/LoadSceneOperation.h")]
	[NativeHeader("Runtime/PreloadManager/PreloadManager.h")]
	[NativeHeader("Runtime/Input/TargetFrameRate.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/Misc/Player.h")]
	[NativeHeader("Runtime/Export/Application/Application.bindings.h")]
	[NativeHeader("Runtime/BaseClasses/IsPlaying.h")]
	[NativeHeader("Runtime/Application/ApplicationInfo.h")]
	[NativeHeader("Runtime/Utilities/Argv.h")]
	[NativeHeader("Runtime/Utilities/URLUtility.h")]
	[NativeHeader("Runtime/File/ApplicationSpecificPersistentDataPath.h")]
	[NativeHeader("Runtime/Application/AdsIdHandler.h")]
	public class Application
	{
		// Token: 0x0600025E RID: 606
		[FreeFunction("GetInputManager().QuitApplication")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Quit(int exitCode);

		// Token: 0x0600025F RID: 607 RVA: 0x00006003 File Offset: 0x00004203
		public static void Quit()
		{
			Application.Quit(0);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000260 RID: 608
		public static extern bool isPlaying
		{
			[FreeFunction("IsWorldPlaying")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000261 RID: 609
		public static extern bool isFocused
		{
			[FreeFunction("IsPlayerFocused")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000262 RID: 610
		public static extern bool runInBackground
		{
			[FreeFunction("GetPlayerSettingsRunInBackground")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000263 RID: 611
		public static extern bool isBatchMode
		{
			[FreeFunction("::IsBatchmode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00006010 File Offset: 0x00004210
		public static string dataPath
		{
			[FreeFunction("GetAppDataPath", IsThreadSafe = true)]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_dataPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00006040 File Offset: 0x00004240
		public static string streamingAssetsPath
		{
			[FreeFunction("GetStreamingAssetsPath", IsThreadSafe = true)]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_streamingAssetsPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00006070 File Offset: 0x00004270
		public static string persistentDataPath
		{
			[FreeFunction("GetPersistentDataPathApplicationSpecific")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_persistentDataPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000060A0 File Offset: 0x000042A0
		public static string unityVersion
		{
			[FreeFunction("Application_Bindings::GetUnityVersion", IsThreadSafe = true)]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_unityVersion_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000060D0 File Offset: 0x000042D0
		public static string version
		{
			[FreeFunction("GetApplicationInfo().GetVersion")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Application.get_version_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00006100 File Offset: 0x00004300
		[FreeFunction("OpenURL")]
		public unsafe static void OpenURL(string url)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Application.OpenURL_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x1700005D RID: 93
		// (set) Token: 0x0600026A RID: 618
		public static extern int targetFrameRate
		{
			[FreeFunction("SetTargetFrameRate")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600026B RID: 619
		public static extern RuntimePlatform platform
		{
			[FreeFunction("systeminfo::GetRuntimePlatform", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006154 File Offset: 0x00004354
		public static bool isMobilePlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				if (runtimePlatform <= RuntimePlatform.Android)
				{
					if (runtimePlatform != RuntimePlatform.IPhonePlayer && runtimePlatform != RuntimePlatform.Android)
					{
						goto IL_003A;
					}
				}
				else
				{
					if (runtimePlatform - RuntimePlatform.MetroPlayerX86 <= 2)
					{
						return SystemInfo.deviceType == DeviceType.Handheld;
					}
					if (runtimePlatform != RuntimePlatform.VisionOS)
					{
						goto IL_003A;
					}
				}
				return true;
				IL_003A:
				return false;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600026D RID: 621
		public static extern SystemLanguage systemLanguage
		{
			[FreeFunction("(SystemLanguage)systeminfo::GetSystemLanguage")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000061A0 File Offset: 0x000043A0
		[RequiredByNativeCode]
		internal static void CallLowMemory(ApplicationMemoryUsage usage)
		{
			Application.MemoryUsageChangedCallback onChanged = Application.memoryUsageChanged;
			bool flag = onChanged != null;
			if (flag)
			{
				ApplicationMemoryUsageChange change = new ApplicationMemoryUsageChange(usage);
				onChanged(in change);
			}
			if (usage > ApplicationMemoryUsage.High)
			{
				if (usage != ApplicationMemoryUsage.Critical)
				{
					throw new Exception(string.Format("Unknown application memory usage: {0}", usage));
				}
				Application.LowMemoryCallback handler = Application.lowMemory;
				bool flag2 = handler != null;
				if (flag2)
				{
					handler();
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006218 File Offset: 0x00004418
		[RequiredByNativeCode]
		internal static bool HasLogCallback()
		{
			return Application.s_LogCallbackHandler != null || Application.s_LogCallbackHandlerThreaded != null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000623C File Offset: 0x0000443C
		[RequiredByNativeCode]
		private static void CallLogCallback(string logString, string stackTrace, LogType type, bool invokedOnMainThread)
		{
			if (invokedOnMainThread)
			{
				Application.LogCallback handler = Application.s_LogCallbackHandler;
				bool flag = handler != null;
				if (flag)
				{
					handler(logString, stackTrace, type);
				}
			}
			Application.LogCallback threadedHandler = Application.s_LogCallbackHandlerThreaded;
			bool flag2 = threadedHandler != null;
			if (flag2)
			{
				threadedHandler(logString, stackTrace, type);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000271 RID: 625 RVA: 0x00006284 File Offset: 0x00004484
		// (remove) Token: 0x06000272 RID: 626 RVA: 0x000062B8 File Offset: 0x000044B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> focusChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000273 RID: 627 RVA: 0x000062EC File Offset: 0x000044EC
		// (remove) Token: 0x06000274 RID: 628 RVA: 0x00006320 File Offset: 0x00004520
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action quitting;

		// Token: 0x06000275 RID: 629 RVA: 0x00006354 File Offset: 0x00004554
		[RequiredByNativeCode]
		private static bool Internal_ApplicationWantsToQuit()
		{
			bool flag = Application.wantsToQuit != null;
			if (flag)
			{
				foreach (Func<bool> continueQuit in Application.wantsToQuit.GetInvocationList())
				{
					try
					{
						bool flag2 = !continueQuit();
						if (flag2)
						{
							return false;
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
			}
			return true;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000063D4 File Offset: 0x000045D4
		public static CancellationToken exitCancellationToken
		{
			get
			{
				return Application.s_currentCancellationTokenSource.Token;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000063E0 File Offset: 0x000045E0
		[RequiredByNativeCode]
		private static void Internal_ApplicationInit()
		{
			Application.s_currentCancellationTokenSource = new CancellationTokenSource();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000063F0 File Offset: 0x000045F0
		[RequiredByNativeCode]
		private static void Internal_ApplicationQuit()
		{
			Application.s_currentCancellationTokenSource.Cancel();
			bool flag = Application.quitting != null;
			if (flag)
			{
				Application.quitting();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006420 File Offset: 0x00004620
		[RequiredByNativeCode]
		private static void Internal_ApplicationUnload()
		{
			bool flag = Application.unloading != null;
			if (flag)
			{
				Application.unloading();
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006445 File Offset: 0x00004645
		[RequiredByNativeCode]
		internal static void InvokeOnBeforeRender()
		{
			BeforeRenderHelper.Invoke();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006450 File Offset: 0x00004650
		[RequiredByNativeCode]
		internal static void InvokeFocusChanged(bool focus)
		{
			bool flag = Application.focusChanged != null;
			if (flag)
			{
				Application.focusChanged(focus);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006478 File Offset: 0x00004678
		[RequiredByNativeCode]
		internal static void InvokeDeepLinkActivated(string url)
		{
			bool flag = Application.deepLinkActivated != null;
			if (flag)
			{
				Application.deepLinkActivated(url);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000064A0 File Offset: 0x000046A0
		public static bool isEditor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600027F RID: 639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_dataPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000280 RID: 640
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_streamingAssetsPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000281 RID: 641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_persistentDataPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000282 RID: 642
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_unityVersion_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000283 RID: 643
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_version_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000284 RID: 644
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OpenURL_Injected(ref ManagedSpanWrapper url);

		// Token: 0x04000151 RID: 337
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Application.LowMemoryCallback lowMemory;

		// Token: 0x04000152 RID: 338
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Application.MemoryUsageChangedCallback memoryUsageChanged;

		// Token: 0x04000153 RID: 339
		private static Application.LogCallback s_LogCallbackHandler;

		// Token: 0x04000154 RID: 340
		private static Application.LogCallback s_LogCallbackHandlerThreaded;

		// Token: 0x04000156 RID: 342
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<string> deepLinkActivated;

		// Token: 0x04000157 RID: 343
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Func<bool> wantsToQuit;

		// Token: 0x04000159 RID: 345
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action unloading;

		// Token: 0x0400015A RID: 346
		private static CancellationTokenSource s_currentCancellationTokenSource = new CancellationTokenSource();

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x06000286 RID: 646
		public delegate void LowMemoryCallback();

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x06000288 RID: 648
		public delegate void MemoryUsageChangedCallback(in ApplicationMemoryUsageChange usage);

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x0600028A RID: 650
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
