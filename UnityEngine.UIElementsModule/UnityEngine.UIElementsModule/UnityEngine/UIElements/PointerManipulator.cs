using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000295 RID: 661
	public abstract class PointerManipulator : MouseManipulator
	{
		// Token: 0x060011E2 RID: 4578 RVA: 0x0004AC8C File Offset: 0x00048E8C
		protected bool CanStartManipulation(IPointerEvent e)
		{
			foreach (ManipulatorActivationFilter activator in base.activators)
			{
				bool flag = activator.Matches(e);
				if (flag)
				{
					this.m_CurrentPointerId = e.pointerId;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004AD00 File Offset: 0x00048F00
		protected bool CanStopManipulation(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && e.pointerId == this.m_CurrentPointerId;
		}

		// Token: 0x04000A4D RID: 2637
		private int m_CurrentPointerId;
	}
}
