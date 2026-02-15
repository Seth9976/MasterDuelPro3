using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AC RID: 940
	public struct DepthState : IEquatable<DepthState>
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x00037718 File Offset: 0x00035918
		public static DepthState defaultValue
		{
			get
			{
				return new DepthState(true, CompareFunction.Less);
			}
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00037731 File Offset: 0x00035931
		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			this.m_WriteEnabled = Convert.ToByte(writeEnabled);
			this.m_CompareFunction = (sbyte)compareFunction;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x00037748 File Offset: 0x00035948
		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)this.m_CompareFunction;
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00037760 File Offset: 0x00035960
		public bool Equals(DepthState other)
		{
			return this.m_WriteEnabled == other.m_WriteEnabled && this.m_CompareFunction == other.m_CompareFunction;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00037794 File Offset: 0x00035994
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DepthState && this.Equals((DepthState)obj);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000377CC File Offset: 0x000359CC
		public override int GetHashCode()
		{
			return (this.m_WriteEnabled.GetHashCode() * 397) ^ this.m_CompareFunction.GetHashCode();
		}

		// Token: 0x04000BEB RID: 3051
		private byte m_WriteEnabled;

		// Token: 0x04000BEC RID: 3052
		private sbyte m_CompareFunction;
	}
}
