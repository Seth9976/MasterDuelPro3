using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200016A RID: 362
	internal class StructMapping : TypeMapping, INameScope
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00054234 File Offset: 0x00052434
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x0005423C File Offset: 0x0005243C
		internal StructMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
				if (!base.IsAnonymousType && this.baseMapping != null)
				{
					this.nextDerivedMapping = this.baseMapping.derivedMappings;
					this.baseMapping.derivedMappings = this;
				}
				if (value.isSequence && !this.isSequence)
				{
					this.isSequence = true;
					if (this.baseMapping.IsSequence)
					{
						for (StructMapping structMapping = this.derivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
						{
							structMapping.SetSequence();
						}
					}
				}
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000542BA File Offset: 0x000524BA
		internal StructMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000542C2 File Offset: 0x000524C2
		internal bool IsFullyInitialized
		{
			get
			{
				return this.baseMapping != null && this.Members != null;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x000542D7 File Offset: 0x000524D7
		internal NameTable LocalElements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new NameTable();
				}
				return this.elements;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000542F2 File Offset: 0x000524F2
		internal NameTable LocalAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new NameTable();
				}
				return this.attributes;
			}
		}

		// Token: 0x170003FB RID: 1019
		object INameScope.this[string name, string ns]
		{
			get
			{
				object obj = this.LocalElements[name, ns];
				if (obj != null)
				{
					return obj;
				}
				if (this.baseMapping != null)
				{
					return ((INameScope)this.baseMapping)[name, ns];
				}
				return null;
			}
			set
			{
				this.LocalElements[name, ns] = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00054357 File Offset: 0x00052557
		internal StructMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0005435F File Offset: 0x0005255F
		internal bool HasSimpleContent
		{
			get
			{
				return this.hasSimpleContent;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00054368 File Offset: 0x00052568
		internal bool HasXmlnsMember
		{
			get
			{
				for (StructMapping structMapping = this; structMapping != null; structMapping = structMapping.BaseMapping)
				{
					if (structMapping.XmlnsMember != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0005438E File Offset: 0x0005258E
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00054396 File Offset: 0x00052596
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0005439F File Offset: 0x0005259F
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x000543A7 File Offset: 0x000525A7
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000543B0 File Offset: 0x000525B0
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000543B8 File Offset: 0x000525B8
		internal bool IsOpenModel
		{
			get
			{
				return this.openModel;
			}
			set
			{
				this.openModel = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000543C1 File Offset: 0x000525C1
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x000543DC File Offset: 0x000525DC
		internal CodeIdentifiers Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new CodeIdentifiers();
				}
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000543E8 File Offset: 0x000525E8
		internal MemberMapping FindDeclaringMapping(MemberMapping member, out StructMapping declaringMapping, string parent)
		{
			declaringMapping = null;
			if (this.BaseMapping != null)
			{
				MemberMapping memberMapping = this.BaseMapping.FindDeclaringMapping(member, out declaringMapping, parent);
				if (memberMapping != null)
				{
					return memberMapping;
				}
			}
			if (this.members == null)
			{
				return null;
			}
			int i = 0;
			while (i < this.members.Length)
			{
				if (this.members[i].Name == member.Name)
				{
					if (this.members[i].TypeDesc != member.TypeDesc)
					{
						throw new InvalidOperationException(Res.GetString("Member {0}.{1} of type {2} hides base class member {3}.{4} of type {5}. Use XmlElementAttribute or XmlAttributeAttribute to specify a new name.", new object[]
						{
							parent,
							member.Name,
							member.TypeDesc.FullName,
							base.TypeName,
							this.members[i].Name,
							this.members[i].TypeDesc.FullName
						}));
					}
					if (!this.members[i].Match(member))
					{
						throw new InvalidOperationException(Res.GetString("Member '{0}.{1}' hides inherited member '{2}.{3}', but has different custom attributes.", new object[]
						{
							parent,
							member.Name,
							base.TypeName,
							this.members[i].Name
						}));
					}
					declaringMapping = this;
					return this.members[i];
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00054524 File Offset: 0x00052724
		internal bool Declares(MemberMapping member, string parent)
		{
			StructMapping structMapping;
			return this.FindDeclaringMapping(member, out structMapping, parent) != null;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00054540 File Offset: 0x00052740
		internal void SetContentModel(TextAccessor text, bool hasElements)
		{
			if (this.BaseMapping == null || this.BaseMapping.TypeDesc.IsRoot)
			{
				this.hasSimpleContent = !hasElements && text != null && !text.Mapping.IsList;
			}
			else if (this.BaseMapping.HasSimpleContent)
			{
				if (text != null || hasElements)
				{
					throw new InvalidOperationException(Res.GetString("Cannot serialize object of type '{0}'. Base type '{1}' has simpleContent and can only be extended by adding XmlAttribute elements. Please consider changing XmlText member of the base class to string array.", new object[]
					{
						base.TypeDesc.FullName,
						this.BaseMapping.TypeDesc.FullName
					}));
				}
				this.hasSimpleContent = true;
			}
			else
			{
				this.hasSimpleContent = false;
			}
			if (!this.hasSimpleContent && text != null && !text.Mapping.TypeDesc.CanBeTextValue)
			{
				throw new InvalidOperationException(Res.GetString("Cannot serialize object of type '{0}'. Consider changing type of XmlText member '{0}.{1}' from {2} to string or string array.", new object[]
				{
					base.TypeDesc.FullName,
					text.Name,
					text.Mapping.TypeDesc.FullName
				}));
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0005463F File Offset: 0x0005283F
		internal bool HasElements
		{
			get
			{
				return this.elements != null && this.elements.Values.Count > 0;
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00054660 File Offset: 0x00052860
		internal bool HasExplicitSequence()
		{
			if (this.members != null)
			{
				for (int i = 0; i < this.members.Length; i++)
				{
					if (this.members[i].IsParticle && this.members[i].IsSequence)
					{
						return true;
					}
				}
			}
			return this.baseMapping != null && this.baseMapping.HasExplicitSequence();
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000546C0 File Offset: 0x000528C0
		internal void SetSequence()
		{
			if (base.TypeDesc.IsRoot)
			{
				return;
			}
			StructMapping structMapping = this;
			while (!structMapping.BaseMapping.IsSequence && structMapping.BaseMapping != null && !structMapping.BaseMapping.TypeDesc.IsRoot)
			{
				structMapping = structMapping.BaseMapping;
			}
			structMapping.IsSequence = true;
			for (StructMapping structMapping2 = structMapping.DerivedMappings; structMapping2 != null; structMapping2 = structMapping2.NextDerivedMapping)
			{
				structMapping2.SetSequence();
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0005472D File Offset: 0x0005292D
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x00054747 File Offset: 0x00052947
		internal bool IsSequence
		{
			get
			{
				return this.isSequence && !base.TypeDesc.IsRoot;
			}
			set
			{
				this.isSequence = value;
			}
		}

		// Token: 0x04000852 RID: 2130
		private MemberMapping[] members;

		// Token: 0x04000853 RID: 2131
		private StructMapping baseMapping;

		// Token: 0x04000854 RID: 2132
		private StructMapping derivedMappings;

		// Token: 0x04000855 RID: 2133
		private StructMapping nextDerivedMapping;

		// Token: 0x04000856 RID: 2134
		private MemberMapping xmlnsMember;

		// Token: 0x04000857 RID: 2135
		private bool hasSimpleContent;

		// Token: 0x04000858 RID: 2136
		private bool openModel;

		// Token: 0x04000859 RID: 2137
		private bool isSequence;

		// Token: 0x0400085A RID: 2138
		private NameTable elements;

		// Token: 0x0400085B RID: 2139
		private NameTable attributes;

		// Token: 0x0400085C RID: 2140
		private CodeIdentifiers scope;
	}
}
