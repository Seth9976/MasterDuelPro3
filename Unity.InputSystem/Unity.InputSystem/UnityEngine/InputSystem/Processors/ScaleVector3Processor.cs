using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001F1 RID: 497
	public class ScaleVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06001235 RID: 4661 RVA: 0x00054FD4 File Offset: 0x000531D4
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return new Vector3(value.x * this.x, value.y * this.y, value.z * this.z);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00055002 File Offset: 0x00053202
		public override string ToString()
		{
			return string.Format("ScaleVector3(x={0},y={1},z={2})", this.x, this.y, this.z);
		}

		// Token: 0x04000ADB RID: 2779
		[Tooltip("Scale factor to multiply the incoming Vector3's X component by.")]
		public float x = 1f;

		// Token: 0x04000ADC RID: 2780
		[Tooltip("Scale factor to multiply the incoming Vector3's Y component by.")]
		public float y = 1f;

		// Token: 0x04000ADD RID: 2781
		[Tooltip("Scale factor to multiply the incoming Vector3's Z component by.")]
		public float z = 1f;
	}
}
