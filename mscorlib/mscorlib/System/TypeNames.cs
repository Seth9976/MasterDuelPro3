using System;

namespace System
{
	// Token: 0x020001EB RID: 491
	internal class TypeNames
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x0004DE37 File Offset: 0x0004C037
		internal static TypeName FromDisplay(string displayName)
		{
			return new TypeNames.Display(displayName);
		}

		// Token: 0x020001EC RID: 492
		internal abstract class ATypeName : TypeName, IEquatable<TypeName>
		{
			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06001304 RID: 4868
			public abstract string DisplayName { get; }

			// Token: 0x06001305 RID: 4869
			public abstract TypeName NestedName(TypeIdentifier innerName);

			// Token: 0x06001306 RID: 4870 RVA: 0x0004DE3F File Offset: 0x0004C03F
			public bool Equals(TypeName other)
			{
				return other != null && this.DisplayName == other.DisplayName;
			}

			// Token: 0x06001307 RID: 4871 RVA: 0x0004DE57 File Offset: 0x0004C057
			public override int GetHashCode()
			{
				return this.DisplayName.GetHashCode();
			}

			// Token: 0x06001308 RID: 4872 RVA: 0x0004DE64 File Offset: 0x0004C064
			public override bool Equals(object other)
			{
				return this.Equals(other as TypeName);
			}
		}

		// Token: 0x020001ED RID: 493
		private class Display : TypeNames.ATypeName
		{
			// Token: 0x0600130A RID: 4874 RVA: 0x0004DE72 File Offset: 0x0004C072
			internal Display(string displayName)
			{
				this.displayName = displayName;
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004DE81 File Offset: 0x0004C081
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x0600130C RID: 4876 RVA: 0x0004DE89 File Offset: 0x0004C089
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return new TypeNames.Display(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000968 RID: 2408
			private string displayName;
		}
	}
}
