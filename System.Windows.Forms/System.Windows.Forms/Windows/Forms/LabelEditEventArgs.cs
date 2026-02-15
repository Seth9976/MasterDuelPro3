using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.ListView.AfterLabelEdit" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FA RID: 250
	public class LabelEditEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> class with the specified index to the <see cref="T:System.Windows.Forms.ListViewItem" /> to edit.</summary>
		/// <param name="item">The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />, containing the label to edit. </param>
		// Token: 0x060008C7 RID: 2247 RVA: 0x000255CA File Offset: 0x000237CA
		public LabelEditEventArgs(int item)
		{
			this.item = item;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> class with the specified index to the <see cref="T:System.Windows.Forms.ListViewItem" /> being edited and the new text for the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <param name="item">The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />, containing the label to edit. </param>
		/// <param name="label">The new text assigned to the label of the <see cref="T:System.Windows.Forms.ListViewItem" />. </param>
		// Token: 0x060008C8 RID: 2248 RVA: 0x000255D9 File Offset: 0x000237D9
		public LabelEditEventArgs(int item, string label)
		{
			this.item = item;
			this.label = label;
		}

		/// <summary>Gets or sets a value indicating whether changes made to the label of the <see cref="T:System.Windows.Forms.ListViewItem" /> should be canceled.</summary>
		/// <returns>true if the edit operation of the label for the <see cref="T:System.Windows.Forms.ListViewItem" /> should be canceled; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x000255EF File Offset: 0x000237EF
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x000255F7 File Offset: 0x000237F7
		public bool CancelEdit
		{
			get
			{
				return this.cancelEdit;
			}
			set
			{
				this.cancelEdit = value;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" /> containing the label to edit.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00025600 File Offset: 0x00023800
		public int Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the new text assigned to the label of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The new text to associate with the <see cref="T:System.Windows.Forms.ListViewItem" /> or null if the text is unchanged. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00025608 File Offset: 0x00023808
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00025610 File Offset: 0x00023810
		internal void SetLabel(string label)
		{
			this.label = label;
		}

		// Token: 0x04000659 RID: 1625
		private int item;

		// Token: 0x0400065A RID: 1626
		private string label;

		// Token: 0x0400065B RID: 1627
		private bool cancelEdit;
	}
}
