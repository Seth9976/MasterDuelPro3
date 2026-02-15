using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013C RID: 316
	[NativeHeader("Runtime/Export/Input/Cursor.bindings.h")]
	public class Cursor
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x0001999C File Offset: 0x00017B9C
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			Cursor.SetCursor_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture), ref hotspot, cursorMode);
		}

		// Token: 0x17000223 RID: 547
		// (set) Token: 0x06000D35 RID: 3381
		public static extern bool visible
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000D36 RID: 3382
		// (set) Token: 0x06000D37 RID: 3383
		public static extern CursorLockMode lockState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000D38 RID: 3384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCursor_Injected(IntPtr texture, [In] ref Vector2 hotspot, CursorMode cursorMode);
	}
}
