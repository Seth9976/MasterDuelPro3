using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x02000146 RID: 326
	internal static class HIDParser
	{
		// Token: 0x06000E7C RID: 3708 RVA: 0x00049CD8 File Offset: 0x00047ED8
		public unsafe static bool ParseReportDescriptor(byte[] buffer, ref HID.HIDDeviceDescriptor deviceDescriptor)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte* bufferPtr;
			if (buffer == null || buffer.Length == 0)
			{
				bufferPtr = null;
			}
			else
			{
				bufferPtr = &buffer[0];
			}
			return HIDParser.ParseReportDescriptor(bufferPtr, buffer.Length, ref deviceDescriptor);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00049D18 File Offset: 0x00047F18
		public unsafe static bool ParseReportDescriptor(byte* bufferPtr, int bufferLength, ref HID.HIDDeviceDescriptor deviceDescriptor)
		{
			HIDParser.HIDItemStateLocal localItemState = default(HIDParser.HIDItemStateLocal);
			HIDParser.HIDItemStateGlobal globalItemState = default(HIDParser.HIDItemStateGlobal);
			List<HIDParser.HIDReportData> reports = new List<HIDParser.HIDReportData>();
			List<HID.HIDElementDescriptor> elements = new List<HID.HIDElementDescriptor>();
			List<HID.HIDCollectionDescriptor> collections = new List<HID.HIDCollectionDescriptor>();
			int currentCollection = -1;
			byte* endPtr = bufferPtr + bufferLength;
			byte* currentPtr = bufferPtr;
			while (currentPtr < endPtr)
			{
				byte b = *currentPtr;
				if (b == 254)
				{
					throw new NotImplementedException("long item support");
				}
				byte itemSize = b & 3;
				byte itemTypeAndTag = b & 252;
				currentPtr++;
				if (itemTypeAndTag <= 84)
				{
					if (itemTypeAndTag <= 24)
					{
						if (itemTypeAndTag <= 8)
						{
							if (itemTypeAndTag != 4)
							{
								if (itemTypeAndTag == 8)
								{
									localItemState.SetUsage(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
								}
							}
							else
							{
								globalItemState.usagePage = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							}
						}
						else if (itemTypeAndTag != 20)
						{
							if (itemTypeAndTag == 24)
							{
								localItemState.usageMinimum = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							}
						}
						else
						{
							globalItemState.logicalMinimum = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
						}
					}
					else if (itemTypeAndTag <= 40)
					{
						if (itemTypeAndTag != 36)
						{
							if (itemTypeAndTag == 40)
							{
								localItemState.usageMaximum = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							}
						}
						else
						{
							globalItemState.logicalMaximum = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
						}
					}
					else if (itemTypeAndTag != 52)
					{
						if (itemTypeAndTag != 68)
						{
							if (itemTypeAndTag == 84)
							{
								globalItemState.unitExponent = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							}
						}
						else
						{
							globalItemState.physicalMaximum = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
						}
					}
					else
					{
						globalItemState.physicalMinimum = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
					}
				}
				else
				{
					if (itemTypeAndTag <= 132)
					{
						if (itemTypeAndTag <= 116)
						{
							if (itemTypeAndTag == 100)
							{
								globalItemState.unit = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
								goto IL_0521;
							}
							if (itemTypeAndTag != 116)
							{
								goto IL_0521;
							}
							globalItemState.reportSize = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							goto IL_0521;
						}
						else if (itemTypeAndTag != 128)
						{
							if (itemTypeAndTag != 132)
							{
								goto IL_0521;
							}
							globalItemState.reportId = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							goto IL_0521;
						}
					}
					else if (itemTypeAndTag <= 148)
					{
						if (itemTypeAndTag != 144)
						{
							if (itemTypeAndTag != 148)
							{
								goto IL_0521;
							}
							globalItemState.reportCount = new int?(HIDParser.ReadData((int)itemSize, currentPtr, endPtr));
							goto IL_0521;
						}
					}
					else
					{
						if (itemTypeAndTag == 160)
						{
							int parentCollection = currentCollection;
							currentCollection = collections.Count;
							collections.Add(new HID.HIDCollectionDescriptor
							{
								type = (HID.HIDCollectionType)HIDParser.ReadData((int)itemSize, currentPtr, endPtr),
								parent = parentCollection,
								usagePage = globalItemState.GetUsagePage(0, ref localItemState),
								usage = localItemState.GetUsage(0),
								firstChild = elements.Count
							});
							HIDParser.HIDItemStateLocal.Reset(ref localItemState);
							goto IL_0521;
						}
						if (itemTypeAndTag != 176)
						{
							if (itemTypeAndTag != 192)
							{
								goto IL_0521;
							}
							if (currentCollection == -1)
							{
								return false;
							}
							HID.HIDCollectionDescriptor collection = collections[currentCollection];
							collection.childCount = elements.Count - collection.firstChild;
							collections[currentCollection] = collection;
							currentCollection = collection.parent;
							HIDParser.HIDItemStateLocal.Reset(ref localItemState);
							goto IL_0521;
						}
					}
					HID.HIDReportType reportType = ((itemTypeAndTag == 128) ? HID.HIDReportType.Input : ((itemTypeAndTag == 144) ? HID.HIDReportType.Output : HID.HIDReportType.Feature));
					int reportIndex = HIDParser.HIDReportData.FindOrAddReport(globalItemState.reportId, reportType, reports);
					HIDParser.HIDReportData report = reports[reportIndex];
					if (report.currentBitOffset == 0 && globalItemState.reportId != null)
					{
						report.currentBitOffset = 8;
					}
					int reportCount = globalItemState.reportCount.GetValueOrDefault(1);
					int flags = HIDParser.ReadData((int)itemSize, currentPtr, endPtr);
					for (int i = 0; i < reportCount; i++)
					{
						HID.HIDElementDescriptor element = new HID.HIDElementDescriptor
						{
							usage = (localItemState.GetUsage(i) & 65535),
							usagePage = globalItemState.GetUsagePage(i, ref localItemState),
							reportType = reportType,
							reportSizeInBits = globalItemState.reportSize.GetValueOrDefault(8),
							reportOffsetInBits = report.currentBitOffset,
							reportId = globalItemState.reportId.GetValueOrDefault(1),
							flags = (HID.HIDElementFlags)flags,
							logicalMin = globalItemState.logicalMinimum.GetValueOrDefault(0),
							logicalMax = globalItemState.logicalMaximum.GetValueOrDefault(0),
							physicalMin = globalItemState.GetPhysicalMin(),
							physicalMax = globalItemState.GetPhysicalMax(),
							unitExponent = globalItemState.unitExponent.GetValueOrDefault(0),
							unit = globalItemState.unit.GetValueOrDefault(0)
						};
						report.currentBitOffset += element.reportSizeInBits;
						elements.Add(element);
					}
					reports[reportIndex] = report;
					HIDParser.HIDItemStateLocal.Reset(ref localItemState);
				}
				IL_0521:
				if (itemSize == 3)
				{
					currentPtr += 4;
				}
				else
				{
					currentPtr += itemSize;
				}
			}
			deviceDescriptor.elements = elements.ToArray();
			deviceDescriptor.collections = collections.ToArray();
			foreach (HID.HIDCollectionDescriptor collection2 in collections)
			{
				if (collection2.parent == -1 && collection2.type == HID.HIDCollectionType.Application)
				{
					deviceDescriptor.usage = collection2.usage;
					deviceDescriptor.usagePage = collection2.usagePage;
					break;
				}
			}
			return true;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0004A2EC File Offset: 0x000484EC
		private unsafe static int ReadData(int itemSize, byte* currentPtr, byte* endPtr)
		{
			if (itemSize == 0)
			{
				return 0;
			}
			if (itemSize == 1)
			{
				if (currentPtr >= endPtr)
				{
					return 0;
				}
				return (int)(*currentPtr);
			}
			else if (itemSize == 2)
			{
				if (currentPtr + 2 >= endPtr)
				{
					return 0;
				}
				byte data = *currentPtr;
				return ((int)currentPtr[1] << 8) | (int)data;
			}
			else
			{
				if (itemSize != 3)
				{
					return 0;
				}
				if (currentPtr + 4 >= endPtr)
				{
					return 0;
				}
				byte data2 = *currentPtr;
				byte data3 = currentPtr[1];
				byte data4 = currentPtr[2];
				return ((int)currentPtr[3] << 24) | ((int)data4 << 24) | ((int)data3 << 8) | (int)data2;
			}
		}

		// Token: 0x02000147 RID: 327
		private struct HIDReportData
		{
			// Token: 0x06000E7F RID: 3711 RVA: 0x0004A350 File Offset: 0x00048550
			public static int FindOrAddReport(int? reportId, HID.HIDReportType reportType, List<HIDParser.HIDReportData> reports)
			{
				int id = 1;
				if (reportId != null)
				{
					id = reportId.Value;
				}
				for (int i = 0; i < reports.Count; i++)
				{
					if (reports[i].reportId == id && reports[i].reportType == reportType)
					{
						return i;
					}
				}
				reports.Add(new HIDParser.HIDReportData
				{
					reportId = id,
					reportType = reportType
				});
				return reports.Count - 1;
			}

			// Token: 0x04000821 RID: 2081
			public int reportId;

			// Token: 0x04000822 RID: 2082
			public HID.HIDReportType reportType;

			// Token: 0x04000823 RID: 2083
			public int currentBitOffset;
		}

		// Token: 0x02000148 RID: 328
		private enum HIDItemTypeAndTag
		{
			// Token: 0x04000825 RID: 2085
			Input = 128,
			// Token: 0x04000826 RID: 2086
			Output = 144,
			// Token: 0x04000827 RID: 2087
			Feature = 176,
			// Token: 0x04000828 RID: 2088
			Collection = 160,
			// Token: 0x04000829 RID: 2089
			EndCollection = 192,
			// Token: 0x0400082A RID: 2090
			UsagePage = 4,
			// Token: 0x0400082B RID: 2091
			LogicalMinimum = 20,
			// Token: 0x0400082C RID: 2092
			LogicalMaximum = 36,
			// Token: 0x0400082D RID: 2093
			PhysicalMinimum = 52,
			// Token: 0x0400082E RID: 2094
			PhysicalMaximum = 68,
			// Token: 0x0400082F RID: 2095
			UnitExponent = 84,
			// Token: 0x04000830 RID: 2096
			Unit = 100,
			// Token: 0x04000831 RID: 2097
			ReportSize = 116,
			// Token: 0x04000832 RID: 2098
			ReportID = 132,
			// Token: 0x04000833 RID: 2099
			ReportCount = 148,
			// Token: 0x04000834 RID: 2100
			Push = 164,
			// Token: 0x04000835 RID: 2101
			Pop = 180,
			// Token: 0x04000836 RID: 2102
			Usage = 8,
			// Token: 0x04000837 RID: 2103
			UsageMinimum = 24,
			// Token: 0x04000838 RID: 2104
			UsageMaximum = 40,
			// Token: 0x04000839 RID: 2105
			DesignatorIndex = 56,
			// Token: 0x0400083A RID: 2106
			DesignatorMinimum = 72,
			// Token: 0x0400083B RID: 2107
			DesignatorMaximum = 88,
			// Token: 0x0400083C RID: 2108
			StringIndex = 120,
			// Token: 0x0400083D RID: 2109
			StringMinimum = 136,
			// Token: 0x0400083E RID: 2110
			StringMaximum = 152,
			// Token: 0x0400083F RID: 2111
			Delimiter = 168
		}

		// Token: 0x02000149 RID: 329
		private struct HIDItemStateLocal
		{
			// Token: 0x06000E80 RID: 3712 RVA: 0x0004A3C8 File Offset: 0x000485C8
			public static void Reset(ref HIDParser.HIDItemStateLocal state)
			{
				List<int> usageList = state.usageList;
				state = default(HIDParser.HIDItemStateLocal);
				if (usageList != null)
				{
					usageList.Clear();
					state.usageList = usageList;
				}
			}

			// Token: 0x06000E81 RID: 3713 RVA: 0x0004A3F4 File Offset: 0x000485F4
			public void SetUsage(int value)
			{
				if (this.usage != null)
				{
					if (this.usageList == null)
					{
						this.usageList = new List<int>();
					}
					this.usageList.Add(this.usage.Value);
				}
				this.usage = new int?(value);
			}

			// Token: 0x06000E82 RID: 3714 RVA: 0x0004A444 File Offset: 0x00048644
			public int GetUsage(int index)
			{
				if (this.usageMinimum != null && this.usageMaximum != null)
				{
					int min = this.usageMinimum.Value;
					int max = this.usageMaximum.Value;
					int range = max - min;
					if (range < 0)
					{
						return 0;
					}
					if (index >= range)
					{
						return max;
					}
					return min + index;
				}
				else if (this.usageList != null && this.usageList.Count > 0)
				{
					int usageCount = this.usageList.Count;
					if (index >= usageCount)
					{
						return this.usage.Value;
					}
					return this.usageList[index];
				}
				else
				{
					if (this.usage != null)
					{
						return this.usage.Value;
					}
					return 0;
				}
			}

			// Token: 0x04000840 RID: 2112
			public int? usage;

			// Token: 0x04000841 RID: 2113
			public int? usageMinimum;

			// Token: 0x04000842 RID: 2114
			public int? usageMaximum;

			// Token: 0x04000843 RID: 2115
			public int? designatorIndex;

			// Token: 0x04000844 RID: 2116
			public int? designatorMinimum;

			// Token: 0x04000845 RID: 2117
			public int? designatorMaximum;

			// Token: 0x04000846 RID: 2118
			public int? stringIndex;

			// Token: 0x04000847 RID: 2119
			public int? stringMinimum;

			// Token: 0x04000848 RID: 2120
			public int? stringMaximum;

			// Token: 0x04000849 RID: 2121
			public List<int> usageList;
		}

		// Token: 0x0200014A RID: 330
		private struct HIDItemStateGlobal
		{
			// Token: 0x06000E83 RID: 3715 RVA: 0x0004A4F0 File Offset: 0x000486F0
			public HID.UsagePage GetUsagePage(int index, ref HIDParser.HIDItemStateLocal localItemState)
			{
				if (this.usagePage == null)
				{
					return (HID.UsagePage)(localItemState.GetUsage(index) >> 16);
				}
				return (HID.UsagePage)this.usagePage.Value;
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x0004A518 File Offset: 0x00048718
			public int GetPhysicalMin()
			{
				if (this.physicalMinimum == null || this.physicalMaximum == null || (this.physicalMinimum.Value == 0 && this.physicalMaximum.Value == 0))
				{
					return this.logicalMinimum.GetValueOrDefault(0);
				}
				return this.physicalMinimum.Value;
			}

			// Token: 0x06000E85 RID: 3717 RVA: 0x0004A574 File Offset: 0x00048774
			public int GetPhysicalMax()
			{
				if (this.physicalMinimum == null || this.physicalMaximum == null || (this.physicalMinimum.Value == 0 && this.physicalMaximum.Value == 0))
				{
					return this.logicalMaximum.GetValueOrDefault(0);
				}
				return this.physicalMaximum.Value;
			}

			// Token: 0x0400084A RID: 2122
			public int? usagePage;

			// Token: 0x0400084B RID: 2123
			public int? logicalMinimum;

			// Token: 0x0400084C RID: 2124
			public int? logicalMaximum;

			// Token: 0x0400084D RID: 2125
			public int? physicalMinimum;

			// Token: 0x0400084E RID: 2126
			public int? physicalMaximum;

			// Token: 0x0400084F RID: 2127
			public int? unitExponent;

			// Token: 0x04000850 RID: 2128
			public int? unit;

			// Token: 0x04000851 RID: 2129
			public int? reportSize;

			// Token: 0x04000852 RID: 2130
			public int? reportCount;

			// Token: 0x04000853 RID: 2131
			public int? reportId;
		}
	}
}
