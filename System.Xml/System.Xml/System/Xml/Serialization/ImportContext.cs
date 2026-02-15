using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Xml.Serialization
{
	/// <summary>Describes the context in which a set of schema is bound to .NET Framework code entities.</summary>
	// Token: 0x0200015A RID: 346
	public class ImportContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.ImportContext" /> class for the given code identifiers, with the given type-sharing option.</summary>
		/// <param name="identifiers">The code entities to which the context applies.</param>
		/// <param name="shareTypes">A <see cref="T:System.Boolean" /> value that determines whether custom types are shared among schema.</param>
		// Token: 0x060010E1 RID: 4321 RVA: 0x00052F87 File Offset: 0x00051187
		public ImportContext(CodeIdentifiers identifiers, bool shareTypes)
		{
			this.typeIdentifiers = identifiers;
			this.shareTypes = shareTypes;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00052F9D File Offset: 0x0005119D
		internal ImportContext()
			: this(null, false)
		{
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00052FA7 File Offset: 0x000511A7
		internal SchemaObjectCache Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new SchemaObjectCache();
				}
				return this.cache;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00052FC2 File Offset: 0x000511C2
		internal Hashtable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new Hashtable();
				}
				return this.elements;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00052FDD File Offset: 0x000511DD
		internal Hashtable Mappings
		{
			get
			{
				if (this.mappings == null)
				{
					this.mappings = new Hashtable();
				}
				return this.mappings;
			}
		}

		/// <summary>Gets a set of code entities to which the context applies.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> that specifies the code entities to which the context applies.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00052FF8 File Offset: 0x000511F8
		public CodeIdentifiers TypeIdentifiers
		{
			get
			{
				if (this.typeIdentifiers == null)
				{
					this.typeIdentifiers = new CodeIdentifiers();
				}
				return this.typeIdentifiers;
			}
		}

		/// <summary>Gets a value that determines whether custom types are shared.</summary>
		/// <returns>true, if custom types are shared among schema; otherwise, false.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00053013 File Offset: 0x00051213
		public bool ShareTypes
		{
			get
			{
				return this.shareTypes;
			}
		}

		/// <summary>Gets a collection of warnings that are generated when importing the code entity descriptions.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains warnings that were generated when importing the code entity descriptions.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0005301B File Offset: 0x0005121B
		public StringCollection Warnings
		{
			get
			{
				return this.Cache.Warnings;
			}
		}

		// Token: 0x0400081F RID: 2079
		private bool shareTypes;

		// Token: 0x04000820 RID: 2080
		private SchemaObjectCache cache;

		// Token: 0x04000821 RID: 2081
		private Hashtable mappings;

		// Token: 0x04000822 RID: 2082
		private Hashtable elements;

		// Token: 0x04000823 RID: 2083
		private CodeIdentifiers typeIdentifiers;
	}
}
