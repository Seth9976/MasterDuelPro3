using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001ED RID: 493
	public class NormalizeVector2Processor : InputProcessor<Vector2>
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x00054F17 File Offset: 0x00053117
		public override Vector2 Process(Vector2 value, InputControl control)
		{
			return value.normalized;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00054F20 File Offset: 0x00053120
		public override string ToString()
		{
			return "NormalizeVector2()";
		}
	}
}
