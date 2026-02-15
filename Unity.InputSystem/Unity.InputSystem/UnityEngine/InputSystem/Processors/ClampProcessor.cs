using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001E6 RID: 486
	public class ClampProcessor : InputProcessor<float>
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x00054BB7 File Offset: 0x00052DB7
		public override float Process(float value, InputControl control)
		{
			return Mathf.Clamp(value, this.min, this.max);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00054BCB File Offset: 0x00052DCB
		public override string ToString()
		{
			return string.Format("Clamp(min={0},max={1})", this.min, this.max);
		}

		// Token: 0x04000ACE RID: 2766
		public float min;

		// Token: 0x04000ACF RID: 2767
		public float max;
	}
}
