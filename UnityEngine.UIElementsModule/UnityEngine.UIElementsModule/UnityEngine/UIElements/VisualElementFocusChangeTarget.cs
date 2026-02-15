using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DD RID: 1245
	internal class VisualElementFocusChangeTarget : FocusChangeDirection
	{
		// Token: 0x060022F3 RID: 8947 RVA: 0x000805B8 File Offset: 0x0007E7B8
		public static VisualElementFocusChangeTarget GetPooled(Focusable target)
		{
			VisualElementFocusChangeTarget r = VisualElementFocusChangeTarget.Pool.Get();
			r.target = target;
			return r;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000805DE File Offset: 0x0007E7DE
		protected override void Dispose()
		{
			this.target = null;
			VisualElementFocusChangeTarget.Pool.Release(this);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000805F5 File Offset: 0x0007E7F5
		internal override void ApplyTo(FocusController focusController, Focusable f)
		{
			focusController.selectedTextElement = null;
			f.Focus();
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00080607 File Offset: 0x0007E807
		public VisualElementFocusChangeTarget()
			: base(FocusChangeDirection.unspecified)
		{
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x0008061B File Offset: 0x0007E81B
		// (set) Token: 0x060022F8 RID: 8952 RVA: 0x00080623 File Offset: 0x0007E823
		public Focusable target { get; private set; }

		// Token: 0x04000FD7 RID: 4055
		private static readonly ObjectPool<VisualElementFocusChangeTarget> Pool = new ObjectPool<VisualElementFocusChangeTarget>(() => new VisualElementFocusChangeTarget(), 100);
	}
}
