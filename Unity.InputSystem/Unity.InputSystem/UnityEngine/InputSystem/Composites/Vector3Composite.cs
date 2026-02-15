using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000273 RID: 627
	[DisplayStringFormat("{up}+{down}/{left}+{right}/{forward}+{backward}")]
	[DisplayName("Up/Down/Left/Right/Forward/Backward Composite")]
	public class Vector3Composite : InputBindingComposite<Vector3>
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x00065A48 File Offset: 0x00063C48
		public override Vector3 ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.mode == Vector3Composite.Mode.Analog)
			{
				float upValue = context.ReadValue<float>(this.up);
				float downValue = context.ReadValue<float>(this.down);
				float leftValue = context.ReadValue<float>(this.left);
				float num = context.ReadValue<float>(this.right);
				float forwardValue = context.ReadValue<float>(this.forward);
				float backwardValue = context.ReadValue<float>(this.backward);
				return new Vector3(num - leftValue, upValue - downValue, forwardValue - backwardValue);
			}
			float upValue2 = (context.ReadValueAsButton(this.up) ? 1f : 0f);
			float downValue2 = (context.ReadValueAsButton(this.down) ? (-1f) : 0f);
			float leftValue2 = (context.ReadValueAsButton(this.left) ? (-1f) : 0f);
			float rightValue = (context.ReadValueAsButton(this.right) ? 1f : 0f);
			float forwardValue2 = (context.ReadValueAsButton(this.forward) ? 1f : 0f);
			float backwardValue2 = (context.ReadValueAsButton(this.backward) ? (-1f) : 0f);
			Vector3 vector = new Vector3(leftValue2 + rightValue, upValue2 + downValue2, forwardValue2 + backwardValue2);
			if (this.mode == Vector3Composite.Mode.DigitalNormalized)
			{
				vector = vector.normalized;
			}
			return vector;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00065B8C File Offset: 0x00063D8C
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context).magnitude;
		}

		// Token: 0x04000CEC RID: 3308
		[InputControl(layout = "Axis")]
		public int up;

		// Token: 0x04000CED RID: 3309
		[InputControl(layout = "Axis")]
		public int down;

		// Token: 0x04000CEE RID: 3310
		[InputControl(layout = "Axis")]
		public int left;

		// Token: 0x04000CEF RID: 3311
		[InputControl(layout = "Axis")]
		public int right;

		// Token: 0x04000CF0 RID: 3312
		[InputControl(layout = "Axis")]
		public int forward;

		// Token: 0x04000CF1 RID: 3313
		[InputControl(layout = "Axis")]
		public int backward;

		// Token: 0x04000CF2 RID: 3314
		public Vector3Composite.Mode mode;

		// Token: 0x02000274 RID: 628
		public enum Mode
		{
			// Token: 0x04000CF4 RID: 3316
			Analog,
			// Token: 0x04000CF5 RID: 3317
			DigitalNormalized,
			// Token: 0x04000CF6 RID: 3318
			Digital
		}
	}
}
