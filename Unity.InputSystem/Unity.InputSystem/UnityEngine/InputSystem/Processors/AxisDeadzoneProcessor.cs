using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001E5 RID: 485
	public class AxisDeadzoneProcessor : InputProcessor<float>
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00054B07 File Offset: 0x00052D07
		private float minOrDefault
		{
			get
			{
				if (this.min != 0f)
				{
					return this.min;
				}
				return InputSystem.settings.defaultDeadzoneMin;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00054B27 File Offset: 0x00052D27
		private float maxOrDefault
		{
			get
			{
				if (this.max != 0f)
				{
					return this.max;
				}
				return InputSystem.settings.defaultDeadzoneMax;
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00054B48 File Offset: 0x00052D48
		public override float Process(float value, InputControl control = null)
		{
			float min = this.minOrDefault;
			float max = this.maxOrDefault;
			float absValue = Mathf.Abs(value);
			if (absValue < min)
			{
				return 0f;
			}
			if (absValue > max)
			{
				return Mathf.Sign(value);
			}
			return Mathf.Sign(value) * ((absValue - min) / (max - min));
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00054B8D File Offset: 0x00052D8D
		public override string ToString()
		{
			return string.Format("AxisDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x04000ACC RID: 2764
		public float min;

		// Token: 0x04000ACD RID: 2765
		public float max;
	}
}
