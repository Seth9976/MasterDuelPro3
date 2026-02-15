using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssetStudio
{
	// Token: 0x020000AA RID: 170
	public class StreamedClip
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000CEFD File Offset: 0x0000B0FD
		public StreamedClip(ObjectReader reader)
		{
			this.data = reader.ReadUInt32Array();
			this.curveCount = reader.ReadUInt32();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000CF20 File Offset: 0x0000B120
		public List<StreamedClip.StreamedFrame> ReadData()
		{
			List<StreamedClip.StreamedFrame> frameList = new List<StreamedClip.StreamedFrame>();
			byte[] buffer = new byte[this.data.Length * 4];
			Buffer.BlockCopy(this.data, 0, buffer, 0, buffer.Length);
			using (BinaryReader reader = new BinaryReader(new MemoryStream(buffer)))
			{
				while (reader.BaseStream.Position < reader.BaseStream.Length)
				{
					frameList.Add(new StreamedClip.StreamedFrame(reader));
				}
			}
			for (int frameIndex = 2; frameIndex < frameList.Count - 1; frameIndex++)
			{
				StreamedClip.StreamedFrame frame = frameList[frameIndex];
				StreamedClip.StreamedCurveKey[] keyList = frame.keyList;
				for (int j = 0; j < keyList.Length; j++)
				{
					StreamedClip.StreamedCurveKey curveKey = keyList[j];
					Func<StreamedClip.StreamedCurveKey, bool> <>9__0;
					for (int i = frameIndex - 1; i >= 0; i--)
					{
						StreamedClip.StreamedFrame preFrame = frameList[i];
						IEnumerable<StreamedClip.StreamedCurveKey> keyList2 = preFrame.keyList;
						Func<StreamedClip.StreamedCurveKey, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (StreamedClip.StreamedCurveKey x) => x.index == curveKey.index);
						}
						StreamedClip.StreamedCurveKey preCurveKey = keyList2.FirstOrDefault(func);
						if (preCurveKey != null)
						{
							curveKey.inSlope = preCurveKey.CalculateNextInSlope(frame.time - preFrame.time, curveKey);
							break;
						}
					}
				}
			}
			return frameList;
		}

		// Token: 0x0400057D RID: 1405
		public uint[] data;

		// Token: 0x0400057E RID: 1406
		public uint curveCount;

		// Token: 0x020000AB RID: 171
		public class StreamedCurveKey
		{
			// Token: 0x060002E4 RID: 740 RVA: 0x0000D078 File Offset: 0x0000B278
			public StreamedCurveKey(BinaryReader reader)
			{
				this.index = reader.ReadInt32();
				this.coeff = reader.ReadSingleArray(4);
				this.outSlope = this.coeff[2];
				this.value = this.coeff[3];
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
			public float CalculateNextInSlope(float dx, StreamedClip.StreamedCurveKey rhs)
			{
				if (this.coeff[0] == 0f && this.coeff[1] == 0f && this.coeff[2] == 0f)
				{
					return float.PositiveInfinity;
				}
				dx = Math.Max(dx, 0.0001f);
				float dy = rhs.value - this.value;
				float length = 1f / (dx * dx);
				float d = this.outSlope * dx;
				return (dy + dy + dy - d - d - this.coeff[1] / length) / dx;
			}

			// Token: 0x0400057F RID: 1407
			public int index;

			// Token: 0x04000580 RID: 1408
			public float[] coeff;

			// Token: 0x04000581 RID: 1409
			public float value;

			// Token: 0x04000582 RID: 1410
			public float outSlope;

			// Token: 0x04000583 RID: 1411
			public float inSlope;
		}

		// Token: 0x020000AC RID: 172
		public class StreamedFrame
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x0000D13C File Offset: 0x0000B33C
			public StreamedFrame(BinaryReader reader)
			{
				this.time = reader.ReadSingle();
				int numKeys = reader.ReadInt32();
				this.keyList = new StreamedClip.StreamedCurveKey[numKeys];
				for (int i = 0; i < numKeys; i++)
				{
					this.keyList[i] = new StreamedClip.StreamedCurveKey(reader);
				}
			}

			// Token: 0x04000584 RID: 1412
			public float time;

			// Token: 0x04000585 RID: 1413
			public StreamedClip.StreamedCurveKey[] keyList;
		}
	}
}
