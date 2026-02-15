using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001B0 RID: 432
	[StructLayout(LayoutKind.Explicit, Size = 132)]
	public struct IMECompositionString : IEnumerable<char>, IEnumerable
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700048A RID: 1162
		public unsafe char this[int index]
		{
			get
			{
				if (index >= this.Count || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				fixed (char* ptr = &this.buffer.FixedElementField)
				{
					return ptr[index];
				}
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0004E6EC File Offset: 0x0004C8EC
		public unsafe IMECompositionString(string characters)
		{
			if (string.IsNullOrEmpty(characters))
			{
				this.size = 0;
				return;
			}
			this.size = characters.Length;
			for (int i = 0; i < this.size; i++)
			{
				*((ref this.buffer.FixedElementField) + (IntPtr)i * 2) = characters[i];
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0004E740 File Offset: 0x0004C940
		public unsafe override string ToString()
		{
			fixed (char* ptr = &this.buffer.FixedElementField)
			{
				return new string(ptr, 0, this.size);
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0004E767 File Offset: 0x0004C967
		public IEnumerator<char> GetEnumerator()
		{
			return new IMECompositionString.Enumerator(this);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0004E779 File Offset: 0x0004C979
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040009E8 RID: 2536
		[FieldOffset(0)]
		private int size;

		// Token: 0x040009E9 RID: 2537
		[FixedBuffer(typeof(char), 64)]
		[FieldOffset(4)]
		private IMECompositionString.<buffer>e__FixedBuffer buffer;

		// Token: 0x020001B1 RID: 433
		internal struct Enumerator : IEnumerator<char>, IEnumerator, IDisposable
		{
			// Token: 0x0600101D RID: 4125 RVA: 0x0004E781 File Offset: 0x0004C981
			public Enumerator(IMECompositionString compositionString)
			{
				this.m_CompositionString = compositionString;
				this.m_CurrentCharacter = '\0';
				this.m_CurrentIndex = -1;
			}

			// Token: 0x0600101E RID: 4126 RVA: 0x0004E798 File Offset: 0x0004C998
			public unsafe bool MoveNext()
			{
				int size = this.m_CompositionString.Count;
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == size)
				{
					return false;
				}
				fixed (char* ptr2 = &this.m_CompositionString.buffer.FixedElementField)
				{
					char* ptr = ptr2;
					this.m_CurrentCharacter = ptr[this.m_CurrentIndex];
				}
				return true;
			}

			// Token: 0x0600101F RID: 4127 RVA: 0x0004E7F4 File Offset: 0x0004C9F4
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06001020 RID: 4128 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x06001021 RID: 4129 RVA: 0x0004E7FD File Offset: 0x0004C9FD
			public char Current
			{
				get
				{
					return this.m_CurrentCharacter;
				}
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x06001022 RID: 4130 RVA: 0x0004E805 File Offset: 0x0004CA05
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040009EA RID: 2538
			private IMECompositionString m_CompositionString;

			// Token: 0x040009EB RID: 2539
			private char m_CurrentCharacter;

			// Token: 0x040009EC RID: 2540
			private int m_CurrentIndex;
		}

		// Token: 0x020001B2 RID: 434
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x040009ED RID: 2541
			public char FixedElementField;
		}
	}
}
