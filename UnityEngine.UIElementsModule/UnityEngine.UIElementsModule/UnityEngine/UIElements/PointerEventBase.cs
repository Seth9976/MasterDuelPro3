using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputForUI;

namespace UnityEngine.UIElements
{
	// Token: 0x02000220 RID: 544
	[EventCategory(EventCategory.Pointer)]
	public abstract class PointerEventBase<T> : EventBase<T>, IPointerEvent, IPointerEventInternal, IPointerOrMouseEvent where T : PointerEventBase<T>, new()
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00041056 File Offset: 0x0003F256
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x0004105E File Offset: 0x0003F25E
		public int pointerId { get; protected set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00041067 File Offset: 0x0003F267
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x0004106F File Offset: 0x0003F26F
		public string pointerType { get; protected set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00041078 File Offset: 0x0003F278
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00041080 File Offset: 0x0003F280
		public bool isPrimary { get; protected set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00041089 File Offset: 0x0003F289
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00041091 File Offset: 0x0003F291
		public int button { get; protected set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0004109A File Offset: 0x0003F29A
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x000410A2 File Offset: 0x0003F2A2
		public int pressedButtons { get; protected set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000410AB File Offset: 0x0003F2AB
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x000410B3 File Offset: 0x0003F2B3
		public Vector3 position { get; protected set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x000410BC File Offset: 0x0003F2BC
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x000410C4 File Offset: 0x0003F2C4
		public Vector3 localPosition { get; protected set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x000410CD File Offset: 0x0003F2CD
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x000410D5 File Offset: 0x0003F2D5
		public Vector3 deltaPosition { get; protected set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x000410DE File Offset: 0x0003F2DE
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x000410E6 File Offset: 0x0003F2E6
		public float deltaTime { get; protected set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x000410EF File Offset: 0x0003F2EF
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x000410F7 File Offset: 0x0003F2F7
		public int clickCount { get; protected set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00041100 File Offset: 0x0003F300
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00041108 File Offset: 0x0003F308
		public float pressure { get; protected set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00041111 File Offset: 0x0003F311
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00041119 File Offset: 0x0003F319
		public float tangentialPressure { get; protected set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00041124 File Offset: 0x0003F324
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00041160 File Offset: 0x0003F360
		public float altitudeAngle
		{
			get
			{
				bool altitudeNeedsConversion = this.m_AltitudeNeedsConversion;
				if (altitudeNeedsConversion)
				{
					this.m_AltitudeAngle = PointerEventBase<T>.TiltToAltitude(this.tilt);
					this.m_AltitudeNeedsConversion = false;
				}
				return this.m_AltitudeAngle;
			}
			protected set
			{
				this.m_AltitudeNeedsConversion = true;
				this.m_AltitudeAngle = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00041174 File Offset: 0x0003F374
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x000411B0 File Offset: 0x0003F3B0
		public float azimuthAngle
		{
			get
			{
				bool azimuthNeedsConversion = this.m_AzimuthNeedsConversion;
				if (azimuthNeedsConversion)
				{
					this.m_AzimuthAngle = PointerEventBase<T>.TiltToAzimuth(this.tilt);
					this.m_AzimuthNeedsConversion = false;
				}
				return this.m_AzimuthAngle;
			}
			protected set
			{
				this.m_AzimuthNeedsConversion = true;
				this.m_AzimuthAngle = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x000411C1 File Offset: 0x0003F3C1
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x000411C9 File Offset: 0x0003F3C9
		public float twist { get; protected set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x000411D4 File Offset: 0x0003F3D4
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0004123B File Offset: 0x0003F43B
		public Vector2 tilt
		{
			get
			{
				bool flag = Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer && this.pointerType == PointerType.touch && this.m_TiltNeeded;
				if (flag)
				{
					this.m_Tilt = PointerEventBase<T>.AzimuthAndAlitutudeToTilt(this.m_AltitudeAngle, this.m_AzimuthAngle);
					this.m_TiltNeeded = false;
				}
				return this.m_Tilt;
			}
			protected set
			{
				this.m_TiltNeeded = true;
				this.m_Tilt = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0004124C File Offset: 0x0003F44C
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00041254 File Offset: 0x0003F454
		public PenStatus penStatus { get; protected set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0004125D File Offset: 0x0003F45D
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00041265 File Offset: 0x0003F465
		public Vector2 radius { get; protected set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0004126E File Offset: 0x0003F46E
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00041276 File Offset: 0x0003F476
		public Vector2 radiusVariance { get; protected set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0004127F File Offset: 0x0003F47F
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x00041287 File Offset: 0x0003F487
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00041290 File Offset: 0x0003F490
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000412B0 File Offset: 0x0003F4B0
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x000412D0 File Offset: 0x0003F4D0
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000412F0 File Offset: 0x0003F4F0
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00041310 File Offset: 0x0003F510
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00041349 File Offset: 0x0003F549
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00041351 File Offset: 0x0003F551
		bool IPointerEventInternal.triggeredByOS { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0004135A File Offset: 0x0003F55A
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00041362 File Offset: 0x0003F562
		IMouseEvent IPointerEventInternal.compatibilityMouseEvent { get; set; }

		// Token: 0x170002D2 RID: 722
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0004136B File Offset: 0x0003F56B
		int IPointerEventInternal.displayIndex
		{
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IPointerEventInternal.displayIndex>k__BackingField = value;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00041374 File Offset: 0x0003F574
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00041388 File Offset: 0x0003F588
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.pointerId = 0;
			this.pointerType = PointerType.unknown;
			this.isPrimary = false;
			this.button = -1;
			this.pressedButtons = 0;
			this.position = Vector3.zero;
			this.localPosition = Vector3.zero;
			this.deltaPosition = Vector3.zero;
			this.deltaTime = 0f;
			this.clickCount = 0;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.tilt = new Vector2(0f, 0f);
			this.twist = 0f;
			this.penStatus = PenStatus.None;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
			this.modifiers = EventModifiers.None;
			((IPointerEventInternal)this).triggeredByOS = false;
			bool flag = ((IPointerEventInternal)this).compatibilityMouseEvent != null;
			if (flag)
			{
				((IDisposable)((IPointerEventInternal)this).compatibilityMouseEvent).Dispose();
				((IPointerEventInternal)this).compatibilityMouseEvent = null;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000414AC File Offset: 0x0003F6AC
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x000414C4 File Offset: 0x0003F6C4
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement element = this.currentTarget as VisualElement;
				bool flag = element != null;
				if (flag)
				{
					this.localPosition = element.WorldToLocal(this.position);
				}
				else
				{
					this.localPosition = this.position;
				}
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00041520 File Offset: 0x0003F720
		private static bool IsMouse(Event systemEvent)
		{
			EventType t = systemEvent.rawType;
			return t == EventType.MouseMove || t == EventType.MouseDown || t == EventType.MouseUp || t == EventType.MouseDrag || t == EventType.ContextClick || t == EventType.MouseEnterWindow || t == EventType.MouseLeaveWindow;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0004155C File Offset: 0x0003F75C
		private static bool IsTouch(Event systemEvent)
		{
			EventType t = systemEvent.rawType;
			return t == EventType.TouchMove || t == EventType.TouchDown || t == EventType.TouchUp || t == EventType.TouchStationary || t == EventType.TouchEnter || t == EventType.TouchLeave;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00041598 File Offset: 0x0003F798
		private static float TiltToAzimuth(Vector2 tilt)
		{
			float azimuth = 0f;
			bool flag = tilt.x != 0f;
			if (flag)
			{
				azimuth = 1.5707964f - Mathf.Atan2(-Mathf.Cos(tilt.x) * Mathf.Sin(tilt.y), Mathf.Cos(tilt.y) * Mathf.Sin(tilt.x));
				bool flag2 = azimuth < 0f;
				if (flag2)
				{
					azimuth += 6.2831855f;
				}
				bool flag3 = azimuth >= 1.5707964f;
				if (flag3)
				{
					azimuth -= 1.5707964f;
				}
				else
				{
					azimuth += 4.712389f;
				}
			}
			return azimuth;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0004163C File Offset: 0x0003F83C
		private static Vector2 AzimuthAndAlitutudeToTilt(float altitude, float azimuth)
		{
			return new Vector2(0f, 0f)
			{
				x = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Cos(altitude) / Mathf.Sin(azimuth)),
				y = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Sin(altitude) / Mathf.Sin(azimuth))
			};
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000416A0 File Offset: 0x0003F8A0
		private static float TiltToAltitude(Vector2 tilt)
		{
			return 1.5707964f - Mathf.Acos(Mathf.Cos(tilt.x) * Mathf.Cos(tilt.y));
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x000416D4 File Offset: 0x0003F8D4
		public static T GetPooled(Event systemEvent)
		{
			T e = EventBase<T>.GetPooled();
			bool flag = !PointerEventBase<T>.IsMouse(systemEvent) && !PointerEventBase<T>.IsTouch(systemEvent) && systemEvent.rawType != EventType.DragUpdated;
			if (flag)
			{
				Debug.Assert(false, string.Concat(new string[]
				{
					"Unexpected event type: ",
					systemEvent.rawType.ToString(),
					" (",
					systemEvent.type.ToString(),
					")"
				}));
			}
			PointerType pointerType = systemEvent.pointerType;
			PointerType pointerType2 = pointerType;
			if (pointerType2 != PointerType.Touch)
			{
				if (pointerType2 != PointerType.Pen)
				{
					e.pointerType = PointerType.mouse;
					e.pointerId = PointerId.mousePointerId;
				}
				else
				{
					e.pointerType = PointerType.pen;
					e.pointerId = PointerId.penPointerIdBase;
					bool flag2 = systemEvent.penStatus == PenStatus.Barrel;
					if (flag2)
					{
						PointerDeviceState.PressButton(e.pointerId, 1);
					}
					else
					{
						PointerDeviceState.ReleaseButton(e.pointerId, 1);
					}
					bool flag3 = systemEvent.penStatus == PenStatus.Eraser;
					if (flag3)
					{
						PointerDeviceState.PressButton(e.pointerId, 5);
					}
					else
					{
						PointerDeviceState.ReleaseButton(e.pointerId, 5);
					}
				}
			}
			else
			{
				e.pointerType = PointerType.touch;
				e.pointerId = PointerId.touchPointerIdBase;
			}
			e.isPrimary = true;
			e.altitudeAngle = 0f;
			e.azimuthAngle = 0f;
			e.radius = Vector2.zero;
			e.radiusVariance = Vector2.zero;
			e.imguiEvent = systemEvent;
			bool flag4 = systemEvent.rawType == EventType.MouseDown || systemEvent.rawType == EventType.TouchDown;
			if (flag4)
			{
				PointerDeviceState.PressButton(e.pointerId, systemEvent.button);
				e.button = systemEvent.button;
			}
			else
			{
				bool flag5 = systemEvent.rawType == EventType.MouseUp || systemEvent.rawType == EventType.TouchUp;
				if (flag5)
				{
					PointerDeviceState.ReleaseButton(e.pointerId, systemEvent.button);
					e.button = systemEvent.button;
				}
				else
				{
					bool flag6 = systemEvent.rawType == EventType.MouseMove || systemEvent.rawType == EventType.TouchMove;
					if (flag6)
					{
						e.button = -1;
					}
				}
			}
			e.pressedButtons = PointerDeviceState.GetPressedButtons(e.pointerId);
			e.position = systemEvent.mousePosition;
			e.localPosition = systemEvent.mousePosition;
			e.deltaPosition = systemEvent.delta;
			e.clickCount = systemEvent.clickCount;
			e.modifiers = systemEvent.modifiers;
			e.tilt = systemEvent.tilt;
			e.penStatus = systemEvent.penStatus;
			e.twist = systemEvent.twist;
			PointerType pointerType3 = systemEvent.pointerType;
			PointerType pointerType4 = pointerType3;
			if (pointerType4 != PointerType.Touch)
			{
				if (pointerType4 != PointerType.Pen)
				{
					e.pressure = ((e.pressedButtons == 0) ? 0f : 0.5f);
				}
				else
				{
					e.pressure = systemEvent.pressure;
				}
			}
			else
			{
				e.pressure = systemEvent.pressure;
			}
			e.tangentialPressure = 0f;
			e.triggeredByOS = true;
			return e;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00041ABC File Offset: 0x0003FCBC
		internal static T GetPooled(EventType eventType, Vector3 mousePosition, Vector2 delta, int button, int clickCount, EventModifiers modifiers, int displayIndex)
		{
			T e = EventBase<T>.GetPooled();
			e.pointerId = PointerId.mousePointerId;
			e.pointerType = PointerType.mouse;
			e.isPrimary = true;
			e.displayIndex = displayIndex;
			bool flag = eventType == EventType.MouseDown;
			if (flag)
			{
				PointerDeviceState.PressButton(e.pointerId, button);
				e.button = button;
			}
			else
			{
				bool flag2 = eventType == EventType.MouseUp;
				if (flag2)
				{
					PointerDeviceState.ReleaseButton(e.pointerId, button);
					e.button = button;
				}
				else
				{
					e.button = -1;
				}
			}
			e.pressedButtons = PointerDeviceState.GetPressedButtons(e.pointerId);
			e.position = mousePosition;
			e.localPosition = mousePosition;
			e.deltaPosition = delta;
			e.clickCount = clickCount;
			e.modifiers = modifiers;
			e.pressure = ((e.pressedButtons == 0) ? 0f : 0.5f);
			e.triggeredByOS = true;
			return e;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00041C10 File Offset: 0x0003FE10
		internal static T GetPooled(Touch touch, EventModifiers modifiers, int displayIndex)
		{
			T e = EventBase<T>.GetPooled();
			e.pointerId = touch.fingerId + PointerId.touchPointerIdBase;
			e.pointerType = PointerType.touch;
			e.displayIndex = displayIndex;
			bool otherTouchDown = false;
			for (int i = PointerId.touchPointerIdBase; i < PointerId.touchPointerIdBase + PointerId.touchPointerCount; i++)
			{
				bool flag = i != e.pointerId && PointerDeviceState.GetPressedButtons(i) != 0;
				if (flag)
				{
					otherTouchDown = true;
					break;
				}
			}
			e.isPrimary = !otherTouchDown;
			bool flag2 = touch.phase == TouchPhase.Began;
			if (flag2)
			{
				PointerDeviceState.PressButton(e.pointerId, 0);
				e.button = 0;
			}
			else
			{
				bool flag3 = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				if (flag3)
				{
					PointerDeviceState.ReleaseButton(e.pointerId, 0);
					e.button = 0;
				}
				else
				{
					e.button = -1;
				}
			}
			e.pressedButtons = PointerDeviceState.GetPressedButtons(e.pointerId);
			e.position = touch.position;
			e.localPosition = touch.position;
			e.deltaPosition = touch.deltaPosition;
			e.deltaTime = touch.deltaTime;
			e.clickCount = touch.tapCount;
			e.pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > 1E-30f) ? (touch.pressure / touch.maximumPossiblePressure) : 1f);
			e.tangentialPressure = 0f;
			e.altitudeAngle = touch.altitudeAngle;
			e.azimuthAngle = touch.azimuthAngle;
			e.twist = 0f;
			e.tilt = new Vector2(0f, 0f);
			e.penStatus = PenStatus.None;
			e.radius = new Vector2(touch.radius, touch.radius);
			e.radiusVariance = new Vector2(touch.radiusVariance, touch.radiusVariance);
			e.modifiers = modifiers;
			e.triggeredByOS = true;
			return e;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00041EC4 File Offset: 0x000400C4
		internal static T GetPooled(PenData pen, EventModifiers modifiers, int displayIndex)
		{
			T e = EventBase<T>.GetPooled();
			e.pointerId = PointerId.penPointerIdBase;
			e.pointerType = PointerType.pen;
			e.displayIndex = displayIndex;
			e.isPrimary = true;
			bool flag = pen.contactType == PenEventType.PenDown;
			if (flag)
			{
				PointerDeviceState.PressButton(e.pointerId, 0);
				e.button = 0;
			}
			else
			{
				bool flag2 = pen.contactType == PenEventType.PenUp;
				if (flag2)
				{
					PointerDeviceState.ReleaseButton(e.pointerId, 0);
					e.button = 0;
				}
				else
				{
					e.button = -1;
				}
			}
			bool flag3 = pen.penStatus == PenStatus.Barrel;
			if (flag3)
			{
				PointerDeviceState.PressButton(e.pointerId, 1);
			}
			else
			{
				PointerDeviceState.ReleaseButton(e.pointerId, 1);
			}
			bool flag4 = pen.penStatus == PenStatus.Eraser;
			if (flag4)
			{
				PointerDeviceState.PressButton(e.pointerId, 5);
			}
			else
			{
				PointerDeviceState.ReleaseButton(e.pointerId, 5);
			}
			e.pressedButtons = PointerDeviceState.GetPressedButtons(e.pointerId);
			e.position = pen.position;
			e.localPosition = pen.position;
			e.deltaPosition = pen.deltaPos;
			e.clickCount = 0;
			e.pressure = pen.pressure;
			e.tangentialPressure = 0f;
			e.twist = pen.twist;
			e.tilt = pen.tilt;
			e.penStatus = pen.penStatus;
			e.radius = Vector2.zero;
			e.radiusVariance = Vector2.zero;
			e.modifiers = modifiers;
			e.triggeredByOS = true;
			return e;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000420F8 File Offset: 0x000402F8
		internal static T GetPooled(PointerEvent pointerEvent, Vector2 position, Vector2 deltaPosition, int pointerId, float deltaTime)
		{
			T e = EventBase<T>.GetPooled();
			e.position = position;
			e.localPosition = position;
			e.deltaPosition = deltaPosition;
			e.pointerId = pointerId;
			e.deltaTime = deltaTime;
			e.displayIndex = pointerEvent.displayIndex;
			e.isPrimary = pointerEvent.isPrimaryPointer;
			e.button = -1;
			bool flag = pointerEvent.eventSource == EventSource.Mouse;
			if (flag)
			{
				e.pointerType = PointerType.mouse;
				Debug.Assert(pointerEvent.isPrimaryPointer, "PointerEvent from Mouse source is expected to be a primary pointer.");
				Debug.Assert(pointerId == PointerId.mousePointerId, "PointerEvent from Mouse source is expected to have mouse pointer id.");
				bool flag2 = pointerEvent.button == PointerEvent.Button.Primary;
				if (flag2)
				{
					e.button = 0;
				}
				else
				{
					bool flag3 = pointerEvent.button == PointerEvent.Button.PenEraserInTouch;
					if (flag3)
					{
						e.button = 1;
					}
					else
					{
						bool flag4 = pointerEvent.button == PointerEvent.Button.PenBarrelButton;
						if (flag4)
						{
							e.button = 2;
						}
					}
				}
			}
			else
			{
				bool flag5 = pointerEvent.eventSource == EventSource.Touch;
				if (flag5)
				{
					e.pointerType = PointerType.touch;
					Debug.Assert(e.pointerId >= PointerId.touchPointerIdBase && e.pointerId < PointerId.touchPointerIdBase + PointerId.touchPointerCount, "PointerEvent from Touch source is expected to have touch-based pointer id.");
					bool flag6 = pointerEvent.button == PointerEvent.Button.Primary;
					if (flag6)
					{
						e.button = 0;
					}
				}
				else
				{
					bool flag7 = pointerEvent.eventSource == EventSource.Pen;
					if (!flag7)
					{
						throw new ArgumentOutOfRangeException("pointerEvent", "Unsupported EventSource for pointer event");
					}
					e.pointerType = PointerType.pen;
					Debug.Assert(e.pointerId >= PointerId.penPointerIdBase && e.pointerId < PointerId.penPointerIdBase + PointerId.penPointerCount, "PointerEvent from Pen source is expected to have pen-based pointer id.");
					bool flag8 = pointerEvent.button == PointerEvent.Button.Primary;
					if (flag8)
					{
						e.button = 0;
					}
					else
					{
						bool flag9 = pointerEvent.button == PointerEvent.Button.PenBarrelButton;
						if (flag9)
						{
							e.button = 1;
						}
						else
						{
							bool flag10 = pointerEvent.button == PointerEvent.Button.PenEraserInTouch;
							if (flag10)
							{
								e.button = 5;
							}
						}
					}
				}
			}
			bool flag11 = pointerEvent.type == PointerEvent.Type.ButtonPressed;
			if (flag11)
			{
				Debug.Assert(e.button != -1, "PointerEvent of type ButtonPressed is expected to have button != -1.");
				PointerDeviceState.PressButton(e.pointerId, e.button);
			}
			else
			{
				bool flag12 = pointerEvent.type == PointerEvent.Type.ButtonReleased;
				if (flag12)
				{
					Debug.Assert(e.button != -1, "PointerEvent of type ButtonReleased is expected to have button != -1.");
					PointerDeviceState.ReleaseButton(e.pointerId, e.button);
				}
				else
				{
					bool flag13 = pointerEvent.type != PointerEvent.Type.TouchCanceled;
					if (flag13)
					{
						Debug.Assert(e.button == -1, "PointerEvent of type other than ButtonPressed, ButtonReleased, or TouchCanceled is expected to have button set to none.");
					}
				}
			}
			e.pressedButtons = PointerDeviceState.GetPressedButtons(e.pointerId);
			bool flag14 = pointerEvent.eventSource == EventSource.Pen;
			if (flag14)
			{
				e.penStatus = PenStatus.None;
				bool flag15 = (e.pressedButtons & 1) != 0;
				if (flag15)
				{
					ref T ptr = ref e;
					ptr.penStatus |= PenStatus.Contact;
				}
				bool flag16 = (e.pressedButtons & 2) != 0;
				if (flag16)
				{
					ref T ptr = ref e;
					ptr.penStatus |= PenStatus.Barrel;
				}
				bool flag17 = (e.pressedButtons & 32) != 0;
				if (flag17)
				{
					ref T ptr = ref e;
					ptr.penStatus |= PenStatus.Eraser;
				}
				bool isInverted = pointerEvent.isInverted;
				if (isInverted)
				{
					ref T ptr = ref e;
					ptr.penStatus |= PenStatus.Inverted;
				}
			}
			e.clickCount = pointerEvent.clickCount;
			e.pressure = pointerEvent.pressure;
			e.altitudeAngle = pointerEvent.altitude;
			e.azimuthAngle = pointerEvent.azimuth;
			e.twist = pointerEvent.twist;
			e.tilt = pointerEvent.tilt;
			EventModifiers modifiers = EventModifiers.None;
			bool isShiftPressed = pointerEvent.eventModifiers.isShiftPressed;
			if (isShiftPressed)
			{
				modifiers |= EventModifiers.Shift;
			}
			bool isCtrlPressed = pointerEvent.eventModifiers.isCtrlPressed;
			if (isCtrlPressed)
			{
				modifiers |= EventModifiers.Control;
			}
			bool isAltPressed = pointerEvent.eventModifiers.isAltPressed;
			if (isAltPressed)
			{
				modifiers |= EventModifiers.Alt;
			}
			bool isMetaPressed = pointerEvent.eventModifiers.isMetaPressed;
			if (isMetaPressed)
			{
				modifiers |= EventModifiers.Command;
			}
			e.modifiers = modifiers;
			e.triggeredByOS = true;
			return e;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00042654 File Offset: 0x00040854
		internal static T GetPooled(IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = PointerEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T e = EventBase<T>.GetPooled();
				e.position = position;
				e.localPosition = position;
				e.pointerId = pointerId;
				e.pointerType = PointerType.GetPointerType(pointerId);
				t = e;
			}
			return t;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000426C4 File Offset: 0x000408C4
		public static T GetPooled(IPointerEvent triggerEvent)
		{
			T e = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				e.pointerId = triggerEvent.pointerId;
				e.pointerType = triggerEvent.pointerType;
				e.isPrimary = triggerEvent.isPrimary;
				e.button = triggerEvent.button;
				e.pressedButtons = triggerEvent.pressedButtons;
				e.position = triggerEvent.position;
				e.localPosition = triggerEvent.localPosition;
				e.deltaPosition = triggerEvent.deltaPosition;
				e.deltaTime = triggerEvent.deltaTime;
				e.clickCount = triggerEvent.clickCount;
				e.pressure = triggerEvent.pressure;
				e.tangentialPressure = triggerEvent.tangentialPressure;
				e.altitudeAngle = triggerEvent.altitudeAngle;
				e.azimuthAngle = triggerEvent.azimuthAngle;
				e.twist = triggerEvent.twist;
				e.tilt = triggerEvent.tilt;
				e.penStatus = triggerEvent.penStatus;
				e.radius = triggerEvent.radius;
				e.radiusVariance = triggerEvent.radiusVariance;
				e.modifiers = triggerEvent.modifiers;
				IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
				bool flag2 = pointerEventInternal != null;
				if (flag2)
				{
					e.triggeredByOS |= pointerEventInternal.triggeredByOS;
				}
			}
			return e;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00042880 File Offset: 0x00040A80
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(this.pointerId, this.position, panel, panel.contextType);
			}
			EventBase eventBase = (EventBase)((IPointerEventInternal)this).compatibilityMouseEvent;
			if (eventBase != null)
			{
				eventBase.PreDispatch(panel);
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000428D8 File Offset: 0x00040AD8
		protected internal override void PostDispatch(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				panel.ProcessPointerCapture(i);
			}
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			EventBase eventBase = (EventBase)((IPointerEventInternal)this).compatibilityMouseEvent;
			if (eventBase != null)
			{
				eventBase.PostDispatch(panel);
			}
			panel.dispatcher.m_ClickDetector.ProcessEvent<T>(this);
			base.PostDispatch(panel);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00042954 File Offset: 0x00040B54
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToCapturingElementOrElementUnderPointer(this, panel, this.pointerId, this.position);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00042970 File Offset: 0x00040B70
		protected PointerEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x04000898 RID: 2200
		private const float k_DefaultButtonPressure = 0.5f;

		// Token: 0x04000899 RID: 2201
		private bool m_AltitudeNeedsConversion = true;

		// Token: 0x0400089A RID: 2202
		private bool m_AzimuthNeedsConversion = true;

		// Token: 0x0400089B RID: 2203
		private float m_AltitudeAngle = 0f;

		// Token: 0x0400089C RID: 2204
		private float m_AzimuthAngle = 0f;

		// Token: 0x0400089D RID: 2205
		private bool m_TiltNeeded = true;

		// Token: 0x0400089E RID: 2206
		private Vector2 m_Tilt = new Vector2(0f, 0f);
	}
}
