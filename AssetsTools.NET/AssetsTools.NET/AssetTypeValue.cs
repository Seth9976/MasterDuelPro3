using System;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000054 RID: 84
	public class AssetTypeValue
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00012D20 File Offset: 0x00010F20
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00012D28 File Offset: 0x00010F28
		public AssetValueType ValueType { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00012D31 File Offset: 0x00010F31
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00012D39 File Offset: 0x00010F39
		private object Value { get; set; }

		// Token: 0x060002AB RID: 683 RVA: 0x00012D42 File Offset: 0x00010F42
		public AssetTypeValue(bool value)
		{
			this.ValueType = AssetValueType.Bool;
			this.Value = value;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00012D61 File Offset: 0x00010F61
		public AssetTypeValue(sbyte value)
		{
			this.ValueType = AssetValueType.Int8;
			this.Value = value;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00012D80 File Offset: 0x00010F80
		public AssetTypeValue(byte value)
		{
			this.ValueType = AssetValueType.UInt8;
			this.Value = value;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00012D9F File Offset: 0x00010F9F
		public AssetTypeValue(short value)
		{
			this.ValueType = AssetValueType.Int16;
			this.Value = value;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00012DBE File Offset: 0x00010FBE
		public AssetTypeValue(ushort value)
		{
			this.ValueType = AssetValueType.UInt16;
			this.Value = value;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00012DDD File Offset: 0x00010FDD
		public AssetTypeValue(int value)
		{
			this.ValueType = AssetValueType.Int32;
			this.Value = value;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00012DFC File Offset: 0x00010FFC
		public AssetTypeValue(uint value)
		{
			this.ValueType = AssetValueType.UInt32;
			this.Value = value;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00012E1B File Offset: 0x0001101B
		public AssetTypeValue(long value)
		{
			this.ValueType = AssetValueType.Int64;
			this.Value = value;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00012E3A File Offset: 0x0001103A
		public AssetTypeValue(ulong value)
		{
			this.ValueType = AssetValueType.UInt64;
			this.Value = value;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00012E5A File Offset: 0x0001105A
		public AssetTypeValue(float value)
		{
			this.ValueType = AssetValueType.Float;
			this.Value = value;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00012E7A File Offset: 0x0001107A
		public AssetTypeValue(double value)
		{
			this.ValueType = AssetValueType.Double;
			this.Value = value;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00012E9A File Offset: 0x0001109A
		public AssetTypeValue(string value)
		{
			this.ValueType = AssetValueType.String;
			this.Value = Encoding.UTF8.GetBytes(value);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00012EBF File Offset: 0x000110BF
		public AssetTypeValue(byte[] value, bool asString)
		{
			this.ValueType = (asString ? AssetValueType.String : AssetValueType.ByteArray);
			this.Value = value;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00012EE1 File Offset: 0x000110E1
		public AssetTypeValue(ManagedReferencesRegistry value)
		{
			this.ValueType = AssetValueType.ManagedReferencesRegistry;
			this.Value = value;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00012EFC File Offset: 0x000110FC
		public AssetTypeValue(AssetValueType valueType, object value = null)
		{
			this.ValueType = valueType;
			string text = value as string;
			bool flag = text != null;
			if (flag)
			{
				this.Value = Encoding.UTF8.GetBytes(text);
			}
			else
			{
				this.Value = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00012F44 File Offset: 0x00011144
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00012FA6 File Offset: 0x000111A6
		public bool AsBool
		{
			get
			{
				object obj = this.Value;
				bool flag;
				bool flag2;
				if (obj is bool)
				{
					flag = (bool)obj;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				bool flag4;
				if (flag3)
				{
					flag4 = flag;
				}
				else
				{
					obj = this.Value;
					byte b;
					bool flag5;
					if (obj is byte)
					{
						b = (byte)obj;
						flag5 = true;
					}
					else
					{
						flag5 = false;
					}
					bool flag6 = flag5;
					flag4 = flag6 && b == 1;
				}
				return flag4;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00012FB8 File Offset: 0x000111B8
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00013006 File Offset: 0x00011206
		public sbyte AsSByte
		{
			get
			{
				object value = this.Value;
				sbyte b;
				bool flag;
				if (value is sbyte)
				{
					b = (sbyte)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				sbyte b2;
				if (flag2)
				{
					b2 = b;
				}
				else
				{
					b2 = (sbyte)Convert.ChangeType(this.Value, typeof(sbyte));
				}
				return b2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00013018 File Offset: 0x00011218
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00013066 File Offset: 0x00011266
		public byte AsByte
		{
			get
			{
				object value = this.Value;
				byte b;
				bool flag;
				if (value is byte)
				{
					b = (byte)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				byte b2;
				if (flag2)
				{
					b2 = b;
				}
				else
				{
					b2 = (byte)Convert.ChangeType(this.Value, typeof(byte));
				}
				return b2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00013078 File Offset: 0x00011278
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000130C6 File Offset: 0x000112C6
		public short AsShort
		{
			get
			{
				object value = this.Value;
				short num;
				bool flag;
				if (value is short)
				{
					num = (short)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				short num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (short)Convert.ChangeType(this.Value, typeof(short));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000130D8 File Offset: 0x000112D8
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00013126 File Offset: 0x00011326
		public ushort AsUShort
		{
			get
			{
				object value = this.Value;
				ushort num;
				bool flag;
				if (value is ushort)
				{
					num = (ushort)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				ushort num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (ushort)Convert.ChangeType(this.Value, typeof(ushort));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00013138 File Offset: 0x00011338
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00013186 File Offset: 0x00011386
		public int AsInt
		{
			get
			{
				object value = this.Value;
				int num;
				bool flag;
				if (value is int)
				{
					num = (int)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				int num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (int)Convert.ChangeType(this.Value, typeof(int));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00013198 File Offset: 0x00011398
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x000131E6 File Offset: 0x000113E6
		public uint AsUInt
		{
			get
			{
				object value = this.Value;
				uint num;
				bool flag;
				if (value is uint)
				{
					num = (uint)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				uint num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (uint)Convert.ChangeType(this.Value, typeof(uint));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000131F8 File Offset: 0x000113F8
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00013246 File Offset: 0x00011446
		public long AsLong
		{
			get
			{
				object value = this.Value;
				long num;
				bool flag;
				if (value is long)
				{
					num = (long)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				long num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (long)Convert.ChangeType(this.Value, typeof(long));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00013258 File Offset: 0x00011458
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000132A6 File Offset: 0x000114A6
		public ulong AsULong
		{
			get
			{
				object value = this.Value;
				ulong num;
				bool flag;
				if (value is ulong)
				{
					num = (ulong)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				ulong num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (ulong)Convert.ChangeType(this.Value, typeof(ulong));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000132B8 File Offset: 0x000114B8
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00013306 File Offset: 0x00011506
		public float AsFloat
		{
			get
			{
				object value = this.Value;
				float num;
				bool flag;
				if (value is float)
				{
					num = (float)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				float num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (float)Convert.ChangeType(this.Value, typeof(float));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00013318 File Offset: 0x00011518
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00013366 File Offset: 0x00011566
		public double AsDouble
		{
			get
			{
				object value = this.Value;
				double num;
				bool flag;
				if (value is double)
				{
					num = (double)value;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				double num2;
				if (flag2)
				{
					num2 = num;
				}
				else
				{
					num2 = (double)Convert.ChangeType(this.Value, typeof(double));
				}
				return num2;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00013378 File Offset: 0x00011578
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00013406 File Offset: 0x00011606
		public string AsString
		{
			get
			{
				bool flag = this.ValueType == AssetValueType.String;
				string text;
				if (flag)
				{
					text = Encoding.UTF8.GetString((byte[])this.Value);
				}
				else
				{
					bool flag2 = this.ValueType == AssetValueType.Bool;
					if (flag2)
					{
						text = (((bool)this.Value) ? "true" : "false");
					}
					else
					{
						bool flag3 = this.ValueType == AssetValueType.ByteArray;
						if (flag3)
						{
							text = this.SimpleHexDump((byte[])this.Value);
						}
						else
						{
							text = this.Value.ToString();
						}
					}
				}
				return text;
			}
			set
			{
				this.Value = Encoding.UTF8.GetBytes(value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0001341A File Offset: 0x0001161A
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00013427 File Offset: 0x00011627
		public AssetTypeArrayInfo AsArray
		{
			get
			{
				return (AssetTypeArrayInfo)this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00013436 File Offset: 0x00011636
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00013443 File Offset: 0x00011643
		public byte[] AsByteArray
		{
			get
			{
				return (byte[])this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0001344D File Offset: 0x0001164D
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00013443 File Offset: 0x00011643
		public ManagedReferencesRegistry AsManagedReferencesRegistry
		{
			get
			{
				return (ManagedReferencesRegistry)this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0001345A File Offset: 0x0001165A
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00013464 File Offset: 0x00011664
		public object AsObject
		{
			get
			{
				return this.Value;
			}
			set
			{
				string text = value as string;
				bool flag = text != null;
				if (flag)
				{
					this.Value = Encoding.UTF8.GetBytes(text);
				}
				else
				{
					this.Value = value;
				}
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000134A0 File Offset: 0x000116A0
		public override string ToString()
		{
			return this.AsString;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000134B8 File Offset: 0x000116B8
		private string SimpleHexDump(byte[] byteArray)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = byteArray.Length == 0;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				int i;
				for (i = 0; i < byteArray.Length - 1; i++)
				{
					stringBuilder.Append(byteArray[i].ToString("x2"));
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(byteArray[i].ToString("x2"));
				text = stringBuilder.ToString();
			}
			return text;
		}
	}
}
