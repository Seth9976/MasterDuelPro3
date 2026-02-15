using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AssetsTools.NET
{
	// Token: 0x02000055 RID: 85
	public class AssetTypeValueField : IEnumerable<AssetTypeValueField>, IEnumerable
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0001353B File Offset: 0x0001173B
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00013543 File Offset: 0x00011743
		public AssetTypeTemplateField TemplateField { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0001354C File Offset: 0x0001174C
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00013554 File Offset: 0x00011754
		public AssetTypeValue Value { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0001355D File Offset: 0x0001175D
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00013565 File Offset: 0x00011765
		public List<AssetTypeValueField> Children { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001356E File Offset: 0x0001176E
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00013576 File Offset: 0x00011776
		public bool IsDummy { get; set; }

		// Token: 0x060002E4 RID: 740 RVA: 0x0001357F File Offset: 0x0001177F
		public void Read(AssetTypeValue value, AssetTypeTemplateField templateField, List<AssetTypeValueField> children)
		{
			this.Value = value;
			this.TemplateField = templateField;
			this.Children = children;
			this.IsDummy = false;
		}

		// Token: 0x1700006E RID: 110
		public AssetTypeValueField this[string name]
		{
			get
			{
				bool isDummy = this.IsDummy;
				if (isDummy)
				{
					throw new DummyFieldAccessException("Cannot access fields of a dummy field!");
				}
				bool flag = name.Contains(".");
				AssetTypeValueField assetTypeValueField3;
				if (flag)
				{
					string[] array = name.Split(new char[] { '.' });
					AssetTypeValueField assetTypeValueField = this;
					foreach (string text in array)
					{
						bool flag2 = false;
						foreach (AssetTypeValueField assetTypeValueField2 in assetTypeValueField.Children)
						{
							bool flag3 = assetTypeValueField2.TemplateField.Name == text;
							if (flag3)
							{
								flag2 = true;
								assetTypeValueField = assetTypeValueField2;
								break;
							}
						}
						bool flag4 = !flag2;
						if (flag4)
						{
							return AssetTypeValueField.DUMMY_FIELD;
						}
					}
					assetTypeValueField3 = assetTypeValueField;
				}
				else
				{
					foreach (AssetTypeValueField assetTypeValueField4 in this.Children)
					{
						bool flag5 = assetTypeValueField4.TemplateField.Name == name;
						if (flag5)
						{
							return assetTypeValueField4;
						}
					}
					assetTypeValueField3 = AssetTypeValueField.DUMMY_FIELD;
				}
				return assetTypeValueField3;
			}
		}

		// Token: 0x1700006F RID: 111
		public AssetTypeValueField this[int index]
		{
			get
			{
				bool isDummy = this.IsDummy;
				if (isDummy)
				{
					throw new DummyFieldAccessException("Cannot access fields of a dummy field!");
				}
				return this.Children[index];
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00013738 File Offset: 0x00011938
		public AssetTypeValueField Get(string name)
		{
			return this[name];
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00013741 File Offset: 0x00011941
		public AssetTypeValueField Get(int index)
		{
			return this[index];
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001374C File Offset: 0x0001194C
		public static AssetValueType GetValueTypeByTypeName(string type)
		{
			if (type != null)
			{
				int length = type.Length;
				switch (length)
				{
				case 3:
					if (!(type == "int"))
					{
						goto IL_036D;
					}
					goto IL_0335;
				case 4:
				{
					char c = type[0];
					if (c != 'b')
					{
						if (c != 'c')
						{
							if (c != 'l')
							{
								goto IL_036D;
							}
							if (!(type == "long"))
							{
								goto IL_036D;
							}
							goto IL_033F;
						}
						else if (!(type == "char"))
						{
							goto IL_036D;
						}
					}
					else
					{
						if (!(type == "bool"))
						{
							goto IL_036D;
						}
						return AssetValueType.Bool;
					}
					break;
				}
				case 5:
				{
					char c = type[0];
					if (c <= 'U')
					{
						if (c != 'A')
						{
							switch (c)
							{
							case 'S':
								if (!(type == "SInt8"))
								{
									goto IL_036D;
								}
								break;
							case 'T':
								if (!(type == "Type*"))
								{
									goto IL_036D;
								}
								goto IL_0335;
							case 'U':
								if (!(type == "UInt8"))
								{
									goto IL_036D;
								}
								goto IL_0326;
							default:
								goto IL_036D;
							}
						}
						else
						{
							if (!(type == "Array"))
							{
								goto IL_036D;
							}
							return AssetValueType.Array;
						}
					}
					else if (c != 'f')
					{
						if (c != 's')
						{
							goto IL_036D;
						}
						if (!(type == "short"))
						{
							goto IL_036D;
						}
						goto IL_032B;
					}
					else
					{
						if (!(type == "float"))
						{
							goto IL_036D;
						}
						return AssetValueType.Float;
					}
					break;
				}
				case 6:
				{
					char c = type[4];
					if (c <= '3')
					{
						if (c != '1')
						{
							if (c != '3')
							{
								goto IL_036D;
							}
							if (type == "SInt32")
							{
								goto IL_0335;
							}
							if (!(type == "UInt32"))
							{
								goto IL_036D;
							}
							goto IL_033A;
						}
						else
						{
							if (type == "SInt16")
							{
								goto IL_032B;
							}
							if (!(type == "UInt16"))
							{
								goto IL_036D;
							}
							goto IL_0330;
						}
					}
					else if (c != '6')
					{
						if (c != 'l')
						{
							if (c != 'n')
							{
								goto IL_036D;
							}
							if (!(type == "string"))
							{
								goto IL_036D;
							}
							return AssetValueType.String;
						}
						else
						{
							if (!(type == "double"))
							{
								goto IL_036D;
							}
							return AssetValueType.Double;
						}
					}
					else
					{
						if (type == "SInt64")
						{
							goto IL_033F;
						}
						if (!(type == "UInt64"))
						{
							goto IL_036D;
						}
						goto IL_0344;
					}
					break;
				}
				case 7:
				case 9:
				case 10:
				case 11:
				case 15:
				case 16:
				case 17:
					goto IL_036D;
				case 8:
					if (!(type == "FileSize"))
					{
						goto IL_036D;
					}
					goto IL_0344;
				case 12:
				{
					char c = type[0];
					if (c != 'T')
					{
						if (c != 'u')
						{
							goto IL_036D;
						}
						if (!(type == "unsigned int"))
						{
							goto IL_036D;
						}
						goto IL_033A;
					}
					else
					{
						if (!(type == "TypelessData"))
						{
							goto IL_036D;
						}
						return AssetValueType.ByteArray;
					}
					break;
				}
				case 13:
					if (!(type == "unsigned char"))
					{
						goto IL_036D;
					}
					goto IL_0326;
				case 14:
					if (!(type == "unsigned short"))
					{
						goto IL_036D;
					}
					goto IL_0330;
				case 18:
					if (!(type == "unsigned long long"))
					{
						goto IL_036D;
					}
					goto IL_0344;
				default:
					if (length != 25)
					{
						goto IL_036D;
					}
					if (!(type == "ManagedReferencesRegistry"))
					{
						goto IL_036D;
					}
					return AssetValueType.ManagedReferencesRegistry;
				}
				return AssetValueType.Int8;
				IL_0326:
				return AssetValueType.UInt8;
				IL_032B:
				return AssetValueType.Int16;
				IL_0330:
				return AssetValueType.UInt16;
				IL_0335:
				return AssetValueType.Int32;
				IL_033A:
				return AssetValueType.UInt32;
				IL_033F:
				return AssetValueType.Int64;
				IL_0344:
				return AssetValueType.UInt64;
			}
			IL_036D:
			return AssetValueType.None;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00013AD0 File Offset: 0x00011CD0
		public void Write(AssetsFileWriter writer)
		{
			bool isArray = this.TemplateField.IsArray;
			if (isArray)
			{
				bool flag = this.TemplateField.ValueType == AssetValueType.ByteArray;
				if (flag)
				{
					byte[] asByteArray = this.AsByteArray;
					writer.Write(asByteArray.Length);
					writer.Write(asByteArray);
					bool isAligned = this.TemplateField.IsAligned;
					if (isAligned)
					{
						writer.Align();
					}
				}
				else
				{
					int count = this.Children.Count;
					writer.Write(count);
					for (int i = 0; i < count; i++)
					{
						this[i].Write(writer);
					}
					bool isAligned2 = this.TemplateField.IsAligned;
					if (isAligned2)
					{
						writer.Align();
					}
				}
			}
			else
			{
				bool flag2 = this.Children.Count == 0;
				if (flag2)
				{
					switch (this.TemplateField.ValueType)
					{
					case AssetValueType.Bool:
					{
						writer.Write(this.AsBool);
						bool isAligned3 = this.TemplateField.IsAligned;
						if (isAligned3)
						{
							writer.Align();
						}
						break;
					}
					case AssetValueType.Int8:
					{
						writer.Write(this.AsSByte);
						bool isAligned4 = this.TemplateField.IsAligned;
						if (isAligned4)
						{
							writer.Align();
						}
						break;
					}
					case AssetValueType.UInt8:
					{
						writer.Write(this.AsByte);
						bool isAligned5 = this.TemplateField.IsAligned;
						if (isAligned5)
						{
							writer.Align();
						}
						break;
					}
					case AssetValueType.Int16:
					{
						writer.Write(this.AsShort);
						bool isAligned6 = this.TemplateField.IsAligned;
						if (isAligned6)
						{
							writer.Align();
						}
						break;
					}
					case AssetValueType.UInt16:
					{
						writer.Write(this.AsUShort);
						bool isAligned7 = this.TemplateField.IsAligned;
						if (isAligned7)
						{
							writer.Align();
						}
						break;
					}
					case AssetValueType.Int32:
						writer.Write(this.AsInt);
						break;
					case AssetValueType.UInt32:
						writer.Write(this.AsUInt);
						break;
					case AssetValueType.Int64:
						writer.Write(this.AsLong);
						break;
					case AssetValueType.UInt64:
						writer.Write(this.AsULong);
						break;
					case AssetValueType.Float:
						writer.Write(this.AsFloat);
						break;
					case AssetValueType.Double:
						writer.Write(this.AsDouble);
						break;
					case AssetValueType.String:
						writer.Write(this.AsByteArray.Length);
						writer.Write(this.AsByteArray);
						writer.Align();
						break;
					case AssetValueType.ManagedReferencesRegistry:
					{
						writer.Write(this.AsManagedReferencesRegistry.version);
						int count2 = this.AsManagedReferencesRegistry.references.Count;
						for (int j = 0; j < count2; j++)
						{
							AssetTypeReferencedObject assetTypeReferencedObject = this.AsManagedReferencesRegistry.references[j];
							bool flag3 = this.AsManagedReferencesRegistry.version != 1;
							if (flag3)
							{
								writer.Write(assetTypeReferencedObject.rid);
							}
							assetTypeReferencedObject.type.WriteAsset(writer);
							assetTypeReferencedObject.data.Write(writer);
						}
						bool flag4 = this.AsManagedReferencesRegistry.version == 1;
						if (flag4)
						{
							AssetTypeReference.TERMINUS.WriteAsset(writer);
						}
						break;
					}
					}
				}
				else
				{
					for (int k = 0; k < this.Children.Count; k++)
					{
						this[k].Write(writer);
					}
					bool isAligned8 = this.TemplateField.IsAligned;
					if (isAligned8)
					{
						writer.Align();
					}
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00013E88 File Offset: 0x00012088
		public byte[] WriteToByteArray(bool bigEndian = false)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (AssetsFileWriter assetsFileWriter = new AssetsFileWriter(memoryStream))
				{
					assetsFileWriter.BigEndian = bigEndian;
					this.Write(assetsFileWriter);
					array = memoryStream.ToArray();
				}
			}
			return array;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00013EF8 File Offset: 0x000120F8
		public IEnumerator<AssetTypeValueField> GetEnumerator()
		{
			return this.Children.GetEnumerator();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00013F1C File Offset: 0x0001211C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Children.GetEnumerator();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00013F40 File Offset: 0x00012140
		public override string ToString()
		{
			bool flag = this.TemplateField != null;
			string text;
			if (flag)
			{
				text = this.TemplateField.ToString();
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00013F6E File Offset: 0x0001216E
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00013F7B File Offset: 0x0001217B
		public bool AsBool
		{
			get
			{
				return this.Value.AsBool;
			}
			set
			{
				this.Value.AsBool = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00013F8A File Offset: 0x0001218A
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00013F97 File Offset: 0x00012197
		public sbyte AsSByte
		{
			get
			{
				return this.Value.AsSByte;
			}
			set
			{
				this.Value.AsSByte = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00013FA6 File Offset: 0x000121A6
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00013FB3 File Offset: 0x000121B3
		public byte AsByte
		{
			get
			{
				return this.Value.AsByte;
			}
			set
			{
				this.Value.AsByte = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00013FC2 File Offset: 0x000121C2
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00013FCF File Offset: 0x000121CF
		public short AsShort
		{
			get
			{
				return this.Value.AsShort;
			}
			set
			{
				this.Value.AsShort = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00013FDE File Offset: 0x000121DE
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00013FEB File Offset: 0x000121EB
		public ushort AsUShort
		{
			get
			{
				return this.Value.AsUShort;
			}
			set
			{
				this.Value.AsUShort = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00013FFA File Offset: 0x000121FA
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00014007 File Offset: 0x00012207
		public int AsInt
		{
			get
			{
				return this.Value.AsInt;
			}
			set
			{
				this.Value.AsInt = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00014016 File Offset: 0x00012216
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00014023 File Offset: 0x00012223
		public uint AsUInt
		{
			get
			{
				return this.Value.AsUInt;
			}
			set
			{
				this.Value.AsUInt = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00014032 File Offset: 0x00012232
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0001403F File Offset: 0x0001223F
		public long AsLong
		{
			get
			{
				return this.Value.AsLong;
			}
			set
			{
				this.Value.AsLong = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0001404E File Offset: 0x0001224E
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0001405B File Offset: 0x0001225B
		public ulong AsULong
		{
			get
			{
				return this.Value.AsULong;
			}
			set
			{
				this.Value.AsULong = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0001406A File Offset: 0x0001226A
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00014077 File Offset: 0x00012277
		public float AsFloat
		{
			get
			{
				return this.Value.AsFloat;
			}
			set
			{
				this.Value.AsFloat = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00014086 File Offset: 0x00012286
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00014093 File Offset: 0x00012293
		public double AsDouble
		{
			get
			{
				return this.Value.AsDouble;
			}
			set
			{
				this.Value.AsDouble = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000140A2 File Offset: 0x000122A2
		// (set) Token: 0x06000306 RID: 774 RVA: 0x000140AF File Offset: 0x000122AF
		public string AsString
		{
			get
			{
				return this.Value.AsString;
			}
			set
			{
				this.Value.AsString = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000140BE File Offset: 0x000122BE
		// (set) Token: 0x06000308 RID: 776 RVA: 0x000140CB File Offset: 0x000122CB
		public object AsObject
		{
			get
			{
				return this.Value.AsObject;
			}
			set
			{
				this.Value.AsObject = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000140DA File Offset: 0x000122DA
		// (set) Token: 0x0600030A RID: 778 RVA: 0x000140E7 File Offset: 0x000122E7
		public AssetTypeArrayInfo AsArray
		{
			get
			{
				return this.Value.AsArray;
			}
			set
			{
				this.Value.AsArray = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000140F6 File Offset: 0x000122F6
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00014103 File Offset: 0x00012303
		public byte[] AsByteArray
		{
			get
			{
				return this.Value.AsByteArray;
			}
			set
			{
				this.Value.AsByteArray = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00014112 File Offset: 0x00012312
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0001411F File Offset: 0x0001231F
		public ManagedReferencesRegistry AsManagedReferencesRegistry
		{
			get
			{
				return this.Value.AsManagedReferencesRegistry;
			}
			set
			{
				this.Value.AsManagedReferencesRegistry = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0001412E File Offset: 0x0001232E
		public string TypeName
		{
			get
			{
				return this.TemplateField.Type;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001413B File Offset: 0x0001233B
		public string FieldName
		{
			get
			{
				return this.TemplateField.Name;
			}
		}

		// Token: 0x040001E5 RID: 485
		public static readonly AssetTypeValueField DUMMY_FIELD = new AssetTypeValueField
		{
			TemplateField = new AssetTypeTemplateField
			{
				Name = "DUMMY",
				HasValue = false,
				IsAligned = false,
				IsArray = false,
				Type = "DUMMY",
				ValueType = AssetValueType.None,
				Children = new List<AssetTypeTemplateField>(0)
			},
			Value = null,
			IsDummy = true,
			Children = new List<AssetTypeValueField>(0)
		};
	}
}
