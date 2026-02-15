using System;

namespace System.Windows.Forms
{
	/// <summary>Provides static, predefined <see cref="T:System.Windows.Forms.Clipboard" /> format names. Use them to identify the format of data that you store in an <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000065 RID: 101
	public class DataFormats
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x00012C5C File Offset: 0x00010E5C
		internal static bool ContainsFormat(int id)
		{
			object obj = DataFormats.lock_object;
			bool flag2;
			lock (obj)
			{
				if (!DataFormats.initialized)
				{
					DataFormats.Init();
				}
				flag2 = DataFormats.Format.Find(id) != null;
			}
			return flag2;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified ID.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
		/// <param name="id">The format ID. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004C5 RID: 1221 RVA: 0x00012CAC File Offset: 0x00010EAC
		public static DataFormats.Format GetFormat(int id)
		{
			object obj = DataFormats.lock_object;
			DataFormats.Format format;
			lock (obj)
			{
				if (!DataFormats.initialized)
				{
					DataFormats.Init();
				}
				format = DataFormats.Format.Find(id);
			}
			return format;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified format.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
		/// <param name="format">The format name. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Registering a new <see cref="T:System.Windows.Forms.Clipboard" /> format failed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004C6 RID: 1222 RVA: 0x00012CFC File Offset: 0x00010EFC
		public static DataFormats.Format GetFormat(string format)
		{
			object obj = DataFormats.lock_object;
			DataFormats.Format format2;
			lock (obj)
			{
				if (!DataFormats.initialized)
				{
					DataFormats.Init();
				}
				format2 = DataFormats.Format.Add(format);
			}
			return format2;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00012D4C File Offset: 0x00010F4C
		private static void Init()
		{
			if (DataFormats.initialized)
			{
				return;
			}
			IntPtr intPtr = XplatUI.ClipboardOpen(false);
			new DataFormats.Format(DataFormats.Text, XplatUI.ClipboardGetID(intPtr, DataFormats.Text));
			new DataFormats.Format(DataFormats.Bitmap, XplatUI.ClipboardGetID(intPtr, DataFormats.Bitmap));
			new DataFormats.Format(DataFormats.MetafilePict, XplatUI.ClipboardGetID(intPtr, DataFormats.MetafilePict));
			new DataFormats.Format(DataFormats.SymbolicLink, XplatUI.ClipboardGetID(intPtr, DataFormats.SymbolicLink));
			new DataFormats.Format(DataFormats.Dif, XplatUI.ClipboardGetID(intPtr, DataFormats.Dif));
			new DataFormats.Format(DataFormats.Tiff, XplatUI.ClipboardGetID(intPtr, DataFormats.Tiff));
			new DataFormats.Format(DataFormats.OemText, XplatUI.ClipboardGetID(intPtr, DataFormats.OemText));
			new DataFormats.Format(DataFormats.Dib, XplatUI.ClipboardGetID(intPtr, DataFormats.Dib));
			new DataFormats.Format(DataFormats.Palette, XplatUI.ClipboardGetID(intPtr, DataFormats.Palette));
			new DataFormats.Format(DataFormats.PenData, XplatUI.ClipboardGetID(intPtr, DataFormats.PenData));
			new DataFormats.Format(DataFormats.Riff, XplatUI.ClipboardGetID(intPtr, DataFormats.Riff));
			new DataFormats.Format(DataFormats.WaveAudio, XplatUI.ClipboardGetID(intPtr, DataFormats.WaveAudio));
			new DataFormats.Format(DataFormats.UnicodeText, XplatUI.ClipboardGetID(intPtr, DataFormats.UnicodeText));
			new DataFormats.Format(DataFormats.EnhancedMetafile, XplatUI.ClipboardGetID(intPtr, DataFormats.EnhancedMetafile));
			new DataFormats.Format(DataFormats.FileDrop, XplatUI.ClipboardGetID(intPtr, DataFormats.FileDrop));
			new DataFormats.Format(DataFormats.Locale, XplatUI.ClipboardGetID(intPtr, DataFormats.Locale));
			new DataFormats.Format(DataFormats.CommaSeparatedValue, XplatUI.ClipboardGetID(intPtr, DataFormats.CommaSeparatedValue));
			new DataFormats.Format(DataFormats.Html, XplatUI.ClipboardGetID(intPtr, DataFormats.Html));
			new DataFormats.Format(DataFormats.Rtf, XplatUI.ClipboardGetID(intPtr, DataFormats.Rtf));
			new DataFormats.Format(DataFormats.Serializable, XplatUI.ClipboardGetID(intPtr, DataFormats.Serializable));
			new DataFormats.Format(DataFormats.StringFormat, XplatUI.ClipboardGetID(intPtr, DataFormats.StringFormat));
			XplatUI.ClipboardClose(intPtr);
			DataFormats.initialized = true;
		}

		/// <summary>Specifies a Windows bitmap format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400029E RID: 670
		public static readonly string Bitmap = "Bitmap";

		/// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets. This format is not used directly by Windows Forms. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400029F RID: 671
		public static readonly string CommaSeparatedValue = "Csv";

		/// <summary>Specifies the Windows device-independent bitmap (DIB) format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A0 RID: 672
		public static readonly string Dib = "DeviceIndependentBitmap";

		/// <summary>Specifies the Windows Data Interchange Format (DIF), which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A1 RID: 673
		public static readonly string Dif = "DataInterchangeFormat";

		/// <summary>Specifies the Windows enhanced metafile format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A2 RID: 674
		public static readonly string EnhancedMetafile = "EnhancedMetafile";

		/// <summary>Specifies the Windows file drop format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A3 RID: 675
		public static readonly string FileDrop = "FileDrop";

		/// <summary>Specifies text in the HTML Clipboard format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A4 RID: 676
		public static readonly string Html = "HTML Format";

		/// <summary>Specifies the Windows culture format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A5 RID: 677
		public static readonly string Locale = "Locale";

		/// <summary>Specifies the Windows metafile format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A6 RID: 678
		public static readonly string MetafilePict = "MetaFilePict";

		/// <summary>Specifies the standard Windows original equipment manufacturer (OEM) text format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A7 RID: 679
		public static readonly string OemText = "OEMText";

		/// <summary>Specifies the Windows palette format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A8 RID: 680
		public static readonly string Palette = "Palette";

		/// <summary>Specifies the Windows pen data format, which consists of pen strokes for handwriting software; Windows Forms does not use this format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002A9 RID: 681
		public static readonly string PenData = "PenData";

		/// <summary>Specifies the Resource Interchange File Format (RIFF) audio format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002AA RID: 682
		public static readonly string Riff = "RiffAudio";

		/// <summary>Specifies text consisting of Rich Text Format (RTF) data. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002AB RID: 683
		public static readonly string Rtf = "Rich Text Format";

		/// <summary>Specifies a format that encapsulates any type of Windows Forms object. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002AC RID: 684
		public static readonly string Serializable = "WindowsForms10PersistentObject";

		/// <summary>Specifies the Windows Forms string class format, which Windows Forms uses to store string objects. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002AD RID: 685
		public static readonly string StringFormat = "System.String";

		/// <summary>Specifies the Windows symbolic link format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002AE RID: 686
		public static readonly string SymbolicLink = "SymbolicLink";

		/// <summary>Specifies the standard ANSI text format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002AF RID: 687
		public static readonly string Text = "Text";

		/// <summary>Specifies the Tagged Image File Format (TIFF), which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002B0 RID: 688
		public static readonly string Tiff = "Tiff";

		/// <summary>Specifies the standard Windows Unicode text format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002B1 RID: 689
		public static readonly string UnicodeText = "UnicodeText";

		/// <summary>Specifies the wave audio format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002B2 RID: 690
		public static readonly string WaveAudio = "WaveAudio";

		// Token: 0x040002B3 RID: 691
		private static object lock_object = new object();

		// Token: 0x040002B4 RID: 692
		private static bool initialized;

		/// <summary>Represents a Clipboard format type.</summary>
		// Token: 0x02000066 RID: 102
		public class Format
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> class with a Boolean that indicates whether a Win32 handle is expected.</summary>
			/// <param name="name">The name of this format. </param>
			/// <param name="id">The ID number for this format. </param>
			// Token: 0x060004C9 RID: 1225 RVA: 0x00013030 File Offset: 0x00011230
			public Format(string name, int id)
			{
				this.name = name;
				this.id = id;
				object obj = DataFormats.Format.lockobj;
				lock (obj)
				{
					if (DataFormats.Format.formats == null)
					{
						DataFormats.Format.formats = this;
					}
					else
					{
						DataFormats.Format format = DataFormats.Format.formats;
						while (format.next != null)
						{
							format = format.next;
						}
						format.next = this;
					}
				}
			}

			/// <summary>Gets the ID number for this format.</summary>
			/// <returns>The ID number for this format.</returns>
			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x000130AC File Offset: 0x000112AC
			public int Id
			{
				get
				{
					return this.id;
				}
			}

			/// <summary>Gets the name of this format.</summary>
			/// <returns>The name of this format.</returns>
			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x000130B4 File Offset: 0x000112B4
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x000130BC File Offset: 0x000112BC
			internal DataFormats.Format Next
			{
				get
				{
					return this.next;
				}
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x000130C4 File Offset: 0x000112C4
			internal static DataFormats.Format Add(string name)
			{
				DataFormats.Format format = DataFormats.Format.Find(name);
				if (format == null)
				{
					IntPtr intPtr = XplatUI.ClipboardOpen(false);
					format = new DataFormats.Format(name, XplatUI.ClipboardGetID(intPtr, name));
					XplatUI.ClipboardClose(intPtr);
				}
				return format;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x000130F8 File Offset: 0x000112F8
			internal static DataFormats.Format Find(int id)
			{
				DataFormats.Format format = DataFormats.Format.formats;
				while (format != null && format.Id != id)
				{
					format = format.next;
				}
				return format;
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00013124 File Offset: 0x00011324
			internal static DataFormats.Format Find(string name)
			{
				DataFormats.Format format = DataFormats.Format.formats;
				while (format != null && !format.Name.Equals(name))
				{
					format = format.next;
				}
				return format;
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00013152 File Offset: 0x00011352
			internal static DataFormats.Format List
			{
				get
				{
					return DataFormats.Format.formats;
				}
			}

			// Token: 0x040002B5 RID: 693
			private static readonly object lockobj = new object();

			// Token: 0x040002B6 RID: 694
			private static DataFormats.Format formats;

			// Token: 0x040002B7 RID: 695
			private string name;

			// Token: 0x040002B8 RID: 696
			private int id;

			// Token: 0x040002B9 RID: 697
			private DataFormats.Format next;

			// Token: 0x040002BA RID: 698
			internal bool is_serializable;
		}
	}
}
