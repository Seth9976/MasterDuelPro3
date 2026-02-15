using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	/// <summary>Provides access to custom attribute data for assemblies, modules, types, members and parameters that are loaded into the reflection-only context.</summary>
	// Token: 0x02000636 RID: 1590
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</summary>
		// Token: 0x06002F39 RID: 12089 RVA: 0x00003CE1 File Offset: 0x00001EE1
		protected CustomAttributeData()
		{
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000B5095 File Offset: 0x000B3295
		internal CustomAttributeData(ConstructorInfo ctorInfo, Assembly assembly, IntPtr data, uint data_length)
		{
			this.ctorInfo = ctorInfo;
			this.lazyData = new CustomAttributeData.LazyCAttrData();
			this.lazyData.assembly = assembly;
			this.lazyData.data = data;
			this.lazyData.data_length = data_length;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000B50D4 File Offset: 0x000B32D4
		internal CustomAttributeData(ConstructorInfo ctorInfo)
			: this(ctorInfo, Array.Empty<CustomAttributeTypedArgument>(), Array.Empty<CustomAttributeNamedArgument>())
		{
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000B50E7 File Offset: 0x000B32E7
		internal CustomAttributeData(ConstructorInfo ctorInfo, IList<CustomAttributeTypedArgument> ctorArgs, IList<CustomAttributeNamedArgument> namedArgs)
		{
			this.ctorInfo = ctorInfo;
			this.ctorArgs = ctorArgs;
			this.namedArgs = namedArgs;
		}

		// Token: 0x06002F3D RID: 12093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveArgumentsInternal(ConstructorInfo ctor, Assembly assembly, IntPtr data, uint data_length, out object[] ctorArgs, out object[] namedArgs);

		// Token: 0x06002F3E RID: 12094 RVA: 0x000B5104 File Offset: 0x000B3304
		private void ResolveArguments()
		{
			if (this.lazyData == null)
			{
				return;
			}
			object[] array;
			object[] array2;
			CustomAttributeData.ResolveArgumentsInternal(this.ctorInfo, this.lazyData.assembly, this.lazyData.data, this.lazyData.data_length, out array, out array2);
			this.ctorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>((array != null) ? CustomAttributeData.UnboxValues<CustomAttributeTypedArgument>(array) : Array.Empty<CustomAttributeTypedArgument>());
			this.namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>((array2 != null) ? CustomAttributeData.UnboxValues<CustomAttributeNamedArgument>(array2) : Array.Empty<CustomAttributeNamedArgument>());
			this.lazyData = null;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor that would have initialized the custom attribute.</summary>
		/// <returns>An object that represents the constructor that would have initialized the custom attribute represented by the current instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</returns>
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000B5187 File Offset: 0x000B3387
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.ctorInfo;
			}
		}

		/// <summary>Gets the list of positional arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the positional arguments specified for the custom attribute instance.</returns>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000B518F File Offset: 0x000B338F
		[ComVisible(true)]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				this.ResolveArguments();
				return this.ctorArgs;
			}
		}

		/// <summary>Gets the list of named arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the named arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x000B519D File Offset: 0x000B339D
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				this.ResolveArguments();
				return this.namedArgs;
			}
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target assembly.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target assembly.</returns>
		/// <param name="target">The assembly whose custom attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002F42 RID: 12098 RVA: 0x000B51AB File Offset: 0x000B33AB
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target member.</returns>
		/// <param name="target">The member whose attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002F43 RID: 12099 RVA: 0x000B51AB File Offset: 0x000B33AB
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000B51AB File Offset: 0x000B33AB
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target module.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target module.</returns>
		/// <param name="target">The module whose custom attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002F45 RID: 12101 RVA: 0x000B51AB File Offset: 0x000B33AB
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target parameter.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target parameter.</returns>
		/// <param name="target">The parameter whose attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002F46 RID: 12102 RVA: 0x000B51AB File Offset: 0x000B33AB
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Gets the type of the attribute.</summary>
		/// <returns>The type of the attribute.</returns>
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000B51B4 File Offset: 0x000B33B4
		public Type AttributeType
		{
			get
			{
				return this.ctorInfo.DeclaringType;
			}
		}

		/// <summary>Returns a string representation of the custom attribute.</summary>
		/// <returns>A string value that represents the custom attribute.</returns>
		// Token: 0x06002F48 RID: 12104 RVA: 0x000B51C4 File Offset: 0x000B33C4
		public override string ToString()
		{
			this.ResolveArguments();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[" + this.ctorInfo.DeclaringType.FullName + "(");
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				stringBuilder.Append(this.ctorArgs[i].ToString());
				if (i + 1 < this.ctorArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			if (this.namedArgs.Count > 0)
			{
				stringBuilder.Append(", ");
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				stringBuilder.Append(this.namedArgs[j].ToString());
				if (j + 1 < this.namedArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.AppendFormat(")]", Array.Empty<object>());
			return stringBuilder.ToString();
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000B52DC File Offset: 0x000B34DC
		private static T[] UnboxValues<T>(object[] values)
		{
			T[] array = new T[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = (T)((object)values[i]);
			}
			return array;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002F4A RID: 12106 RVA: 0x000B5310 File Offset: 0x000B3510
		public override bool Equals(object obj)
		{
			CustomAttributeData customAttributeData = obj as CustomAttributeData;
			if (customAttributeData == null || customAttributeData.ctorInfo != this.ctorInfo || customAttributeData.ctorArgs.Count != this.ctorArgs.Count || customAttributeData.namedArgs.Count != this.namedArgs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				if (this.ctorArgs[i].Equals(customAttributeData.ctorArgs[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				bool flag = false;
				for (int k = 0; k < customAttributeData.namedArgs.Count; k++)
				{
					if (this.namedArgs[j].Equals(customAttributeData.namedArgs[k]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000B5420 File Offset: 0x000B3620
		public override int GetHashCode()
		{
			int num = ((this.ctorInfo == null) ? 13 : (this.ctorInfo.GetHashCode() << 16));
			if (this.ctorArgs != null)
			{
				for (int i = 0; i < this.ctorArgs.Count; i++)
				{
					num += num ^ (7 + this.ctorArgs[i].GetHashCode() << i * 4);
				}
			}
			if (this.namedArgs != null)
			{
				for (int j = 0; j < this.namedArgs.Count; j++)
				{
					num += this.namedArgs[j].GetHashCode() << 5;
				}
			}
			return num;
		}

		// Token: 0x0400183F RID: 6207
		private ConstructorInfo ctorInfo;

		// Token: 0x04001840 RID: 6208
		private IList<CustomAttributeTypedArgument> ctorArgs;

		// Token: 0x04001841 RID: 6209
		private IList<CustomAttributeNamedArgument> namedArgs;

		// Token: 0x04001842 RID: 6210
		private CustomAttributeData.LazyCAttrData lazyData;

		// Token: 0x02000637 RID: 1591
		private class LazyCAttrData
		{
			// Token: 0x04001843 RID: 6211
			internal Assembly assembly;

			// Token: 0x04001844 RID: 6212
			internal IntPtr data;

			// Token: 0x04001845 RID: 6213
			internal uint data_length;
		}
	}
}
