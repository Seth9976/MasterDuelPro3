using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001EF RID: 495
	public class ScaleProcessor : InputProcessor<float>
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x00054F3F File Offset: 0x0005313F
		public override float Process(float value, InputControl control)
		{
			return value * this.factor;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00054F49 File Offset: 0x00053149
		public override string ToString()
		{
			return string.Format("Scale(factor={0})", this.factor);
		}

		// Token: 0x04000AD8 RID: 2776
		[Tooltip("Scale factor to multiply incoming float values by.")]
		public float factor = 1f;
	}
}
