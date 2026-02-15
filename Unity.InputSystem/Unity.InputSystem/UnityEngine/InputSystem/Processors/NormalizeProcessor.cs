using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x020001EC RID: 492
	public class NormalizeProcessor : InputProcessor<float>
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x00054E54 File Offset: 0x00053054
		public override float Process(float value, InputControl control)
		{
			return NormalizeProcessor.Normalize(value, this.min, this.max, this.zero);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00054E70 File Offset: 0x00053070
		public static float Normalize(float value, float min, float max, float zero)
		{
			if (zero < min)
			{
				zero = min;
			}
			if (Mathf.Approximately(value, min))
			{
				if (min < zero)
				{
					return -1f;
				}
				return 0f;
			}
			else
			{
				float percentage = (value - min) / (max - min);
				if (min < zero)
				{
					return 2f * percentage - 1f;
				}
				return percentage;
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00054EB8 File Offset: 0x000530B8
		internal static float Denormalize(float value, float min, float max, float zero)
		{
			if (zero < min)
			{
				zero = min;
			}
			if (min >= zero)
			{
				return min + (max - min) * value;
			}
			if (value < 0f)
			{
				return min + (zero - min) * (value * -1f);
			}
			return zero + (max - zero) * value;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00054EEA File Offset: 0x000530EA
		public override string ToString()
		{
			return string.Format("Normalize(min={0},max={1},zero={2})", this.min, this.max, this.zero);
		}

		// Token: 0x04000AD5 RID: 2773
		public float min;

		// Token: 0x04000AD6 RID: 2774
		public float max;

		// Token: 0x04000AD7 RID: 2775
		public float zero;
	}
}
