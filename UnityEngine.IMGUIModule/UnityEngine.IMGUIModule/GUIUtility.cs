using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[NativeHeader("Modules/IMGUI/GUIManager.h")]
	[NativeHeader("Runtime/Camera/RenderLayers/GUITexture.h")]
	[NativeHeader("Runtime/Utilities/CopyPaste.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Modules/IMGUI/GUIUtility.h")]
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class GUIUtility
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000150 RID: 336
		// (set) Token: 0x06000151 RID: 337
		[NativeProperty("GetGUIState().m_PixelsPerPoint", true, TargetType.Field)]
		internal static extern float pixelsPerPoint
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000152 RID: 338
		[NativeProperty("GetGUIState().m_OnGUIDepth", true, TargetType.Field)]
		internal static extern int guiDepth
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000153 RID: 339
		[StaticAccessor("GetInputManager()", StaticAccessorType.Dot)]
		internal static extern bool textFieldInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000629C File Offset: 0x0000449C
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000062CC File Offset: 0x000044CC
		public unsafe static string systemCopyBuffer
		{
			[FreeFunction("GetCopyBuffer")]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					GUIUtility.get_systemCopyBuffer_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction("SetCopyBuffer")]
			set
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					GUIUtility.set_systemCopyBuffer_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006320 File Offset: 0x00004520
		[FreeFunction("GetGUIState().GetControlID")]
		private static int Internal_GetControlID(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.Internal_GetControlID_Injected(hint, focusType, ref rect);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006338 File Offset: 0x00004538
		public static int GetControlID(int hint, FocusType focusType, Rect rect)
		{
			GUIUtility.s_ControlCount++;
			return GUIUtility.Internal_GetControlID(hint, focusType, rect);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006360 File Offset: 0x00004560
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void BeginContainerFromOwner(ScriptableObject owner)
		{
			GUIUtility.BeginContainerFromOwner_Injected(Object.MarshalledUnityObject.Marshal<ScriptableObject>(owner));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006378 File Offset: 0x00004578
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void BeginContainer(ObjectGUIState objectGUIState)
		{
			GUIUtility.BeginContainer_Injected((objectGUIState == null) ? ((IntPtr)0) : ObjectGUIState.BindingsMarshaller.ConvertToNative(objectGUIState));
		}

		// Token: 0x0600015A RID: 346
		[NativeMethod("EndContainer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_EndContainer();

		// Token: 0x0600015B RID: 347 RVA: 0x0000639C File Offset: 0x0000459C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static int CheckForTabEvent(Event evt)
		{
			return GUIUtility.CheckForTabEvent_Injected((evt == null) ? ((IntPtr)0) : Event.BindingsMarshaller.ConvertToNative(evt));
		}

		// Token: 0x0600015C RID: 348
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetKeyboardControlToFirstControlId();

		// Token: 0x0600015D RID: 349
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetKeyboardControlToLastControlId();

		// Token: 0x0600015E RID: 350
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasFocusableControls();

		// Token: 0x0600015F RID: 351
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool OwnsId(int id);

		// Token: 0x06000160 RID: 352 RVA: 0x000063C0 File Offset: 0x000045C0
		public static Rect AlignRectToDevice(Rect rect, out int widthInPixels, out int heightInPixels)
		{
			Rect rect2;
			GUIUtility.AlignRectToDevice_Injected(ref rect, out widthInPixels, out heightInPixels, out rect2);
			return rect2;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000063DC File Offset: 0x000045DC
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static string compositionString
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					GUIUtility.get_compositionString_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000065 RID: 101
		// (set) Token: 0x06000162 RID: 354
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static extern IMECompositionMode imeCompositionMode
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000640C File Offset: 0x0000460C
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static Vector2 compositionCursorPos
		{
			set
			{
				GUIUtility.set_compositionCursorPos_Injected(ref value);
			}
		}

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHotControl();

		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetKeyboardControl();

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetKeyboardControl(int value);

		// Token: 0x06000168 RID: 360
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object Internal_GetDefaultSkin(int skinMode);

		// Token: 0x06000169 RID: 361
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExitGUI();

		// Token: 0x0600016A RID: 362 RVA: 0x00006420 File Offset: 0x00004620
		[RequiredByNativeCode]
		private static void MarkGUIChanged()
		{
			Action action = GUIUtility.guiChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006434 File Offset: 0x00004634
		public static int GetControlID(int hint, FocusType focus)
		{
			return GUIUtility.GetControlID(hint, focus, Rect.zero);
		}

		// Token: 0x17000067 RID: 103
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006452 File Offset: 0x00004652
		internal static bool guiIsExiting
		{
			[CompilerGenerated]
			set
			{
				GUIUtility.<guiIsExiting>k__BackingField = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000645C File Offset: 0x0000465C
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00006473 File Offset: 0x00004673
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000647D File Offset: 0x0000467D
		[RequiredByNativeCode]
		internal static void TakeCapture()
		{
			Action action = GUIUtility.takeCapture;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006491 File Offset: 0x00004691
		[RequiredByNativeCode]
		internal static void RemoveCapture()
		{
			Action action = GUIUtility.releaseCapture;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000064A8 File Offset: 0x000046A8
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000064BF File Offset: 0x000046BF
		public static int keyboardControl
		{
			get
			{
				return GUIUtility.Internal_GetKeyboardControl();
			}
			set
			{
				GUIUtility.Internal_SetKeyboardControl(value);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000064CC File Offset: 0x000046CC
		internal static bool HasKeyFocus(int controlID)
		{
			return controlID == GUIUtility.keyboardControl && (GUIUtility.s_HasCurrentWindowKeyFocusFunc == null || GUIUtility.s_HasCurrentWindowKeyFocusFunc());
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000064FD File Offset: 0x000046FD
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006508 File Offset: 0x00004708
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode) as GUISkin;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000652C File Offset: 0x0000472C
		[RequiredByNativeCode]
		internal static void ProcessEvent(int instanceID, IntPtr nativeEventPtr, out bool result)
		{
			bool flag = GUIUtility.beforeEventProcessed != null;
			if (flag)
			{
				GUIUtility.m_Event.CopyFromPtr(nativeEventPtr);
				GUIUtility.beforeEventProcessed(GUIUtility.m_Event.type, GUIUtility.m_Event.keyCode, GUIUtility.m_Event.modifiers);
			}
			result = false;
			bool flag2 = GUIUtility.processEvent != null;
			if (flag2)
			{
				foreach (Delegate invocation in GUIUtility.processEvent.GetInvocationList())
				{
					Func<int, IntPtr, bool> typed = invocation as Func<int, IntPtr, bool>;
					bool flag3 = typed == null;
					if (!flag3)
					{
						result |= typed(instanceID, nativeEventPtr);
					}
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000065D6 File Offset: 0x000047D6
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void EndContainer()
		{
			GUIUtility.Internal_EndContainer();
			GUIUtility.Internal_ExitGUI();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000065E8 File Offset: 0x000047E8
		[RequiredByNativeCode]
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUIUtility.ResetGlobalState();
			bool flag = useGUILayout != 0;
			if (flag)
			{
				GUILayoutUtility.Begin(instanceID);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006619 File Offset: 0x00004819
		[RequiredByNativeCode]
		internal static void DestroyGUI(int instanceID)
		{
			GUILayoutUtility.RemoveSelectedIdList(instanceID, false);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006624 File Offset: 0x00004824
		[RequiredByNativeCode]
		internal static void EndGUI(int layoutType)
		{
			try
			{
				bool flag = Event.current.type == EventType.Layout;
				if (flag)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000066A0 File Offset: 0x000048A0
		[RequiredByNativeCode]
		internal static bool EndGUIFromException(Exception exception)
		{
			GUIUtility.Internal_ExitGUI();
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000066C0 File Offset: 0x000048C0
		[RequiredByNativeCode]
		internal static bool EndContainerGUIFromException(Exception exception)
		{
			bool flag = GUIUtility.endContainerGUIFromException != null;
			return flag && GUIUtility.endContainerGUIFromException(exception);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000066ED File Offset: 0x000048ED
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void ResetGlobalState()
		{
			GUI.skin = null;
			GUIUtility.guiIsExiting = false;
			GUI.changed = false;
			GUI.scrollViewStates.Clear();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006710 File Offset: 0x00004910
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool IsExitGUIException(Exception exception)
		{
			while (exception is TargetInvocationException && exception.InnerException != null)
			{
				exception = exception.InnerException;
			}
			return exception is ExitGUIException;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006750 File Offset: 0x00004950
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool ShouldRethrowException(Exception exception)
		{
			return GUIUtility.IsExitGUIException(exception);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006768 File Offset: 0x00004968
		internal static void CheckOnGUI()
		{
			bool flag = GUIUtility.guiDepth <= 0;
			if (flag)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006790 File Offset: 0x00004990
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float RoundToPixelGrid(float v)
		{
			return Mathf.Floor(v * GUIUtility.pixelsPerPoint + 0.48f) / GUIUtility.pixelsPerPoint;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000067BC File Offset: 0x000049BC
		public static Rect AlignRectToDevice(Rect rect)
		{
			int width;
			int height;
			return GUIUtility.AlignRectToDevice(rect, out width, out height);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000067D8 File Offset: 0x000049D8
		internal static bool HitTest(Rect rect, Vector2 point, int offset)
		{
			return point.x >= rect.xMin - (float)offset && point.x < rect.xMax + (float)offset && point.y >= rect.yMin - (float)offset && point.y < rect.yMax + (float)offset;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006838 File Offset: 0x00004A38
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static bool HitTest(Rect rect, Vector2 point, bool isDirectManipulationDevice)
		{
			int offset = 0;
			return GUIUtility.HitTest(rect, point, offset);
		}

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_systemCopyBuffer_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_systemCopyBuffer_Injected(ref ManagedSpanWrapper value);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetControlID_Injected(int hint, FocusType focusType, [In] ref Rect rect);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginContainerFromOwner_Injected(IntPtr owner);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginContainer_Injected(IntPtr objectGUIState);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CheckForTabEvent_Injected(IntPtr evt);

		// Token: 0x0600018C RID: 396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AlignRectToDevice_Injected([In] ref Rect rect, out int widthInPixels, out int heightInPixels, out Rect ret);

		// Token: 0x0600018D RID: 397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionString_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600018E RID: 398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected([In] ref Vector2 value);

		// Token: 0x040000B5 RID: 181
		internal static int s_ControlCount = 0;

		// Token: 0x040000B6 RID: 182
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static int s_SkinMode;

		// Token: 0x040000B7 RID: 183
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static int s_OriginalID;

		// Token: 0x040000B8 RID: 184
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action takeCapture;

		// Token: 0x040000B9 RID: 185
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action releaseCapture;

		// Token: 0x040000BA RID: 186
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Func<int, IntPtr, bool> processEvent;

		// Token: 0x040000BB RID: 187
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action cleanupRoots;

		// Token: 0x040000BC RID: 188
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Func<Exception, bool> endContainerGUIFromException;

		// Token: 0x040000BD RID: 189
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static Action guiChanged;

		// Token: 0x040000BE RID: 190
		internal static Action<EventType, KeyCode, EventModifiers> beforeEventProcessed;

		// Token: 0x040000BF RID: 191
		private static Event m_Event = new Event();

		// Token: 0x040000C1 RID: 193
		internal static Func<bool> s_HasCurrentWindowKeyFocusFunc;
	}
}
