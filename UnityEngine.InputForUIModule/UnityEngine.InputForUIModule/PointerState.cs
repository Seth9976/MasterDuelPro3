using System;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200002C RID: 44
	internal struct PointerState
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005034 File Offset: 0x00003234
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000503C File Offset: 0x0000323C
		public PointerEvent.Button LastPressedButton { readonly get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005045 File Offset: 0x00003245
		public PointerEvent.ButtonsState ButtonsState
		{
			get
			{
				return this._buttonsState;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000504D File Offset: 0x0000324D
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00005055 File Offset: 0x00003255
		public DiscreteTime NextPressTime { readonly get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000505E File Offset: 0x0000325E
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00005066 File Offset: 0x00003266
		public int ClickCount { readonly get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000506F File Offset: 0x0000326F
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00005077 File Offset: 0x00003277
		public Vector2 LastPosition { readonly get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005080 File Offset: 0x00003280
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00005088 File Offset: 0x00003288
		public int LastDisplayIndex { readonly get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005091 File Offset: 0x00003291
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005099 File Offset: 0x00003299
		public bool LastPositionValid { readonly get; set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x000050A4 File Offset: 0x000032A4
		public void OnButtonDown(DiscreteTime currentTime, PointerEvent.Button button)
		{
			bool flag = this.LastPressedButton != button || currentTime >= this.NextPressTime;
			if (flag)
			{
				this.ClickCount = 0;
			}
			this.LastPressedButton = button;
			this._buttonsState.Set(button, true);
			int clickCount = this.ClickCount;
			this.ClickCount = clickCount + 1;
			this.NextPressTime = currentTime + PointerState.kClickDelay;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005110 File Offset: 0x00003310
		public void OnButtonUp(DiscreteTime currentTime, PointerEvent.Button button)
		{
			bool flag = this.LastPressedButton != button;
			if (flag)
			{
				this.ClickCount = 1;
			}
			this._buttonsState.Set(button, false);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005144 File Offset: 0x00003344
		public void OnButtonChange(DiscreteTime currentTime, PointerEvent.Button button, bool previousState, bool newState)
		{
			bool flag = newState && !previousState;
			if (flag)
			{
				this.OnButtonDown(currentTime, button);
			}
			else
			{
				bool flag2 = !newState && previousState;
				if (flag2)
				{
					this.OnButtonUp(currentTime, button);
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005180 File Offset: 0x00003380
		public void OnMove(DiscreteTime currentTime, Vector2 position, int displayIndex)
		{
			this.LastPosition = position;
			this.LastDisplayIndex = displayIndex;
			this.LastPositionValid = true;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000519C File Offset: 0x0000339C
		public void Reset()
		{
			this.LastPressedButton = PointerEvent.Button.None;
			this.ButtonsState.Reset();
			this.NextPressTime = DiscreteTime.Zero;
			this.ClickCount = 0;
			this.LastPosition = Vector2.zero;
			this.LastDisplayIndex = 0;
			this.LastPositionValid = false;
		}

		// Token: 0x040000D5 RID: 213
		private PointerEvent.ButtonsState _buttonsState;

		// Token: 0x040000DB RID: 219
		private static readonly DiscreteTime kClickDelay = new DiscreteTime((double)Event.GetDoubleClickTime() / 1000.0);
	}
}
