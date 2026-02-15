using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000D1 RID: 209
	[NativeHeader("Runtime/Graphics/DisplayManager.h")]
	[UsedByNativeCode]
	public class Display
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0000BD9C File Offset: 0x00009F9C
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000BDB2 File Offset: 0x00009FB2
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public int renderingWidth
		{
			get
			{
				int w = 0;
				int h = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out w, out h);
				return w;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public int renderingHeight
		{
			get
			{
				int w = 0;
				int h = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out w, out h);
				return h;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000BE14 File Offset: 0x0000A014
		public int systemWidth
		{
			get
			{
				int w = 0;
				int h = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out w, out h);
				return w;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000BE3C File Offset: 0x0000A03C
		public int systemHeight
		{
			get
			{
				int w = 0;
				int h = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out w, out h);
				return h;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000BE64 File Offset: 0x0000A064
		public bool requiresSrgbBlitToBackbuffer
		{
			get
			{
				return Display.RequiresSrgbBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000BE84 File Offset: 0x0000A084
		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int rx = 0;
			int ry = 0;
			int x = (int)inputMouseCoordinates.x;
			int y = (int)inputMouseCoordinates.y;
			Vector3 vec;
			vec.z = (float)Display.RelativeMouseAtImpl(x, y, out rx, out ry);
			vec.x = (float)rx;
			vec.y = (float)ry;
			return vec;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		[RequiredByNativeCode]
		internal static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			bool flag = nativeDisplay.Length == 0;
			if (!flag)
			{
				Display.displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					Display.displays[i] = new Display(nativeDisplay[i]);
				}
				Display._mainDisplay = Display.displays[0];
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000BF40 File Offset: 0x0000A140
		[RequiredByNativeCode]
		internal static void FireDisplaysUpdated()
		{
			bool flag = Display.onDisplaysUpdated != null;
			if (flag)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x06000566 RID: 1382
		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000567 RID: 1383
		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000568 RID: 1384
		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		// Token: 0x06000569 RID: 1385
		[FreeFunction("UnityDisplayManager_RequiresSRGBBlitToBackbuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RequiresSrgbBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x0400027B RID: 635
		internal IntPtr nativeDisplay;

		// Token: 0x0400027C RID: 636
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x0400027D RID: 637
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x0400027E RID: 638
		private static int m_ActiveEditorGameViewTarget = -1;

		// Token: 0x0400027F RID: 639
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Display.DisplaysUpdatedDelegate onDisplaysUpdated = null;

		// Token: 0x020000D2 RID: 210
		// (Invoke) Token: 0x0600056C RID: 1388
		public delegate void DisplaysUpdatedDelegate();
	}
}
