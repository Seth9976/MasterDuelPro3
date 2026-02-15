using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000D8 RID: 216
	[NativeHeader("Runtime/Graphics/ScreenManager.h")]
	[NativeHeader("Runtime/Graphics/WindowLayout.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GetScreenManager()", StaticAccessorType.Dot)]
	public sealed class Screen
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600057A RID: 1402
		public static extern int width
		{
			[NativeMethod(Name = "GetWidth", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600057B RID: 1403
		public static extern int height
		{
			[NativeMethod(Name = "GetHeight", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600057C RID: 1404
		public static extern float dpi
		{
			[NativeName("GetDPI")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600057D RID: 1405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScreenOrientation GetScreenOrientation();

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.GetScreenOrientation();
			}
		}

		// Token: 0x170000FA RID: 250
		// (set) Token: 0x0600057F RID: 1407
		[NativeProperty("ScreenTimeout")]
		public static extern int sleepTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public static Resolution currentResolution
		{
			get
			{
				Resolution resolution;
				Screen.get_currentResolution_Injected(out resolution);
				return resolution;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000581 RID: 1409
		public static extern bool fullScreen
		{
			[NativeName("IsFullscreen")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000C30C File Offset: 0x0000A50C
		public static Rect safeArea
		{
			get
			{
				Rect rect;
				Screen.get_safeArea_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000C324 File Offset: 0x0000A524
		[NativeName("RequestResolution")]
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode, RefreshRate preferredRefreshRate)
		{
			Screen.SetResolution_Injected(width, height, fullscreenMode, ref preferredRefreshRate);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000C33C File Offset: 0x0000A53C
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, new RefreshRate
			{
				numerator = 0U,
				denominator = 1U
			});
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000C36C File Offset: 0x0000A56C
		[Obsolete("SetResolution(int, int, bool, int) is obsolete. Use SetResolution(int, int, FullScreenMode, RefreshRate) instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SetResolution(int width, int height, bool fullscreen, [UnityEngine.Internal.DefaultValue("0")] int preferredRefreshRate)
		{
			bool flag = preferredRefreshRate < 0;
			if (flag)
			{
				preferredRefreshRate = 0;
			}
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, new RefreshRate
			{
				numerator = (uint)preferredRefreshRate,
				denominator = 1U
			});
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000C3AD File Offset: 0x0000A5AD
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}

		// Token: 0x06000587 RID: 1415
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMSAASamples(int numSamples);

		// Token: 0x06000588 RID: 1416
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMSAASamples();

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		public static int msaaSamples
		{
			get
			{
				return Screen.GetMSAASamples();
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public static Resolution[] resolutions
		{
			[FreeFunction("ScreenScripting::GetResolutions")]
			get
			{
				Resolution[] array2;
				try
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Screen.get_resolutions_Injected(out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Resolution[] array;
					blittableArrayWrapper.Unmarshal<Resolution>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x0600058B RID: 1419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentResolution_Injected(out Resolution ret);

		// Token: 0x0600058C RID: 1420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_safeArea_Injected(out Rect ret);

		// Token: 0x0600058D RID: 1421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetResolution_Injected(int width, int height, FullScreenMode fullscreenMode, [In] ref RefreshRate preferredRefreshRate);

		// Token: 0x0600058E RID: 1422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_resolutions_Injected(out BlittableArrayWrapper ret);
	}
}
