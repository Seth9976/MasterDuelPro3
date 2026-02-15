using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class Input
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002209 File Offset: 0x00000409
		public static float GetAxis(string axisName)
		{
			return InputUnsafeUtility.GetAxis(axisName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002211 File Offset: 0x00000411
		public static float GetAxisRaw(string axisName)
		{
			return InputUnsafeUtility.GetAxisRaw(axisName);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002219 File Offset: 0x00000419
		public static bool GetButtonDown(string buttonName)
		{
			return InputUnsafeUtility.GetButtonDown(buttonName);
		}

		// Token: 0x06000019 RID: 25
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(KeyCode key);

		// Token: 0x0600001A RID: 26
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x0600001B RID: 27
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x0600001C RID: 28
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x0600001D RID: 29 RVA: 0x00002224 File Offset: 0x00000424
		[NativeThrows]
		public static Touch GetTouch(int index)
		{
			Touch touch;
			Input.GetTouch_Injected(index, out touch);
			return touch;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000223C File Offset: 0x0000043C
		[NativeThrows]
		public static PenData GetLastPenContactEvent()
		{
			PenData penData;
			Input.GetLastPenContactEvent_Injected(out penData);
			return penData;
		}

		// Token: 0x0600001F RID: 31
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearLastPenContactEvent();

		// Token: 0x06000020 RID: 32 RVA: 0x00002251 File Offset: 0x00000451
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt(key);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33
		[NativeThrows]
		public static extern bool anyKey
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000225C File Offset: 0x0000045C
		[NativeThrows]
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 vector;
				Input.get_mousePosition_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002274 File Offset: 0x00000474
		[NativeThrows]
		public static Vector2 mouseScrollDelta
		{
			get
			{
				Vector2 vector;
				Input.get_mouseScrollDelta_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36
		// (set) Token: 0x06000025 RID: 37
		public static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000228C File Offset: 0x0000048C
		public static string compositionString
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Input.get_compositionString_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022D4 File Offset: 0x000004D4
		public static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 vector;
				Input.get_compositionCursorPos_Injected(out vector);
				return vector;
			}
			set
			{
				Input.set_compositionCursorPos_Injected(ref value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022E8 File Offset: 0x000004E8
		internal static bool simulateTouchEnabled { get; }

		// Token: 0x0600002A RID: 42
		[FreeFunction("GetMousePresent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMousePresentInternal();

		// Token: 0x0600002B RID: 43
		[FreeFunction("IsTouchSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetTouchSupportedInternal();

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022EF File Offset: 0x000004EF
		public static bool mousePresent
		{
			get
			{
				return !Input.simulateTouchEnabled && Input.GetMousePresentInternal();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002300 File Offset: 0x00000500
		public static bool touchSupported
		{
			get
			{
				return Input.simulateTouchEnabled || Input.GetTouchSupportedInternal();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002E RID: 46
		public static extern int touchCount
		{
			[FreeFunction("GetTouchCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002314 File Offset: 0x00000514
		public static Touch[] touches
		{
			get
			{
				int count = Input.touchCount;
				Touch[] touches = new Touch[count];
				for (int q = 0; q < count; q++)
				{
					touches[q] = Input.GetTouch(q);
				}
				return touches;
			}
		}

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckDisabled();

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTouch_Injected(int index, out Touch ret);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastPenContactEvent_Injected(out PenData ret);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePosition_Injected(out Vector3 ret);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mouseScrollDelta_Injected(out Vector2 ret);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionString_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected([In] ref Vector2 value);
	}
}
