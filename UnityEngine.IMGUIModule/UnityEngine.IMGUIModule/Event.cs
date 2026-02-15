using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/IMGUI/Event.bindings.h")]
	[StaticAccessor("GUIEvent", StaticAccessorType.DoubleColon)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Event
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[NativeProperty("type", false, TargetType.Field)]
		public EventType rawType
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_rawType_Injected(intPtr);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
		[NativeProperty("mousePosition", false, TargetType.Field)]
		public Vector2 mousePosition
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Event.get_mousePosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_mousePosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		[NativeProperty("delta", false, TargetType.Field)]
		public Vector2 delta
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Event.get_delta_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_delta_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		[NativeProperty("pointerType", false, TargetType.Field)]
		public PointerType pointerType
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_pointerType_Injected(intPtr);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		[NativeProperty("button", false, TargetType.Field)]
		public int button
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_button_Injected(intPtr);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002178 File Offset: 0x00000378
		[NativeProperty("modifiers", false, TargetType.Field)]
		public EventModifiers modifiers
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_modifiers_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_modifiers_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
		[NativeProperty("pressure", false, TargetType.Field)]
		public float pressure
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_pressure_Injected(intPtr);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021C0 File Offset: 0x000003C0
		[NativeProperty("twist", false, TargetType.Field)]
		public float twist
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_twist_Injected(intPtr);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021E4 File Offset: 0x000003E4
		[NativeProperty("tilt", false, TargetType.Field)]
		public Vector2 tilt
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Event.get_tilt_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000220C File Offset: 0x0000040C
		[NativeProperty("penStatus", false, TargetType.Field)]
		public PenStatus penStatus
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_penStatus_Injected(intPtr);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002230 File Offset: 0x00000430
		[NativeProperty("clickCount", false, TargetType.Field)]
		public int clickCount
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_clickCount_Injected(intPtr);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002278 File Offset: 0x00000478
		[NativeProperty("character", false, TargetType.Field)]
		public char character
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_character_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_character_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000229C File Offset: 0x0000049C
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022C0 File Offset: 0x000004C0
		[NativeProperty("keycode", false, TargetType.Field)]
		private KeyCode Internal_keyCode
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_Internal_keyCode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_Internal_keyCode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E4 File Offset: 0x000004E4
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000233D File Offset: 0x0000053D
		public KeyCode keyCode
		{
			get
			{
				KeyCode key = (this.isMouse ? (KeyCode.Mouse0 + this.button) : this.Internal_keyCode);
				bool isScrollWheel = this.isScrollWheel;
				if (isScrollWheel)
				{
					key = ((this.delta.y < 0f) ? KeyCode.WheelUp : KeyCode.WheelDown);
				}
				return key;
			}
			set
			{
				this.Internal_keyCode = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002348 File Offset: 0x00000548
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000236C File Offset: 0x0000056C
		[NativeProperty("displayIndex", false, TargetType.Field)]
		public int displayIndex
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_displayIndex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_displayIndex_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002390 File Offset: 0x00000590
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023B4 File Offset: 0x000005B4
		public EventType type
		{
			[FreeFunction("GUIEvent::GetType", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_type_Injected(intPtr);
			}
			[FreeFunction("GUIEvent::SetType", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_type_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002418 File Offset: 0x00000618
		public unsafe string commandName
		{
			[FreeFunction("GUIEvent::GetCommandName", HasExplicitThis = true)]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Event.get_commandName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction("GUIEvent::SetCommandName", HasExplicitThis = true)]
			set
			{
				try
				{
					IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Event.set_commandName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000247C File Offset: 0x0000067C
		[NativeMethod("Use")]
		private void Internal_Use()
		{
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Event.Internal_Use_Injected(intPtr);
		}

		// Token: 0x0600001C RID: 28
		[FreeFunction("GUIEvent::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(int displayIndex);

		// Token: 0x0600001D RID: 29
		[FreeFunction("GUIEvent::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600001E RID: 30 RVA: 0x000024A0 File Offset: 0x000006A0
		[FreeFunction("GUIEvent::CopyFromPtr", IsThreadSafe = true, HasExplicitThis = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void CopyFromPtr(IntPtr ptr)
		{
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Event.CopyFromPtr_Injected(intPtr, ptr);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024C4 File Offset: 0x000006C4
		public static bool PopEvent([NotNull] Event outEvent)
		{
			if (outEvent == null)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(outEvent);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			return Event.PopEvent_Injected(intPtr);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024FC File Offset: 0x000006FC
		[VisibleToOtherModules(new string[] { "UnityEngine.InputForUIModule" })]
		internal static void GetEventAtIndex(int index, [NotNull] Event outEvent)
		{
			if (outEvent == null)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(outEvent);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			Event.GetEventAtIndex_Injected(index, intPtr);
		}

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEventCount();

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x06000023 RID: 35 RVA: 0x00002534 File Offset: 0x00000734
		[RequiredByNativeCode]
		internal static void Internal_MakeMasterEventCurrent(int displayIndex)
		{
			bool flag = Event.s_MasterEvent == null;
			if (flag)
			{
				Event.s_MasterEvent = new Event(displayIndex);
			}
			Event.s_MasterEvent.displayIndex = displayIndex;
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x06000024 RID: 36
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.InputForUIModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDoubleClickTime();

		// Token: 0x06000025 RID: 37 RVA: 0x0000257F File Offset: 0x0000077F
		public Event()
		{
			this.m_Ptr = Event.Internal_Create(0);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002595 File Offset: 0x00000795
		public Event(int displayIndex)
		{
			this.m_Ptr = Event.Internal_Create(displayIndex);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025AC File Offset: 0x000007AC
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Event.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002604 File Offset: 0x00000804
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void CopyFrom(Event e)
		{
			bool flag = e.m_Ptr != this.m_Ptr;
			if (flag)
			{
				this.CopyFromPtr(e.m_Ptr);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002638 File Offset: 0x00000838
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002658 File Offset: 0x00000858
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002678 File Offset: 0x00000878
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002698 File Offset: 0x00000898
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026B8 File Offset: 0x000008B8
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) > EventModifiers.None;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026D8 File Offset: 0x000008D8
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) > EventModifiers.None;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026F6 File Offset: 0x000008F6
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) > EventModifiers.None;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002704 File Offset: 0x00000904
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000271B File Offset: 0x0000091B
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				Event.s_Current = value ?? Event.s_MasterEvent;
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002740 File Offset: 0x00000940
		public bool isKey
		{
			get
			{
				EventType t = this.type;
				return t == EventType.KeyDown || t == EventType.KeyUp;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002764 File Offset: 0x00000964
		public bool isMouse
		{
			get
			{
				EventType t = this.type;
				return t == EventType.MouseMove || t == EventType.MouseDown || t == EventType.MouseUp || t == EventType.MouseDrag || t == EventType.ContextClick || t == EventType.MouseEnterWindow || t == EventType.MouseLeaveWindow;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000027A0 File Offset: 0x000009A0
		public bool isScrollWheel
		{
			get
			{
				EventType t = this.type;
				return t == EventType.ScrollWheel;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027C0 File Offset: 0x000009C0
		internal bool isDirectManipulationDevice
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				return this.pointerType == PointerType.Pen || this.pointerType == PointerType.Touch;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027E8 File Offset: 0x000009E8
		public static Event KeyboardEvent(string key)
		{
			Event evt = new Event(0)
			{
				type = EventType.KeyDown
			};
			bool flag = string.IsNullOrEmpty(key);
			Event @event;
			if (flag)
			{
				@event = evt;
			}
			else
			{
				int startIdx = 0;
				for (;;)
				{
					bool found = true;
					bool flag2 = startIdx >= key.Length;
					if (flag2)
					{
						break;
					}
					char c = key[startIdx];
					char c2 = c;
					switch (c2)
					{
					case '#':
						evt.modifiers |= EventModifiers.Shift;
						startIdx++;
						break;
					case '$':
						goto IL_00CA;
					case '%':
						evt.modifiers |= EventModifiers.Command;
						startIdx++;
						break;
					case '&':
						evt.modifiers |= EventModifiers.Alt;
						startIdx++;
						break;
					default:
						if (c2 != '^')
						{
							goto IL_00CA;
						}
						evt.modifiers |= EventModifiers.Control;
						startIdx++;
						break;
					}
					IL_00CE:
					if (!found)
					{
						break;
					}
					continue;
					IL_00CA:
					found = false;
					goto IL_00CE;
				}
				string subStr = key.Substring(startIdx, key.Length - startIdx).ToLowerInvariant();
				string text = subStr;
				string text2 = text;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 2049299002U)
				{
					if (num <= 1035581717U)
					{
						if (num <= 388133425U)
						{
							if (num <= 306900080U)
							{
								if (num != 203579616U)
								{
									if (num != 220357235U)
									{
										if (num == 306900080U)
										{
											if (text2 == "left")
											{
												evt.keyCode = KeyCode.LeftArrow;
												evt.modifiers |= EventModifiers.FunctionKey;
												goto IL_0EF8;
											}
										}
									}
									else if (text2 == "f8")
									{
										evt.keyCode = KeyCode.F8;
										evt.modifiers |= EventModifiers.FunctionKey;
										goto IL_0EF8;
									}
								}
								else if (text2 == "f9")
								{
									evt.keyCode = KeyCode.F9;
									evt.modifiers |= EventModifiers.FunctionKey;
									goto IL_0EF8;
								}
							}
							else if (num != 337800568U)
							{
								if (num != 371355806U)
								{
									if (num == 388133425U)
									{
										if (text2 == "f2")
										{
											evt.keyCode = KeyCode.F2;
											evt.modifiers |= EventModifiers.FunctionKey;
											goto IL_0EF8;
										}
									}
								}
								else if (text2 == "f3")
								{
									evt.keyCode = KeyCode.F3;
									evt.modifiers |= EventModifiers.FunctionKey;
									goto IL_0EF8;
								}
							}
							else if (text2 == "f1")
							{
								evt.keyCode = KeyCode.F1;
								evt.modifiers |= EventModifiers.FunctionKey;
								goto IL_0EF8;
							}
						}
						else if (num <= 438466282U)
						{
							if (num != 404911044U)
							{
								if (num != 421688663U)
								{
									if (num == 438466282U)
									{
										if (text2 == "f7")
										{
											evt.keyCode = KeyCode.F7;
											evt.modifiers |= EventModifiers.FunctionKey;
											goto IL_0EF8;
										}
									}
								}
								else if (text2 == "f4")
								{
									evt.keyCode = KeyCode.F4;
									evt.modifiers |= EventModifiers.FunctionKey;
									goto IL_0EF8;
								}
							}
							else if (text2 == "f5")
							{
								evt.keyCode = KeyCode.F5;
								evt.modifiers |= EventModifiers.FunctionKey;
								goto IL_0EF8;
							}
						}
						else if (num != 455243901U)
						{
							if (num != 894689925U)
							{
								if (num == 1035581717U)
								{
									if (text2 == "down")
									{
										evt.keyCode = KeyCode.DownArrow;
										evt.modifiers |= EventModifiers.FunctionKey;
										goto IL_0EF8;
									}
								}
							}
							else if (text2 == "space")
							{
								evt.keyCode = KeyCode.Space;
								evt.character = ' ';
								evt.modifiers &= ~EventModifiers.FunctionKey;
								goto IL_0EF8;
							}
						}
						else if (text2 == "f6")
						{
							evt.keyCode = KeyCode.F6;
							evt.modifiers |= EventModifiers.FunctionKey;
							goto IL_0EF8;
						}
					}
					else if (num <= 1980614408U)
					{
						if (num <= 1193063839U)
						{
							if (num != 1113118030U)
							{
								if (num != 1128467232U)
								{
									if (num == 1193063839U)
									{
										if (text2 == "page up")
										{
											evt.keyCode = KeyCode.PageUp;
											evt.modifiers |= EventModifiers.FunctionKey;
											goto IL_0EF8;
										}
									}
								}
								else if (text2 == "up")
								{
									evt.keyCode = KeyCode.UpArrow;
									evt.modifiers |= EventModifiers.FunctionKey;
									goto IL_0EF8;
								}
							}
							else if (text2 == "[equals]")
							{
								evt.character = '=';
								evt.keyCode = KeyCode.KeypadEquals;
								goto IL_0EF8;
							}
						}
						else if (num != 1740784714U)
						{
							if (num != 1787721130U)
							{
								if (num == 1980614408U)
								{
									if (text2 == "[=]")
									{
										evt.character = '=';
										evt.keyCode = KeyCode.KeypadEquals;
										goto IL_0EF8;
									}
								}
							}
							else if (text2 == "end")
							{
								evt.keyCode = KeyCode.End;
								evt.modifiers |= EventModifiers.FunctionKey;
								goto IL_0EF8;
							}
						}
						else if (text2 == "delete")
						{
							evt.keyCode = KeyCode.Delete;
							evt.modifiers |= EventModifiers.FunctionKey;
							goto IL_0EF8;
						}
					}
					else if (num <= 1981894336U)
					{
						if (num != 1980761503U)
						{
							if (num != 1981202788U)
							{
								if (num == 1981894336U)
								{
									if (text2 == "[5]")
									{
										evt.character = '5';
										evt.keyCode = KeyCode.Keypad5;
										goto IL_0EF8;
									}
								}
							}
							else if (text2 == "[1]")
							{
								evt.character = '1';
								evt.keyCode = KeyCode.Keypad1;
								goto IL_0EF8;
							}
						}
						else if (text2 == "[2]")
						{
							evt.character = '2';
							evt.keyCode = KeyCode.Keypad2;
							goto IL_0EF8;
						}
					}
					else if (num != 2028154341U)
					{
						if (num != 2048857717U)
						{
							if (num == 2049299002U)
							{
								if (text2 == "[+]")
								{
									evt.character = '+';
									evt.keyCode = KeyCode.KeypadPlus;
									goto IL_0EF8;
								}
							}
						}
						else if (text2 == "[4]")
						{
							evt.character = '4';
							evt.keyCode = KeyCode.Keypad4;
							goto IL_0EF8;
						}
					}
					else if (text2 == "right")
					{
						evt.keyCode = KeyCode.RightArrow;
						evt.modifiers |= EventModifiers.FunctionKey;
						goto IL_0EF8;
					}
				}
				else if (num <= 3121933785U)
				{
					if (num <= 3053690476U)
					{
						if (num <= 2235328556U)
						{
							if (num != 2049990550U)
							{
								if (num != 2130866490U)
								{
									if (num == 2235328556U)
									{
										if (text2 == "backspace")
										{
											evt.keyCode = KeyCode.Backspace;
											evt.modifiers |= EventModifiers.FunctionKey;
											goto IL_0EF8;
										}
									}
								}
								else if (text2 == "page down")
								{
									evt.keyCode = KeyCode.PageDown;
									evt.modifiers |= EventModifiers.FunctionKey;
									goto IL_0EF8;
								}
							}
							else if (text2 == "[/]")
							{
								evt.character = '/';
								evt.keyCode = KeyCode.KeypadDivide;
								goto IL_0EF8;
							}
						}
						else if (num != 2246981567U)
						{
							if (num != 2566336076U)
							{
								if (num == 3053690476U)
								{
									if (text2 == "[9]")
									{
										evt.character = '9';
										evt.keyCode = KeyCode.Keypad9;
										goto IL_0EF8;
									}
								}
							}
							else if (text2 == "tab")
							{
								evt.keyCode = KeyCode.Tab;
								goto IL_0EF8;
							}
						}
						else if (text2 == "return")
						{
							evt.character = '\n';
							evt.keyCode = KeyCode.Return;
							evt.modifiers &= ~EventModifiers.FunctionKey;
							goto IL_0EF8;
						}
					}
					else if (num <= 3056941880U)
					{
						if (num != 3055117499U)
						{
							if (num != 3056397427U)
							{
								if (num == 3056941880U)
								{
									if (text2 == "[-]")
									{
										evt.character = '-';
										evt.keyCode = KeyCode.KeypadMinus;
										goto IL_0EF8;
									}
								}
							}
							else if (text2 == "[.]")
							{
								evt.character = '.';
								evt.keyCode = KeyCode.KeypadPeriod;
								goto IL_0EF8;
							}
						}
						else if (text2 == "[6]")
						{
							evt.character = '6';
							evt.keyCode = KeyCode.Keypad6;
							goto IL_0EF8;
						}
					}
					else if (num != 3120653857U)
					{
						if (num != 3121786690U)
						{
							if (num == 3121933785U)
							{
								if (text2 == "[0]")
								{
									evt.character = '0';
									evt.keyCode = KeyCode.Keypad0;
									goto IL_0EF8;
								}
							}
						}
						else if (text2 == "[3]")
						{
							evt.character = '3';
							evt.keyCode = KeyCode.Keypad3;
							goto IL_0EF8;
						}
					}
					else if (text2 == "[8]")
					{
						evt.character = '8';
						evt.keyCode = KeyCode.Keypad8;
						goto IL_0EF8;
					}
				}
				else if (num <= 4197582936U)
				{
					if (num <= 3536372366U)
					{
						if (num != 3122375070U)
						{
							if (num != 3332609576U)
							{
								if (num == 3536372366U)
								{
									if (text2 == "home")
									{
										evt.keyCode = KeyCode.Home;
										evt.modifiers |= EventModifiers.FunctionKey;
										goto IL_0EF8;
									}
								}
							}
							else if (text2 == "insert")
							{
								evt.keyCode = KeyCode.Insert;
								evt.modifiers |= EventModifiers.FunctionKey;
								goto IL_0EF8;
							}
						}
						else if (text2 == "[7]")
						{
							evt.character = '7';
							evt.keyCode = KeyCode.Keypad7;
							goto IL_0EF8;
						}
					}
					else if (num != 3906143141U)
					{
						if (num != 3984432914U)
						{
							if (num == 4197582936U)
							{
								if (text2 == "f10")
								{
									evt.keyCode = KeyCode.F10;
									evt.modifiers |= EventModifiers.FunctionKey;
									goto IL_0EF8;
								}
							}
						}
						else if (text2 == "[esc]")
						{
							evt.keyCode = KeyCode.Escape;
							goto IL_0EF8;
						}
					}
					else if (text2 == "pgup")
					{
						evt.keyCode = KeyCode.PageDown;
						evt.modifiers |= EventModifiers.FunctionKey;
						goto IL_0EF8;
					}
				}
				else if (num <= 4227375619U)
				{
					if (num != 4213014532U)
					{
						if (num != 4214360555U)
						{
							if (num == 4227375619U)
							{
								if (text2 == "[enter]")
								{
									evt.character = '\n';
									evt.keyCode = KeyCode.KeypadEnter;
									goto IL_0EF8;
								}
							}
						}
						else if (text2 == "f11")
						{
							evt.keyCode = KeyCode.F11;
							evt.modifiers |= EventModifiers.FunctionKey;
							goto IL_0EF8;
						}
					}
					else if (text2 == "pgdown")
					{
						evt.keyCode = KeyCode.PageUp;
						evt.modifiers |= EventModifiers.FunctionKey;
						goto IL_0EF8;
					}
				}
				else if (num <= 4247915793U)
				{
					if (num != 4231138174U)
					{
						if (num == 4247915793U)
						{
							if (text2 == "f13")
							{
								evt.keyCode = KeyCode.F13;
								evt.modifiers |= EventModifiers.FunctionKey;
								goto IL_0EF8;
							}
						}
					}
					else if (text2 == "f12")
					{
						evt.keyCode = KeyCode.F12;
						evt.modifiers |= EventModifiers.FunctionKey;
						goto IL_0EF8;
					}
				}
				else if (num != 4264693412U)
				{
					if (num == 4281471031U)
					{
						if (text2 == "f15")
						{
							evt.keyCode = KeyCode.F15;
							evt.modifiers |= EventModifiers.FunctionKey;
							goto IL_0EF8;
						}
					}
				}
				else if (text2 == "f14")
				{
					evt.keyCode = KeyCode.F14;
					evt.modifiers |= EventModifiers.FunctionKey;
					goto IL_0EF8;
				}
				bool flag3 = subStr.Length != 1;
				if (flag3)
				{
					try
					{
						evt.keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), subStr, true);
					}
					catch (ArgumentException)
					{
						Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[] { subStr }));
					}
				}
				else
				{
					evt.character = subStr.ToLower()[0];
					evt.keyCode = (KeyCode)evt.character;
					bool flag4 = evt.modifiers > EventModifiers.None;
					if (flag4)
					{
						evt.character = '\0';
					}
				}
				IL_0EF8:
				@event = evt;
			}
			return @event;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003704 File Offset: 0x00001904
		public override int GetHashCode()
		{
			int hc = 1;
			bool isKey = this.isKey;
			if (isKey)
			{
				hc = (int)((ushort)this.keyCode);
			}
			bool isMouse = this.isMouse;
			if (isMouse)
			{
				hc = this.mousePosition.GetHashCode();
			}
			return (hc * 37) | (int)this.modifiers;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003758 File Offset: 0x00001958
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == obj;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = obj.GetType() != base.GetType();
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						Event rhs = (Event)obj;
						bool flag5 = this.type != rhs.type || (this.modifiers & ~EventModifiers.CapsLock) != (rhs.modifiers & ~EventModifiers.CapsLock);
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bool isKey = this.isKey;
							if (isKey)
							{
								flag2 = this.keyCode == rhs.keyCode;
							}
							else
							{
								bool isMouse = this.isMouse;
								flag2 = isMouse && this.mousePosition == rhs.mousePosition;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000381C File Offset: 0x00001A1C
		public override string ToString()
		{
			bool isKey = this.isKey;
			string text;
			if (isKey)
			{
				bool flag = this.character == '\0';
				if (flag)
				{
					text = UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[] { this.type, this.modifiers, this.keyCode });
				}
				else
				{
					text = string.Concat(new string[]
					{
						"Event:",
						this.type.ToString(),
						"   Character:",
						((int)this.character).ToString(),
						"   Modifiers:",
						this.modifiers.ToString(),
						"   KeyCode:",
						this.keyCode.ToString()
					});
				}
			}
			else
			{
				bool isMouse = this.isMouse;
				if (isMouse)
				{
					text = UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[] { this.type, this.mousePosition, this.modifiers });
				}
				else
				{
					bool flag2 = this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand;
					if (flag2)
					{
						text = UnityString.Format("Event: {0}  \"{1}\"", new object[] { this.type, this.commandName });
					}
					else
					{
						text = this.type.ToString() ?? "";
					}
				}
			}
			return text;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000039C0 File Offset: 0x00001BC0
		public void Use()
		{
			bool flag = this.type == EventType.Repaint || this.type == EventType.Layout;
			if (flag)
			{
				Debug.LogWarning(UnityString.Format("Event.Use() should not be called for events of type {0}", new object[] { this.type }));
			}
			this.Internal_Use();
		}

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventType get_rawType_Injected(IntPtr _unity_self);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePosition_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mousePosition_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_delta_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_delta_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PointerType get_pointerType_Injected(IntPtr _unity_self);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_button_Injected(IntPtr _unity_self);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventModifiers get_modifiers_Injected(IntPtr _unity_self);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_modifiers_Injected(IntPtr _unity_self, EventModifiers value);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pressure_Injected(IntPtr _unity_self);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_twist_Injected(IntPtr _unity_self);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tilt_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PenStatus get_penStatus_Injected(IntPtr _unity_self);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_clickCount_Injected(IntPtr _unity_self);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern char get_character_Injected(IntPtr _unity_self);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_character_Injected(IntPtr _unity_self, char value);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern KeyCode get_Internal_keyCode_Injected(IntPtr _unity_self);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Internal_keyCode_Injected(IntPtr _unity_self, KeyCode value);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_displayIndex_Injected(IntPtr _unity_self);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_displayIndex_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventType get_type_Injected(IntPtr _unity_self);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_type_Injected(IntPtr _unity_self, EventType value);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_commandName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_commandName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Use_Injected(IntPtr _unity_self);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyFromPtr_Injected(IntPtr _unity_self, IntPtr ptr);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PopEvent_Injected(IntPtr outEvent);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetEventAtIndex_Injected(int index, IntPtr outEvent);

		// Token: 0x04000001 RID: 1
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000002 RID: 2
		internal const float scrollWheelDeltaPerTick = 3f;

		// Token: 0x04000003 RID: 3
		internal static bool ignoreGuiDepth;

		// Token: 0x04000004 RID: 4
		private static Event s_Current;

		// Token: 0x04000005 RID: 5
		private static Event s_MasterEvent;

		// Token: 0x02000003 RID: 3
		internal static class BindingsMarshaller
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00003A12 File Offset: 0x00001C12
			public static IntPtr ConvertToNative(Event e)
			{
				return e.m_Ptr;
			}
		}
	}
}
