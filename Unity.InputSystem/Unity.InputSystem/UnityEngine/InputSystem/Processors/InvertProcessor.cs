using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001E9 RID: 489
	public class InvertProcessor : InputProcessor<float>
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x00054D39 File Offset: 0x00052F39
		public override float Process(float value, InputControl control)
		{
			return value * -1f;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00054D42 File Offset: 0x00052F42
		public override string ToString()
		{
			return "Invert()";
		}
	}
}
