using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200026B RID: 619
	[DisplayStringFormat("{negative}/{positive}")]
	[DisplayName("Positive/Negative Binding")]
	public class AxisComposite : InputBindingComposite<float>
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00065415 File Offset: 0x00063615
		public float midPoint
		{
			get
			{
				return (this.maxValue + this.minValue) / 2f;
			}
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0006542C File Offset: 0x0006362C
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			float negativeValue = Mathf.Abs(context.ReadValue<float>(this.negative));
			float positiveValue = Mathf.Abs(context.ReadValue<float>(this.positive));
			bool negativeIsActuated = negativeValue > Mathf.Epsilon;
			bool positiveIsActuated = positiveValue > Mathf.Epsilon;
			if (negativeIsActuated == positiveIsActuated)
			{
				switch (this.whichSideWins)
				{
				case AxisComposite.WhichSideWins.Neither:
					return this.midPoint;
				case AxisComposite.WhichSideWins.Positive:
					negativeIsActuated = false;
					break;
				}
			}
			float mid = this.midPoint;
			if (negativeIsActuated)
			{
				return mid - (mid - this.minValue) * negativeValue;
			}
			return mid + (this.maxValue - mid) * positiveValue;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000654C8 File Offset: 0x000636C8
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			float value = this.ReadValue(ref context);
			if (value < this.midPoint)
			{
				value = Mathf.Abs(value - this.midPoint);
				return NormalizeProcessor.Normalize(value, 0f, Mathf.Abs(this.minValue), 0f);
			}
			value = Mathf.Abs(value - this.midPoint);
			return NormalizeProcessor.Normalize(value, 0f, Mathf.Abs(this.maxValue), 0f);
		}

		// Token: 0x04000CC5 RID: 3269
		[InputControl(layout = "Axis")]
		public int negative;

		// Token: 0x04000CC6 RID: 3270
		[InputControl(layout = "Axis")]
		public int positive;

		// Token: 0x04000CC7 RID: 3271
		[Tooltip("Value to return when the negative side is fully actuated.")]
		public float minValue = -1f;

		// Token: 0x04000CC8 RID: 3272
		[Tooltip("Value to return when the positive side is fully actuated.")]
		public float maxValue = 1f;

		// Token: 0x04000CC9 RID: 3273
		[Tooltip("If both the positive and negative side are actuated, decides what value to return. 'Neither' (default) means that the resulting value is the midpoint between min and max. 'Positive' means that max will be returned. 'Negative' means that min will be returned.")]
		public AxisComposite.WhichSideWins whichSideWins;

		// Token: 0x0200026C RID: 620
		public enum WhichSideWins
		{
			// Token: 0x04000CCB RID: 3275
			Neither,
			// Token: 0x04000CCC RID: 3276
			Positive,
			// Token: 0x04000CCD RID: 3277
			Negative
		}
	}
}
