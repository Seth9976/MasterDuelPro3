using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000271 RID: 625
	[DisplayStringFormat("{up}/{left}/{down}/{right}")]
	[DisplayName("Up/Down/Left/Right Composite")]
	public class Vector2Composite : InputBindingComposite<Vector2>
	{
		// Token: 0x06001694 RID: 5780 RVA: 0x00065978 File Offset: 0x00063B78
		public override Vector2 ReadValue(ref InputBindingCompositeContext context)
		{
			Vector2Composite.Mode mode = this.mode;
			if (mode == Vector2Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(this.up);
				float downValue = context.ReadValue<float>(this.down);
				float leftValue = context.ReadValue<float>(this.left);
				float rightValue = context.ReadValue<float>(this.right);
				return DpadControl.MakeDpadVector(num, downValue, leftValue, rightValue);
			}
			bool flag = context.ReadValueAsButton(this.up);
			bool downIsPressed = context.ReadValueAsButton(this.down);
			bool leftIsPressed = context.ReadValueAsButton(this.left);
			bool rightIsPressed = context.ReadValueAsButton(this.right);
			if (!this.normalize)
			{
				mode = Vector2Composite.Mode.Digital;
			}
			return DpadControl.MakeDpadVector(flag, downIsPressed, leftIsPressed, rightIsPressed, mode == Vector2Composite.Mode.DigitalNormalized);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00065A1C File Offset: 0x00063C1C
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context).magnitude;
		}

		// Token: 0x04000CE2 RID: 3298
		[InputControl(layout = "Axis")]
		public int up;

		// Token: 0x04000CE3 RID: 3299
		[InputControl(layout = "Axis")]
		public int down;

		// Token: 0x04000CE4 RID: 3300
		[InputControl(layout = "Axis")]
		public int left;

		// Token: 0x04000CE5 RID: 3301
		[InputControl(layout = "Axis")]
		public int right;

		// Token: 0x04000CE6 RID: 3302
		[Obsolete("Use Mode.DigitalNormalized with 'mode' instead")]
		public bool normalize = true;

		// Token: 0x04000CE7 RID: 3303
		public Vector2Composite.Mode mode;

		// Token: 0x02000272 RID: 626
		public enum Mode
		{
			// Token: 0x04000CE9 RID: 3305
			Analog = 2,
			// Token: 0x04000CEA RID: 3306
			DigitalNormalized = 0,
			// Token: 0x04000CEB RID: 3307
			Digital
		}
	}
}
