using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200026E RID: 622
	[DesignTimeVisible(false)]
	[DisplayStringFormat("{modifier1}+{modifier2}+{button}")]
	public class ButtonWithTwoModifiers : InputBindingComposite<float>
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x000655E8 File Offset: 0x000637E8
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00065608 File Offset: 0x00063808
		private bool ModifiersArePressed(ref InputBindingCompositeContext context)
		{
			bool modifiersDown = context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2);
			if (modifiersDown && !this.overrideModifiersNeedToBePressedFirst)
			{
				double timestamp = context.GetPressTime(this.button);
				double pressTime = context.GetPressTime(this.modifier1);
				double timestamp2 = context.GetPressTime(this.modifier2);
				return pressTime <= timestamp && timestamp2 <= timestamp;
			}
			return modifiersDown;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000655BA File Offset: 0x000637BA
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00065672 File Offset: 0x00063872
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x04000CD1 RID: 3281
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x04000CD2 RID: 3282
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x04000CD3 RID: 3283
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x04000CD4 RID: 3284
		public bool overrideModifiersNeedToBePressedFirst;
	}
}
