using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a parameter and provides access to parameter metadata.</summary>
	// Token: 0x02000612 RID: 1554
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterInfo : ICustomAttributeProvider, IObjectReference, _ParameterInfo
	{
		/// <summary>Initializes a new instance of the ParameterInfo class.</summary>
		// Token: 0x06002D55 RID: 11605 RVA: 0x00003CE1 File Offset: 0x00001EE1
		protected ParameterInfo()
		{
		}

		/// <summary>Gets the attributes for this parameter.</summary>
		/// <returns>A ParameterAttributes object representing the attributes for this parameter.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000B23A0 File Offset: 0x000B05A0
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		/// <summary>Gets a value indicating the member in which the parameter is implemented.</summary>
		/// <returns>The member which implanted the parameter represented by this <see cref="T:System.Reflection.ParameterInfo" />.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000B23A8 File Offset: 0x000B05A8
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>The simple name of this parameter.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000B23B0 File Offset: 0x000B05B0
		public virtual string Name
		{
			get
			{
				return this.NameImpl;
			}
		}

		/// <summary>Gets the Type of this parameter.</summary>
		/// <returns>The Type object that represents the Type of this parameter.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000B23B8 File Offset: 0x000B05B8
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		/// <summary>Gets the zero-based position of the parameter in the formal parameter list.</summary>
		/// <returns>An integer representing the position this parameter occupies in the parameter list.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000B23C0 File Offset: 0x000B05C0
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		/// <summary>Gets a value indicating whether this is an input parameter.</summary>
		/// <returns>true if the parameter is an input parameter; otherwise, false.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000B23C8 File Offset: 0x000B05C8
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is optional.</summary>
		/// <returns>true if the parameter is optional; otherwise, false.</returns>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000B23D5 File Offset: 0x000B05D5
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is an output parameter.</summary>
		/// <returns>true if the parameter is an output parameter; otherwise, false.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000B23E3 File Offset: 0x000B05E3
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0003398A File Offset: 0x00031B8A
		public virtual object DefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Determines whether the custom attribute of the specified type or its derived types is applied to this parameter.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> or its derived types are applied to this parameter; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to search for. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the common language runtime.</exception>
		// Token: 0x06002D5F RID: 11615 RVA: 0x000B23F0 File Offset: 0x000B05F0
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		/// <summary>Gets all the custom attributes defined on this parameter.</summary>
		/// <returns>An array that contains all the custom attributes applied to this parameter.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x06002D60 RID: 11616 RVA: 0x000B2407 File Offset: 0x000B0607
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return Array.Empty<object>();
		}

		/// <summary>Gets the custom attributes of the specified type or its derived types that are applied to this parameter.</summary>
		/// <returns>An array that contains the custom attributes of the specified type or its derived types.</returns>
		/// <param name="attributeType">The custom attributes identified by type. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x06002D61 RID: 11617 RVA: 0x000B240E File Offset: 0x000B060E
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return Array.Empty<object>();
		}

		/// <summary>Returns the real object that should be deserialized instead of the object that the serialized stream specifies.</summary>
		/// <returns>The actual object that is put into the graph.</returns>
		/// <param name="context">The serialized stream from which the current object is deserialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The parameter's position in the parameter list of its associated member is not valid for that member's type.</exception>
		// Token: 0x06002D62 RID: 11618 RVA: 0x000B242C File Offset: 0x000B062C
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException("Insufficient state to return the real object.");
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException("Serialized member does not have a ParameterInfo.");
				}
				ParameterInfo[] array = ((PropertyInfo)this.MemberImpl).GetIndexParameters();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
		}

		/// <summary>Gets the parameter type and name represented as a string.</summary>
		/// <returns>A string containing the type and the name of the parameter.</returns>
		// Token: 0x06002D63 RID: 11619 RVA: 0x000B251E File Offset: 0x000B071E
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		/// <summary>The attributes of the parameter.</summary>
		// Token: 0x04001763 RID: 5987
		protected ParameterAttributes AttrsImpl;

		/// <summary>The Type of the parameter.</summary>
		// Token: 0x04001764 RID: 5988
		protected Type ClassImpl;

		/// <summary>The default value of the parameter.</summary>
		// Token: 0x04001765 RID: 5989
		protected object DefaultValueImpl;

		/// <summary>The member in which the field is implemented.</summary>
		// Token: 0x04001766 RID: 5990
		protected MemberInfo MemberImpl;

		/// <summary>The name of the parameter.</summary>
		// Token: 0x04001767 RID: 5991
		protected string NameImpl;

		/// <summary>The zero-based position of the parameter in the parameter list.</summary>
		// Token: 0x04001768 RID: 5992
		protected int PositionImpl;

		// Token: 0x04001769 RID: 5993
		private const int MetadataToken_ParamDef = 134217728;
	}
}
