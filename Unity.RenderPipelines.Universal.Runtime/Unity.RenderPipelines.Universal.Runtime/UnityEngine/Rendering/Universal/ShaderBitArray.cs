using System;
using System.Text;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200018C RID: 396
	internal struct ShaderBitArray
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0002804F File Offset: 0x0002624F
		public int elemLength
		{
			get
			{
				if (this.m_Data != null)
				{
					return this.m_Data.Length;
				}
				return 0;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00028063 File Offset: 0x00026263
		public int bitCapacity
		{
			get
			{
				return this.elemLength * 32;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002806E File Offset: 0x0002626E
		public float[] data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00028078 File Offset: 0x00026278
		public void Resize(int bitCount)
		{
			if (this.bitCapacity > bitCount)
			{
				return;
			}
			int newElemCount = (bitCount + 31) / 32;
			int num = newElemCount;
			float[] data = this.m_Data;
			int? num2 = ((data != null) ? new int?(data.Length) : null);
			if ((num == num2.GetValueOrDefault()) & (num2 != null))
			{
				return;
			}
			float[] newData = new float[newElemCount];
			if (this.m_Data != null)
			{
				for (int i = 0; i < this.m_Data.Length; i++)
				{
					newData[i] = this.m_Data[i];
				}
			}
			this.m_Data = newData;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00028104 File Offset: 0x00026304
		public void Clear()
		{
			for (int i = 0; i < this.m_Data.Length; i++)
			{
				this.m_Data[i] = 0f;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00028131 File Offset: 0x00026331
		private void GetElementIndexAndBitOffset(int index, out int elemIndex, out int bitOffset)
		{
			elemIndex = index >> 5;
			bitOffset = index & 31;
		}

		// Token: 0x170001C5 RID: 453
		public unsafe bool this[int index]
		{
			get
			{
				int elemIndex;
				int bitOffset;
				this.GetElementIndexAndBitOffset(index, out elemIndex, out bitOffset);
				float[] data;
				float* floatData;
				if ((data = this.m_Data) == null || data.Length == 0)
				{
					floatData = null;
				}
				else
				{
					floatData = &data[0];
				}
				uint* uintElem = (uint*)(floatData + elemIndex);
				return (*uintElem & (1U << bitOffset)) > 0U;
			}
			set
			{
				int elemIndex;
				int bitOffset;
				this.GetElementIndexAndBitOffset(index, out elemIndex, out bitOffset);
				float[] array;
				float* floatData;
				if ((array = this.m_Data) == null || array.Length == 0)
				{
					floatData = null;
				}
				else
				{
					floatData = &array[0];
				}
				uint* uintElem = (uint*)(floatData + elemIndex);
				if (value)
				{
					*uintElem |= 1U << bitOffset;
				}
				else
				{
					*uintElem &= ~(1U << bitOffset);
				}
				array = null;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000281EC File Offset: 0x000263EC
		public unsafe override string ToString()
		{
			int len = Math.Min(this.bitCapacity, 4096);
			byte* buf = stackalloc byte[(UIntPtr)len];
			for (int i = 0; i < len; i++)
			{
				buf[i] = (this[i] ? 49 : 48);
			}
			return new string((sbyte*)buf, 0, len, Encoding.UTF8);
		}

		// Token: 0x040008D0 RID: 2256
		private const int k_BitsPerElement = 32;

		// Token: 0x040008D1 RID: 2257
		private const int k_ElementShift = 5;

		// Token: 0x040008D2 RID: 2258
		private const int k_ElementMask = 31;

		// Token: 0x040008D3 RID: 2259
		private float[] m_Data;
	}
}
