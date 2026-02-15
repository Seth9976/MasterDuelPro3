using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200026D RID: 621
	[DesignTimeVisible(false)]
	[DisplayStringFormat("{modifier}+{button}")]
	public class ButtonWithOneModifier : InputBindingComposite<float>
	{
		// Token: 0x06001679 RID: 5753 RVA: 0x00065557 File Offset: 0x00063757
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00065574 File Offset: 0x00063774
		private bool ModifierIsPressed(ref InputBindingCompositeContext context)
		{
			bool modifierDown = context.ReadValueAsButton(this.modifier);
			if (modifierDown && !this.overrideModifiersNeedToBePressedFirst)
			{
				double timestamp = context.GetPressTime(this.button);
				return context.GetPressTime(this.modifier) <= timestamp;
			}
			return modifierDown;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000655BA File Offset: 0x000637BA
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000655C3 File Offset: 0x000637C3
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x04000CCE RID: 3278
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x04000CCF RID: 3279
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x04000CD0 RID: 3280
		public bool overrideModifiersNeedToBePressedFirst;
	}
}
