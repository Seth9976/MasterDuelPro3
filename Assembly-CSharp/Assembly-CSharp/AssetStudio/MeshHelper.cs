using System;

namespace AssetStudio
{
	// Token: 0x020000FB RID: 251
	public static class MeshHelper
	{
		// Token: 0x06000348 RID: 840 RVA: 0x000115C4 File Offset: 0x0000F7C4
		public static MeshHelper.VertexFormat ToVertexFormat(int format, int[] version)
		{
			if (version[0] < 2017)
			{
				switch (format)
				{
				case 0:
					return MeshHelper.VertexFormat.Float;
				case 1:
					return MeshHelper.VertexFormat.Float16;
				case 2:
					return MeshHelper.VertexFormat.UNorm8;
				case 3:
					return MeshHelper.VertexFormat.UInt8;
				case 4:
					return MeshHelper.VertexFormat.UInt32;
				default:
					throw new ArgumentOutOfRangeException("format", format, null);
				}
			}
			else
			{
				if (version[0] >= 2019)
				{
					return (MeshHelper.VertexFormat)format;
				}
				switch (format)
				{
				case 0:
					return MeshHelper.VertexFormat.Float;
				case 1:
					return MeshHelper.VertexFormat.Float16;
				case 2:
				case 3:
					return MeshHelper.VertexFormat.UNorm8;
				case 4:
					return MeshHelper.VertexFormat.SNorm8;
				case 5:
					return MeshHelper.VertexFormat.UNorm16;
				case 6:
					return MeshHelper.VertexFormat.SNorm16;
				case 7:
					return MeshHelper.VertexFormat.UInt8;
				case 8:
					return MeshHelper.VertexFormat.SInt8;
				case 9:
					return MeshHelper.VertexFormat.UInt16;
				case 10:
					return MeshHelper.VertexFormat.SInt16;
				case 11:
					return MeshHelper.VertexFormat.UInt32;
				case 12:
					return MeshHelper.VertexFormat.SInt32;
				default:
					throw new ArgumentOutOfRangeException("format", format, null);
				}
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001168C File Offset: 0x0000F88C
		public static uint GetFormatSize(MeshHelper.VertexFormat format)
		{
			switch (format)
			{
			case MeshHelper.VertexFormat.Float:
			case MeshHelper.VertexFormat.UInt32:
			case MeshHelper.VertexFormat.SInt32:
				return 4U;
			case MeshHelper.VertexFormat.Float16:
			case MeshHelper.VertexFormat.UNorm16:
			case MeshHelper.VertexFormat.SNorm16:
			case MeshHelper.VertexFormat.UInt16:
			case MeshHelper.VertexFormat.SInt16:
				return 2U;
			case MeshHelper.VertexFormat.UNorm8:
			case MeshHelper.VertexFormat.SNorm8:
			case MeshHelper.VertexFormat.UInt8:
			case MeshHelper.VertexFormat.SInt8:
				return 1U;
			default:
				throw new ArgumentOutOfRangeException("format", format, null);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000116E8 File Offset: 0x0000F8E8
		public static bool IsIntFormat(MeshHelper.VertexFormat format)
		{
			return format >= MeshHelper.VertexFormat.UInt8;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public static float[] BytesToFloatArray(byte[] inputBytes, MeshHelper.VertexFormat format)
		{
			uint size = MeshHelper.GetFormatSize(format);
			long len = (long)inputBytes.Length / (long)((ulong)size);
			float[] result = new float[len];
			int i = 0;
			while ((long)i < len)
			{
				switch (format)
				{
				case MeshHelper.VertexFormat.Float:
					result[i] = BitConverter.ToSingle(inputBytes, i * 4);
					break;
				case MeshHelper.VertexFormat.Float16:
					result[i] = Half.ToHalf(inputBytes, i * 2);
					break;
				case MeshHelper.VertexFormat.UNorm8:
					result[i] = (float)inputBytes[i] / 255f;
					break;
				case MeshHelper.VertexFormat.SNorm8:
					result[i] = Math.Max((float)((sbyte)inputBytes[i]) / 127f, -1f);
					break;
				case MeshHelper.VertexFormat.UNorm16:
					result[i] = (float)BitConverter.ToUInt16(inputBytes, i * 2) / 65535f;
					break;
				case MeshHelper.VertexFormat.SNorm16:
					result[i] = Math.Max((float)BitConverter.ToInt16(inputBytes, i * 2) / 32767f, -1f);
					break;
				}
				i++;
			}
			return result;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000117C8 File Offset: 0x0000F9C8
		public static int[] BytesToIntArray(byte[] inputBytes, MeshHelper.VertexFormat format)
		{
			uint size = MeshHelper.GetFormatSize(format);
			long len = (long)inputBytes.Length / (long)((ulong)size);
			int[] result = new int[len];
			int i = 0;
			while ((long)i < len)
			{
				switch (format)
				{
				case MeshHelper.VertexFormat.UInt8:
				case MeshHelper.VertexFormat.SInt8:
					result[i] = (int)inputBytes[i];
					break;
				case MeshHelper.VertexFormat.UInt16:
				case MeshHelper.VertexFormat.SInt16:
					result[i] = (int)BitConverter.ToInt16(inputBytes, i * 2);
					break;
				case MeshHelper.VertexFormat.UInt32:
				case MeshHelper.VertexFormat.SInt32:
					result[i] = BitConverter.ToInt32(inputBytes, i * 4);
					break;
				}
				i++;
			}
			return result;
		}

		// Token: 0x020000FC RID: 252
		public enum VertexChannelFormat
		{
			// Token: 0x0400073A RID: 1850
			Float,
			// Token: 0x0400073B RID: 1851
			Float16,
			// Token: 0x0400073C RID: 1852
			Color,
			// Token: 0x0400073D RID: 1853
			Byte,
			// Token: 0x0400073E RID: 1854
			UInt32
		}

		// Token: 0x020000FD RID: 253
		public enum VertexFormat2017
		{
			// Token: 0x04000740 RID: 1856
			Float,
			// Token: 0x04000741 RID: 1857
			Float16,
			// Token: 0x04000742 RID: 1858
			Color,
			// Token: 0x04000743 RID: 1859
			UNorm8,
			// Token: 0x04000744 RID: 1860
			SNorm8,
			// Token: 0x04000745 RID: 1861
			UNorm16,
			// Token: 0x04000746 RID: 1862
			SNorm16,
			// Token: 0x04000747 RID: 1863
			UInt8,
			// Token: 0x04000748 RID: 1864
			SInt8,
			// Token: 0x04000749 RID: 1865
			UInt16,
			// Token: 0x0400074A RID: 1866
			SInt16,
			// Token: 0x0400074B RID: 1867
			UInt32,
			// Token: 0x0400074C RID: 1868
			SInt32
		}

		// Token: 0x020000FE RID: 254
		public enum VertexFormat
		{
			// Token: 0x0400074E RID: 1870
			Float,
			// Token: 0x0400074F RID: 1871
			Float16,
			// Token: 0x04000750 RID: 1872
			UNorm8,
			// Token: 0x04000751 RID: 1873
			SNorm8,
			// Token: 0x04000752 RID: 1874
			UNorm16,
			// Token: 0x04000753 RID: 1875
			SNorm16,
			// Token: 0x04000754 RID: 1876
			UInt8,
			// Token: 0x04000755 RID: 1877
			SInt8,
			// Token: 0x04000756 RID: 1878
			UInt16,
			// Token: 0x04000757 RID: 1879
			SInt16,
			// Token: 0x04000758 RID: 1880
			UInt32,
			// Token: 0x04000759 RID: 1881
			SInt32
		}
	}
}
