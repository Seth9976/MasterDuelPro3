using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Pool;

namespace Unity.Properties
{
	// Token: 0x0200001B RID: 27
	public readonly struct PropertyPath : IEquatable<PropertyPath>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public int Length { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002BAD File Offset: 0x00000DAD
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000014 RID: 20
		public PropertyPathPart this[int index]
		{
			get
			{
				PropertyPathPart propertyPathPart;
				switch (index)
				{
				case 0:
				{
					bool flag = this.Length < 1;
					if (flag)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part0;
					break;
				}
				case 1:
				{
					bool flag2 = this.Length < 2;
					if (flag2)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part1;
					break;
				}
				case 2:
				{
					bool flag3 = this.Length < 3;
					if (flag3)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part2;
					break;
				}
				case 3:
				{
					bool flag4 = this.Length < 4;
					if (flag4)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part3;
					break;
				}
				default:
					propertyPathPart = this.m_AdditionalParts[index - 4];
					break;
				}
				return propertyPathPart;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C70 File Offset: 0x00000E70
		public PropertyPath(string path)
		{
			PropertyPath p = PropertyPath.ConstructFromPath(path);
			this.m_Part0 = p.m_Part0;
			this.m_Part1 = p.m_Part1;
			this.m_Part2 = p.m_Part2;
			this.m_Part3 = p.m_Part3;
			this.m_AdditionalParts = p.m_AdditionalParts;
			this.Length = p.Length;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002CD0 File Offset: 0x00000ED0
		private PropertyPath(in PropertyPathPart part)
		{
			this.m_Part0 = part;
			this.m_Part1 = default(PropertyPathPart);
			this.m_Part2 = default(PropertyPathPart);
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = null;
			this.Length = 1;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D1C File Offset: 0x00000F1C
		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1)
		{
			this.m_Part0 = part0;
			this.m_Part1 = part1;
			this.m_Part2 = default(PropertyPathPart);
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = null;
			this.Length = 2;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D68 File Offset: 0x00000F68
		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1, in PropertyPathPart part2)
		{
			this.m_Part0 = part0;
			this.m_Part1 = part1;
			this.m_Part2 = part2;
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = null;
			this.Length = 3;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1, in PropertyPathPart part2, in PropertyPathPart part3)
		{
			this.m_Part0 = part0;
			this.m_Part1 = part1;
			this.m_Part2 = part2;
			this.m_Part3 = part3;
			this.m_AdditionalParts = null;
			this.Length = 4;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E04 File Offset: 0x00001004
		internal PropertyPath(List<PropertyPathPart> parts)
		{
			this.m_Part0 = default(PropertyPathPart);
			this.m_Part1 = default(PropertyPathPart);
			this.m_Part2 = default(PropertyPathPart);
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = ((parts.Count > 4) ? new PropertyPathPart[parts.Count - 4] : null);
			for (int i = 0; i < parts.Count; i++)
			{
				switch (i)
				{
				case 0:
					this.m_Part0 = parts[i];
					break;
				case 1:
					this.m_Part1 = parts[i];
					break;
				case 2:
					this.m_Part2 = parts[i];
					break;
				case 3:
					this.m_Part3 = parts[i];
					break;
				default:
					this.m_AdditionalParts[i - 4] = parts[i];
					break;
				}
			}
			this.Length = parts.Count;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EF8 File Offset: 0x000010F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromIndex(int index)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(index);
			return new PropertyPath(in propertyPathPart);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F14 File Offset: 0x00001114
		public static PropertyPath Combine(in PropertyPath path, in PropertyPath pathToAppend)
		{
			bool isEmpty = path.IsEmpty;
			PropertyPath propertyPath;
			if (isEmpty)
			{
				propertyPath = pathToAppend;
			}
			else
			{
				bool isEmpty2 = pathToAppend.IsEmpty;
				if (isEmpty2)
				{
					propertyPath = path;
				}
				else
				{
					int firstPathLength = path.Length;
					int secondPathLength = pathToAppend.Length;
					int totalCount = firstPathLength + secondPathLength;
					bool flag = totalCount <= 4;
					if (flag)
					{
						int secondIndex = 0;
						PropertyPathPart part0 = path.m_Part0;
						PropertyPathPart part = ((firstPathLength > 1) ? path.m_Part1 : pathToAppend[secondIndex++]);
						PropertyPathPart part2 = ((totalCount > 2) ? ((firstPathLength > 2) ? path.m_Part2 : pathToAppend[secondIndex++]) : default(PropertyPathPart));
						PropertyPathPart part3 = ((totalCount > 3) ? ((firstPathLength > 3) ? path.m_Part3 : pathToAppend[secondIndex]) : default(PropertyPathPart));
						switch (totalCount)
						{
						case 2:
							return new PropertyPath(in part0, in part);
						case 3:
							return new PropertyPath(in part0, in part, in part2);
						case 4:
							return new PropertyPath(in part0, in part, in part2, in part3);
						}
					}
					List<PropertyPathPart> parts = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
					try
					{
						PropertyPath.GetParts(in path, parts);
						PropertyPath.GetParts(in pathToAppend, parts);
						propertyPath = new PropertyPath(parts);
					}
					finally
					{
						CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(parts);
					}
				}
			}
			return propertyPath;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003078 File Offset: 0x00001278
		public static PropertyPath AppendPart(in PropertyPath path, in PropertyPathPart part)
		{
			bool isEmpty = path.IsEmpty;
			PropertyPath propertyPath;
			if (isEmpty)
			{
				propertyPath = new PropertyPath(in part);
			}
			else
			{
				switch (path.Length + 1)
				{
				case 2:
					propertyPath = new PropertyPath(in path.m_Part0, in part);
					break;
				case 3:
					propertyPath = new PropertyPath(in path.m_Part0, in path.m_Part1, in part);
					break;
				case 4:
					propertyPath = new PropertyPath(in path.m_Part0, in path.m_Part1, in path.m_Part2, in part);
					break;
				default:
				{
					List<PropertyPathPart> parts = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
					try
					{
						PropertyPath.GetParts(in path, parts);
						parts.Add(part);
						propertyPath = new PropertyPath(parts);
					}
					finally
					{
						CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(parts);
					}
					break;
				}
				}
			}
			return propertyPath;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003140 File Offset: 0x00001340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendIndex(in PropertyPath path, int index)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(index);
			return PropertyPath.AppendPart(in path, in propertyPathPart);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000315C File Offset: 0x0000135C
		public static PropertyPath AppendProperty(in PropertyPath path, IProperty property)
		{
			if (!true)
			{
			}
			IListElementProperty listElementProperty = property as IListElementProperty;
			PropertyPath propertyPath;
			if (listElementProperty == null)
			{
				ISetElementProperty setElementProperty = property as ISetElementProperty;
				if (setElementProperty == null)
				{
					IDictionaryElementProperty dictionaryElementProperty = property as IDictionaryElementProperty;
					if (dictionaryElementProperty == null)
					{
						PropertyPathPart propertyPathPart = new PropertyPathPart(property.Name);
						propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
					}
					else
					{
						PropertyPathPart propertyPathPart = new PropertyPathPart(dictionaryElementProperty.ObjectKey);
						propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
					}
				}
				else
				{
					PropertyPathPart propertyPathPart = new PropertyPathPart(setElementProperty.ObjectKey);
					propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
				}
			}
			else
			{
				PropertyPathPart propertyPathPart = new PropertyPathPart(listElementProperty.Index);
				propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
			}
			if (!true)
			{
			}
			return propertyPath;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031FF File Offset: 0x000013FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath Pop(in PropertyPath path)
		{
			return PropertyPath.SubPath(in path, 0, path.Length - 1);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003210 File Offset: 0x00001410
		public static PropertyPath SubPath(in PropertyPath path, int startIndex, int length)
		{
			int count = path.Length;
			bool flag = startIndex < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			bool flag2 = startIndex > count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			bool flag3 = length < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			bool flag4 = startIndex > count - length;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			bool flag5 = length == 0;
			PropertyPath propertyPath;
			if (flag5)
			{
				propertyPath = default(PropertyPath);
			}
			else
			{
				bool flag6 = startIndex == 0 && length == count;
				if (flag6)
				{
					propertyPath = path;
				}
				else
				{
					switch (length)
					{
					case 1:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						propertyPath = new PropertyPath(in propertyPathPart);
						break;
					}
					case 2:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						PropertyPathPart propertyPathPart2 = path[startIndex + 1];
						propertyPath = new PropertyPath(in propertyPathPart, in propertyPathPart2);
						break;
					}
					case 3:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						PropertyPathPart propertyPathPart2 = path[startIndex + 1];
						PropertyPathPart propertyPathPart3 = path[startIndex + 2];
						propertyPath = new PropertyPath(in propertyPathPart, in propertyPathPart2, in propertyPathPart3);
						break;
					}
					case 4:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						PropertyPathPart propertyPathPart2 = path[startIndex + 1];
						PropertyPathPart propertyPathPart3 = path[startIndex + 2];
						PropertyPathPart propertyPathPart4 = path[startIndex + 3];
						propertyPath = new PropertyPath(in propertyPathPart, in propertyPathPart2, in propertyPathPart3, in propertyPathPart4);
						break;
					}
					default:
					{
						List<PropertyPathPart> parts = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
						try
						{
							for (int i = startIndex; i < startIndex + length; i++)
							{
								parts.Add(path[i]);
							}
							propertyPath = new PropertyPath(parts);
						}
						finally
						{
							CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(parts);
						}
						break;
					}
					}
				}
			}
			return propertyPath;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000033CC File Offset: 0x000015CC
		public override string ToString()
		{
			bool flag = this.Length == 0;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				bool flag2 = this.Length == 1 && this.m_Part0.IsName;
				if (flag2)
				{
					text = this.m_Part0.Name;
				}
				else
				{
					StringBuilder builder = new StringBuilder(32);
					bool flag3 = this.Length > 0;
					if (flag3)
					{
						PropertyPath.AppendToBuilder(in this.m_Part0, builder);
					}
					bool flag4 = this.Length > 1;
					if (flag4)
					{
						PropertyPath.AppendToBuilder(in this.m_Part1, builder);
					}
					bool flag5 = this.Length > 2;
					if (flag5)
					{
						PropertyPath.AppendToBuilder(in this.m_Part2, builder);
					}
					bool flag6 = this.Length > 3;
					if (flag6)
					{
						PropertyPath.AppendToBuilder(in this.m_Part3, builder);
					}
					bool flag7 = this.Length > 4;
					if (flag7)
					{
						foreach (PropertyPathPart part in this.m_AdditionalParts)
						{
							PropertyPath.AppendToBuilder(in part, builder);
						}
					}
					text = builder.ToString();
				}
			}
			return text;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000034E4 File Offset: 0x000016E4
		private static void AppendToBuilder(in PropertyPathPart part, StringBuilder builder)
		{
			PropertyPathPartKind kind = part.Kind;
			PropertyPathPartKind propertyPathPartKind = kind;
			if (propertyPathPartKind != PropertyPathPartKind.Name)
			{
				if (propertyPathPartKind - PropertyPathPartKind.Index > 1)
				{
					throw new ArgumentOutOfRangeException();
				}
				builder.Append(part.ToString());
			}
			else
			{
				bool flag = builder.Length > 0;
				if (flag)
				{
					builder.Append('.');
				}
				builder.Append(part.ToString());
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003550 File Offset: 0x00001750
		private static void GetParts(in PropertyPath path, List<PropertyPathPart> parts)
		{
			int count = path.Length;
			for (int i = 0; i < count; i++)
			{
				parts.Add(path[i]);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003584 File Offset: 0x00001784
		private static PropertyPath ConstructFromPath(string path)
		{
			PropertyPath.<>c__DisplayClass36_0 CS$<>8__locals1;
			CS$<>8__locals1.path = path;
			bool flag = string.IsNullOrWhiteSpace(CS$<>8__locals1.path);
			PropertyPath propertyPath;
			if (flag)
			{
				propertyPath = default(PropertyPath);
			}
			else
			{
				CS$<>8__locals1.index = 0;
				CS$<>8__locals1.length = CS$<>8__locals1.path.Length;
				CS$<>8__locals1.state = 0;
				List<PropertyPathPart> parts = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
				try
				{
					parts.Clear();
					while (CS$<>8__locals1.index < CS$<>8__locals1.length)
					{
						switch (CS$<>8__locals1.state)
						{
						case 0:
						{
							PropertyPath.<ConstructFromPath>g__TrimStart|36_0(ref CS$<>8__locals1);
							bool flag2 = CS$<>8__locals1.index == CS$<>8__locals1.length;
							if (!flag2)
							{
								bool flag3 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '.';
								if (flag3)
								{
									throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
								}
								bool flag4 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '[';
								if (flag4)
								{
									CS$<>8__locals1.state = 2;
								}
								else
								{
									bool flag5 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '"';
									if (flag5)
									{
										throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
									}
									CS$<>8__locals1.state = 1;
								}
							}
							break;
						}
						case 1:
						{
							int startIndex = CS$<>8__locals1.index;
							while (CS$<>8__locals1.index < CS$<>8__locals1.length)
							{
								bool flag6 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '.' || CS$<>8__locals1.path[CS$<>8__locals1.index] == '[';
								if (flag6)
								{
									break;
								}
								int num = CS$<>8__locals1.index + 1;
								CS$<>8__locals1.index = num;
							}
							bool flag7 = startIndex == CS$<>8__locals1.index;
							if (flag7)
							{
								throw new ArgumentException("Invalid PropertyPath: Name is empty.");
							}
							bool flag8 = CS$<>8__locals1.index == CS$<>8__locals1.length;
							if (flag8)
							{
								parts.Add(new PropertyPathPart(CS$<>8__locals1.path.Substring(startIndex)));
								CS$<>8__locals1.state = 0;
							}
							else
							{
								parts.Add(new PropertyPathPart(CS$<>8__locals1.path.Substring(startIndex, CS$<>8__locals1.index - startIndex)));
								PropertyPath.<ConstructFromPath>g__ReadNext|36_1(ref CS$<>8__locals1);
							}
							break;
						}
						case 2:
						{
							bool flag9 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '[';
							if (flag9)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							bool flag10 = CS$<>8__locals1.index + 1 < CS$<>8__locals1.length && CS$<>8__locals1.path[CS$<>8__locals1.index + 1] == '"';
							if (flag10)
							{
								CS$<>8__locals1.state = 4;
							}
							else
							{
								CS$<>8__locals1.state = 3;
							}
							break;
						}
						case 3:
						{
							bool flag11 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '[';
							if (flag11)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							int num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							int startIndex2 = CS$<>8__locals1.index;
							while (CS$<>8__locals1.index < CS$<>8__locals1.length)
							{
								char ci = CS$<>8__locals1.path[CS$<>8__locals1.index];
								bool flag12 = ci == ']';
								if (flag12)
								{
									break;
								}
								num = CS$<>8__locals1.index + 1;
								CS$<>8__locals1.index = num;
							}
							bool flag13 = CS$<>8__locals1.path[CS$<>8__locals1.index] != ']';
							if (flag13)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							string indexStr = CS$<>8__locals1.path.Substring(startIndex2, CS$<>8__locals1.index - startIndex2);
							int partIndex;
							bool flag14 = !int.TryParse(indexStr, out partIndex);
							if (flag14)
							{
								throw new ArgumentException("Indices in PropertyPath must be a numeric value.");
							}
							bool flag15 = partIndex < 0;
							if (flag15)
							{
								throw new ArgumentException("Invalid PropertyPath: Negative indices are not supported.");
							}
							parts.Add(new PropertyPathPart(partIndex));
							num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							bool flag16 = CS$<>8__locals1.index == CS$<>8__locals1.length;
							if (!flag16)
							{
								PropertyPath.<ConstructFromPath>g__ReadNext|36_1(ref CS$<>8__locals1);
							}
							break;
						}
						case 4:
						{
							bool flag17 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '[';
							if (flag17)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							int num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							bool flag18 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '"';
							if (flag18)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							int startIndex3 = CS$<>8__locals1.index;
							while (CS$<>8__locals1.index < CS$<>8__locals1.length)
							{
								char ci2 = CS$<>8__locals1.path[CS$<>8__locals1.index];
								bool flag19 = ci2 == '"';
								if (flag19)
								{
									break;
								}
								num = CS$<>8__locals1.index + 1;
								CS$<>8__locals1.index = num;
							}
							bool flag20 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '"';
							if (flag20)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							bool flag21 = CS$<>8__locals1.index + 1 < CS$<>8__locals1.length && CS$<>8__locals1.path[CS$<>8__locals1.index + 1] == ']';
							if (!flag21)
							{
								throw new ArgumentException("Invalid PropertyPath: No matching end quote for key.");
							}
							string keyStr = CS$<>8__locals1.path.Substring(startIndex3, CS$<>8__locals1.index - startIndex3);
							parts.Add(new PropertyPathPart(keyStr));
							CS$<>8__locals1.index += 2;
							PropertyPath.<ConstructFromPath>g__ReadNext|36_1(ref CS$<>8__locals1);
							break;
						}
						}
					}
					propertyPath = new PropertyPath(parts);
				}
				finally
				{
					CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(parts);
				}
			}
			return propertyPath;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C84 File Offset: 0x00001E84
		public static bool operator ==(PropertyPath lhs, PropertyPath rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public static bool operator !=(PropertyPath lhs, PropertyPath rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003CBC File Offset: 0x00001EBC
		public bool Equals(PropertyPath other)
		{
			bool flag = this.Length != other.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.Length; i++)
				{
					bool flag3 = !this[i].Equals(other[i]);
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D28 File Offset: 0x00001F28
		public override bool Equals(object obj)
		{
			PropertyPath path;
			bool flag;
			if (obj is PropertyPath)
			{
				path = (PropertyPath)obj;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(path);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D5C File Offset: 0x00001F5C
		public override int GetHashCode()
		{
			int hashcode = 19;
			int count = this.Length;
			bool flag = count == 0;
			int num;
			if (flag)
			{
				num = hashcode;
			}
			else
			{
				bool flag2 = count > 0;
				if (flag2)
				{
					hashcode = hashcode * 31 + this.m_Part0.GetHashCode();
				}
				bool flag3 = count > 1;
				if (flag3)
				{
					hashcode = hashcode * 31 + this.m_Part1.GetHashCode();
				}
				bool flag4 = count > 2;
				if (flag4)
				{
					hashcode = hashcode * 31 + this.m_Part2.GetHashCode();
				}
				bool flag5 = count > 3;
				if (flag5)
				{
					hashcode = hashcode * 31 + this.m_Part3.GetHashCode();
				}
				bool flag6 = count <= 4;
				if (flag6)
				{
					num = hashcode;
				}
				else
				{
					foreach (PropertyPathPart part in this.m_AdditionalParts)
					{
						hashcode = hashcode * 31 + part.GetHashCode();
					}
					num = hashcode;
				}
			}
			return num;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003E5C File Offset: 0x0000205C
		[CompilerGenerated]
		internal static void <ConstructFromPath>g__TrimStart|36_0(ref PropertyPath.<>c__DisplayClass36_0 A_0)
		{
			while (A_0.index < A_0.length && A_0.path[A_0.index] == ' ')
			{
				int num = A_0.index + 1;
				A_0.index = num;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003EA8 File Offset: 0x000020A8
		[CompilerGenerated]
		internal static void <ConstructFromPath>g__ReadNext|36_1(ref PropertyPath.<>c__DisplayClass36_0 A_0)
		{
			bool flag = A_0.index == A_0.length;
			if (flag)
			{
				A_0.state = 0;
			}
			else
			{
				char c = A_0.path[A_0.index];
				char c2 = c;
				if (c2 != '.')
				{
					if (c2 != '[')
					{
						throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", A_0.path[A_0.index], A_0.index));
					}
					A_0.state = 2;
				}
				else
				{
					int num = A_0.index + 1;
					A_0.index = num;
					A_0.state = 0;
				}
			}
		}

		// Token: 0x0400002C RID: 44
		internal const int k_InlineCount = 4;

		// Token: 0x0400002D RID: 45
		private readonly PropertyPathPart m_Part0;

		// Token: 0x0400002E RID: 46
		private readonly PropertyPathPart m_Part1;

		// Token: 0x0400002F RID: 47
		private readonly PropertyPathPart m_Part2;

		// Token: 0x04000030 RID: 48
		private readonly PropertyPathPart m_Part3;

		// Token: 0x04000031 RID: 49
		private readonly PropertyPathPart[] m_AdditionalParts;
	}
}
