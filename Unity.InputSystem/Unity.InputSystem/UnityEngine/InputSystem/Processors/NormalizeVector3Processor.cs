using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001EE RID: 494
	public class NormalizeVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x00054F2F File Offset: 0x0005312F
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00054F38 File Offset: 0x00053138
		public override string ToString()
		{
			return "NormalizeVector3()";
		}
	}
}
