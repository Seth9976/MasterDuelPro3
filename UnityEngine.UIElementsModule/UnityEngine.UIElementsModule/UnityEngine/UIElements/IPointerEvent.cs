using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021C RID: 540
	public interface IPointerEvent
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000EA4 RID: 3748
		int pointerId { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000EA5 RID: 3749
		string pointerType { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000EA6 RID: 3750
		bool isPrimary { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000EA7 RID: 3751
		int button { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000EA8 RID: 3752
		int pressedButtons { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000EA9 RID: 3753
		Vector3 position { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000EAA RID: 3754
		Vector3 localPosition { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000EAB RID: 3755
		Vector3 deltaPosition { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000EAC RID: 3756
		float deltaTime { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000EAD RID: 3757
		int clickCount { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000EAE RID: 3758
		float pressure { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000EAF RID: 3759
		float tangentialPressure { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000EB0 RID: 3760
		float altitudeAngle { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000EB1 RID: 3761
		float azimuthAngle { get; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000EB2 RID: 3762
		float twist { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000EB3 RID: 3763
		Vector2 tilt { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000EB4 RID: 3764
		PenStatus penStatus { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000EB5 RID: 3765
		Vector2 radius { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000EB6 RID: 3766
		Vector2 radiusVariance { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000EB7 RID: 3767
		EventModifiers modifiers { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000EB8 RID: 3768
		bool shiftKey { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000EB9 RID: 3769
		bool ctrlKey { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000EBA RID: 3770
		bool commandKey { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000EBB RID: 3771
		bool altKey { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000EBC RID: 3772
		bool actionKey { get; }
	}
}
