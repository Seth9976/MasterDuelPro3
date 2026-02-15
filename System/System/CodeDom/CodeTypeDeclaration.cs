using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a type declaration for a class, structure, interface, or enumeration.</summary>
	// Token: 0x0200021C RID: 540
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class.</summary>
		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003B707 File Offset: 0x00039907
		public CodeTypeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class with the specified name.</summary>
		/// <param name="name">The name for the new type. </param>
		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003B72C File Offset: 0x0003992C
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the attributes of the type.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object that indicates the attributes of the type.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0003B758 File Offset: 0x00039958
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0003B760 File Offset: 0x00039960
		public TypeAttributes TypeAttributes { get; set; } = TypeAttributes.Public;

		/// <summary>Gets the base types of the type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that indicates the base types of the type.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0003B769 File Offset: 0x00039969
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateBaseTypes = this.PopulateBaseTypes;
					if (populateBaseTypes != null)
					{
						populateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this._baseTypes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a class or reference type.</summary>
		/// <returns>true if the type is a class or reference type; otherwise, false.</returns>
		// Token: 0x170002A3 RID: 675
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0003B7A0 File Offset: 0x000399A0
		public bool IsClass
		{
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this.TypeAttributes |= TypeAttributes.NotPublic;
					this._isStruct = false;
					this._isEnum = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a value type (struct).</summary>
		/// <returns>true if the type is a value type; otherwise, false.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0003B7D0 File Offset: 0x000399D0
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0003B7D8 File Offset: 0x000399D8
		public bool IsStruct
		{
			get
			{
				return this._isStruct;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isEnum = false;
				}
				this._isStruct = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an enumeration.</summary>
		/// <returns>true if the type is an enumeration; otherwise, false.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0003B7FA File Offset: 0x000399FA
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0003B802 File Offset: 0x00039A02
		public bool IsEnum
		{
			get
			{
				return this._isEnum;
			}
			set
			{
				if (value)
				{
					this.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
					this._isStruct = false;
				}
				this._isEnum = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an interface.</summary>
		/// <returns>true if the type is an interface; otherwise, false.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0003B824 File Offset: 0x00039A24
		public bool IsInterface
		{
			get
			{
				return (this.TypeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type declaration is complete or partial.</summary>
		/// <returns>true if the class or structure declaration is a partial representation of the implementation; false if the declaration is a complete implementation of the class or structure. The default is false.</returns>
		// Token: 0x170002A7 RID: 679
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x0003B833 File Offset: 0x00039A33
		public bool IsPartial
		{
			[CompilerGenerated]
			set
			{
				this.<IsPartial>k__BackingField = value;
			}
		}

		/// <summary>Gets the collection of class members for the represented type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> object that indicates the class members.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0003B83C File Offset: 0x00039A3C
		public CodeTypeMemberCollection Members
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateMembers = this.PopulateMembers;
					if (populateMembers != null)
					{
						populateMembers(this, EventArgs.Empty);
					}
				}
				return this._members;
			}
		}

		/// <summary>Gets the type parameters for the type declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the type declaration.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0003B874 File Offset: 0x00039A74
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				CodeTypeParameterCollection codeTypeParameterCollection;
				if ((codeTypeParameterCollection = this._typeParameters) == null)
				{
					codeTypeParameterCollection = (this._typeParameters = new CodeTypeParameterCollection());
				}
				return codeTypeParameterCollection;
			}
		}

		// Token: 0x040008F9 RID: 2297
		private readonly CodeTypeReferenceCollection _baseTypes = new CodeTypeReferenceCollection();

		// Token: 0x040008FA RID: 2298
		private readonly CodeTypeMemberCollection _members = new CodeTypeMemberCollection();

		// Token: 0x040008FB RID: 2299
		private bool _isEnum;

		// Token: 0x040008FC RID: 2300
		private bool _isStruct;

		// Token: 0x040008FD RID: 2301
		private int _populated;

		// Token: 0x040008FE RID: 2302
		private CodeTypeParameterCollection _typeParameters;

		// Token: 0x040008FF RID: 2303
		[CompilerGenerated]
		private EventHandler PopulateBaseTypes;

		// Token: 0x04000900 RID: 2304
		[CompilerGenerated]
		private EventHandler PopulateMembers;
	}
}
