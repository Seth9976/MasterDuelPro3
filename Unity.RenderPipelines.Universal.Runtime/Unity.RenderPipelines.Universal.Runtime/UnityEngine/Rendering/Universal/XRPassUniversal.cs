using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001EB RID: 491
	internal class XRPassUniversal : XRPass
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x000391E7 File Offset: 0x000373E7
		public static XRPass Create(XRPassCreateInfo createInfo)
		{
			XRPassUniversal xrpassUniversal = GenericPool<XRPassUniversal>.Get();
			xrpassUniversal.InitBase(createInfo);
			xrpassUniversal.isLateLatchEnabled = false;
			xrpassUniversal.canMarkLateLatch = false;
			xrpassUniversal.hasMarkedLateLatch = false;
			xrpassUniversal.canFoveateIntermediatePasses = true;
			return xrpassUniversal;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00039211 File Offset: 0x00037411
		public override void Release()
		{
			GenericPool<XRPassUniversal>.Release(this);
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00039219 File Offset: 0x00037419
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00039221 File Offset: 0x00037421
		internal bool isLateLatchEnabled { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0003922A File Offset: 0x0003742A
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00039232 File Offset: 0x00037432
		internal bool canMarkLateLatch { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0003923B File Offset: 0x0003743B
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00039243 File Offset: 0x00037443
		internal bool hasMarkedLateLatch { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0003924C File Offset: 0x0003744C
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00039254 File Offset: 0x00037454
		internal bool canFoveateIntermediatePasses { get; set; }
	}
}
