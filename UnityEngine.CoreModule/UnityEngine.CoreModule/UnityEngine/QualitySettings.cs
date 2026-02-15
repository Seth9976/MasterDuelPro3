using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E7 RID: 231
	[NativeHeader("Runtime/Graphics/QualitySettings.h")]
	[StaticAccessor("GetQualitySettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	public sealed class QualitySettings : Object
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		[RequiredByNativeCode]
		internal static void OnActiveQualityLevelChanged(int previousQualityLevel, int currentQualityLevel)
		{
			Action<int, int> action = QualitySettings.activeQualityLevelChanged;
			if (action != null)
			{
				action(previousQualityLevel, currentQualityLevel);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000CCFE File Offset: 0x0000AEFE
		public static void SetQualityLevel(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600060A RID: 1546
		// (set) Token: 0x0600060B RID: 1547
		public static extern int pixelLightCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600060C RID: 1548
		[NativeProperty("ShadowmaskMode")]
		public static extern ShadowmaskMode shadowmaskMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600060D RID: 1549
		[NativeProperty("LODBias")]
		public static extern float lodBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600060E RID: 1550
		public static extern int maximumLODLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700011D RID: 285
		// (set) Token: 0x0600060F RID: 1551
		public static extern bool enableLODCrossFade
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011E RID: 286
		// (set) Token: 0x06000610 RID: 1552
		public static extern int vSyncCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000611 RID: 1553
		// (set) Token: 0x06000612 RID: 1554
		public static extern int antiAliasing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000613 RID: 1555
		public static extern bool billboardsFaceCameraPosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000614 RID: 1556
		[NativeName("SetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000615 RID: 1557
		public static extern ColorSpace desiredColorSpace
		{
			[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[NativeName("GetColorSpace")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000616 RID: 1558
		public static extern ColorSpace activeColorSpace
		{
			[NativeName("GetColorSpace")]
			[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x040002B1 RID: 689
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<int, int> activeQualityLevelChanged;
	}
}
