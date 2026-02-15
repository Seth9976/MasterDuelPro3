using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200057D RID: 1405
	public abstract class ColorContainer : MonoBehaviour
	{
		// Token: 0x06002C9F RID: 11423 RVA: 0x000F1D32 File Offset: 0x000EFF32
		protected Color GetColorUnselected()
		{
			return this.baseColor * this.colorUnselected * this.intensityUnselected;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000F1D50 File Offset: 0x000EFF50
		protected Color GetColorSelected()
		{
			return this.baseColor * this.colorSelected * this.intensitySelected;
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000F1D6E File Offset: 0x000EFF6E
		protected Color GetColorButtonDown()
		{
			return this.baseColor * this.colorButtonDown * this.intensityButtonDown;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000F1D8C File Offset: 0x000EFF8C
		protected Color GetColorButtonEnter()
		{
			return this.baseColor * this.colorButtonEnter * this.intensityButtonEnter;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000F1DAA File Offset: 0x000EFFAA
		protected Color GetColorButtonInactive()
		{
			return this.baseColor * this.colorButtonInactive * this.intensityButtonInactive;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetColor(ColorContainer.SelectMode select_mode, ColorContainer.StatusMode status_mode, bool is_active = true)
		{
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reapply()
		{
		}

		// Token: 0x04002AD5 RID: 10965
		public Color baseColor;

		// Token: 0x04002AD6 RID: 10966
		[SerializeField]
		protected bool inheritParentColorSetting;

		// Token: 0x04002AD7 RID: 10967
		[SerializeField]
		private Color colorUnselected;

		// Token: 0x04002AD8 RID: 10968
		[SerializeField]
		private Color colorSelected;

		// Token: 0x04002AD9 RID: 10969
		[SerializeField]
		private Color colorButtonDown;

		// Token: 0x04002ADA RID: 10970
		[SerializeField]
		private Color colorButtonEnter;

		// Token: 0x04002ADB RID: 10971
		[SerializeField]
		private Color colorButtonInactive;

		// Token: 0x04002ADC RID: 10972
		[SerializeField]
		[ColorLabelString]
		private string colorLabelUnselected;

		// Token: 0x04002ADD RID: 10973
		[SerializeField]
		[ColorLabelString]
		private string colorLabelSelected;

		// Token: 0x04002ADE RID: 10974
		[ColorLabelString]
		[SerializeField]
		private string colorLabelButtonDown;

		// Token: 0x04002ADF RID: 10975
		[ColorLabelString]
		[SerializeField]
		private string colorLabelButtonEnter;

		// Token: 0x04002AE0 RID: 10976
		[ColorLabelString]
		[SerializeField]
		private string colorLabelButtonInactive;

		// Token: 0x04002AE1 RID: 10977
		public int index;

		// Token: 0x04002AE2 RID: 10978
		[SerializeField]
		protected ColorContainer.ColorMode colorModeUnselected;

		// Token: 0x04002AE3 RID: 10979
		[SerializeField]
		protected ColorContainer.ColorMode colorModeSelected;

		// Token: 0x04002AE4 RID: 10980
		[SerializeField]
		protected ColorContainer.ColorMode colorModeButtonDown;

		// Token: 0x04002AE5 RID: 10981
		[SerializeField]
		protected ColorContainer.ColorMode colorModeButtonEnter;

		// Token: 0x04002AE6 RID: 10982
		[SerializeField]
		protected ColorContainer.ColorMode colorModeButtonInactive;

		// Token: 0x04002AE7 RID: 10983
		[SerializeField]
		protected float intensityUnselected;

		// Token: 0x04002AE8 RID: 10984
		[SerializeField]
		protected float intensitySelected;

		// Token: 0x04002AE9 RID: 10985
		[SerializeField]
		protected float intensityButtonDown;

		// Token: 0x04002AEA RID: 10986
		[SerializeField]
		protected float intensityButtonEnter;

		// Token: 0x04002AEB RID: 10987
		[SerializeField]
		protected float intensityButtonInactive;

		// Token: 0x04002AEC RID: 10988
		private ColorContainer.SelectMode currentSelectMode;

		// Token: 0x04002AED RID: 10989
		private ColorContainer.StatusMode currentStatusMode;

		// Token: 0x04002AEE RID: 10990
		private bool currentIsActive;

		// Token: 0x0200057E RID: 1406
		public enum SelectMode
		{
			// Token: 0x04002AF0 RID: 10992
			Unselected,
			// Token: 0x04002AF1 RID: 10993
			Selected
		}

		// Token: 0x0200057F RID: 1407
		public enum StatusMode
		{
			// Token: 0x04002AF3 RID: 10995
			Normal,
			// Token: 0x04002AF4 RID: 10996
			Down,
			// Token: 0x04002AF5 RID: 10997
			Enter
		}

		// Token: 0x02000580 RID: 1408
		public enum ColorMode
		{
			// Token: 0x04002AF7 RID: 10999
			Multiple,
			// Token: 0x04002AF8 RID: 11000
			Override
		}
	}
}
