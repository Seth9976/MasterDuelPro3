using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001E7 RID: 487
	[DesignTimeVisible(false)]
	internal class CompensateDirectionProcessor : InputProcessor<Vector3>
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x00054BF0 File Offset: 0x00052DF0
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			if (!InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			Quaternion rotation = Quaternion.identity;
			switch (InputRuntime.s_Instance.screenOrientation)
			{
			case ScreenOrientation.PortraitUpsideDown:
				rotation = Quaternion.Euler(0f, 0f, 180f);
				break;
			case ScreenOrientation.LandscapeLeft:
				rotation = Quaternion.Euler(0f, 0f, 90f);
				break;
			case ScreenOrientation.LandscapeRight:
				rotation = Quaternion.Euler(0f, 0f, 270f);
				break;
			}
			return rotation * value;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00054C7C File Offset: 0x00052E7C
		public override string ToString()
		{
			return "CompensateDirection()";
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00034CA5 File Offset: 0x00032EA5
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
