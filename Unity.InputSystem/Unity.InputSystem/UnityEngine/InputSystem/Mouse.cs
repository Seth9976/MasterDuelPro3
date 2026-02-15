using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200008F RID: 143
	[InputControlLayout(stateType = typeof(MouseState), isGenericTypeOfDevice = true)]
	public class Mouse : Pointer, IInputStateCallbackReceiver
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001B6D8 File Offset: 0x000198D8
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public DeltaControl scroll { get; protected set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001B6E9 File Offset: 0x000198E9
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001B6F1 File Offset: 0x000198F1
		public ButtonControl leftButton { get; protected set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001B6FA File Offset: 0x000198FA
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001B702 File Offset: 0x00019902
		public ButtonControl middleButton { get; protected set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001B70B File Offset: 0x0001990B
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001B713 File Offset: 0x00019913
		public ButtonControl rightButton { get; protected set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001B71C File Offset: 0x0001991C
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001B724 File Offset: 0x00019924
		public ButtonControl backButton { get; protected set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001B72D File Offset: 0x0001992D
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001B735 File Offset: 0x00019935
		public ButtonControl forwardButton { get; protected set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001B73E File Offset: 0x0001993E
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001B746 File Offset: 0x00019946
		public IntegerControl clickCount { get; protected set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001B74F File Offset: 0x0001994F
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001B756 File Offset: 0x00019956
		public new static Mouse current { get; private set; }

		// Token: 0x06000755 RID: 1877 RVA: 0x0001B75E File Offset: 0x0001995E
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Mouse.current = this;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001B76C File Offset: 0x0001996C
		protected override void OnAdded()
		{
			base.OnAdded();
			if (base.native && Mouse.s_PlatformMouseDevice == null)
			{
				Mouse.s_PlatformMouseDevice = this;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001B789 File Offset: 0x00019989
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Mouse.current == this)
			{
				Mouse.current = null;
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001B7A0 File Offset: 0x000199A0
		public void WarpCursorPosition(Vector2 position)
		{
			WarpMousePositionCommand command = WarpMousePositionCommand.Create(position);
			base.ExecuteCommand<WarpMousePositionCommand>(ref command);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001B7C0 File Offset: 0x000199C0
		protected override void FinishSetup()
		{
			this.scroll = base.GetChildControl<DeltaControl>("scroll");
			this.leftButton = base.GetChildControl<ButtonControl>("leftButton");
			this.middleButton = base.GetChildControl<ButtonControl>("middleButton");
			this.rightButton = base.GetChildControl<ButtonControl>("rightButton");
			this.forwardButton = base.GetChildControl<ButtonControl>("forwardButton");
			this.backButton = base.GetChildControl<ButtonControl>("backButton");
			base.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			this.clickCount = base.GetChildControl<IntegerControl>("clickCount");
			base.FinishSetup();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001B85C File Offset: 0x00019A5C
		protected new void OnNextUpdate()
		{
			base.OnNextUpdate();
			InputState.Change<Vector2>(this.scroll, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001B889 File Offset: 0x00019A89
		protected new void OnStateEvent(InputEventPtr eventPtr)
		{
			this.scroll.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			base.OnStateEvent(eventPtr);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001B8AC File Offset: 0x00019AAC
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x040003C6 RID: 966
		internal static Mouse s_PlatformMouseDevice;
	}
}
