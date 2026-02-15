using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001F2 RID: 498
	public class StickDeadzoneProcessor : InputProcessor<Vector2>
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00055058 File Offset: 0x00053258
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

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00055078 File Offset: 0x00053278
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

		// Token: 0x0600123A RID: 4666 RVA: 0x00055098 File Offset: 0x00053298
		public override Vector2 Process(Vector2 value, InputControl control = null)
		{
			float magnitude = value.magnitude;
			float newMagnitude = this.GetDeadZoneAdjustedValue(magnitude);
			if (newMagnitude == 0f)
			{
				value = Vector2.zero;
			}
			else
			{
				value *= newMagnitude / magnitude;
			}
			return value;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000550D4 File Offset: 0x000532D4
		private float GetDeadZoneAdjustedValue(float value)
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

		// Token: 0x0600123C RID: 4668 RVA: 0x00055119 File Offset: 0x00053319
		public override string ToString()
		{
			return string.Format("StickDeadzone(min={0},max={1})", this.minOrDefault, this.maxOrDefault);
		}

		// Token: 0x04000ADE RID: 2782
		public float min;

		// Token: 0x04000ADF RID: 2783
		public float max;
	}
}
