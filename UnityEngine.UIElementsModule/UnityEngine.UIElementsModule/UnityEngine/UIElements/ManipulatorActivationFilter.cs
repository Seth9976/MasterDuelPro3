using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026F RID: 623
	public struct ManipulatorActivationFilter : IEquatable<ManipulatorActivationFilter>
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00048A16 File Offset: 0x00046C16
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00048A1E File Offset: 0x00046C1E
		public MouseButton button { readonly get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00048A27 File Offset: 0x00046C27
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00048A2F File Offset: 0x00046C2F
		public EventModifiers modifiers { readonly get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00048A38 File Offset: 0x00046C38
		public readonly int clickCount { get; }

		// Token: 0x060010DC RID: 4316 RVA: 0x00048A40 File Offset: 0x00046C40
		public override bool Equals(object obj)
		{
			return obj is ManipulatorActivationFilter && this.Equals((ManipulatorActivationFilter)obj);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00048A6C File Offset: 0x00046C6C
		public bool Equals(ManipulatorActivationFilter other)
		{
			return this.button == other.button && this.modifiers == other.modifiers && this.clickCount == other.clickCount;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00048AB0 File Offset: 0x00046CB0
		public override int GetHashCode()
		{
			int hashCode = 390957112;
			hashCode = hashCode * -1521134295 + this.button.GetHashCode();
			hashCode = hashCode * -1521134295 + this.modifiers.GetHashCode();
			return hashCode * -1521134295 + this.clickCount.GetHashCode();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00048B1C File Offset: 0x00046D1C
		public bool Matches(IPointerEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool minClickCount = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && minClickCount;
			}
			return flag2;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00048B74 File Offset: 0x00046D74
		private bool HasModifiers(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00048BB0 File Offset: 0x00046DB0
		private bool MatchModifiers(bool alt, bool ctrl, bool shift, bool command)
		{
			bool flag = ((this.modifiers & EventModifiers.Alt) != EventModifiers.None && !alt) || ((this.modifiers & EventModifiers.Alt) == EventModifiers.None && alt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = ((this.modifiers & EventModifiers.Control) != EventModifiers.None && !ctrl) || ((this.modifiers & EventModifiers.Control) == EventModifiers.None && ctrl);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = ((this.modifiers & EventModifiers.Shift) != EventModifiers.None && !shift) || ((this.modifiers & EventModifiers.Shift) == EventModifiers.None && shift);
					flag2 = !flag4 && ((this.modifiers & EventModifiers.Command) == EventModifiers.None || command) && ((this.modifiers & EventModifiers.Command) != EventModifiers.None || !command);
				}
			}
			return flag2;
		}
	}
}
