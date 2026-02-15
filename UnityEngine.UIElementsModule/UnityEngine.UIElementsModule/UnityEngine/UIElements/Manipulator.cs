using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000271 RID: 625
	public abstract class Manipulator : IManipulator
	{
		// Token: 0x060010E3 RID: 4323
		protected abstract void RegisterCallbacksOnTarget();

		// Token: 0x060010E4 RID: 4324
		protected abstract void UnregisterCallbacksFromTarget();

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00048C5C File Offset: 0x00046E5C
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00048C74 File Offset: 0x00046E74
		public VisualElement target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnregisterCallbacksFromTarget();
				}
				this.m_Target = value;
				bool flag2 = this.target != null;
				if (flag2)
				{
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x0400098A RID: 2442
		private VisualElement m_Target;
	}
}
