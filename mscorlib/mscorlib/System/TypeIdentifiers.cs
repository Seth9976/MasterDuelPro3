using System;

namespace System
{
	// Token: 0x020001EE RID: 494
	internal class TypeIdentifiers
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0004DEA6 File Offset: 0x0004C0A6
		internal static TypeIdentifier FromDisplay(string displayName)
		{
			return new TypeIdentifiers.Display(displayName);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004DEAE File Offset: 0x0004C0AE
		internal static TypeIdentifier FromInternal(string internalName)
		{
			return new TypeIdentifiers.Internal(internalName);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004DEB6 File Offset: 0x0004C0B6
		internal static TypeIdentifier FromInternal(string internalNameSpace, TypeIdentifier typeName)
		{
			return new TypeIdentifiers.Internal(internalNameSpace, typeName);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004DEBF File Offset: 0x0004C0BF
		internal static TypeIdentifier WithoutEscape(string simpleName)
		{
			return new TypeIdentifiers.NoEscape(simpleName);
		}

		// Token: 0x020001EF RID: 495
		private class Display : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001311 RID: 4881 RVA: 0x0004DEC7 File Offset: 0x0004C0C7
			internal Display(string displayName)
			{
				this.displayName = displayName;
				this.internal_name = null;
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06001312 RID: 4882 RVA: 0x0004DEDD File Offset: 0x0004C0DD
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06001313 RID: 4883 RVA: 0x0004DEE5 File Offset: 0x0004C0E5
			public string InternalName
			{
				get
				{
					if (this.internal_name == null)
					{
						this.internal_name = this.GetInternalName();
					}
					return this.internal_name;
				}
			}

			// Token: 0x06001314 RID: 4884 RVA: 0x0004DF01 File Offset: 0x0004C101
			private string GetInternalName()
			{
				return TypeSpec.UnescapeInternalName(this.displayName);
			}

			// Token: 0x06001315 RID: 4885 RVA: 0x0004DF0E File Offset: 0x0004C10E
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04000969 RID: 2409
			private string displayName;

			// Token: 0x0400096A RID: 2410
			private string internal_name;
		}

		// Token: 0x020001F0 RID: 496
		private class Internal : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001316 RID: 4886 RVA: 0x0004DF2B File Offset: 0x0004C12B
			internal Internal(string internalName)
			{
				this.internalName = internalName;
				this.display_name = null;
			}

			// Token: 0x06001317 RID: 4887 RVA: 0x0004DF41 File Offset: 0x0004C141
			internal Internal(string nameSpaceInternal, TypeIdentifier typeName)
			{
				this.internalName = nameSpaceInternal + "." + typeName.InternalName;
				this.display_name = null;
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06001318 RID: 4888 RVA: 0x0004DF67 File Offset: 0x0004C167
			public override string DisplayName
			{
				get
				{
					if (this.display_name == null)
					{
						this.display_name = this.GetDisplayName();
					}
					return this.display_name;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06001319 RID: 4889 RVA: 0x0004DF83 File Offset: 0x0004C183
			public string InternalName
			{
				get
				{
					return this.internalName;
				}
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x0004DF8B File Offset: 0x0004C18B
			private string GetDisplayName()
			{
				return TypeSpec.EscapeDisplayName(this.internalName);
			}

			// Token: 0x0600131B RID: 4891 RVA: 0x0004DF0E File Offset: 0x0004C10E
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x0400096B RID: 2411
			private string internalName;

			// Token: 0x0400096C RID: 2412
			private string display_name;
		}

		// Token: 0x020001F1 RID: 497
		private class NoEscape : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x0600131C RID: 4892 RVA: 0x0004DF98 File Offset: 0x0004C198
			internal NoEscape(string simpleName)
			{
				this.simpleName = simpleName;
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x0600131D RID: 4893 RVA: 0x0004DFA7 File Offset: 0x0004C1A7
			public override string DisplayName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004DFA7 File Offset: 0x0004C1A7
			public string InternalName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x0600131F RID: 4895 RVA: 0x0004DF0E File Offset: 0x0004C10E
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x0400096D RID: 2413
			private string simpleName;
		}
	}
}
