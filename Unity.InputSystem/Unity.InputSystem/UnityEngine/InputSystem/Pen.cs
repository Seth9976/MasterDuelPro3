using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000091 RID: 145
	[InputControlLayout(stateType = typeof(PenState), isGenericTypeOfDevice = true)]
	public class Pen : Pointer
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001B8C5 File Offset: 0x00019AC5
		public ButtonControl tip { get; protected set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001B8CE File Offset: 0x00019ACE
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001B8D6 File Offset: 0x00019AD6
		public ButtonControl eraser { get; protected set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001B8DF File Offset: 0x00019ADF
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001B8E7 File Offset: 0x00019AE7
		public ButtonControl firstBarrelButton { get; protected set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public ButtonControl secondBarrelButton { get; protected set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001B901 File Offset: 0x00019B01
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0001B909 File Offset: 0x00019B09
		public ButtonControl thirdBarrelButton { get; protected set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001B912 File Offset: 0x00019B12
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0001B91A File Offset: 0x00019B1A
		public ButtonControl fourthBarrelButton { get; protected set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001B923 File Offset: 0x00019B23
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0001B92B File Offset: 0x00019B2B
		public ButtonControl inRange { get; protected set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001B934 File Offset: 0x00019B34
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0001B93C File Offset: 0x00019B3C
		public Vector2Control tilt { get; protected set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001B945 File Offset: 0x00019B45
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0001B94D File Offset: 0x00019B4D
		public AxisControl twist { get; protected set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001B956 File Offset: 0x00019B56
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0001B95D File Offset: 0x00019B5D
		public new static Pen current { get; internal set; }

		// Token: 0x17000273 RID: 627
		public ButtonControl this[PenButton button]
		{
			get
			{
				switch (button)
				{
				case PenButton.Tip:
					return this.tip;
				case PenButton.Eraser:
					return this.eraser;
				case PenButton.BarrelFirst:
					return this.firstBarrelButton;
				case PenButton.BarrelSecond:
					return this.secondBarrelButton;
				case PenButton.InRange:
					return this.inRange;
				case PenButton.BarrelThird:
					return this.thirdBarrelButton;
				case PenButton.BarrelFourth:
					return this.fourthBarrelButton;
				default:
					throw new InvalidEnumArgumentException("button", (int)button, typeof(PenButton));
				}
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001B9DF File Offset: 0x00019BDF
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Pen.current = this;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001B9ED File Offset: 0x00019BED
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Pen.current == this)
			{
				Pen.current = null;
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001BA04 File Offset: 0x00019C04
		protected override void FinishSetup()
		{
			this.tip = base.GetChildControl<ButtonControl>("tip");
			this.eraser = base.GetChildControl<ButtonControl>("eraser");
			this.firstBarrelButton = base.GetChildControl<ButtonControl>("barrel1");
			this.secondBarrelButton = base.GetChildControl<ButtonControl>("barrel2");
			this.thirdBarrelButton = base.GetChildControl<ButtonControl>("barrel3");
			this.fourthBarrelButton = base.GetChildControl<ButtonControl>("barrel4");
			this.inRange = base.GetChildControl<ButtonControl>("inRange");
			this.tilt = base.GetChildControl<Vector2Control>("tilt");
			this.twist = base.GetChildControl<AxisControl>("twist");
			base.FinishSetup();
		}
	}
}
