using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200011D RID: 285
	internal struct PointerModel
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00044E61 File Offset: 0x00043061
		public UIPointerType pointerType
		{
			get
			{
				return this.eventData.pointerType;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00044E6E File Offset: 0x0004306E
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00044E76 File Offset: 0x00043076
		public Vector2 screenPosition
		{
			get
			{
				return this.m_ScreenPosition;
			}
			set
			{
				if (this.m_ScreenPosition != value)
				{
					this.m_ScreenPosition = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00044E94 File Offset: 0x00043094
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00044E9C File Offset: 0x0004309C
		public Vector3 worldPosition
		{
			get
			{
				return this.m_WorldPosition;
			}
			set
			{
				if (this.m_WorldPosition != value)
				{
					this.m_WorldPosition = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00044EBA File Offset: 0x000430BA
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00044EC2 File Offset: 0x000430C2
		public Quaternion worldOrientation
		{
			get
			{
				return this.m_WorldOrientation;
			}
			set
			{
				if (this.m_WorldOrientation != value)
				{
					this.m_WorldOrientation = value;
					this.changedThisFrame = true;
				}
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00044EE0 File Offset: 0x000430E0
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00044EE8 File Offset: 0x000430E8
		public Vector2 scrollDelta
		{
			get
			{
				return this.m_ScrollDelta;
			}
			set
			{
				if (this.m_ScrollDelta != value)
				{
					this.changedThisFrame = true;
					this.m_ScrollDelta = value;
				}
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00044F06 File Offset: 0x00043106
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00044F0E File Offset: 0x0004310E
		public float pressure
		{
			get
			{
				return this.m_Pressure;
			}
			set
			{
				if (this.m_Pressure != value)
				{
					this.changedThisFrame = true;
					this.m_Pressure = value;
				}
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00044F27 File Offset: 0x00043127
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00044F2F File Offset: 0x0004312F
		public float azimuthAngle
		{
			get
			{
				return this.m_AzimuthAngle;
			}
			set
			{
				if (this.m_AzimuthAngle != value)
				{
					this.changedThisFrame = true;
					this.m_AzimuthAngle = value;
				}
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00044F48 File Offset: 0x00043148
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00044F50 File Offset: 0x00043150
		public float altitudeAngle
		{
			get
			{
				return this.m_AltitudeAngle;
			}
			set
			{
				if (this.m_AltitudeAngle != value)
				{
					this.changedThisFrame = true;
					this.m_AltitudeAngle = value;
				}
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00044F69 File Offset: 0x00043169
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00044F71 File Offset: 0x00043171
		public float twist
		{
			get
			{
				return this.m_Twist;
			}
			set
			{
				if (this.m_Twist != value)
				{
					this.changedThisFrame = true;
					this.m_Twist = value;
				}
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00044F8A File Offset: 0x0004318A
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00044F92 File Offset: 0x00043192
		public Vector2 radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				if (this.m_Radius != value)
				{
					this.changedThisFrame = true;
					this.m_Radius = value;
				}
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00044FB0 File Offset: 0x000431B0
		public PointerModel(ExtendedPointerEventData eventData)
		{
			this.eventData = eventData;
			this.changedThisFrame = false;
			this.leftButton = default(PointerModel.ButtonState);
			this.leftButton.OnEndFrame();
			this.rightButton = default(PointerModel.ButtonState);
			this.rightButton.OnEndFrame();
			this.middleButton = default(PointerModel.ButtonState);
			this.middleButton.OnEndFrame();
			this.m_ScreenPosition = default(Vector2);
			this.m_ScrollDelta = default(Vector2);
			this.m_WorldOrientation = default(Quaternion);
			this.m_WorldPosition = default(Vector3);
			this.m_Pressure = 0f;
			this.m_AzimuthAngle = 0f;
			this.m_AltitudeAngle = 0f;
			this.m_Twist = 0f;
			this.m_Radius = default(Vector2);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00045078 File Offset: 0x00043278
		public void OnFrameFinished()
		{
			this.changedThisFrame = false;
			this.scrollDelta = default(Vector2);
			this.leftButton.OnEndFrame();
			this.rightButton.OnEndFrame();
			this.middleButton.OnEndFrame();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000450BC File Offset: 0x000432BC
		public void CopyTouchOrPenStateFrom(PointerEventData eventData)
		{
			this.pressure = eventData.pressure;
			this.azimuthAngle = eventData.azimuthAngle;
			this.altitudeAngle = eventData.altitudeAngle;
			this.twist = eventData.twist;
			this.radius = eventData.radius;
		}

		// Token: 0x04000688 RID: 1672
		public bool changedThisFrame;

		// Token: 0x04000689 RID: 1673
		public PointerModel.ButtonState leftButton;

		// Token: 0x0400068A RID: 1674
		public PointerModel.ButtonState rightButton;

		// Token: 0x0400068B RID: 1675
		public PointerModel.ButtonState middleButton;

		// Token: 0x0400068C RID: 1676
		public ExtendedPointerEventData eventData;

		// Token: 0x0400068D RID: 1677
		private Vector2 m_ScreenPosition;

		// Token: 0x0400068E RID: 1678
		private Vector2 m_ScrollDelta;

		// Token: 0x0400068F RID: 1679
		private Vector3 m_WorldPosition;

		// Token: 0x04000690 RID: 1680
		private Quaternion m_WorldOrientation;

		// Token: 0x04000691 RID: 1681
		private float m_Pressure;

		// Token: 0x04000692 RID: 1682
		private float m_AzimuthAngle;

		// Token: 0x04000693 RID: 1683
		private float m_AltitudeAngle;

		// Token: 0x04000694 RID: 1684
		private float m_Twist;

		// Token: 0x04000695 RID: 1685
		private Vector2 m_Radius;

		// Token: 0x0200011E RID: 286
		public struct ButtonState
		{
			// Token: 0x1700039A RID: 922
			// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000450FA File Offset: 0x000432FA
			// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00045104 File Offset: 0x00043304
			public bool isPressed
			{
				get
				{
					return this.m_IsPressed;
				}
				set
				{
					if (this.m_IsPressed != value)
					{
						this.m_IsPressed = value;
						if (this.m_FramePressState == PointerEventData.FramePressState.NotChanged && value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.Pressed;
							return;
						}
						if (this.m_FramePressState == PointerEventData.FramePressState.NotChanged && !value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.Released;
							return;
						}
						if (this.m_FramePressState == PointerEventData.FramePressState.Pressed && !value)
						{
							this.m_FramePressState = PointerEventData.FramePressState.PressedAndReleased;
						}
					}
				}
			}

			// Token: 0x1700039B RID: 923
			// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0004515C File Offset: 0x0004335C
			// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00045164 File Offset: 0x00043364
			public bool ignoreNextClick
			{
				get
				{
					return this.m_IgnoreNextClick;
				}
				set
				{
					this.m_IgnoreNextClick = value;
				}
			}

			// Token: 0x1700039C RID: 924
			// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0004516D File Offset: 0x0004336D
			// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00045175 File Offset: 0x00043375
			public float pressTime
			{
				get
				{
					return this.m_PressTime;
				}
				set
				{
					this.m_PressTime = value;
				}
			}

			// Token: 0x1700039D RID: 925
			// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0004517E File Offset: 0x0004337E
			// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00045186 File Offset: 0x00043386
			public bool clickedOnSameGameObject
			{
				get
				{
					return this.m_ClickedOnSameGameObject;
				}
				set
				{
					this.m_ClickedOnSameGameObject = value;
				}
			}

			// Token: 0x1700039E RID: 926
			// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0004518F File Offset: 0x0004338F
			public bool wasPressedThisFrame
			{
				get
				{
					return this.m_FramePressState == PointerEventData.FramePressState.Pressed || this.m_FramePressState == PointerEventData.FramePressState.PressedAndReleased;
				}
			}

			// Token: 0x1700039F RID: 927
			// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x000451A4 File Offset: 0x000433A4
			public bool wasReleasedThisFrame
			{
				get
				{
					return this.m_FramePressState == PointerEventData.FramePressState.Released || this.m_FramePressState == PointerEventData.FramePressState.PressedAndReleased;
				}
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x000451BC File Offset: 0x000433BC
			public void CopyPressStateTo(PointerEventData eventData)
			{
				eventData.pointerPressRaycast = this.m_PressRaycast;
				eventData.pressPosition = this.m_PressPosition;
				eventData.clickCount = this.m_ClickCount;
				eventData.clickTime = this.m_ClickTime;
				eventData.pointerPress = this.m_LastPressObject;
				eventData.pointerPress = this.m_PressObject;
				eventData.rawPointerPress = this.m_RawPressObject;
				eventData.pointerDrag = this.m_DragObject;
				eventData.dragging = this.m_Dragging;
				if (this.ignoreNextClick)
				{
					eventData.eligibleForClick = false;
				}
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x00045244 File Offset: 0x00043444
			public void CopyPressStateFrom(PointerEventData eventData)
			{
				this.m_PressRaycast = eventData.pointerPressRaycast;
				this.m_PressObject = eventData.pointerPress;
				this.m_RawPressObject = eventData.rawPointerPress;
				this.m_LastPressObject = eventData.lastPress;
				this.m_PressPosition = eventData.pressPosition;
				this.m_ClickTime = eventData.clickTime;
				this.m_ClickCount = eventData.clickCount;
				this.m_DragObject = eventData.pointerDrag;
				this.m_Dragging = eventData.dragging;
			}

			// Token: 0x06000DBC RID: 3516 RVA: 0x000452BD File Offset: 0x000434BD
			public void OnEndFrame()
			{
				this.m_FramePressState = PointerEventData.FramePressState.NotChanged;
			}

			// Token: 0x04000696 RID: 1686
			private bool m_IsPressed;

			// Token: 0x04000697 RID: 1687
			private PointerEventData.FramePressState m_FramePressState;

			// Token: 0x04000698 RID: 1688
			private float m_PressTime;

			// Token: 0x04000699 RID: 1689
			private RaycastResult m_PressRaycast;

			// Token: 0x0400069A RID: 1690
			private GameObject m_PressObject;

			// Token: 0x0400069B RID: 1691
			private GameObject m_RawPressObject;

			// Token: 0x0400069C RID: 1692
			private GameObject m_LastPressObject;

			// Token: 0x0400069D RID: 1693
			private GameObject m_DragObject;

			// Token: 0x0400069E RID: 1694
			private Vector2 m_PressPosition;

			// Token: 0x0400069F RID: 1695
			private float m_ClickTime;

			// Token: 0x040006A0 RID: 1696
			private int m_ClickCount;

			// Token: 0x040006A1 RID: 1697
			private bool m_Dragging;

			// Token: 0x040006A2 RID: 1698
			private bool m_ClickedOnSameGameObject;

			// Token: 0x040006A3 RID: 1699
			private bool m_IgnoreNextClick;
		}
	}
}
