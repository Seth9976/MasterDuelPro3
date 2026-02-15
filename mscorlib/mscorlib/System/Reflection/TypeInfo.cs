using System;
using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>Represents type declarations for class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types. </summary>
	// Token: 0x02000629 RID: 1577
	public abstract class TypeInfo : Type, IReflectableType
	{
		/// <summary>Returns a representation of the current type as a <see cref="T:System.Reflection.TypeInfo" /> object.</summary>
		/// <returns>A reference to the current type.</returns>
		// Token: 0x06002E60 RID: 11872 RVA: 0x00002645 File Offset: 0x00000845
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		/// <summary>Gets a collection of the fields defined by the current type.</summary>
		/// <returns>A collection of the fields defined by the current type.</returns>
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000B3134 File Offset: 0x000B1334
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the properties defined by the current type. </summary>
		/// <returns>A collection of the properties defined by the current type.</returns>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000B313E File Offset: 0x000B133E
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the interfaces implemented by the current type.</summary>
		/// <returns>A collection of the interfaces implemented by the current type.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000B3148 File Offset: 0x000B1348
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			get
			{
				return this.GetInterfaces();
			}
		}
	}
}
