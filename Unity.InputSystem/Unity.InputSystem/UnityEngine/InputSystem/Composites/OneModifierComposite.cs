using System;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200026F RID: 623
	[DisplayStringFormat("{modifier}+{binding}")]
	[DisplayName("Binding With One Modifier")]
	public class OneModifierComposite : InputBindingComposite
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0006568F File Offset: 0x0006388F
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00065697 File Offset: 0x00063897
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0006569F File Offset: 0x0006389F
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000656BC File Offset: 0x000638BC
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifierIsPressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000656E4 File Offset: 0x000638E4
		private bool ModifierIsPressed(ref InputBindingCompositeContext context)
		{
			bool modifierDown = context.ReadValueAsButton(this.modifier);
			if (modifierDown && this.m_BindingIsButton && !this.overrideModifiersNeedToBePressedFirst)
			{
				double timestamp = context.GetPressTime(this.binding);
				return context.GetPressTime(this.modifier) <= timestamp;
			}
			return modifierDown;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00065732 File Offset: 0x00063932
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0006576D File Offset: 0x0006396D
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0006578C File Offset: 0x0006398C
		internal static void DetermineValueTypeAndSize(ref InputBindingCompositeContext context, int part, out Type valueType, out int valueSizeInBytes, out bool isButton)
		{
			valueSizeInBytes = 0;
			isButton = true;
			Type type = null;
			foreach (InputBindingCompositeContext.PartBinding control in context.controls)
			{
				if (control.part == part)
				{
					Type controlType = control.control.valueType;
					if (type == null || controlType.IsAssignableFrom(type))
					{
						type = controlType;
					}
					else if (!type.IsAssignableFrom(controlType))
					{
						type = typeof(Object);
					}
					valueSizeInBytes = Math.Max(control.control.valueSizeInBytes, valueSizeInBytes);
					isButton &= control.control.isButton;
				}
			}
			valueType = type;
		}

		// Token: 0x04000CD5 RID: 3285
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x04000CD6 RID: 3286
		[InputControl]
		public int binding;

		// Token: 0x04000CD7 RID: 3287
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x04000CD8 RID: 3288
		private int m_ValueSizeInBytes;

		// Token: 0x04000CD9 RID: 3289
		private Type m_ValueType;

		// Token: 0x04000CDA RID: 3290
		private bool m_BindingIsButton;
	}
}
