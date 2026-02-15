using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015F RID: 351
	[Serializable]
	public struct ToggleButtonGroupState : IEquatable<ToggleButtonGroupState>, IComparable<ToggleButtonGroupState>
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x0003338C File Offset: 0x0003158C
		public ToggleButtonGroupState(ulong optionsBitMask, int length)
		{
			bool flag = length < 0 || length > 64;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("length of {0} should be greater than or equal to 0 and less than or equal to {1}.", length, 64));
			}
			this.m_Data = optionsBitMask;
			this.m_Length = length;
			this.ResetOptions(this.m_Length);
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x000333E7 File Offset: 0x000315E7
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x000333EF File Offset: 0x000315EF
		public int length
		{
			get
			{
				return this.m_Length;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x000333F8 File Offset: 0x000315F8
		internal ulong data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x170001CD RID: 461
		public bool this[int index]
		{
			get
			{
				bool flag = index < 0 || index >= this.m_Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("index of {0} should be in the range of 0 and {1} inclusively.", index, this.m_Length - 1));
				}
				ulong bit = 1UL << index;
				return (this.m_Data & bit) == bit;
			}
			set
			{
				bool flag = index < 0 || index >= this.m_Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index", string.Format("index of {0} should be in the range of 0 and {1} inclusively.", index, this.m_Length - 1));
				}
				ulong option = 1UL << index;
				if (value)
				{
					this.m_Data |= option;
				}
				else
				{
					this.m_Data &= ~option;
				}
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000334E0 File Offset: 0x000316E0
		public unsafe Span<int> GetActiveOptions(Span<int> activeOptionsIndices)
		{
			bool flag = activeOptionsIndices.Length < this.m_Length;
			if (flag)
			{
				throw new ArgumentException(string.Format("indices' length ({0}) should be equal to or greater than the ToggleButtonGroupState's length ({1}).", activeOptionsIndices.Length, this.m_Length));
			}
			int count = 0;
			for (int i = 0; i < this.m_Length; i++)
			{
				bool flag2 = !this[i];
				if (!flag2)
				{
					*activeOptionsIndices[count] = i;
					count++;
				}
			}
			return activeOptionsIndices.Slice(0, count);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00033570 File Offset: 0x00031770
		public void ResetAllOptions()
		{
			this.m_Data = 0UL;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0003357C File Offset: 0x0003177C
		public int CompareTo(ToggleButtonGroupState other)
		{
			return (other == this) ? 1 : (-1);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000335A0 File Offset: 0x000317A0
		private void ResetOptions(int startingIndex)
		{
			for (int i = startingIndex; i < 64; i++)
			{
				ulong option = 1UL << i;
				this.m_Data &= ~option;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000335D8 File Offset: 0x000317D8
		public static bool operator ==(ToggleButtonGroupState lhs, ToggleButtonGroupState rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000335F4 File Offset: 0x000317F4
		public static bool operator !=(ToggleButtonGroupState lhs, ToggleButtonGroupState rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00033610 File Offset: 0x00031810
		public bool Equals(ToggleButtonGroupState other)
		{
			return this.m_Data == other.m_Data && this.m_Length == other.m_Length;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00033644 File Offset: 0x00031844
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is ToggleButtonGroupState)
			{
				ToggleButtonGroupState other = (ToggleButtonGroupState)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00033670 File Offset: 0x00031870
		public override int GetHashCode()
		{
			return HashCode.Combine<ulong, int>(this.m_Data, this.m_Length);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00033694 File Offset: 0x00031894
		public override string ToString()
		{
			return Convert.ToString((long)this.m_Data, 2).PadLeft(this.length, '0');
		}

		// Token: 0x040006D9 RID: 1753
		[SerializeField]
		private ulong m_Data;

		// Token: 0x040006DA RID: 1754
		[SerializeField]
		private int m_Length;
	}
}
