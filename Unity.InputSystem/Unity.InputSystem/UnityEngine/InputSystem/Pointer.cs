using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000092 RID: 146
	[InputControlLayout(stateType = typeof(PointerState), isGenericTypeOfDevice = true)]
	public class Pointer : InputDevice, IInputStateCallbackReceiver
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		public Vector2Control position { get; protected set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001BAC1 File Offset: 0x00019CC1
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001BAC9 File Offset: 0x00019CC9
		public DeltaControl delta { get; protected set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001BAD2 File Offset: 0x00019CD2
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001BADA File Offset: 0x00019CDA
		public Vector2Control radius { get; protected set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001BAE3 File Offset: 0x00019CE3
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0001BAEB File Offset: 0x00019CEB
		public AxisControl pressure { get; protected set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001BAFC File Offset: 0x00019CFC
		public ButtonControl press { get; protected set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001BB05 File Offset: 0x00019D05
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0001BB0D File Offset: 0x00019D0D
		public IntegerControl displayIndex { get; protected set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001BB16 File Offset: 0x00019D16
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0001BB1D File Offset: 0x00019D1D
		public static Pointer current { get; internal set; }

		// Token: 0x06000786 RID: 1926 RVA: 0x0001BB25 File Offset: 0x00019D25
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Pointer.current = this;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001BB33 File Offset: 0x00019D33
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Pointer.current == this)
			{
				Pointer.current = null;
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001BB4C File Offset: 0x00019D4C
		protected override void FinishSetup()
		{
			this.position = base.GetChildControl<Vector2Control>("position");
			this.delta = base.GetChildControl<DeltaControl>("delta");
			this.radius = base.GetChildControl<Vector2Control>("radius");
			this.pressure = base.GetChildControl<AxisControl>("pressure");
			this.press = base.GetChildControl<ButtonControl>("press");
			this.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			base.FinishSetup();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001BBC8 File Offset: 0x00019DC8
		protected void OnNextUpdate()
		{
			InputState.Change<Vector2>(this.delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001BBEF File Offset: 0x00019DEF
		protected void OnStateEvent(InputEventPtr eventPtr)
		{
			this.delta.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001BC0B File Offset: 0x00019E0B
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001BC13 File Offset: 0x00019E13
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001751C File Offset: 0x0001571C
		bool IInputStateCallbackReceiver.GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}
	}
}
