using System;
using MDPro3.Servant;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001409 RID: 5129
	[RequireComponent(typeof(Scrollbar))]
	public class ScrollWithGamepad : MonoBehaviour
	{
		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06009470 RID: 38000 RVA: 0x00152BF8 File Offset: 0x00150DF8
		private Scrollbar Scrollbar
		{
			get
			{
				return this.m_Scrollbar = ((this.m_Scrollbar != null) ? this.m_Scrollbar : base.GetComponent<Scrollbar>());
			}
		}

		// Token: 0x06009471 RID: 38001 RVA: 0x00152C2C File Offset: 0x00150E2C
		private void Update()
		{
			if (this.Scrollbar == null)
			{
				return;
			}
			if (this.parentServant == null || !this.parentServant.NeedResponseInput())
			{
				return;
			}
			Vector2 offsets = ((this.responseScrollWheel == ScrollWithGamepad.ResponseScrollWheel.Left) ? UserInput.LeftScrollWheel : UserInput.RightScrollWheel);
			float offset = ((this.direction == ScrollWithGamepad.Direction.Vertical) ? offsets.y : offsets.x);
			this.Scrollbar.value = Mathf.Clamp01(this.Scrollbar.value + offset * this.speed * Time.unscaledDeltaTime);
		}

		// Token: 0x0400D2AB RID: 53931
		[SerializeField]
		private ScrollWithGamepad.ResponseScrollWheel responseScrollWheel;

		// Token: 0x0400D2AC RID: 53932
		[SerializeField]
		private ScrollWithGamepad.Direction direction;

		// Token: 0x0400D2AD RID: 53933
		[SerializeField]
		private float speed = 2f;

		// Token: 0x0400D2AE RID: 53934
		private Scrollbar m_Scrollbar;

		// Token: 0x0400D2AF RID: 53935
		[SerializeField]
		private Servant parentServant;

		// Token: 0x0200140A RID: 5130
		private enum ResponseScrollWheel
		{
			// Token: 0x0400D2B1 RID: 53937
			Left,
			// Token: 0x0400D2B2 RID: 53938
			Right
		}

		// Token: 0x0200140B RID: 5131
		private enum Direction
		{
			// Token: 0x0400D2B4 RID: 53940
			Vertical,
			// Token: 0x0400D2B5 RID: 53941
			Horizontal
		}
	}
}
