using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BE RID: 446
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	internal sealed class UnhandledExceptionHandler
	{
		// Token: 0x0600113E RID: 4414 RVA: 0x00024F25 File Offset: 0x00023125
		[RequiredByNativeCode]
		private static void RegisterUECatcher()
		{
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
			{
				Debug.LogException(e.ExceptionObject as Exception);
			};
		}
	}
}
