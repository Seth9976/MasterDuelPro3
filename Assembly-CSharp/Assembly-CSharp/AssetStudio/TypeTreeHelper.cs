using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace AssetStudio
{
	// Token: 0x02000180 RID: 384
	public static class TypeTreeHelper
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x00019404 File Offset: 0x00017604
		public static string ReadTypeString(TypeTree m_Type, ObjectReader reader)
		{
			reader.Reset();
			StringBuilder sb = new StringBuilder();
			List<TypeTreeNode> m_Nodes = m_Type.m_Nodes;
			for (int i = 0; i < m_Nodes.Count; i++)
			{
				TypeTreeHelper.ReadStringValue(sb, m_Nodes, reader, ref i);
			}
			long readed = reader.Position - reader.byteStart;
			if (readed != (long)((ulong)reader.byteSize))
			{
				Logger.Info(string.Format("Error while read type, read {0} bytes but expected {1} bytes", readed, reader.byteSize));
			}
			return sb.ToString();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00019480 File Offset: 0x00017680
		private static void ReadStringValue(StringBuilder sb, List<TypeTreeNode> m_Nodes, BinaryReader reader, ref int i)
		{
			TypeTreeNode typeTreeNode = m_Nodes[i];
			int level = typeTreeNode.m_Level;
			string varTypeStr = typeTreeNode.m_Type;
			string varNameStr = typeTreeNode.m_Name;
			object value = null;
			bool append = true;
			bool align = (typeTreeNode.m_MetaFlag & 16384) != 0;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(varTypeStr);
			if (num <= 2682341561U)
			{
				if (num <= 1059919963U)
				{
					if (num <= 883112130U)
					{
						if (num != 398550328U)
						{
							if (num != 883112130U)
							{
								goto IL_0667;
							}
							if (!(varTypeStr == "FileSize"))
							{
								goto IL_0667;
							}
							goto IL_042A;
						}
						else
						{
							if (!(varTypeStr == "string"))
							{
								goto IL_0667;
							}
							append = false;
							string str = reader.ReadAlignedString();
							sb.AppendFormat("{0}{1} {2} = \"{3}\"\r\n", new object[]
							{
								new string('\t', level),
								varTypeStr,
								varNameStr,
								str
							});
							List<TypeTreeNode> toSkip = TypeTreeHelper.GetNodes(m_Nodes, i);
							i += toSkip.Count - 1;
							goto IL_07D0;
						}
					}
					else if (num != 951188382U)
					{
						if (num != 1018593048U)
						{
							if (num != 1059919963U)
							{
								goto IL_0667;
							}
							if (!(varTypeStr == "UInt8"))
							{
								goto IL_0667;
							}
							value = reader.ReadByte();
							goto IL_07D0;
						}
						else
						{
							if (!(varTypeStr == "SInt16"))
							{
								goto IL_0667;
							}
							goto IL_03D5;
						}
					}
					else if (!(varTypeStr == "SInt32"))
					{
						goto IL_0667;
					}
				}
				else if (num <= 1324880019U)
				{
					if (num != 1288423394U)
					{
						if (num != 1323747186U)
						{
							if (num != 1324880019U)
							{
								goto IL_0667;
							}
							if (!(varTypeStr == "UInt64"))
							{
								goto IL_0667;
							}
							goto IL_042A;
						}
						else
						{
							if (!(varTypeStr == "UInt16"))
							{
								goto IL_0667;
							}
							goto IL_03E6;
						}
					}
					else
					{
						if (!(varTypeStr == "unsigned long long"))
						{
							goto IL_0667;
						}
						goto IL_042A;
					}
				}
				else if (num != 1673294503U)
				{
					if (num != 2515107422U)
					{
						if (num != 2682341561U)
						{
							goto IL_0667;
						}
						if (!(varTypeStr == "SInt8"))
						{
							goto IL_0667;
						}
						value = reader.ReadSByte();
						goto IL_07D0;
					}
					else if (!(varTypeStr == "int"))
					{
						goto IL_0667;
					}
				}
				else
				{
					if (!(varTypeStr == "long long"))
					{
						goto IL_0667;
					}
					goto IL_0419;
				}
				value = reader.ReadInt32();
				goto IL_07D0;
				IL_042A:
				value = reader.ReadUInt64();
				goto IL_07D0;
			}
			if (num > 3365180733U)
			{
				if (num <= 3751997361U)
				{
					if (num != 3507937221U)
					{
						if (num != 3538687084U)
						{
							if (num != 3751997361U)
							{
								goto IL_0667;
							}
							if (!(varTypeStr == "map"))
							{
								goto IL_0667;
							}
							if ((m_Nodes[i + 1].m_MetaFlag & 16384) != 0)
							{
								align = true;
							}
							append = false;
							sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level), varTypeStr, varNameStr);
							sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level + 1), "Array", "Array");
							int size = reader.ReadInt32();
							sb.AppendFormat("{0}{1} {2} = {3}\r\n", new object[]
							{
								new string('\t', level + 1),
								"int",
								"size",
								size
							});
							List<TypeTreeNode> map = TypeTreeHelper.GetNodes(m_Nodes, i);
							i += map.Count - 1;
							List<TypeTreeNode> first = TypeTreeHelper.GetNodes(map, 4);
							int next = 4 + first.Count;
							List<TypeTreeNode> second = TypeTreeHelper.GetNodes(map, next);
							for (int j = 0; j < size; j++)
							{
								sb.AppendFormat("{0}[{1}]\r\n", new string('\t', level + 2), j);
								sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level + 2), "pair", "data");
								int tmp = 0;
								int tmp2 = 0;
								TypeTreeHelper.ReadStringValue(sb, first, reader, ref tmp);
								TypeTreeHelper.ReadStringValue(sb, second, reader, ref tmp2);
							}
							goto IL_07D0;
						}
						else if (!(varTypeStr == "UInt32"))
						{
							goto IL_0667;
						}
					}
					else if (!(varTypeStr == "Type*"))
					{
						goto IL_0667;
					}
				}
				else if (num != 3800087380U)
				{
					if (num != 3893626296U)
					{
						if (num != 4036501227U)
						{
							goto IL_0667;
						}
						if (!(varTypeStr == "unsigned int"))
						{
							goto IL_0667;
						}
					}
					else
					{
						if (!(varTypeStr == "TypelessData"))
						{
							goto IL_0667;
						}
						append = false;
						int size2 = reader.ReadInt32();
						reader.ReadBytes(size2);
						i += 2;
						sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level), varTypeStr, varNameStr);
						sb.AppendFormat("{0}{1} {2} = {3}\r\n", new object[]
						{
							new string('\t', level),
							"int",
							"size",
							size2
						});
						goto IL_07D0;
					}
				}
				else
				{
					if (!(varTypeStr == "unsigned short"))
					{
						goto IL_0667;
					}
					goto IL_03E6;
				}
				value = reader.ReadUInt32();
				goto IL_07D0;
			}
			if (num <= 2823553821U)
			{
				if (num != 2699759368U)
				{
					if (num != 2797886853U)
					{
						if (num != 2823553821U)
						{
							goto IL_0667;
						}
						if (!(varTypeStr == "char"))
						{
							goto IL_0667;
						}
						value = BitConverter.ToChar(reader.ReadBytes(2), 0);
						goto IL_07D0;
					}
					else
					{
						if (!(varTypeStr == "float"))
						{
							goto IL_0667;
						}
						value = reader.ReadSingle();
						goto IL_07D0;
					}
				}
				else
				{
					if (!(varTypeStr == "double"))
					{
						goto IL_0667;
					}
					value = reader.ReadDouble();
					goto IL_07D0;
				}
			}
			else if (num != 2965238137U)
			{
				if (num != 3122818005U)
				{
					if (num != 3365180733U)
					{
						goto IL_0667;
					}
					if (!(varTypeStr == "bool"))
					{
						goto IL_0667;
					}
					value = reader.ReadBoolean();
					goto IL_07D0;
				}
				else if (!(varTypeStr == "short"))
				{
					goto IL_0667;
				}
			}
			else
			{
				if (!(varTypeStr == "SInt64"))
				{
					goto IL_0667;
				}
				goto IL_0419;
			}
			IL_03D5:
			value = reader.ReadInt16();
			goto IL_07D0;
			IL_03E6:
			value = reader.ReadUInt16();
			goto IL_07D0;
			IL_0419:
			value = reader.ReadInt64();
			goto IL_07D0;
			IL_0667:
			if (i < m_Nodes.Count - 1 && m_Nodes[i + 1].m_Type == "Array")
			{
				if ((m_Nodes[i + 1].m_MetaFlag & 16384) != 0)
				{
					align = true;
				}
				append = false;
				sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level), varTypeStr, varNameStr);
				sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level + 1), "Array", "Array");
				int size3 = reader.ReadInt32();
				sb.AppendFormat("{0}{1} {2} = {3}\r\n", new object[]
				{
					new string('\t', level + 1),
					"int",
					"size",
					size3
				});
				List<TypeTreeNode> vector = TypeTreeHelper.GetNodes(m_Nodes, i);
				i += vector.Count - 1;
				for (int k = 0; k < size3; k++)
				{
					sb.AppendFormat("{0}[{1}]\r\n", new string('\t', level + 2), k);
					int tmp3 = 3;
					TypeTreeHelper.ReadStringValue(sb, vector, reader, ref tmp3);
				}
			}
			else
			{
				append = false;
				sb.AppendFormat("{0}{1} {2}\r\n", new string('\t', level), varTypeStr, varNameStr);
				List<TypeTreeNode> @class = TypeTreeHelper.GetNodes(m_Nodes, i);
				i += @class.Count - 1;
				for (int l = 1; l < @class.Count; l++)
				{
					TypeTreeHelper.ReadStringValue(sb, @class, reader, ref l);
				}
			}
			IL_07D0:
			if (append)
			{
				sb.AppendFormat("{0}{1} {2} = {3}\r\n", new object[]
				{
					new string('\t', level),
					varTypeStr,
					varNameStr,
					value
				});
			}
			if (align)
			{
				reader.AlignStream();
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019C94 File Offset: 0x00017E94
		public static OrderedDictionary ReadType(TypeTree m_Types, ObjectReader reader)
		{
			reader.Reset();
			OrderedDictionary obj = new OrderedDictionary();
			List<TypeTreeNode> m_Nodes = m_Types.m_Nodes;
			for (int i = 1; i < m_Nodes.Count; i++)
			{
				string varNameStr = m_Nodes[i].m_Name;
				obj[varNameStr] = TypeTreeHelper.ReadValue(m_Nodes, reader, ref i);
			}
			long readed = reader.Position - reader.byteStart;
			if (readed != (long)((ulong)reader.byteSize))
			{
				Logger.Info(string.Format("Error while read type, read {0} bytes but expected {1} bytes", readed, reader.byteSize));
			}
			return obj;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00019D20 File Offset: 0x00017F20
		private static object ReadValue(List<TypeTreeNode> m_Nodes, BinaryReader reader, ref int i)
		{
			TypeTreeNode typeTreeNode = m_Nodes[i];
			string varTypeStr = typeTreeNode.m_Type;
			bool align = (typeTreeNode.m_MetaFlag & 16384) != 0;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(varTypeStr);
			object value;
			if (num <= 2682341561U)
			{
				if (num <= 1059919963U)
				{
					if (num <= 883112130U)
					{
						if (num != 398550328U)
						{
							if (num != 883112130U)
							{
								goto IL_0539;
							}
							if (!(varTypeStr == "FileSize"))
							{
								goto IL_0539;
							}
							goto IL_0416;
						}
						else
						{
							if (!(varTypeStr == "string"))
							{
								goto IL_0539;
							}
							value = reader.ReadAlignedString();
							List<TypeTreeNode> toSkip = TypeTreeHelper.GetNodes(m_Nodes, i);
							i += toSkip.Count - 1;
							goto IL_062A;
						}
					}
					else if (num != 951188382U)
					{
						if (num != 1018593048U)
						{
							if (num != 1059919963U)
							{
								goto IL_0539;
							}
							if (!(varTypeStr == "UInt8"))
							{
								goto IL_0539;
							}
							value = reader.ReadByte();
							goto IL_062A;
						}
						else
						{
							if (!(varTypeStr == "SInt16"))
							{
								goto IL_0539;
							}
							goto IL_03C1;
						}
					}
					else if (!(varTypeStr == "SInt32"))
					{
						goto IL_0539;
					}
				}
				else if (num <= 1324880019U)
				{
					if (num != 1288423394U)
					{
						if (num != 1323747186U)
						{
							if (num != 1324880019U)
							{
								goto IL_0539;
							}
							if (!(varTypeStr == "UInt64"))
							{
								goto IL_0539;
							}
							goto IL_0416;
						}
						else
						{
							if (!(varTypeStr == "UInt16"))
							{
								goto IL_0539;
							}
							goto IL_03D2;
						}
					}
					else
					{
						if (!(varTypeStr == "unsigned long long"))
						{
							goto IL_0539;
						}
						goto IL_0416;
					}
				}
				else if (num != 1673294503U)
				{
					if (num != 2515107422U)
					{
						if (num != 2682341561U)
						{
							goto IL_0539;
						}
						if (!(varTypeStr == "SInt8"))
						{
							goto IL_0539;
						}
						value = reader.ReadSByte();
						goto IL_062A;
					}
					else if (!(varTypeStr == "int"))
					{
						goto IL_0539;
					}
				}
				else
				{
					if (!(varTypeStr == "long long"))
					{
						goto IL_0539;
					}
					goto IL_0405;
				}
				value = reader.ReadInt32();
				goto IL_062A;
				IL_0416:
				value = reader.ReadUInt64();
				goto IL_062A;
			}
			if (num > 3365180733U)
			{
				if (num <= 3751997361U)
				{
					if (num != 3507937221U)
					{
						if (num != 3538687084U)
						{
							if (num != 3751997361U)
							{
								goto IL_0539;
							}
							if (!(varTypeStr == "map"))
							{
								goto IL_0539;
							}
							if ((m_Nodes[i + 1].m_MetaFlag & 16384) != 0)
							{
								align = true;
							}
							List<TypeTreeNode> map = TypeTreeHelper.GetNodes(m_Nodes, i);
							i += map.Count - 1;
							List<TypeTreeNode> first = TypeTreeHelper.GetNodes(map, 4);
							int next = 4 + first.Count;
							List<TypeTreeNode> second = TypeTreeHelper.GetNodes(map, next);
							int size = reader.ReadInt32();
							List<KeyValuePair<object, object>> dic = new List<KeyValuePair<object, object>>(size);
							for (int j = 0; j < size; j++)
							{
								int tmp = 0;
								int tmp2 = 0;
								dic.Add(new KeyValuePair<object, object>(TypeTreeHelper.ReadValue(first, reader, ref tmp), TypeTreeHelper.ReadValue(second, reader, ref tmp2)));
							}
							value = dic;
							goto IL_062A;
						}
						else if (!(varTypeStr == "UInt32"))
						{
							goto IL_0539;
						}
					}
					else if (!(varTypeStr == "Type*"))
					{
						goto IL_0539;
					}
				}
				else if (num != 3800087380U)
				{
					if (num != 3893626296U)
					{
						if (num != 4036501227U)
						{
							goto IL_0539;
						}
						if (!(varTypeStr == "unsigned int"))
						{
							goto IL_0539;
						}
					}
					else
					{
						if (!(varTypeStr == "TypelessData"))
						{
							goto IL_0539;
						}
						int size2 = reader.ReadInt32();
						value = reader.ReadBytes(size2);
						i += 2;
						goto IL_062A;
					}
				}
				else
				{
					if (!(varTypeStr == "unsigned short"))
					{
						goto IL_0539;
					}
					goto IL_03D2;
				}
				value = reader.ReadUInt32();
				goto IL_062A;
			}
			if (num <= 2823553821U)
			{
				if (num != 2699759368U)
				{
					if (num != 2797886853U)
					{
						if (num != 2823553821U)
						{
							goto IL_0539;
						}
						if (!(varTypeStr == "char"))
						{
							goto IL_0539;
						}
						value = BitConverter.ToChar(reader.ReadBytes(2), 0);
						goto IL_062A;
					}
					else
					{
						if (!(varTypeStr == "float"))
						{
							goto IL_0539;
						}
						value = reader.ReadSingle();
						goto IL_062A;
					}
				}
				else
				{
					if (!(varTypeStr == "double"))
					{
						goto IL_0539;
					}
					value = reader.ReadDouble();
					goto IL_062A;
				}
			}
			else if (num != 2965238137U)
			{
				if (num != 3122818005U)
				{
					if (num != 3365180733U)
					{
						goto IL_0539;
					}
					if (!(varTypeStr == "bool"))
					{
						goto IL_0539;
					}
					value = reader.ReadBoolean();
					goto IL_062A;
				}
				else if (!(varTypeStr == "short"))
				{
					goto IL_0539;
				}
			}
			else
			{
				if (!(varTypeStr == "SInt64"))
				{
					goto IL_0539;
				}
				goto IL_0405;
			}
			IL_03C1:
			value = reader.ReadInt16();
			goto IL_062A;
			IL_03D2:
			value = reader.ReadUInt16();
			goto IL_062A;
			IL_0405:
			value = reader.ReadInt64();
			goto IL_062A;
			IL_0539:
			if (i < m_Nodes.Count - 1 && m_Nodes[i + 1].m_Type == "Array")
			{
				if ((m_Nodes[i + 1].m_MetaFlag & 16384) != 0)
				{
					align = true;
				}
				List<TypeTreeNode> vector = TypeTreeHelper.GetNodes(m_Nodes, i);
				i += vector.Count - 1;
				int size3 = reader.ReadInt32();
				List<object> list = new List<object>(size3);
				for (int k = 0; k < size3; k++)
				{
					int tmp3 = 3;
					list.Add(TypeTreeHelper.ReadValue(vector, reader, ref tmp3));
				}
				value = list;
			}
			else
			{
				List<TypeTreeNode> @class = TypeTreeHelper.GetNodes(m_Nodes, i);
				i += @class.Count - 1;
				OrderedDictionary obj = new OrderedDictionary();
				for (int l = 1; l < @class.Count; l++)
				{
					string name = @class[l].m_Name;
					obj[name] = TypeTreeHelper.ReadValue(@class, reader, ref l);
				}
				value = obj;
			}
			IL_062A:
			if (align)
			{
				reader.AlignStream();
			}
			return value;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001A364 File Offset: 0x00018564
		private static List<TypeTreeNode> GetNodes(List<TypeTreeNode> m_Nodes, int index)
		{
			List<TypeTreeNode> nodes = new List<TypeTreeNode>();
			nodes.Add(m_Nodes[index]);
			int level = m_Nodes[index].m_Level;
			for (int i = index + 1; i < m_Nodes.Count; i++)
			{
				TypeTreeNode member = m_Nodes[i];
				if (member.m_Level <= level)
				{
					return nodes;
				}
				nodes.Add(member);
			}
			return nodes;
		}
	}
}
