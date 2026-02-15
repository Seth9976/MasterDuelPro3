using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a type.</summary>
	// Token: 0x020001D7 RID: 471
	[Serializable]
	public class CodeTypeReference : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class. </summary>
		// Token: 0x06000B7E RID: 2942 RVA: 0x00039F1D File Offset: 0x0003811D
		public CodeTypeReference()
		{
			this._baseType = string.Empty;
			this.ArrayRank = 0;
			this.ArrayElementType = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to reference. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type " />is null.</exception>
		// Token: 0x06000B7F RID: 2943 RVA: 0x00039F40 File Offset: 0x00038140
		public CodeTypeReference(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsArray)
			{
				this.ArrayRank = type.GetArrayRank();
				this.ArrayElementType = new CodeTypeReference(type.GetElementType());
				this._baseType = null;
			}
			else
			{
				this.InitializeFromType(type);
				this.ArrayRank = 0;
				this.ArrayElementType = null;
			}
			this._isInterface = type.IsInterface;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name and code type reference option.</summary>
		/// <param name="typeName">The name of the type to reference.</param>
		/// <param name="codeTypeReferenceOption">The code type reference option, one of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values.</param>
		// Token: 0x06000B80 RID: 2944 RVA: 0x00039FB6 File Offset: 0x000381B6
		public CodeTypeReference(string typeName, CodeTypeReferenceOptions codeTypeReferenceOption)
		{
			this.Initialize(typeName, codeTypeReferenceOption);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name.</summary>
		/// <param name="typeName">The name of the type to reference. </param>
		// Token: 0x06000B81 RID: 2945 RVA: 0x00039FC6 File Offset: 0x000381C6
		public CodeTypeReference(string typeName)
		{
			this.Initialize(typeName);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00039FD8 File Offset: 0x000381D8
		private void InitializeFromType(Type type)
		{
			this._baseType = type.Name;
			if (!type.IsGenericParameter)
			{
				Type type2 = type;
				while (type2.IsNested)
				{
					type2 = type2.DeclaringType;
					this._baseType = type2.Name + "+" + this._baseType;
				}
				if (!string.IsNullOrEmpty(type.Namespace))
				{
					this._baseType = type.Namespace + "." + this._baseType;
				}
			}
			if (type.IsGenericType && !type.ContainsGenericParameters)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					this.TypeArguments.Add(new CodeTypeReference(genericArguments[i]));
				}
				return;
			}
			if (!type.IsGenericTypeDefinition)
			{
				this._needsFixup = true;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003A09A File Offset: 0x0003829A
		private void Initialize(string typeName)
		{
			this.Initialize(typeName, this.Options);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003A0AC File Offset: 0x000382AC
		private void Initialize(string typeName, CodeTypeReferenceOptions options)
		{
			this.Options = options;
			if (string.IsNullOrEmpty(typeName))
			{
				typeName = typeof(void).FullName;
				this._baseType = typeName;
				this.ArrayRank = 0;
				this.ArrayElementType = null;
				return;
			}
			typeName = this.RipOffAssemblyInformationFromTypeName(typeName);
			int num = typeName.Length - 1;
			int i = num;
			this._needsFixup = true;
			Queue<int> queue = new Queue<int>();
			while (i >= 0)
			{
				int num2 = 1;
				if (typeName[i--] != ']')
				{
					break;
				}
				while (i >= 0 && typeName[i] == ',')
				{
					num2++;
					i--;
				}
				if (i < 0 || typeName[i] != '[')
				{
					break;
				}
				queue.Enqueue(num2);
				i--;
				num = i;
			}
			i = num;
			List<CodeTypeReference> list = new List<CodeTypeReference>();
			Stack<string> stack = new Stack<string>();
			if (i > 0 && typeName[i--] == ']')
			{
				this._needsFixup = false;
				int num3 = 1;
				int num4 = num;
				while (i >= 0)
				{
					if (typeName[i] == '[')
					{
						if (--num3 == 0)
						{
							break;
						}
					}
					else if (typeName[i] == ']')
					{
						num3++;
					}
					else if (typeName[i] == ',' && num3 == 1)
					{
						if (i + 1 < num4)
						{
							stack.Push(typeName.Substring(i + 1, num4 - i - 1));
						}
						num4 = i;
					}
					i--;
				}
				if (i > 0 && num - i - 1 > 0)
				{
					if (i + 1 < num4)
					{
						stack.Push(typeName.Substring(i + 1, num4 - i - 1));
					}
					while (stack.Count > 0)
					{
						string text = this.RipOffAssemblyInformationFromTypeName(stack.Pop());
						list.Add(new CodeTypeReference(text));
					}
					num = i - 1;
				}
			}
			if (num < 0)
			{
				this._baseType = typeName;
				return;
			}
			if (queue.Count > 0)
			{
				CodeTypeReference codeTypeReference = new CodeTypeReference(typeName.Substring(0, num + 1), this.Options);
				for (int j = 0; j < list.Count; j++)
				{
					codeTypeReference.TypeArguments.Add(list[j]);
				}
				while (queue.Count > 1)
				{
					codeTypeReference = new CodeTypeReference(codeTypeReference, queue.Dequeue());
				}
				this._baseType = null;
				this.ArrayRank = queue.Dequeue();
				this.ArrayElementType = codeTypeReference;
			}
			else if (list.Count > 0)
			{
				for (int k = 0; k < list.Count; k++)
				{
					this.TypeArguments.Add(list[k]);
				}
				this._baseType = typeName.Substring(0, num + 1);
			}
			else
			{
				this._baseType = typeName;
			}
			if (this._baseType != null && this._baseType.IndexOf('`') != -1)
			{
				this._needsFixup = false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified array type and rank.</summary>
		/// <param name="arrayType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array. </param>
		/// <param name="rank">The number of dimensions in the array. </param>
		// Token: 0x06000B85 RID: 2949 RVA: 0x0003A344 File Offset: 0x00038544
		public CodeTypeReference(CodeTypeReference arrayType, int rank)
		{
			this._baseType = null;
			this.ArrayRank = rank;
			this.ArrayElementType = arrayType;
		}

		/// <summary>Gets or sets the type of the elements in the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array elements.</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0003A361 File Offset: 0x00038561
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0003A369 File Offset: 0x00038569
		public CodeTypeReference ArrayElementType { get; set; }

		/// <summary>Gets or sets the array rank of the array.</summary>
		/// <returns>The number of dimensions of the array.</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0003A372 File Offset: 0x00038572
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0003A37A File Offset: 0x0003857A
		public int ArrayRank { get; set; }

		/// <summary>Gets or sets the name of the type being referenced.</summary>
		/// <returns>The name of the type being referenced.</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0003A384 File Offset: 0x00038584
		public string BaseType
		{
			get
			{
				if (this.ArrayRank > 0 && this.ArrayElementType != null)
				{
					return this.ArrayElementType.BaseType;
				}
				if (string.IsNullOrEmpty(this._baseType))
				{
					return string.Empty;
				}
				string baseType = this._baseType;
				if (!this._needsFixup || this.TypeArguments.Count <= 0)
				{
					return baseType;
				}
				return baseType + "`" + this.TypeArguments.Count.ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the code type reference option.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values. </returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0003A403 File Offset: 0x00038603
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0003A40B File Offset: 0x0003860B
		public CodeTypeReferenceOptions Options { get; set; }

		/// <summary>Gets the type arguments for the current generic type reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current <see cref="T:System.CodeDom.CodeTypeReference" /> object.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0003A414 File Offset: 0x00038614
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.ArrayRank > 0 && this.ArrayElementType != null)
				{
					return this.ArrayElementType.TypeArguments;
				}
				if (this._typeArguments == null)
				{
					this._typeArguments = new CodeTypeReferenceCollection();
				}
				return this._typeArguments;
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003A44C File Offset: 0x0003864C
		private string RipOffAssemblyInformationFromTypeName(string typeName)
		{
			int i = 0;
			int num = typeName.Length - 1;
			string text = typeName;
			while (i < typeName.Length)
			{
				if (!char.IsWhiteSpace(typeName[i]))
				{
					break;
				}
				i++;
			}
			while (num >= 0 && char.IsWhiteSpace(typeName[num]))
			{
				num--;
			}
			if (i < num)
			{
				if (typeName[i] == '[' && typeName[num] == ']')
				{
					i++;
					num--;
				}
				if (typeName[num] != ']')
				{
					int num2 = 0;
					for (int j = num; j >= i; j--)
					{
						if (typeName[j] == ',')
						{
							num2++;
							if (num2 == 4)
							{
								text = typeName.Substring(i, j - i);
								break;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0400086B RID: 2155
		private string _baseType;

		// Token: 0x0400086C RID: 2156
		private readonly bool _isInterface;

		// Token: 0x0400086D RID: 2157
		private CodeTypeReferenceCollection _typeArguments;

		// Token: 0x0400086E RID: 2158
		private bool _needsFixup;
	}
}
