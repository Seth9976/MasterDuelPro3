using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001E8 RID: 488
	[DesignTimeVisible(false)]
	internal class CompensateRotationProcessor : InputProcessor<Quaternion>
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x00054C8C File Offset: 0x00052E8C
		public override Quaternion Process(Quaternion value, InputControl control)
		{
			if (!InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			Quaternion q = Quaternion.identity;
			switch (InputRuntime.s_Instance.screenOrientation)
			{
			case ScreenOrientation.PortraitUpsideDown:
				q = new Quaternion(0f, 0f, 1f, 0f);
				break;
			case ScreenOrientation.LandscapeLeft:
				q = new Quaternion(0f, 0f, 0.70710677f, -0.70710677f);
				break;
			case ScreenOrientation.LandscapeRight:
				q = new Quaternion(0f, 0f, -0.70710677f, -0.70710677f);
				break;
			}
			return value * q;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00054D2A File Offset: 0x00052F2A
		public override string ToString()
		{
			return "CompensateRotation()";
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00034CA5 File Offset: 0x00032EA5
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
