using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000168 RID: 360
	[UsedByNativeCode]
	public struct PropertyName : IEquatable<PropertyName>
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x000202E0 File Offset: 0x0001E4E0
		public PropertyName(string name)
		{
			this = new PropertyName(PropertyNameUtils.PropertyNameFromString(name));
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000202F0 File Offset: 0x0001E4F0
		public PropertyName(PropertyName other)
		{
			this.id = other.id;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00020300 File Offset: 0x0001E500
		public static bool IsNullOrEmpty(PropertyName prop)
		{
			return prop.id == 0;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002031C File Offset: 0x0001E51C
		public static bool operator ==(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002033C File Offset: 0x0001E53C
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00020354 File Offset: 0x0001E554
		public override bool Equals(object other)
		{
			return other is PropertyName && this.Equals((PropertyName)other);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00020380 File Offset: 0x0001E580
		public bool Equals(PropertyName other)
		{
			return this == other;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000203A0 File Offset: 0x0001E5A0
		public static implicit operator PropertyName(string name)
		{
			return new PropertyName(name);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000203B8 File Offset: 0x0001E5B8
		public override string ToString()
		{
			return string.Format("Unknown:{0}", this.id);
		}

		// Token: 0x04000606 RID: 1542
		internal int id;
	}
}
