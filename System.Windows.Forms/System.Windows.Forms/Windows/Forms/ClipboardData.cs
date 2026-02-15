using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200022C RID: 556
	internal class ClipboardData
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x00070635 File Offset: 0x0006E835
		public ClipboardData()
		{
			this.source_data = new ListDictionary();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00070648 File Offset: 0x0006E848
		public void ClearSources()
		{
			this.source_data.Clear();
			this.plain_text_source = null;
			this.image_source = null;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00070664 File Offset: 0x0006E864
		public void AddSource(int type, object source)
		{
			if (source is string && (type == DataFormats.GetFormat(DataFormats.Text).Id || type == -1))
			{
				this.plain_text_source = source as string;
			}
			else if (source is Image)
			{
				this.image_source = source as Image;
			}
			this.source_data[type] = source;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000706C3 File Offset: 0x0006E8C3
		public object GetSource(int type)
		{
			return this.source_data[type];
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000706D6 File Offset: 0x0006E8D6
		public string GetPlainText()
		{
			return this.plain_text_source;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000706E0 File Offset: 0x0006E8E0
		public string GetRtfText()
		{
			DataFormats.Format format = DataFormats.GetFormat(DataFormats.Rtf);
			if (format == null)
			{
				return null;
			}
			return (string)this.GetSource(format.Id);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0007070E File Offset: 0x0006E90E
		public bool IsSourceText
		{
			get
			{
				return this.plain_text_source != null;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00070719 File Offset: 0x0006E919
		public bool IsSourceImage
		{
			get
			{
				return this.image_source != null;
			}
		}

		// Token: 0x04000DB9 RID: 3513
		private ListDictionary source_data;

		// Token: 0x04000DBA RID: 3514
		private string plain_text_source;

		// Token: 0x04000DBB RID: 3515
		private Image image_source;

		// Token: 0x04000DBC RID: 3516
		internal object Item;

		// Token: 0x04000DBD RID: 3517
		internal ArrayList Formats;

		// Token: 0x04000DBE RID: 3518
		internal bool Retrieving;

		// Token: 0x04000DBF RID: 3519
		internal bool Enumerating;

		// Token: 0x04000DC0 RID: 3520
		internal XplatUI.ObjectToClipboard Converter;
	}
}
