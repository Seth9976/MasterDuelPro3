using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200021B RID: 539
	public class DpadControl : Vector2Control
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0005B983 File Offset: 0x00059B83
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0005B98B File Offset: 0x00059B8B
		[InputControl(name = "x", layout = "DpadAxis", useStateFrom = "right", synthetic = true)]
		[InputControl(name = "y", layout = "DpadAxis", useStateFrom = "up", synthetic = true)]
		[InputControl(bit = 0U, displayName = "Up")]
		public ButtonControl up { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0005B994 File Offset: 0x00059B94
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0005B99C File Offset: 0x00059B9C
		[InputControl(bit = 1U, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0005B9A5 File Offset: 0x00059BA5
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0005B9AD File Offset: 0x00059BAD
		[InputControl(bit = 2U, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005B9B6 File Offset: 0x00059BB6
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0005B9BE File Offset: 0x00059BBE
		[InputControl(bit = 3U, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x060013D9 RID: 5081 RVA: 0x0005B9C7 File Offset: 0x00059BC7
		public DpadControl()
		{
			this.m_StateBlock.sizeInBits = 4U;
			this.m_StateBlock.format = InputStateBlock.FormatBit;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0005B9EC File Offset: 0x00059BEC
		protected override void FinishSetup()
		{
			this.up = base.GetChildControl<ButtonControl>("up");
			this.down = base.GetChildControl<ButtonControl>("down");
			this.left = base.GetChildControl<ButtonControl>("left");
			this.right = base.GetChildControl<ButtonControl>("right");
			base.FinishSetup();
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0005BA44 File Offset: 0x00059C44
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			bool flag = this.up.ReadValueFromStateWithCaching(statePtr) >= this.up.pressPointOrDefault;
			bool downIsPressed = this.down.ReadValueFromStateWithCaching(statePtr) >= this.down.pressPointOrDefault;
			bool leftIsPressed = this.left.ReadValueFromStateWithCaching(statePtr) >= this.left.pressPointOrDefault;
			bool rightIsPressed = this.right.ReadValueFromStateWithCaching(statePtr) >= this.right.pressPointOrDefault;
			return DpadControl.MakeDpadVector(flag, downIsPressed, leftIsPressed, rightIsPressed, true);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0005BAD0 File Offset: 0x00059CD0
		public unsafe override void WriteValueIntoState(Vector2 value, void* statePtr)
		{
			bool upIsPressed = this.up.IsValueConsideredPressed(value.y);
			bool downIsPressed = this.down.IsValueConsideredPressed(value.y * -1f);
			bool leftIsPressed = this.left.IsValueConsideredPressed(value.x * -1f);
			bool rightIsPressed = this.right.IsValueConsideredPressed(value.x);
			this.up.WriteValueIntoState((upIsPressed && !downIsPressed) ? value.y : 0f, statePtr);
			this.down.WriteValueIntoState((downIsPressed && !upIsPressed) ? (value.y * -1f) : 0f, statePtr);
			this.left.WriteValueIntoState((leftIsPressed && !rightIsPressed) ? (value.x * -1f) : 0f, statePtr);
			this.right.WriteValueIntoState((rightIsPressed && !leftIsPressed) ? value.x : 0f, statePtr);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0005BBBC File Offset: 0x00059DBC
		public static Vector2 MakeDpadVector(bool up, bool down, bool left, bool right, bool normalize = true)
		{
			float upValue = (up ? 1f : 0f);
			float downValue = (down ? (-1f) : 0f);
			float leftValue = (left ? (-1f) : 0f);
			float rightValue = (right ? 1f : 0f);
			Vector2 result = new Vector2(leftValue + rightValue, upValue + downValue);
			if (normalize && result.x != 0f && result.y != 0f)
			{
				result = new Vector2(result.x * 0.707107f, result.y * 0.707107f);
			}
			return result;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0005BC59 File Offset: 0x00059E59
		public static Vector2 MakeDpadVector(float up, float down, float left, float right)
		{
			return new Vector2(-left + right, up - down);
		}

		// Token: 0x0200021C RID: 540
		[InputControlLayout(hideInUI = true)]
		public class DpadAxisControl : AxisControl
		{
			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x060013DF RID: 5087 RVA: 0x0005BC67 File Offset: 0x00059E67
			// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0005BC6F File Offset: 0x00059E6F
			public int component { get; set; }

			// Token: 0x060013E1 RID: 5089 RVA: 0x0005BC78 File Offset: 0x00059E78
			protected override void FinishSetup()
			{
				base.FinishSetup();
				this.component = ((base.name == "x") ? 0 : 1);
				this.m_StateBlock = this.m_Parent.m_StateBlock;
			}

			// Token: 0x060013E2 RID: 5090 RVA: 0x0005BCB0 File Offset: 0x00059EB0
			public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
			{
				return ((DpadControl)this.m_Parent).ReadUnprocessedValueFromState(statePtr)[this.component];
			}
		}

		// Token: 0x0200021D RID: 541
		internal enum ButtonBits
		{
			// Token: 0x04000BF1 RID: 3057
			Up,
			// Token: 0x04000BF2 RID: 3058
			Down,
			// Token: 0x04000BF3 RID: 3059
			Left,
			// Token: 0x04000BF4 RID: 3060
			Right
		}
	}
}
