using System;
using System.Globalization;
using Internal.Runtime.Augments;

namespace System.Reflection
{
	/// <summary>Represents a named argument of a custom attribute in the reflection-only context.</summary>
	// Token: 0x0200062A RID: 1578
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x06002E64 RID: 11876 RVA: 0x000B3150 File Offset: 0x000B1350
		internal CustomAttributeNamedArgument(Type attributeType, string memberName, bool isField, CustomAttributeTypedArgument typedValue)
		{
			this.IsField = isField;
			this.MemberName = memberName;
			this.TypedValue = typedValue;
			this._attributeType = attributeType;
			this._lazyMemberInfo = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> class, which represents the specified field or property of the custom attribute, and specifies the value of the field or property.</summary>
		/// <param name="memberInfo">A field or property of the custom attribute. The new <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> object represents this member and its value.</param>
		/// <param name="value">The value of the field or property of the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="memberInfo" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="memberInfo" /> is not a field or property of the custom attribute.</exception>
		// Token: 0x06002E65 RID: 11877 RVA: 0x000B3178 File Offset: 0x000B1378
		public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			Type type;
			if (fieldInfo != null)
			{
				type = fieldInfo.FieldType;
			}
			else
			{
				if (!(propertyInfo != null))
				{
					throw new ArgumentException("The member must be either a field or a property.");
				}
				type = propertyInfo.PropertyType;
			}
			this._lazyMemberInfo = memberInfo;
			this._attributeType = memberInfo.DeclaringType;
			if (value is CustomAttributeTypedArgument)
			{
				CustomAttributeTypedArgument customAttributeTypedArgument = (CustomAttributeTypedArgument)value;
				this.TypedValue = customAttributeTypedArgument;
			}
			else
			{
				this.TypedValue = new CustomAttributeTypedArgument(type, value);
			}
			this.IsField = fieldInfo != null;
			this.MemberName = memberInfo.Name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> class, which represents the specified field or property of the custom attribute, and specifies a <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> object that describes the type and value of the field or property.</summary>
		/// <param name="memberInfo">A field or property of the custom attribute. The new <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> object represents this member and its value.</param>
		/// <param name="typedArgument">An object that describes the type and value of the field or property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="memberInfo" /> is null.</exception>
		// Token: 0x06002E66 RID: 11878 RVA: 0x000B322C File Offset: 0x000B142C
		public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			this._lazyMemberInfo = memberInfo;
			this._attributeType = memberInfo.DeclaringType;
			this.TypedValue = typedArgument;
			this.IsField = memberInfo is FieldInfo;
			this.MemberName = memberInfo.Name;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure that can be used to obtain the type and value of the current named argument.</summary>
		/// <returns>A structure that can be used to obtain the type and value of the current named argument.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000B3284 File Offset: 0x000B1484
		public readonly CustomAttributeTypedArgument TypedValue { get; }

		/// <summary>Gets a value that indicates whether the named argument is a field.</summary>
		/// <returns>true if the named argument is a field; otherwise, false.</returns>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000B328C File Offset: 0x000B148C
		public readonly bool IsField { get; }

		/// <summary>Gets the name of the attribute member that would be used to set the named argument.</summary>
		/// <returns>The name of the attribute member that would be used to set the named argument.</returns>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000B3294 File Offset: 0x000B1494
		public readonly string MemberName { get; }

		/// <summary>Gets the attribute member that would be used to set the named argument.</summary>
		/// <returns>The attribute member that would be used to set the named argument.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000B329C File Offset: 0x000B149C
		public MemberInfo MemberInfo
		{
			get
			{
				MemberInfo memberInfo = this._lazyMemberInfo;
				if (memberInfo == null)
				{
					if (this.IsField)
					{
						memberInfo = this._attributeType.GetField(this.MemberName, BindingFlags.Instance | BindingFlags.Public);
					}
					else
					{
						memberInfo = this._attributeType.GetProperty(this.MemberName, BindingFlags.Instance | BindingFlags.Public);
					}
					if (memberInfo == null)
					{
						throw RuntimeAugments.Callbacks.CreateMissingMetadataException(this._attributeType);
					}
					this._lazyMemberInfo = memberInfo;
				}
				return memberInfo;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002E6B RID: 11883 RVA: 0x000B3311 File Offset: 0x000B1511
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002E6C RID: 11884 RVA: 0x000B3321 File Offset: 0x000B1521
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The structure to the left of the equality operator.</param>
		/// <param name="right">The structure to the right of the equality operator.</param>
		// Token: 0x06002E6D RID: 11885 RVA: 0x000B3333 File Offset: 0x000B1533
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The structure to the left of the inequality operator.</param>
		/// <param name="right">The structure to the right of the inequality operator.</param>
		// Token: 0x06002E6E RID: 11886 RVA: 0x000B3348 File Offset: 0x000B1548
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a string that consists of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string that consists of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x06002E6F RID: 11887 RVA: 0x000B3360 File Offset: 0x000B1560
		public override string ToString()
		{
			if (this._attributeType == null)
			{
				return base.ToString();
			}
			string text;
			try
			{
				bool flag = this._lazyMemberInfo == null || (this.IsField ? ((FieldInfo)this._lazyMemberInfo).FieldType : ((PropertyInfo)this._lazyMemberInfo).PropertyType) != typeof(object);
				text = string.Format(CultureInfo.CurrentCulture, "{0} = {1}", this.MemberName, this.TypedValue.ToString(flag));
			}
			catch (MissingMetadataException)
			{
				text = base.ToString();
			}
			return text;
		}

		// Token: 0x040017B7 RID: 6071
		private readonly Type _attributeType;

		// Token: 0x040017B8 RID: 6072
		private volatile MemberInfo _lazyMemberInfo;
	}
}
