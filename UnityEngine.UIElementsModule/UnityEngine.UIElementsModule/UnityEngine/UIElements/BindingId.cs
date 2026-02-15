using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D0 RID: 1232
	public readonly struct BindingId : IEquatable<BindingId>
	{
		// Token: 0x060022D1 RID: 8913 RVA: 0x000800D7 File Offset: 0x0007E2D7
		public BindingId(string path)
		{
			this.m_PropertyPath = new PropertyPath(path);
			this.m_Path = path;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000800ED File Offset: 0x0007E2ED
		public BindingId(in PropertyPath path)
		{
			this.m_PropertyPath = path;
			this.m_Path = path.ToString();
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00080110 File Offset: 0x0007E310
		public static implicit operator PropertyPath(in BindingId vep)
		{
			return vep.m_PropertyPath;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00080128 File Offset: 0x0007E328
		public static implicit operator string(in BindingId vep)
		{
			return vep.m_Path;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00080140 File Offset: 0x0007E340
		public static implicit operator BindingId(string name)
		{
			return new BindingId(name);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00080158 File Offset: 0x0007E358
		public static implicit operator BindingId(in PropertyPath path)
		{
			return new BindingId(in path);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00080170 File Offset: 0x0007E370
		public override string ToString()
		{
			return this.m_Path;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00080188 File Offset: 0x0007E388
		public bool Equals(BindingId other)
		{
			return this.m_PropertyPath == other.m_PropertyPath;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000801AC File Offset: 0x0007E3AC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is BindingId)
			{
				BindingId other = (BindingId)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000801D8 File Offset: 0x0007E3D8
		public override int GetHashCode()
		{
			return this.m_PropertyPath.GetHashCode();
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000801FC File Offset: 0x0007E3FC
		public static bool operator ==(in BindingId lhs, in BindingId rhs)
		{
			return lhs.m_PropertyPath == rhs.m_PropertyPath;
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00080220 File Offset: 0x0007E420
		public static bool operator !=(in BindingId lhs, in BindingId rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x04000FA9 RID: 4009
		public static readonly BindingId Invalid = default(BindingId);

		// Token: 0x04000FAA RID: 4010
		private readonly PropertyPath m_PropertyPath;

		// Token: 0x04000FAB RID: 4011
		private readonly string m_Path;
	}
}
