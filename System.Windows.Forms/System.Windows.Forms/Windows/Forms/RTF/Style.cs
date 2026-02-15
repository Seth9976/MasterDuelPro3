using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200038C RID: 908
	internal class Style
	{
		// Token: 0x06001D74 RID: 7540 RVA: 0x00090D10 File Offset: 0x0008EF10
		public Style(RTF rtf)
		{
			this.num = -1;
			this.type = StyleType.Paragraph;
			this.based_on = 222;
			this.next_par = -1;
			lock (rtf)
			{
				if (rtf.Styles == null)
				{
					rtf.Styles = this;
				}
				else
				{
					Style styles = rtf.Styles;
					while (styles.next != null)
					{
						styles = styles.next;
					}
					styles.next = this;
				}
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x00090D9C File Offset: 0x0008EF9C
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x00090DA4 File Offset: 0x0008EFA4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00090DAD File Offset: 0x0008EFAD
		public StyleType Type
		{
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x00090DB6 File Offset: 0x0008EFB6
		public bool Additive
		{
			set
			{
				this.additive = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x00090DBF File Offset: 0x0008EFBF
		public int BasedOn
		{
			set
			{
				this.based_on = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00090DC8 File Offset: 0x0008EFC8
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x00090DD0 File Offset: 0x0008EFD0
		public StyleElement Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00090DD9 File Offset: 0x0008EFD9
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00090DE1 File Offset: 0x0008EFE1
		public int NextPar
		{
			get
			{
				return this.next_par;
			}
			set
			{
				this.next_par = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00090DEA File Offset: 0x0008EFEA
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00090DF2 File Offset: 0x0008EFF2
		public int Num
		{
			get
			{
				return this.num;
			}
			set
			{
				this.num = value;
			}
		}

		// Token: 0x04001CBC RID: 7356
		private string name;

		// Token: 0x04001CBD RID: 7357
		private StyleType type;

		// Token: 0x04001CBE RID: 7358
		private bool additive;

		// Token: 0x04001CBF RID: 7359
		private int num;

		// Token: 0x04001CC0 RID: 7360
		private int based_on;

		// Token: 0x04001CC1 RID: 7361
		private int next_par;

		// Token: 0x04001CC2 RID: 7362
		private StyleElement elements;

		// Token: 0x04001CC3 RID: 7363
		private Style next;
	}
}
