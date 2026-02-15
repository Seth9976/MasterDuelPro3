using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001EA RID: 490
	public class InvertVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x00054D49 File Offset: 0x00052F49
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			if (this.invertX)
			{
				value.x *= -1f;
			}
			if (this.invertY)
			{
				value.y *= -1f;
			}
			return value;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00054D7C File Offset: 0x00052F7C
		public override string ToString()
		{
			return string.Format("InvertVector2(invertX={0},invertY={1})", this.invertX, this.invertY);
		}

		// Token: 0x04000AD0 RID: 2768
		public bool invertX = true;

		// Token: 0x04000AD1 RID: 2769
		public bool invertY = true;
	}
}
