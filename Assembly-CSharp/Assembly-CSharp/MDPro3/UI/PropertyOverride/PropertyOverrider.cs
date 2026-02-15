using System;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3.UI.PropertyOverride
{
	// Token: 0x02001447 RID: 5191
	public abstract class PropertyOverrider : MonoBehaviour
	{
		// Token: 0x0600969D RID: 38557 RVA: 0x0015D6D0 File Offset: 0x0015B8D0
		public static bool NeedMobileLayout()
		{
			float mode = Config.GetFloat("Layout", 0f);
			if (mode == 0f)
			{
				return DeviceInfo.OnMobile();
			}
			return mode != 1f;
		}

		// Token: 0x0600969E RID: 38558 RVA: 0x0015D706 File Offset: 0x0015B906
		protected virtual void Awake()
		{
			this.Override();
		}

		// Token: 0x0600969F RID: 38559 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void DefaultOverride()
		{
		}

		// Token: 0x060096A0 RID: 38560 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void MobileOverride()
		{
		}

		// Token: 0x060096A1 RID: 38561 RVA: 0x0015D70E File Offset: 0x0015B90E
		public virtual void Override()
		{
			if (this.decideByUser)
			{
				if (PropertyOverrider.NeedMobileLayout())
				{
					this.MobileOverride();
					return;
				}
				this.DefaultOverride();
				return;
			}
			else
			{
				if (DeviceInfo.OnMobile())
				{
					this.MobileOverride();
					return;
				}
				this.DefaultOverride();
				return;
			}
		}

		// Token: 0x0400D4EA RID: 54506
		[SerializeField]
		protected bool decideByUser = true;
	}
}
