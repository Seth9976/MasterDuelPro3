using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	[NativeHeader("Runtime/Mono/AssemblyFullName.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	internal struct AssemblyFullName
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		public override bool Equals(object other)
		{
			if (other is AssemblyFullName)
			{
				AssemblyFullName otherVersion = (AssemblyFullName)other;
				if (this.Name == otherVersion.Name && this.Version == otherVersion.Version && this.PublicKeyToken == otherVersion.PublicKeyToken)
				{
					return this.Culture == otherVersion.Culture;
				}
			}
			return false;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00021B48 File Offset: 0x0001FD48
		public override int GetHashCode()
		{
			return HashCode.Combine<string, AssemblyVersion, string, string>(this.Name, this.Version, this.PublicKeyToken, this.Culture);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00021B78 File Offset: 0x0001FD78
		public override string ToString()
		{
			return string.Format("{0}, Version={1}, Culture={2}, PublicKeyToken={3}", new object[]
			{
				this.Name,
				this.Version,
				string.IsNullOrEmpty(this.Culture) ? "neutral" : this.Culture,
				this.PublicKeyToken
			});
		}

		// Token: 0x0400064E RID: 1614
		[NativeName("name")]
		public string Name;

		// Token: 0x0400064F RID: 1615
		[NativeName("version")]
		public AssemblyVersion Version;

		// Token: 0x04000650 RID: 1616
		[NativeName("publicKeyToken")]
		public string PublicKeyToken;

		// Token: 0x04000651 RID: 1617
		[NativeName("culture")]
		public string Culture;
	}
}
