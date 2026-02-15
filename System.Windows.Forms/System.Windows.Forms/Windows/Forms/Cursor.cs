using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents the image used to paint the mouse pointer.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Windows.Forms.Cursors" />
	// Token: 0x0200005E RID: 94
	[Editor("System.Drawing.Design.CursorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter(typeof(CursorConverter))]
	[Serializable]
	public sealed class Cursor : IDisposable, ISerializable
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00011790 File Offset: 0x0000F990
		private void CreateCursor(Stream stream)
		{
			this.InitFromStream(stream);
			this.shape = this.ToBitmap(true, false);
			this.mask = this.ToBitmap(false, false);
			this.handle = XplatUI.DefineCursor(this.shape, this.mask, Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255), (int)this.cursor_dir.idEntries[this.id].xHotspot, (int)this.cursor_dir.idEntries[this.id].yHotspot);
			this.shape.Dispose();
			this.shape = null;
			this.mask.Dispose();
			this.mask = null;
			if (this.handle != IntPtr.Zero)
			{
				this.cursor = this.ToBitmap(true, true);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011879 File Offset: 0x0000FA79
		internal Cursor(StdCursor cursor)
			: this(XplatUI.DefineStdCursor(cursor))
		{
			this.std_cursor = cursor;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001188E File Offset: 0x0000FA8E
		private Cursor(SerializationInfo info, StreamingContext context)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001188E File Offset: 0x0000FA8E
		private Cursor()
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000118A0 File Offset: 0x0000FAA0
		~Cursor()
		{
			this.Dispose();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified Windows handle.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that represents the Windows handle of the cursor to create. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is <see cref="F:System.IntPtr.Zero" />. </exception>
		// Token: 0x06000492 RID: 1170 RVA: 0x000118CC File Offset: 0x0000FACC
		public Cursor(IntPtr handle)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			this.handle = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream to load the <see cref="T:System.Windows.Forms.Cursor" /> from. </param>
		// Token: 0x06000493 RID: 1171 RVA: 0x000118E2 File Offset: 0x0000FAE2
		public Cursor(Stream stream)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			this.CreateCursor(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified resource with the specified resource type.</summary>
		/// <param name="type">The resource <see cref="T:System.Type" />. </param>
		/// <param name="resource">The name of the resource. </param>
		// Token: 0x06000494 RID: 1172 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public Cursor(Type type, string resource)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			using (Stream manifestResourceStream = type.Assembly.GetManifestResourceStream(type, resource))
			{
				if (manifestResourceStream != null)
				{
					this.CreateCursor(manifestResourceStream);
					return;
				}
			}
			using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
			{
				if (manifestResourceStream2 != null)
				{
					this.CreateCursor(manifestResourceStream2);
					return;
				}
			}
			throw new FileNotFoundException("Resource name was not found: `" + resource + "'");
		}

		/// <summary>Gets or sets the cursor's position.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the cursor's position in screen coordinates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00011990 File Offset: 0x0000FB90
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000119B2 File Offset: 0x0000FBB2
		public static Point Position
		{
			get
			{
				int num;
				int num2;
				XplatUI.GetCursorPos(IntPtr.Zero, out num, out num2);
				return new Point(num, num2);
			}
			set
			{
				XplatUI.SetCursorPos(IntPtr.Zero, value.X, value.Y);
			}
		}

		/// <summary>Gets the handle of the cursor.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
		/// <exception cref="T:System.Exception">The handle value is <see cref="F:System.IntPtr.Zero" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000119CC File Offset: 0x0000FBCC
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal.</summary>
		/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000498 RID: 1176 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public static bool operator !=(Cursor left, Cursor right)
		{
			return left != right && (left == null || right == null || !(left.handle == right.handle));
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal.</summary>
		/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000499 RID: 1177 RVA: 0x000119FA File Offset: 0x0000FBFA
		public static bool operator ==(Cursor left, Cursor right)
		{
			return left == right || (left != null && right != null && left.handle == right.handle);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600049A RID: 1178 RVA: 0x00011A20 File Offset: 0x0000FC20
		public void Dispose()
		{
			if (this.cursor != null)
			{
				this.cursor.Dispose();
				this.cursor = null;
			}
			if (this.shape != null)
			{
				this.shape.Dispose();
				this.shape = null;
			}
			if (this.mask != null)
			{
				this.mask.Dispose();
				this.mask = null;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Returns a value indicating whether this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>true if this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600049B RID: 1179 RVA: 0x00011A81 File Offset: 0x0000FC81
		public override bool Equals(object obj)
		{
			return obj is Cursor && ((Cursor)obj).handle == this.handle;
		}

		/// <summary>Retrieves the hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600049C RID: 1180 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Retrieves a human readable string representing this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents this <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600049D RID: 1181 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		public override string ToString()
		{
			if (this.name != null)
			{
				return "[Cursor:" + this.name + "]";
			}
			throw new FormatException("Cannot convert custom cursors to string.");
		}

		/// <summary>Serializes the object.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> class.</param>
		// Token: 0x0600049E RID: 1182 RVA: 0x00011ADC File Offset: 0x0000FCDC
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			Cursor.CursorImage cursorImage = this.cursor_data[this.id];
			binaryWriter.Write(0);
			binaryWriter.Write(2);
			binaryWriter.Write(1);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].width);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].height);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].colorCount);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].reserved);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].xHotspot);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].yHotspot);
			binaryWriter.Write((uint)(40 + cursorImage.cursorColors.Length * 4 + cursorImage.cursorXOR.Length + cursorImage.cursorAND.Length));
			binaryWriter.Write(22U);
			binaryWriter.Write(cursorImage.cursorHeader.biSize);
			binaryWriter.Write(cursorImage.cursorHeader.biWidth);
			binaryWriter.Write(cursorImage.cursorHeader.biHeight);
			binaryWriter.Write(cursorImage.cursorHeader.biPlanes);
			binaryWriter.Write(cursorImage.cursorHeader.biBitCount);
			binaryWriter.Write(cursorImage.cursorHeader.biCompression);
			binaryWriter.Write(cursorImage.cursorHeader.biSizeImage);
			binaryWriter.Write(cursorImage.cursorHeader.biXPelsPerMeter);
			binaryWriter.Write(cursorImage.cursorHeader.biYPelsPerMeter);
			binaryWriter.Write(cursorImage.cursorHeader.biClrUsed);
			binaryWriter.Write(cursorImage.cursorHeader.biClrImportant);
			for (int i = 0; i < cursorImage.cursorColors.Length; i++)
			{
				binaryWriter.Write(cursorImage.cursorColors[i]);
			}
			binaryWriter.Write(cursorImage.cursorXOR);
			binaryWriter.Write(cursorImage.cursorAND);
			binaryWriter.Flush();
			si.AddValue("CursorData", memoryStream.ToArray());
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00011D1C File Offset: 0x0000FF1C
		private void InitFromStream(Stream stream)
		{
			if (stream == null || stream.Length == 0L)
			{
				throw new ArgumentException("The argument 'stream' must be a picture that can be used as a cursor", "stream");
			}
			BinaryReader binaryReader = new BinaryReader(stream);
			this.cursor_dir = default(Cursor.CursorDir);
			this.cursor_dir.idReserved = binaryReader.ReadUInt16();
			this.cursor_dir.idType = binaryReader.ReadUInt16();
			if (this.cursor_dir.idReserved != 0 || (this.cursor_dir.idType != 2 && this.cursor_dir.idType != 1))
			{
				throw new ArgumentException("Invalid Argument, format error", "stream");
			}
			ushort num = binaryReader.ReadUInt16();
			this.cursor_dir.idCount = num;
			this.cursor_dir.idEntries = new Cursor.CursorEntry[(int)num];
			this.cursor_data = new Cursor.CursorImage[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				Cursor.CursorEntry cursorEntry = default(Cursor.CursorEntry);
				cursorEntry.width = binaryReader.ReadByte();
				cursorEntry.height = binaryReader.ReadByte();
				cursorEntry.colorCount = binaryReader.ReadByte();
				cursorEntry.reserved = binaryReader.ReadByte();
				cursorEntry.xHotspot = binaryReader.ReadUInt16();
				cursorEntry.yHotspot = binaryReader.ReadUInt16();
				if (this.cursor_dir.idType == 1)
				{
					cursorEntry.xHotspot = (ushort)(cursorEntry.width / 2);
					cursorEntry.yHotspot = (ushort)(cursorEntry.height / 2);
				}
				cursorEntry.sizeInBytes = binaryReader.ReadUInt32();
				cursorEntry.fileOffset = binaryReader.ReadUInt32();
				this.cursor_dir.idEntries[i] = cursorEntry;
			}
			uint num2 = 0U;
			for (int j = 0; j < (int)num; j++)
			{
				if (this.cursor_dir.idEntries[j].sizeInBytes >= num2)
				{
					num2 = this.cursor_dir.idEntries[j].sizeInBytes;
					this.id = (int)((ushort)j);
					this.size.Height = (int)this.cursor_dir.idEntries[j].height;
					this.size.Width = (int)this.cursor_dir.idEntries[j].width;
				}
			}
			for (int k = 0; k < (int)num; k++)
			{
				Cursor.CursorImage cursorImage = default(Cursor.CursorImage);
				Cursor.CursorInfoHeader cursorInfoHeader = default(Cursor.CursorInfoHeader);
				stream.Seek((long)((ulong)this.cursor_dir.idEntries[k].fileOffset), SeekOrigin.Begin);
				byte[] array = new byte[this.cursor_dir.idEntries[k].sizeInBytes];
				stream.Read(array, 0, array.Length);
				BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(array));
				cursorInfoHeader.biSize = binaryReader2.ReadUInt32();
				if (cursorInfoHeader.biSize != 40U)
				{
					throw new ArgumentException("Invalid cursor file", "stream");
				}
				cursorInfoHeader.biWidth = binaryReader2.ReadInt32();
				cursorInfoHeader.biHeight = binaryReader2.ReadInt32();
				cursorInfoHeader.biPlanes = binaryReader2.ReadUInt16();
				cursorInfoHeader.biBitCount = binaryReader2.ReadUInt16();
				cursorInfoHeader.biCompression = binaryReader2.ReadUInt32();
				cursorInfoHeader.biSizeImage = binaryReader2.ReadUInt32();
				cursorInfoHeader.biXPelsPerMeter = binaryReader2.ReadInt32();
				cursorInfoHeader.biYPelsPerMeter = binaryReader2.ReadInt32();
				cursorInfoHeader.biClrUsed = binaryReader2.ReadUInt32();
				cursorInfoHeader.biClrImportant = binaryReader2.ReadUInt32();
				cursorImage.cursorHeader = cursorInfoHeader;
				ushort biBitCount = cursorInfoHeader.biBitCount;
				int num3;
				if (biBitCount != 1)
				{
					if (biBitCount != 4)
					{
						if (biBitCount != 8)
						{
							num3 = 0;
						}
						else
						{
							num3 = 256;
						}
					}
					else
					{
						num3 = 16;
					}
				}
				else
				{
					num3 = 2;
				}
				cursorImage.cursorColors = new uint[num3];
				for (int l = 0; l < num3; l++)
				{
					cursorImage.cursorColors[l] = binaryReader2.ReadUInt32();
				}
				int num4 = cursorInfoHeader.biHeight / 2;
				int num5 = (cursorInfoHeader.biWidth * (int)cursorInfoHeader.biPlanes * (int)cursorInfoHeader.biBitCount + 31 >> 5 << 2) * num4;
				cursorImage.cursorXOR = new byte[num5];
				for (int m = 0; m < num5; m++)
				{
					cursorImage.cursorXOR[m] = binaryReader2.ReadByte();
				}
				int num6 = (int)(binaryReader2.BaseStream.Length - binaryReader2.BaseStream.Position);
				cursorImage.cursorAND = new byte[num6];
				for (int n = 0; n < num6; n++)
				{
					cursorImage.cursorAND[n] = binaryReader2.ReadByte();
				}
				this.cursor_data[k] = cursorImage;
				binaryReader2.Close();
			}
			binaryReader.Close();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000121A8 File Offset: 0x000103A8
		private Bitmap ToBitmap(bool xor, bool transparent)
		{
			if (this.cursor_data == null)
			{
				return new Bitmap(32, 32);
			}
			Cursor.CursorImage cursorImage = this.cursor_data[this.id];
			Cursor.CursorInfoHeader cursorHeader = cursorImage.cursorHeader;
			int num = cursorHeader.biHeight / 2;
			Bitmap bitmap;
			if (!xor)
			{
				bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format1bppIndexed);
				ColorPalette colorPalette = bitmap.Palette;
				colorPalette.Entries[0] = Color.FromArgb(0, 0, 0);
				colorPalette.Entries[1] = Color.FromArgb(-1);
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
				for (int i = 0; i < num; i++)
				{
					Marshal.Copy(cursorImage.cursorAND, bitmapData.Stride * i, (IntPtr)(bitmapData.Scan0.ToInt64() + (long)(bitmapData.Stride * (num - 1 - i))), bitmapData.Stride);
				}
				bitmap.UnlockBits(bitmapData);
			}
			else
			{
				if (cursorHeader.biClrUsed == 0U)
				{
					ushort biBitCount = cursorHeader.biBitCount;
				}
				ushort biBitCount2 = cursorHeader.biBitCount;
				if (biBitCount2 <= 4)
				{
					if (biBitCount2 == 1)
					{
						bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format1bppIndexed);
						goto IL_01A8;
					}
					if (biBitCount2 == 4)
					{
						bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format4bppIndexed);
						goto IL_01A8;
					}
				}
				else
				{
					if (biBitCount2 == 8)
					{
						bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format8bppIndexed);
						goto IL_01A8;
					}
					if (biBitCount2 == 24 || biBitCount2 == 32)
					{
						bitmap = new Bitmap(cursorHeader.biWidth, num, PixelFormat.Format32bppArgb);
						goto IL_01A8;
					}
				}
				throw new Exception("Unexpected number of bits:" + cursorHeader.biBitCount.ToString());
				IL_01A8:
				if (cursorHeader.biBitCount < 24)
				{
					ColorPalette colorPalette = bitmap.Palette;
					for (int j = 0; j < cursorImage.cursorColors.Length; j++)
					{
						colorPalette.Entries[j] = Color.FromArgb((int)(cursorImage.cursorColors[j] | 4278190080U));
					}
					bitmap.Palette = colorPalette;
				}
				int num2 = ((cursorHeader.biWidth * (int)cursorHeader.biBitCount + 31) & -32) >> 3;
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
				for (int k = 0; k < num; k++)
				{
					Marshal.Copy(cursorImage.cursorXOR, num2 * k, (IntPtr)(bitmapData.Scan0.ToInt64() + (long)(bitmapData.Stride * (num - 1 - k))), num2);
				}
				bitmap.UnlockBits(bitmapData);
			}
			if (transparent)
			{
				bitmap = new Bitmap(bitmap);
				for (int l = 0; l < num; l++)
				{
					for (int m = 0; m < cursorHeader.biWidth / 8; m++)
					{
						for (int n = 7; n >= 0; n--)
						{
							if (((cursorImage.cursorAND[l * cursorHeader.biWidth / 8 + m] >> n) & 1) != 0)
							{
								bitmap.SetPixel(m * 8 + 7 - n, num - l - 1, Color.Transparent);
							}
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x0400025C RID: 604
		private Cursor.CursorDir cursor_dir;

		// Token: 0x0400025D RID: 605
		private Cursor.CursorImage[] cursor_data;

		// Token: 0x0400025E RID: 606
		private int id;

		// Token: 0x0400025F RID: 607
		internal IntPtr handle;

		// Token: 0x04000260 RID: 608
		private Size size;

		// Token: 0x04000261 RID: 609
		private Bitmap shape;

		// Token: 0x04000262 RID: 610
		private Bitmap mask;

		// Token: 0x04000263 RID: 611
		private Bitmap cursor;

		// Token: 0x04000264 RID: 612
		internal string name;

		// Token: 0x04000265 RID: 613
		private StdCursor std_cursor;

		// Token: 0x0200005F RID: 95
		private struct CursorDir
		{
			// Token: 0x04000266 RID: 614
			internal ushort idReserved;

			// Token: 0x04000267 RID: 615
			internal ushort idType;

			// Token: 0x04000268 RID: 616
			internal ushort idCount;

			// Token: 0x04000269 RID: 617
			internal Cursor.CursorEntry[] idEntries;
		}

		// Token: 0x02000060 RID: 96
		private struct CursorEntry
		{
			// Token: 0x0400026A RID: 618
			internal byte width;

			// Token: 0x0400026B RID: 619
			internal byte height;

			// Token: 0x0400026C RID: 620
			internal byte colorCount;

			// Token: 0x0400026D RID: 621
			internal byte reserved;

			// Token: 0x0400026E RID: 622
			internal ushort xHotspot;

			// Token: 0x0400026F RID: 623
			internal ushort yHotspot;

			// Token: 0x04000270 RID: 624
			internal ushort bitCount;

			// Token: 0x04000271 RID: 625
			internal uint sizeInBytes;

			// Token: 0x04000272 RID: 626
			internal uint fileOffset;
		}

		// Token: 0x02000061 RID: 97
		private struct CursorInfoHeader
		{
			// Token: 0x04000273 RID: 627
			internal uint biSize;

			// Token: 0x04000274 RID: 628
			internal int biWidth;

			// Token: 0x04000275 RID: 629
			internal int biHeight;

			// Token: 0x04000276 RID: 630
			internal ushort biPlanes;

			// Token: 0x04000277 RID: 631
			internal ushort biBitCount;

			// Token: 0x04000278 RID: 632
			internal uint biCompression;

			// Token: 0x04000279 RID: 633
			internal uint biSizeImage;

			// Token: 0x0400027A RID: 634
			internal int biXPelsPerMeter;

			// Token: 0x0400027B RID: 635
			internal int biYPelsPerMeter;

			// Token: 0x0400027C RID: 636
			internal uint biClrUsed;

			// Token: 0x0400027D RID: 637
			internal uint biClrImportant;
		}

		// Token: 0x02000062 RID: 98
		private struct CursorImage
		{
			// Token: 0x0400027E RID: 638
			internal Cursor.CursorInfoHeader cursorHeader;

			// Token: 0x0400027F RID: 639
			internal uint[] cursorColors;

			// Token: 0x04000280 RID: 640
			internal byte[] cursorXOR;

			// Token: 0x04000281 RID: 641
			internal byte[] cursorAND;
		}
	}
}
