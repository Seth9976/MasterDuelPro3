using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Resources
{
	// Token: 0x020005DE RID: 1502
	[DefaultMember("Item")]
	internal class Win32VersionResource : Win32Resource
	{
		// Token: 0x06002C6C RID: 11372 RVA: 0x000B0D14 File Offset: 0x000AEF14
		public Win32VersionResource(int id, int language, bool compilercontext)
			: base(Win32ResourceType.RT_VERSION, id, language)
		{
			this.signature = (long)((ulong)(-17890115));
			this.struct_version = 65536;
			this.file_flags_mask = 63;
			this.file_flags = 0;
			this.file_os = 4;
			this.file_type = 2;
			this.file_subtype = 0;
			this.file_date = 0L;
			this.file_lang = (compilercontext ? 0 : 127);
			this.file_codepage = 1200;
			this.properties = new Hashtable();
			string text = (compilercontext ? string.Empty : " ");
			foreach (string text2 in this.WellKnownProperties)
			{
				this.properties[text2] = text;
			}
			this.LegalCopyright = " ";
			this.FileDescription = " ";
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000B0E2C File Offset: 0x000AF02C
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x000B0EC0 File Offset: 0x000AF0C0
		public string Version
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.file_version >> 48).ToString(),
					".",
					((this.file_version >> 32) & 65535L).ToString(),
					".",
					((this.file_version >> 16) & 65535L).ToString(),
					".",
					(this.file_version & 65535L).ToString()
				});
			}
			set
			{
				long[] array = new long[4];
				if (value != null)
				{
					string[] array2 = value.Split('.', StringSplitOptions.None);
					try
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (i < array.Length)
							{
								array[i] = (long)int.Parse(array2[i]);
							}
						}
					}
					catch (FormatException)
					{
					}
				}
				this.file_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
				this.properties["FileVersion"] = this.Version;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (set) Token: 0x06002C6F RID: 11375 RVA: 0x000B0F4C File Offset: 0x000AF14C
		public virtual string Comments
		{
			set
			{
				this.properties["Comments"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000B0F73 File Offset: 0x000AF173
		public virtual string CompanyName
		{
			set
			{
				this.properties["CompanyName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000B0F9A File Offset: 0x000AF19A
		public virtual string LegalCopyright
		{
			set
			{
				this.properties["LegalCopyright"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (set) Token: 0x06002C72 RID: 11378 RVA: 0x000B0FC1 File Offset: 0x000AF1C1
		public virtual string LegalTrademarks
		{
			set
			{
				this.properties["LegalTrademarks"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (set) Token: 0x06002C73 RID: 11379 RVA: 0x000B0FE8 File Offset: 0x000AF1E8
		public virtual string OriginalFilename
		{
			set
			{
				this.properties["OriginalFilename"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x000B100F File Offset: 0x000AF20F
		public virtual string ProductName
		{
			set
			{
				this.properties["ProductName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (set) Token: 0x06002C75 RID: 11381 RVA: 0x000B1038 File Offset: 0x000AF238
		public virtual string ProductVersion
		{
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split('.', StringSplitOptions.None);
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["ProductVersion"] = value;
				this.product_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (set) Token: 0x06002C76 RID: 11382 RVA: 0x000B10CC File Offset: 0x000AF2CC
		public virtual string InternalName
		{
			set
			{
				this.properties["InternalName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x000B10F3 File Offset: 0x000AF2F3
		public virtual string FileDescription
		{
			set
			{
				this.properties["FileDescription"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000B111A File Offset: 0x000AF31A
		public virtual int FileLanguage
		{
			set
			{
				this.file_lang = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x000B1124 File Offset: 0x000AF324
		public virtual string FileVersion
		{
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split('.', StringSplitOptions.None);
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["FileVersion"] = value;
				this.file_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000B11B8 File Offset: 0x000AF3B8
		private void emit_padding(BinaryWriter w)
		{
			if (w.BaseStream.Position % 4L != 0L)
			{
				w.Write(0);
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000B11D4 File Offset: 0x000AF3D4
		private void patch_length(BinaryWriter w, long len_pos)
		{
			Stream baseStream = w.BaseStream;
			long position = baseStream.Position;
			baseStream.Position = len_pos;
			w.Write((short)(position - len_pos));
			baseStream.Position = position;
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000B1208 File Offset: 0x000AF408
		public override void WriteTo(Stream ms)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(ms, Encoding.Unicode))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(52);
				binaryWriter.Write(0);
				binaryWriter.Write("VS_VERSION_INFO".ToCharArray());
				binaryWriter.Write(0);
				this.emit_padding(binaryWriter);
				binaryWriter.Write((uint)this.signature);
				binaryWriter.Write(this.struct_version);
				binaryWriter.Write((int)(this.file_version >> 32));
				binaryWriter.Write((int)(this.file_version & (long)((ulong)(-1))));
				binaryWriter.Write((int)(this.product_version >> 32));
				binaryWriter.Write((int)(this.product_version & (long)((ulong)(-1))));
				binaryWriter.Write(this.file_flags_mask);
				binaryWriter.Write(this.file_flags);
				binaryWriter.Write(this.file_os);
				binaryWriter.Write(this.file_type);
				binaryWriter.Write(this.file_subtype);
				binaryWriter.Write((int)(this.file_date >> 32));
				binaryWriter.Write((int)(this.file_date & (long)((ulong)(-1))));
				this.emit_padding(binaryWriter);
				long position = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("VarFileInfo".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				long position2 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(4);
				binaryWriter.Write(0);
				binaryWriter.Write("Translation".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write((short)this.file_lang);
				binaryWriter.Write((short)this.file_codepage);
				this.patch_length(binaryWriter, position2);
				this.patch_length(binaryWriter, position);
				long position3 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("StringFileInfo".ToCharArray());
				this.emit_padding(binaryWriter);
				long position4 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write(string.Format("{0:x4}{1:x4}", this.file_lang, this.file_codepage).ToCharArray());
				this.emit_padding(binaryWriter);
				foreach (object obj in this.properties.Keys)
				{
					string text = (string)obj;
					string text2 = (string)this.properties[text];
					long position5 = ms.Position;
					binaryWriter.Write(0);
					binaryWriter.Write((short)(text2.ToCharArray().Length + 1));
					binaryWriter.Write(1);
					binaryWriter.Write(text.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					binaryWriter.Write(text2.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					this.patch_length(binaryWriter, position5);
				}
				this.patch_length(binaryWriter, position4);
				this.patch_length(binaryWriter, position3);
				this.patch_length(binaryWriter, 0L);
			}
		}

		// Token: 0x040016A4 RID: 5796
		public string[] WellKnownProperties = new string[] { "Comments", "CompanyName", "FileVersion", "InternalName", "LegalTrademarks", "OriginalFilename", "ProductName", "ProductVersion" };

		// Token: 0x040016A5 RID: 5797
		private long signature;

		// Token: 0x040016A6 RID: 5798
		private int struct_version;

		// Token: 0x040016A7 RID: 5799
		private long file_version;

		// Token: 0x040016A8 RID: 5800
		private long product_version;

		// Token: 0x040016A9 RID: 5801
		private int file_flags_mask;

		// Token: 0x040016AA RID: 5802
		private int file_flags;

		// Token: 0x040016AB RID: 5803
		private int file_os;

		// Token: 0x040016AC RID: 5804
		private int file_type;

		// Token: 0x040016AD RID: 5805
		private int file_subtype;

		// Token: 0x040016AE RID: 5806
		private long file_date;

		// Token: 0x040016AF RID: 5807
		private int file_lang;

		// Token: 0x040016B0 RID: 5808
		private int file_codepage;

		// Token: 0x040016B1 RID: 5809
		private Hashtable properties;
	}
}
