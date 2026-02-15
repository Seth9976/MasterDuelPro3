using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001EB RID: 491
	public class InvertVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x00054DB4 File Offset: 0x00052FB4
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			if (this.invertX)
			{
				value.x *= -1f;
			}
			if (this.invertY)
			{
				value.y *= -1f;
			}
			if (this.invertZ)
			{
				value.z *= -1f;
			}
			return value;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00054E0A File Offset: 0x0005300A
		public override string ToString()
		{
			return string.Format("InvertVector3(invertX={0},invertY={1},invertZ={2})", this.invertX, this.invertY, this.invertZ);
		}

		// Token: 0x04000AD2 RID: 2770
		public bool invertX = true;

		// Token: 0x04000AD3 RID: 2771
		public bool invertY = true;

		// Token: 0x04000AD4 RID: 2772
		public bool invertZ = true;
	}
}
