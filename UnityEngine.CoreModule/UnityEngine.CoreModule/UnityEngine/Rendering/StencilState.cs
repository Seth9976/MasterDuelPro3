using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003CF RID: 975
	public struct StencilState : IEquatable<StencilState>
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0003A254 File Offset: 0x00038454
		public static StencilState defaultValue
		{
			get
			{
				return new StencilState(true, byte.MaxValue, byte.MaxValue, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0003A27C File Offset: 0x0003847C
		public StencilState(bool enabled = true, byte readMask = 255, byte writeMask = 255, CompareFunction compareFunction = CompareFunction.Always, StencilOp passOperation = StencilOp.Keep, StencilOp failOperation = StencilOp.Keep, StencilOp zFailOperation = StencilOp.Keep)
		{
			this = new StencilState(enabled, readMask, writeMask, compareFunction, passOperation, failOperation, zFailOperation, compareFunction, passOperation, failOperation, zFailOperation);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0003A2A4 File Offset: 0x000384A4
		public StencilState(bool enabled, byte readMask, byte writeMask, CompareFunction compareFunctionFront, StencilOp passOperationFront, StencilOp failOperationFront, StencilOp zFailOperationFront, CompareFunction compareFunctionBack, StencilOp passOperationBack, StencilOp failOperationBack, StencilOp zFailOperationBack)
		{
			this.m_Enabled = Convert.ToByte(enabled);
			this.m_ReadMask = readMask;
			this.m_WriteMask = writeMask;
			this.m_Padding = 0;
			this.m_CompareFunctionFront = (byte)compareFunctionFront;
			this.m_PassOperationFront = (byte)passOperationFront;
			this.m_FailOperationFront = (byte)failOperationFront;
			this.m_ZFailOperationFront = (byte)zFailOperationFront;
			this.m_CompareFunctionBack = (byte)compareFunctionBack;
			this.m_PassOperationBack = (byte)passOperationBack;
			this.m_FailOperationBack = (byte)failOperationBack;
			this.m_ZFailOperationBack = (byte)zFailOperationBack;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0003A31C File Offset: 0x0003851C
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x0003A339 File Offset: 0x00038539
		public bool enabled
		{
			get
			{
				return Convert.ToBoolean(this.m_Enabled);
			}
			set
			{
				this.m_Enabled = Convert.ToByte(value);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0003A348 File Offset: 0x00038548
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0003A360 File Offset: 0x00038560
		public byte readMask
		{
			get
			{
				return this.m_ReadMask;
			}
			set
			{
				this.m_ReadMask = value;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0003A36C File Offset: 0x0003856C
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x0003A384 File Offset: 0x00038584
		public byte writeMask
		{
			get
			{
				return this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = value;
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0003A38E File Offset: 0x0003858E
		public void SetCompareFunction(CompareFunction value)
		{
			this.compareFunctionFront = value;
			this.compareFunctionBack = value;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0003A3A1 File Offset: 0x000385A1
		public void SetPassOperation(StencilOp value)
		{
			this.passOperationFront = value;
			this.passOperationBack = value;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public void SetFailOperation(StencilOp value)
		{
			this.failOperationFront = value;
			this.failOperationBack = value;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0003A3C7 File Offset: 0x000385C7
		public void SetZFailOperation(StencilOp value)
		{
			this.zFailOperationFront = value;
			this.zFailOperationBack = value;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0003A3DC File Offset: 0x000385DC
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x0003A3F4 File Offset: 0x000385F4
		public CompareFunction compareFunctionFront
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionFront;
			}
			set
			{
				this.m_CompareFunctionFront = (byte)value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0003A400 File Offset: 0x00038600
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x0003A418 File Offset: 0x00038618
		public StencilOp passOperationFront
		{
			get
			{
				return (StencilOp)this.m_PassOperationFront;
			}
			set
			{
				this.m_PassOperationFront = (byte)value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0003A424 File Offset: 0x00038624
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0003A43C File Offset: 0x0003863C
		public StencilOp failOperationFront
		{
			get
			{
				return (StencilOp)this.m_FailOperationFront;
			}
			set
			{
				this.m_FailOperationFront = (byte)value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0003A448 File Offset: 0x00038648
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0003A460 File Offset: 0x00038660
		public StencilOp zFailOperationFront
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationFront;
			}
			set
			{
				this.m_ZFailOperationFront = (byte)value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0003A46C File Offset: 0x0003866C
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0003A484 File Offset: 0x00038684
		public CompareFunction compareFunctionBack
		{
			get
			{
				return (CompareFunction)this.m_CompareFunctionBack;
			}
			set
			{
				this.m_CompareFunctionBack = (byte)value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0003A490 File Offset: 0x00038690
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0003A4A8 File Offset: 0x000386A8
		public StencilOp passOperationBack
		{
			get
			{
				return (StencilOp)this.m_PassOperationBack;
			}
			set
			{
				this.m_PassOperationBack = (byte)value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0003A4B4 File Offset: 0x000386B4
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x0003A4CC File Offset: 0x000386CC
		public StencilOp failOperationBack
		{
			get
			{
				return (StencilOp)this.m_FailOperationBack;
			}
			set
			{
				this.m_FailOperationBack = (byte)value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0003A4D8 File Offset: 0x000386D8
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0003A4F0 File Offset: 0x000386F0
		public StencilOp zFailOperationBack
		{
			get
			{
				return (StencilOp)this.m_ZFailOperationBack;
			}
			set
			{
				this.m_ZFailOperationBack = (byte)value;
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0003A4FC File Offset: 0x000386FC
		public bool Equals(StencilState other)
		{
			return this.m_Enabled == other.m_Enabled && this.m_ReadMask == other.m_ReadMask && this.m_WriteMask == other.m_WriteMask && this.m_CompareFunctionFront == other.m_CompareFunctionFront && this.m_PassOperationFront == other.m_PassOperationFront && this.m_FailOperationFront == other.m_FailOperationFront && this.m_ZFailOperationFront == other.m_ZFailOperationFront && this.m_CompareFunctionBack == other.m_CompareFunctionBack && this.m_PassOperationBack == other.m_PassOperationBack && this.m_FailOperationBack == other.m_FailOperationBack && this.m_ZFailOperationBack == other.m_ZFailOperationBack;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0003A5B4 File Offset: 0x000387B4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is StencilState && this.Equals((StencilState)obj);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0003A5EC File Offset: 0x000387EC
		public override int GetHashCode()
		{
			int hashCode = this.m_Enabled.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ReadMask.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_WriteMask.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_CompareFunctionFront.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_PassOperationFront.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_FailOperationFront.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_ZFailOperationFront.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_CompareFunctionBack.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_PassOperationBack.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_FailOperationBack.GetHashCode();
			return (hashCode * 397) ^ this.m_ZFailOperationBack.GetHashCode();
		}

		// Token: 0x04000C9B RID: 3227
		private byte m_Enabled;

		// Token: 0x04000C9C RID: 3228
		private byte m_ReadMask;

		// Token: 0x04000C9D RID: 3229
		private byte m_WriteMask;

		// Token: 0x04000C9E RID: 3230
		private byte m_Padding;

		// Token: 0x04000C9F RID: 3231
		private byte m_CompareFunctionFront;

		// Token: 0x04000CA0 RID: 3232
		private byte m_PassOperationFront;

		// Token: 0x04000CA1 RID: 3233
		private byte m_FailOperationFront;

		// Token: 0x04000CA2 RID: 3234
		private byte m_ZFailOperationFront;

		// Token: 0x04000CA3 RID: 3235
		private byte m_CompareFunctionBack;

		// Token: 0x04000CA4 RID: 3236
		private byte m_PassOperationBack;

		// Token: 0x04000CA5 RID: 3237
		private byte m_FailOperationBack;

		// Token: 0x04000CA6 RID: 3238
		private byte m_ZFailOperationBack;
	}
}
