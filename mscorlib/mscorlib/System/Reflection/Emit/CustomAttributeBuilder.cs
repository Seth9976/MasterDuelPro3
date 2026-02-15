using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Helps build custom attributes.</summary>
	// Token: 0x0200065A RID: 1626
	[ComDefaultInterface(typeof(_CustomAttributeBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class CustomAttributeBuilder : _CustomAttributeBuilder
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000B9E83 File Offset: 0x000B8083
		internal ConstructorInfo Ctor
		{
			get
			{
				return this.ctor;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000B9E8B File Offset: 0x000B808B
		internal byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06003167 RID: 12647
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetBlob(Assembly asmb, ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues);

		// Token: 0x06003168 RID: 12648 RVA: 0x000B9E94 File Offset: 0x000B8094
		internal object Invoke()
		{
			object obj = this.ctor.Invoke(this.args);
			for (int i = 0; i < this.namedFields.Length; i++)
			{
				this.namedFields[i].SetValue(obj, this.fieldValues[i]);
			}
			for (int j = 0; j < this.namedProperties.Length; j++)
			{
				this.namedProperties[j].SetValue(obj, this.propertyValues[j]);
			}
			return obj;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000B9F08 File Offset: 0x000B8108
		internal CustomAttributeBuilder(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.ctor = con;
			this.data = (byte[])binaryAttribute.Clone();
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute and the arguments to the constructor.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor. -or-A supplied argument is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="constructorArgs" /> is null. </exception>
		// Token: 0x0600316A RID: 12650 RVA: 0x000B9F55 File Offset: 0x000B8155
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000B9F80 File Offset: 0x000B8180
		private bool IsValidType(Type t)
		{
			if (t.IsArray && t.GetArrayRank() > 1)
			{
				return false;
			}
			if (t is TypeBuilder && t.IsEnum)
			{
				Enum.GetUnderlyingType(t);
			}
			return (!t.IsClass || t.IsArray || t == typeof(object) || t == typeof(Type) || t == typeof(string) || t.Assembly.GetName().Name == "mscorlib") && (!t.IsValueType || t.IsPrimitive || t.IsEnum || (t.Assembly is AssemblyBuilder && t.Assembly.GetName().Name == "mscorlib"));
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000BA060 File Offset: 0x000B8260
		private bool IsValidParam(object o, Type paramType)
		{
			Type type = o.GetType();
			if (!this.IsValidType(type))
			{
				return false;
			}
			if (paramType == typeof(object))
			{
				if (type.IsArray && type.GetArrayRank() == 1)
				{
					return this.IsValidType(type.GetElementType());
				}
				if (!type.IsPrimitive && !typeof(Type).IsAssignableFrom(type) && type != typeof(string) && !type.IsEnum)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000BA0E8 File Offset: 0x000B82E8
		private static bool IsValidValue(Type type, object value)
		{
			if (type.IsValueType && value == null)
			{
				return false;
			}
			if (type.IsArray && type.GetElementType().IsValueType)
			{
				using (IEnumerator enumerator = ((Array)value).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000BA160 File Offset: 0x000B8360
		private void Initialize(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.ctor = con;
			this.args = constructorArgs;
			this.namedProperties = namedProperties;
			this.propertyValues = propertyValues;
			this.namedFields = namedFields;
			this.fieldValues = fieldValues;
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (constructorArgs == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (namedProperties == null)
			{
				throw new ArgumentNullException("namedProperties");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (namedFields == null)
			{
				throw new ArgumentNullException("namedFields");
			}
			if (fieldValues == null)
			{
				throw new ArgumentNullException("fieldValues");
			}
			if (con.GetParametersCount() != constructorArgs.Length)
			{
				throw new ArgumentException("Parameter count does not match passed in argument value count.");
			}
			if (namedProperties.Length != propertyValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedProperties, propertyValues");
			}
			if (namedFields.Length != fieldValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedFields, fieldValues");
			}
			if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
			{
				throw new ArgumentException("Cannot have private or static constructor.");
			}
			Type declaringType = this.ctor.DeclaringType;
			int num = 0;
			foreach (FieldInfo fieldInfo in namedFields)
			{
				Type declaringType2 = fieldInfo.DeclaringType;
				if (declaringType != declaringType2 && !declaringType2.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType2))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (!this.IsValidType(fieldInfo.FieldType))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not have a valid type.");
				}
				if (!CustomAttributeBuilder.IsValidValue(fieldInfo.FieldType, fieldValues[num]))
				{
					throw new ArgumentException("Field " + fieldInfo.Name + " is not a valid value.");
				}
				if (fieldValues[num] != null && !(fieldInfo.FieldType is TypeBuilder) && !fieldInfo.FieldType.IsEnum && !fieldInfo.FieldType.IsInstanceOfType(fieldValues[num]) && !fieldInfo.FieldType.IsArray)
				{
					string text = "Value of field '";
					string name = fieldInfo.Name;
					string text2 = "' does not match field type: ";
					Type fieldType = fieldInfo.FieldType;
					throw new ArgumentException(text + name + text2 + ((fieldType != null) ? fieldType.ToString() : null));
				}
				num++;
			}
			num = 0;
			foreach (PropertyInfo propertyInfo in namedProperties)
			{
				if (!propertyInfo.CanWrite)
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a setter.");
				}
				Type declaringType3 = propertyInfo.DeclaringType;
				if (declaringType != declaringType3 && !declaringType3.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType3))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (!this.IsValidType(propertyInfo.PropertyType))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a valid type.");
				}
				if (!CustomAttributeBuilder.IsValidValue(propertyInfo.PropertyType, propertyValues[num]))
				{
					throw new ArgumentException("Property " + propertyInfo.Name + " is not a valid value.");
				}
				if (propertyValues[num] != null && !(propertyInfo.PropertyType is TypeBuilder) && !propertyInfo.PropertyType.IsEnum && !propertyInfo.PropertyType.IsInstanceOfType(propertyValues[num]) && !propertyInfo.PropertyType.IsArray)
				{
					string[] array = new string[6];
					array[0] = "Value of property '";
					array[1] = propertyInfo.Name;
					array[2] = "' does not match property type: ";
					int num2 = 3;
					Type propertyType = propertyInfo.PropertyType;
					array[num2] = ((propertyType != null) ? propertyType.ToString() : null);
					array[4] = " -> ";
					int num3 = 5;
					object obj = propertyValues[num];
					array[num3] = ((obj != null) ? obj.ToString() : null);
					throw new ArgumentException(string.Concat(array));
				}
				num++;
			}
			num = 0;
			foreach (ParameterInfo parameterInfo in CustomAttributeBuilder.GetParameters(con))
			{
				if (parameterInfo != null)
				{
					Type parameterType = parameterInfo.ParameterType;
					if (!this.IsValidType(parameterType))
					{
						throw new ArgumentException("Parameter " + num.ToString() + " does not have a valid type.");
					}
					if (!CustomAttributeBuilder.IsValidValue(parameterType, constructorArgs[num]))
					{
						throw new ArgumentException("Parameter " + num.ToString() + " is not a valid value.");
					}
					if (constructorArgs[num] != null)
					{
						if (!(parameterType is TypeBuilder) && !parameterType.IsEnum && !parameterType.IsInstanceOfType(constructorArgs[num]) && !parameterType.IsArray)
						{
							string[] array2 = new string[6];
							array2[0] = "Value of argument ";
							array2[1] = num.ToString();
							array2[2] = " does not match parameter type: ";
							int num4 = 3;
							Type type = parameterType;
							array2[num4] = ((type != null) ? type.ToString() : null);
							array2[4] = " -> ";
							int num5 = 5;
							object obj2 = constructorArgs[num];
							array2[num5] = ((obj2 != null) ? obj2.ToString() : null);
							throw new ArgumentException(string.Concat(array2));
						}
						if (!this.IsValidParam(constructorArgs[num], parameterType))
						{
							string text3 = "Cannot emit a CustomAttribute with argument of type ";
							Type type2 = constructorArgs[num].GetType();
							throw new ArgumentException(text3 + ((type2 != null) ? type2.ToString() : null) + ".");
						}
					}
				}
				num++;
			}
			this.data = CustomAttributeBuilder.GetBlob(declaringType.Assembly, con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000BA684 File Offset: 0x000B8884
		internal static int decode_len(byte[] data, int pos, out int rpos)
		{
			int num;
			if ((data[pos] & 128) == 0)
			{
				num = (int)(data[pos++] & 127);
			}
			else if ((data[pos] & 64) == 0)
			{
				num = ((int)(data[pos] & 63) << 8) + (int)data[pos + 1];
				pos += 2;
			}
			else
			{
				num = ((int)(data[pos] & 31) << 24) + ((int)data[pos + 1] << 16) + ((int)data[pos + 2] << 8) + (int)data[pos + 3];
				pos += 4;
			}
			rpos = pos;
			return num;
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000BA6F4 File Offset: 0x000B88F4
		internal static string string_from_bytes(byte[] data, int pos, int len)
		{
			return Encoding.UTF8.GetString(data, pos, len);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000BA704 File Offset: 0x000B8904
		internal static string decode_string(byte[] data, int pos, out int rpos)
		{
			if (data[pos] == 255)
			{
				rpos = pos + 1;
				return null;
			}
			int num = CustomAttributeBuilder.decode_len(data, pos, out pos);
			string text = CustomAttributeBuilder.string_from_bytes(data, pos, num);
			pos += num;
			rpos = pos;
			return text;
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000BA73C File Offset: 0x000B893C
		internal string string_arg()
		{
			int num = 2;
			return CustomAttributeBuilder.decode_string(this.data, num, out num);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000BA75C File Offset: 0x000B895C
		private static ParameterInfo[] GetParameters(ConstructorInfo ctor)
		{
			ConstructorBuilder constructorBuilder = ctor as ConstructorBuilder;
			if (constructorBuilder != null)
			{
				return constructorBuilder.GetParametersInternal();
			}
			return ctor.GetParametersInternal();
		}

		// Token: 0x04001930 RID: 6448
		private ConstructorInfo ctor;

		// Token: 0x04001931 RID: 6449
		private byte[] data;

		// Token: 0x04001932 RID: 6450
		private object[] args;

		// Token: 0x04001933 RID: 6451
		private PropertyInfo[] namedProperties;

		// Token: 0x04001934 RID: 6452
		private object[] propertyValues;

		// Token: 0x04001935 RID: 6453
		private FieldInfo[] namedFields;

		// Token: 0x04001936 RID: 6454
		private object[] fieldValues;
	}
}
