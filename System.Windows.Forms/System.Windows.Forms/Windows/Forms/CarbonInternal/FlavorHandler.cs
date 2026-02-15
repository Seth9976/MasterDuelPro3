using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x0200039D RID: 925
	internal class FlavorHandler
	{
		// Token: 0x06001DDE RID: 7646 RVA: 0x000944A0 File Offset: 0x000926A0
		internal FlavorHandler(IntPtr dragref, IntPtr itemref, uint counter)
		{
			FlavorHandler.GetFlavorType(dragref, itemref, counter, ref this.flavorref);
			FlavorHandler.GetFlavorFlags(dragref, itemref, this.flavorref, ref this.flags);
			byte[] bytes = BitConverter.GetBytes((int)this.flavorref);
			this.fourcc = string.Format("{0}{1}{2}{3}", new object[]
			{
				(char)bytes[3],
				(char)bytes[2],
				(char)bytes[1],
				(char)bytes[0]
			});
			this.dragref = dragref;
			this.itemref = itemref;
			this.GetData();
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00094540 File Offset: 0x00092740
		internal void GetData()
		{
			FlavorHandler.GetFlavorDataSize(this.dragref, this.itemref, this.flavorref, ref this.size);
			this.data = new byte[this.size];
			FlavorHandler.GetFlavorData(this.dragref, this.itemref, this.flavorref, this.data, ref this.size, 0U);
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000945A1 File Offset: 0x000927A1
		internal string DataString
		{
			get
			{
				return Encoding.Default.GetString(this.data);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000945B3 File Offset: 0x000927B3
		internal byte[] DataArray
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000945BB File Offset: 0x000927BB
		internal IntPtr DataPtr
		{
			get
			{
				return (IntPtr)BitConverter.ToInt32(this.data, 0);
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000945D0 File Offset: 0x000927D0
		internal bool Supported
		{
			get
			{
				string text = this.fourcc;
				return text == "furl" || text == "mono" || text == "mser";
			}
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x00094614 File Offset: 0x00092814
		internal DataObject Convert(ArrayList flavorlist)
		{
			string text = this.fourcc;
			if (text == "furl")
			{
				return this.ConvertToFileDrop(flavorlist);
			}
			if (text == "mono")
			{
				return this.ConvertToObject(flavorlist);
			}
			if (!(text == "mser"))
			{
				return new DataObject();
			}
			return this.DeserializeObject(flavorlist);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00094670 File Offset: 0x00092870
		internal DataObject DeserializeObject(ArrayList flavorlist)
		{
			DataObject dataObject = new DataObject();
			MemoryStream memoryStream = new MemoryStream(this.DataArray);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			if (memoryStream.Length == 0L)
			{
				return dataObject;
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			dataObject.SetData(binaryFormatter.Deserialize(memoryStream));
			return dataObject;
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000946B8 File Offset: 0x000928B8
		internal DataObject ConvertToObject(ArrayList flavorlist)
		{
			DataObject dataObject = new DataObject();
			foreach (object obj in flavorlist)
			{
				dataObject.SetData(((GCHandle)((FlavorHandler)obj).DataPtr).Target);
			}
			return dataObject;
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00094724 File Offset: 0x00092924
		internal DataObject ConvertToFileDrop(ArrayList flavorlist)
		{
			DataObject dataObject = new DataObject();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in flavorlist)
			{
				FlavorHandler flavorHandler = (FlavorHandler)obj;
				try
				{
					arrayList.Add(new Uri(flavorHandler.DataString).LocalPath);
				}
				catch
				{
				}
			}
			string[] array = (string[])arrayList.ToArray(typeof(string));
			if (array.Length < 1)
			{
				return dataObject;
			}
			dataObject.SetData(DataFormats.FileDrop, array);
			dataObject.SetData("FileName", array[0]);
			dataObject.SetData("FileNameW", array[0]);
			return dataObject;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000947F4 File Offset: 0x000929F4
		public override string ToString()
		{
			return this.fourcc;
		}

		// Token: 0x06001DE9 RID: 7657
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorDataSize(IntPtr dragref, IntPtr itemref, IntPtr flavorref, ref int size);

		// Token: 0x06001DEA RID: 7658
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorData(IntPtr dragref, IntPtr itemref, IntPtr flavorref, [In] [Out] byte[] data, ref int size, uint offset);

		// Token: 0x06001DEB RID: 7659
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorFlags(IntPtr dragref, IntPtr itemref, IntPtr flavorref, ref uint flags);

		// Token: 0x06001DEC RID: 7660
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetFlavorType(IntPtr dragref, IntPtr itemref, uint index, ref IntPtr flavor);

		// Token: 0x04001CED RID: 7405
		internal IntPtr flavorref;

		// Token: 0x04001CEE RID: 7406
		internal IntPtr dragref;

		// Token: 0x04001CEF RID: 7407
		internal IntPtr itemref;

		// Token: 0x04001CF0 RID: 7408
		internal int size;

		// Token: 0x04001CF1 RID: 7409
		internal uint flags;

		// Token: 0x04001CF2 RID: 7410
		internal byte[] data;

		// Token: 0x04001CF3 RID: 7411
		internal string fourcc;
	}
}
