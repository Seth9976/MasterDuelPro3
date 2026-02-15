using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000B9 RID: 185
	public class BaseInput : UIBehaviour
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001A21F File Offset: 0x0001841F
		public virtual string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001A226 File Offset: 0x00018426
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001A22D File Offset: 0x0001842D
		public virtual IMECompositionMode imeCompositionMode
		{
			get
			{
				return Input.imeCompositionMode;
			}
			set
			{
				Input.imeCompositionMode = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001A235 File Offset: 0x00018435
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001A23C File Offset: 0x0001843C
		public virtual Vector2 compositionCursorPos
		{
			get
			{
				return Input.compositionCursorPos;
			}
			set
			{
				Input.compositionCursorPos = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001A244 File Offset: 0x00018444
		public virtual bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001A24B File Offset: 0x0001844B
		public virtual bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001A253 File Offset: 0x00018453
		public virtual bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001A25B File Offset: 0x0001845B
		public virtual bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001A263 File Offset: 0x00018463
		public virtual Vector2 mousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001A26F File Offset: 0x0001846F
		public virtual Vector2 mouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001A276 File Offset: 0x00018476
		public virtual float mouseScrollDeltaPerTick
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001A27D File Offset: 0x0001847D
		public virtual bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001A284 File Offset: 0x00018484
		public virtual int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001A28B File Offset: 0x0001848B
		public virtual Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001A293 File Offset: 0x00018493
		public virtual float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001A29B File Offset: 0x0001849B
		public virtual bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}
	}
}
