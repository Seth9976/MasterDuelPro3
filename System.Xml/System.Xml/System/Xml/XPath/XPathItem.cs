using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	/// <summary>Represents an item in the XQuery 1.0 and XPath 2.0 Data Model.</summary>
	// Token: 0x0200013B RID: 315
	public abstract class XPathItem
	{
		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the item.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the item.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F72 RID: 3954
		public abstract XmlSchemaType XmlType { get; }

		/// <summary>When overridden in a derived class, gets the string value of the item.</summary>
		/// <returns>The string value of the item.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F73 RID: 3955
		public abstract string Value { get; }

		/// <summary>When overridden in a derived class, gets the current item as a boxed object of the most appropriate .NET Framework 2.0 type according to its schema type.</summary>
		/// <returns>The current item as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F74 RID: 3956
		public abstract object TypedValue { get; }

		/// <summary>When overridden in a derived class, gets the .NET Framework 2.0 type of the item.</summary>
		/// <returns>The .NET Framework type of the item. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000F75 RID: 3957
		public abstract Type ValueType { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Boolean" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000F76 RID: 3958
		public abstract bool ValueAsBoolean { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.DateTime" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000F77 RID: 3959
		public abstract DateTime ValueAsDateTime { get; }

		/// <summary>When overridden in a derived class, gets the item's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The item's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000F78 RID: 3960
		public abstract double ValueAsDouble { get; }

		/// <summary>When overridden in a derived class, gets the item's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The item's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Int32" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000F79 RID: 3961
		public abstract int ValueAsInt { get; }

		/// <summary>When overridden in a derived class, gets the item's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The item's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the <see cref="T:System.Int64" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000F7A RID: 3962
		public abstract long ValueAsLong { get; }

		/// <summary>Returns the item's value as the specified type.</summary>
		/// <returns>The value of the item as the type requested.</returns>
		/// <param name="returnType">The type to return the item value as.</param>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06000F7B RID: 3963 RVA: 0x0004CFD9 File Offset: 0x0004B1D9
		public virtual object ValueAs(Type returnType)
		{
			return this.ValueAs(returnType, null);
		}

		/// <summary>When overridden in a derived class, returns the item's value as the type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the item as the type requested.</returns>
		/// <param name="returnType">The type to return the item's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The item's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x06000F7C RID: 3964
		public abstract object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver);
	}
}
