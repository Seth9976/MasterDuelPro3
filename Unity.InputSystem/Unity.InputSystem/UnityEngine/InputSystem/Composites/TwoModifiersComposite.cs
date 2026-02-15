using System;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000270 RID: 624
	[DisplayStringFormat("{modifier1}+{modifier2}+{binding}")]
	[DisplayName("Binding With Two Modifiers")]
	public class TwoModifiersComposite : InputBindingComposite
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00065848 File Offset: 0x00063A48
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00065850 File Offset: 0x00063A50
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00065858 File Offset: 0x00063A58
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00065875 File Offset: 0x00063A75
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifiersArePressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0006589C File Offset: 0x00063A9C
		private bool ModifiersArePressed(ref InputBindingCompositeContext context)
		{
			bool modifiersDown = context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2);
			if (modifiersDown && this.m_BindingIsButton && !this.overrideModifiersNeedToBePressedFirst)
			{
				double timestamp = context.GetPressTime(this.binding);
				double pressTime = context.GetPressTime(this.modifier1);
				double timestamp2 = context.GetPressTime(this.modifier2);
				return pressTime <= timestamp && timestamp2 <= timestamp;
			}
			return modifiersDown;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0006590E File Offset: 0x00063B0E
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00065949 File Offset: 0x00063B49
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x04000CDB RID: 3291
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x04000CDC RID: 3292
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x04000CDD RID: 3293
		[InputControl]
		public int binding;

		// Token: 0x04000CDE RID: 3294
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x04000CDF RID: 3295
		private int m_ValueSizeInBytes;

		// Token: 0x04000CE0 RID: 3296
		private Type m_ValueType;

		// Token: 0x04000CE1 RID: 3297
		private bool m_BindingIsButton;
	}
}
