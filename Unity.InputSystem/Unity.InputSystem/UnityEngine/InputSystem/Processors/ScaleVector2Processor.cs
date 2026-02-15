using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001F0 RID: 496
	public class ScaleVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x00054F73 File Offset: 0x00053173
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return new Vector2(value.x * this.x, value.y * this.y);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00054F94 File Offset: 0x00053194
		public override string ToString()
		{
			return string.Format("ScaleVector2(x={0},y={1})", this.x, this.y);
		}

		// Token: 0x04000AD9 RID: 2777
		[Tooltip("Scale factor to multiply the incoming Vector2's X component by.")]
		public float x = 1f;

		// Token: 0x04000ADA RID: 2778
		[Tooltip("Scale factor to multiply the incoming Vector2's Y component by.")]
		public float y = 1f;
	}
}
