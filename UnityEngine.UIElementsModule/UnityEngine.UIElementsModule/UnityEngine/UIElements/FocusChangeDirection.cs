using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000249 RID: 585
	public class FocusChangeDirection : IDisposable
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00043BC0 File Offset: 0x00041DC0
		public static FocusChangeDirection unspecified { get; } = new FocusChangeDirection(-1);

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00043BC7 File Offset: 0x00041DC7
		public static FocusChangeDirection none { get; } = new FocusChangeDirection(0);

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00043BCE File Offset: 0x00041DCE
		protected static FocusChangeDirection lastValue { get; } = FocusChangeDirection.none;

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00043BD5 File Offset: 0x00041DD5
		protected FocusChangeDirection(int value)
		{
			this.m_Value = value;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00043BE8 File Offset: 0x00041DE8
		public static implicit operator int(FocusChangeDirection fcd)
		{
			return (fcd != null) ? fcd.m_Value : 0;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00043C06 File Offset: 0x00041E06
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000020EA File Offset: 0x000002EA
		protected virtual void Dispose()
		{
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00043C0F File Offset: 0x00041E0F
		internal virtual void ApplyTo(FocusController focusController, Focusable f)
		{
			focusController.SwitchFocus(f, this, false, DispatchMode.Default);
		}

		// Token: 0x040008DF RID: 2271
		private readonly int m_Value;
	}
}
