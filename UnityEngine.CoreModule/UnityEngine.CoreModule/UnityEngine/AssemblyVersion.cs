using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000194 RID: 404
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Runtime/Mono/AssemblyFullName.h")]
	internal struct AssemblyVersion
	{
		// Token: 0x06000FEF RID: 4079 RVA: 0x000219A0 File Offset: 0x0001FBA0
		public static bool operator ==(AssemblyVersion lhs, AssemblyVersion rhs)
		{
			return lhs.major == rhs.major && lhs.minor == rhs.minor && lhs.build == rhs.build && lhs.revision == rhs.revision;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}.{3}", new object[] { this.major, this.minor, this.build, this.revision });
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00021A4C File Offset: 0x0001FC4C
		public override bool Equals(object other)
		{
			if (other is AssemblyVersion)
			{
				AssemblyVersion otherVersion = (AssemblyVersion)other;
				if (this.major == otherVersion.major && this.minor == otherVersion.minor && this.build == otherVersion.build)
				{
					return this.revision == otherVersion.revision;
				}
			}
			return false;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		public override int GetHashCode()
		{
			return HashCode.Combine<ushort, ushort, ushort, ushort>(this.major, this.minor, this.build, this.revision);
		}

		// Token: 0x0400064A RID: 1610
		public ushort major;

		// Token: 0x0400064B RID: 1611
		public ushort minor;

		// Token: 0x0400064C RID: 1612
		public ushort build;

		// Token: 0x0400064D RID: 1613
		public ushort revision;
	}
}
