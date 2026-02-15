using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200016B RID: 363
	internal abstract class AccessorMapping : Mapping
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x0005422C File Offset: 0x0005242C
		internal AccessorMapping()
		{
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00054750 File Offset: 0x00052950
		protected AccessorMapping(AccessorMapping mapping)
			: base(mapping)
		{
			this.typeDesc = mapping.typeDesc;
			this.attribute = mapping.attribute;
			this.elements = mapping.elements;
			this.sortedElements = mapping.sortedElements;
			this.text = mapping.text;
			this.choiceIdentifier = mapping.choiceIdentifier;
			this.xmlns = mapping.xmlns;
			this.ignore = mapping.ignore;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x000547C4 File Offset: 0x000529C4
		internal bool IsAttribute
		{
			get
			{
				return this.attribute != null;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x000547CF File Offset: 0x000529CF
		internal bool IsText
		{
			get
			{
				return this.text != null && (this.elements == null || this.elements.Length == 0);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x000547EF File Offset: 0x000529EF
		internal bool IsParticle
		{
			get
			{
				return this.elements != null && this.elements.Length != 0;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00054805 File Offset: 0x00052A05
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0005480D File Offset: 0x00052A0D
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00054816 File Offset: 0x00052A16
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x0005481E File Offset: 0x00052A1E
		internal AttributeAccessor Attribute
		{
			get
			{
				return this.attribute;
			}
			set
			{
				this.attribute = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00054827 File Offset: 0x00052A27
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x0005482F File Offset: 0x00052A2F
		internal ElementAccessor[] Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
				this.sortedElements = null;
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0005483F File Offset: 0x00052A3F
		internal static void SortMostToLeastDerived(ElementAccessor[] elements)
		{
			Array.Sort(elements, new AccessorMapping.AccessorComparer());
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0005484C File Offset: 0x00052A4C
		internal ElementAccessor[] ElementsSortedByDerivation
		{
			get
			{
				if (this.sortedElements != null)
				{
					return this.sortedElements;
				}
				if (this.elements == null)
				{
					return null;
				}
				this.sortedElements = new ElementAccessor[this.elements.Length];
				Array.Copy(this.elements, 0, this.sortedElements, 0, this.elements.Length);
				AccessorMapping.SortMostToLeastDerived(this.sortedElements);
				return this.sortedElements;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000548B1 File Offset: 0x00052AB1
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x000548B9 File Offset: 0x00052AB9
		internal TextAccessor Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000548C2 File Offset: 0x00052AC2
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x000548CA File Offset: 0x00052ACA
		internal ChoiceIdentifierAccessor ChoiceIdentifier
		{
			get
			{
				return this.choiceIdentifier;
			}
			set
			{
				this.choiceIdentifier = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000548D3 File Offset: 0x00052AD3
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x000548DB File Offset: 0x00052ADB
		internal XmlnsAccessor Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000548E4 File Offset: 0x00052AE4
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x000548EC File Offset: 0x00052AEC
		internal bool Ignore
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000548F5 File Offset: 0x00052AF5
		internal Accessor Accessor
		{
			get
			{
				if (this.xmlns != null)
				{
					return this.xmlns;
				}
				if (this.attribute != null)
				{
					return this.attribute;
				}
				if (this.elements != null && this.elements.Length != 0)
				{
					return this.elements[0];
				}
				return this.text;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00054938 File Offset: 0x00052B38
		private static bool IsNeedNullableMember(ElementAccessor element)
		{
			if (element.Mapping is ArrayMapping)
			{
				ArrayMapping arrayMapping = (ArrayMapping)element.Mapping;
				return arrayMapping.Elements != null && arrayMapping.Elements.Length == 1 && AccessorMapping.IsNeedNullableMember(arrayMapping.Elements[0]);
			}
			return element.IsNullable && element.Mapping.TypeDesc.IsValueType;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0005499B File Offset: 0x00052B9B
		internal bool IsNeedNullable
		{
			get
			{
				return this.xmlns == null && this.attribute == null && (this.elements != null && this.elements.Length == 1) && AccessorMapping.IsNeedNullableMember(this.elements[0]);
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000549D4 File Offset: 0x00052BD4
		internal static bool ElementsMatch(ElementAccessor[] a, ElementAccessor[] b)
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i].Name != b[i].Name || a[i].Namespace != b[i].Namespace || a[i].Form != b[i].Form || a[i].IsNullable != b[i].IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00054A60 File Offset: 0x00052C60
		internal bool Match(AccessorMapping mapping)
		{
			if (this.Elements != null && this.Elements.Length != 0)
			{
				if (!AccessorMapping.ElementsMatch(this.Elements, mapping.Elements))
				{
					return false;
				}
				if (this.Text == null)
				{
					return mapping.Text == null;
				}
			}
			if (this.Attribute != null)
			{
				return mapping.Attribute != null && (this.Attribute.Name == mapping.Attribute.Name && this.Attribute.Namespace == mapping.Attribute.Namespace) && this.Attribute.Form == mapping.Attribute.Form;
			}
			if (this.Text != null)
			{
				return mapping.Text != null;
			}
			return mapping.Accessor == null;
		}

		// Token: 0x0400085D RID: 2141
		private TypeDesc typeDesc;

		// Token: 0x0400085E RID: 2142
		private AttributeAccessor attribute;

		// Token: 0x0400085F RID: 2143
		private ElementAccessor[] elements;

		// Token: 0x04000860 RID: 2144
		private ElementAccessor[] sortedElements;

		// Token: 0x04000861 RID: 2145
		private TextAccessor text;

		// Token: 0x04000862 RID: 2146
		private ChoiceIdentifierAccessor choiceIdentifier;

		// Token: 0x04000863 RID: 2147
		private XmlnsAccessor xmlns;

		// Token: 0x04000864 RID: 2148
		private bool ignore;

		// Token: 0x0200016C RID: 364
		internal class AccessorComparer : IComparer
		{
			// Token: 0x06001192 RID: 4498 RVA: 0x00054B28 File Offset: 0x00052D28
			public int Compare(object o1, object o2)
			{
				if (o1 == o2)
				{
					return 0;
				}
				Accessor accessor = (Accessor)o1;
				Accessor accessor2 = (Accessor)o2;
				int weight = accessor.Mapping.TypeDesc.Weight;
				int weight2 = accessor2.Mapping.TypeDesc.Weight;
				if (weight == weight2)
				{
					return 0;
				}
				if (weight < weight2)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
