using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x0200010F RID: 271
	public struct InputUserAccountHandle : IEquatable<InputUserAccountHandle>
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00041DCE File Offset: 0x0003FFCE
		public string apiName
		{
			get
			{
				return this.m_ApiName;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00041DD6 File Offset: 0x0003FFD6
		public ulong handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00041DDE File Offset: 0x0003FFDE
		public InputUserAccountHandle(string apiName, ulong handle)
		{
			if (string.IsNullOrEmpty(apiName))
			{
				throw new ArgumentNullException("apiName");
			}
			this.m_ApiName = apiName;
			this.m_Handle = handle;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00041E01 File Offset: 0x00040001
		public override string ToString()
		{
			if (this.m_ApiName == null)
			{
				return base.ToString();
			}
			return string.Format("{0}({1})", this.m_ApiName, this.m_Handle);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00041E37 File Offset: 0x00040037
		public bool Equals(InputUserAccountHandle other)
		{
			return string.Equals(this.apiName, other.apiName) && object.Equals(this.handle, other.handle);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00041E6B File Offset: 0x0004006B
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputUserAccountHandle && this.Equals((InputUserAccountHandle)obj);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00041E88 File Offset: 0x00040088
		public static bool operator ==(InputUserAccountHandle left, InputUserAccountHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00041E92 File Offset: 0x00040092
		public static bool operator !=(InputUserAccountHandle left, InputUserAccountHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00041EA0 File Offset: 0x000400A0
		public override int GetHashCode()
		{
			return (((this.apiName != null) ? this.apiName.GetHashCode() : 0) * 397) ^ this.handle.GetHashCode();
		}

		// Token: 0x04000620 RID: 1568
		private string m_ApiName;

		// Token: 0x04000621 RID: 1569
		private ulong m_Handle;
	}
}
