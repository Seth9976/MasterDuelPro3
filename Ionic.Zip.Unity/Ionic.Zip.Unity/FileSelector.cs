using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001F RID: 31
	public class FileSelector
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public FileSelector(string selectionCriteria)
			: this(selectionCriteria, true)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002BCD File Offset: 0x00000DCD
		public FileSelector(string selectionCriteria, bool traverseDirectoryReparsePoints)
		{
			if (!string.IsNullOrEmpty(selectionCriteria))
			{
				this._Criterion = FileSelector._ParseCriterion(selectionCriteria);
			}
			this.TraverseReparsePoints = traverseDirectoryReparsePoints;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002BF0 File Offset: 0x00000DF0
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002C07 File Offset: 0x00000E07
		public string SelectionCriteria
		{
			get
			{
				if (this._Criterion == null)
				{
					return null;
				}
				return this._Criterion.ToString();
			}
			set
			{
				if (value == null)
				{
					this._Criterion = null;
					return;
				}
				if (value.Trim() == "")
				{
					this._Criterion = null;
					return;
				}
				this._Criterion = FileSelector._ParseCriterion(value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002C3A File Offset: 0x00000E3A
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002C42 File Offset: 0x00000E42
		public bool TraverseReparsePoints { get; set; }

		// Token: 0x06000082 RID: 130 RVA: 0x00002C4C File Offset: 0x00000E4C
		private static string NormalizeCriteriaExpression(string source)
		{
			string[][] array = new string[][]
			{
				new string[] { "([^']*)\\(\\(([^']+)", "$1( ($2" },
				new string[] { "(.)\\)\\)", "$1) )" },
				new string[] { "\\((\\S)", "( $1" },
				new string[] { "(\\S)\\)", "$1 )" },
				new string[] { "^\\)", " )" },
				new string[] { "(\\S)\\(", "$1 (" },
				new string[] { "\\)(\\S)", ") $1" },
				new string[] { "(=)('[^']*')", "$1 $2" },
				new string[] { "([^ !><])(>|<|!=|=)", "$1 $2" },
				new string[] { "(>|<|!=|=)([^ =])", "$1 $2" },
				new string[] { "/", "\\" }
			};
			string text = source;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = FileSelector.RegexAssertions.PrecededByEvenNumberOfSingleQuotes + array[i][0] + FileSelector.RegexAssertions.FollowedByEvenNumberOfSingleQuotesAndLineEnd;
				text = Regex.Replace(text, text2, array[i][1]);
			}
			string text3 = "/" + FileSelector.RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			text = Regex.Replace(text, text3, "\\");
			text3 = " " + FileSelector.RegexAssertions.FollowedByOddNumberOfSingleQuotesAndLineEnd;
			return Regex.Replace(text, text3, "\u0006");
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002E38 File Offset: 0x00001038
		private static SelectionCriterion _ParseCriterion(string s)
		{
			if (s == null)
			{
				return null;
			}
			s = FileSelector.NormalizeCriteriaExpression(s);
			if (s.IndexOf(" ") == -1)
			{
				s = "name = " + s;
			}
			string[] array = s.Trim().Split(new char[] { ' ', '\t' });
			if (array.Length < 3)
			{
				throw new ArgumentException(s);
			}
			SelectionCriterion selectionCriterion = null;
			Stack<FileSelector.ParseState> stack = new Stack<FileSelector.ParseState>();
			Stack<SelectionCriterion> stack2 = new Stack<SelectionCriterion>();
			stack.Push(FileSelector.ParseState.Start);
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i].ToLower();
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{0D533E08-1E7F-4C0B-8CE7-7E465A4986C6}.$$method0x6000083-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(14);
						dictionary.Add("and", 0);
						dictionary.Add("xor", 1);
						dictionary.Add("or", 2);
						dictionary.Add("(", 3);
						dictionary.Add(")", 4);
						dictionary.Add("atime", 5);
						dictionary.Add("ctime", 6);
						dictionary.Add("mtime", 7);
						dictionary.Add("length", 8);
						dictionary.Add("size", 9);
						dictionary.Add("filename", 10);
						dictionary.Add("name", 11);
						dictionary.Add("type", 12);
						dictionary.Add("", 13);
						<PrivateImplementationDetails>{0D533E08-1E7F-4C0B-8CE7-7E465A4986C6}.$$method0x6000083-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{0D533E08-1E7F-4C0B-8CE7-7E465A4986C6}.$$method0x6000083-1.TryGetValue(text2, ref num))
					{
						FileSelector.ParseState parseState;
						switch (num)
						{
						case 0:
						case 1:
						case 2:
						{
							parseState = stack.Peek();
							if (parseState != FileSelector.ParseState.CriterionDone)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							if (array.Length <= i + 3)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							LogicalConjunction logicalConjunction = (LogicalConjunction)Enum.Parse(typeof(LogicalConjunction), array[i].ToUpper(), true);
							selectionCriterion = new CompoundCriterion
							{
								Left = selectionCriterion,
								Right = null,
								Conjunction = logicalConjunction
							};
							stack.Push(parseState);
							stack.Push(FileSelector.ParseState.ConjunctionPending);
							stack2.Push(selectionCriterion);
							break;
						}
						case 3:
							parseState = stack.Peek();
							if (parseState != FileSelector.ParseState.Start && parseState != FileSelector.ParseState.ConjunctionPending && parseState != FileSelector.ParseState.OpenParen)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							if (array.Length <= i + 4)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							stack.Push(FileSelector.ParseState.OpenParen);
							break;
						case 4:
							parseState = stack.Pop();
							if (stack.Peek() != FileSelector.ParseState.OpenParen)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							stack.Pop();
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						case 5:
						case 6:
						case 7:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							DateTime dateTime;
							try
							{
								dateTime = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd-HH:mm:ss", null);
							}
							catch (FormatException)
							{
								try
								{
									dateTime = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd-HH:mm:ss", null);
								}
								catch (FormatException)
								{
									try
									{
										dateTime = DateTime.ParseExact(array[i + 2], "yyyy/MM/dd", null);
									}
									catch (FormatException)
									{
										try
										{
											dateTime = DateTime.ParseExact(array[i + 2], "MM/dd/yyyy", null);
										}
										catch (FormatException)
										{
											dateTime = DateTime.ParseExact(array[i + 2], "yyyy-MM-dd", null);
										}
									}
								}
							}
							dateTime = DateTime.SpecifyKind(dateTime, 2).ToUniversalTime();
							selectionCriterion = new TimeCriterion
							{
								Which = (WhichTime)Enum.Parse(typeof(WhichTime), array[i], true),
								Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]),
								Time = dateTime
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 8:
						case 9:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							string text3 = array[i + 2];
							long num2;
							if (text3.ToUpper().EndsWith("K"))
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L;
							}
							else if (text3.ToUpper().EndsWith("KB"))
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L;
							}
							else if (text3.ToUpper().EndsWith("M"))
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L * 1024L;
							}
							else if (text3.ToUpper().EndsWith("MB"))
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L * 1024L;
							}
							else if (text3.ToUpper().EndsWith("G"))
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 1)) * 1024L * 1024L * 1024L;
							}
							else if (text3.ToUpper().EndsWith("GB"))
							{
								num2 = long.Parse(text3.Substring(0, text3.Length - 2)) * 1024L * 1024L * 1024L;
							}
							else
							{
								num2 = long.Parse(array[i + 2]);
							}
							selectionCriterion = new SizeCriterion
							{
								Size = num2,
								Operator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1])
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 10:
						case 11:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							ComparisonOperator comparisonOperator = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
							if (comparisonOperator != ComparisonOperator.NotEqualTo && comparisonOperator != ComparisonOperator.EqualTo)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							string text4 = array[i + 2];
							if (text4.StartsWith("'") && text4.EndsWith("'"))
							{
								text4 = text4.Substring(1, text4.Length - 2).Replace("\u0006", " ");
							}
							selectionCriterion = new NameCriterion
							{
								MatchingFileSpec = text4,
								Operator = comparisonOperator
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 12:
						{
							if (array.Length <= i + 2)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							ComparisonOperator comparisonOperator2 = (ComparisonOperator)EnumUtil.Parse(typeof(ComparisonOperator), array[i + 1]);
							if (comparisonOperator2 != ComparisonOperator.NotEqualTo && comparisonOperator2 != ComparisonOperator.EqualTo)
							{
								throw new ArgumentException(string.Join(" ", array, i, array.Length - i));
							}
							selectionCriterion = new TypeCriterion
							{
								AttributeString = array[i + 2],
								Operator = comparisonOperator2
							};
							i += 2;
							stack.Push(FileSelector.ParseState.CriterionDone);
							break;
						}
						case 13:
							stack.Push(FileSelector.ParseState.Whitespace);
							break;
						default:
							goto IL_075E;
						}
						parseState = stack.Peek();
						if (parseState == FileSelector.ParseState.CriterionDone)
						{
							stack.Pop();
							if (stack.Peek() == FileSelector.ParseState.ConjunctionPending)
							{
								while (stack.Peek() == FileSelector.ParseState.ConjunctionPending)
								{
									CompoundCriterion compoundCriterion = stack2.Pop() as CompoundCriterion;
									compoundCriterion.Right = selectionCriterion;
									selectionCriterion = compoundCriterion;
									stack.Pop();
									parseState = stack.Pop();
									if (parseState != FileSelector.ParseState.CriterionDone)
									{
										throw new ArgumentException("??");
									}
								}
							}
							else
							{
								stack.Push(FileSelector.ParseState.CriterionDone);
							}
						}
						if (parseState == FileSelector.ParseState.Whitespace)
						{
							stack.Pop();
						}
						i++;
						continue;
					}
				}
				IL_075E:
				throw new ArgumentException("'" + array[i] + "'");
			}
			return selectionCriterion;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000367C File Offset: 0x0000187C
		public override string ToString()
		{
			return "FileSelector(" + this._Criterion.ToString() + ")";
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003698 File Offset: 0x00001898
		private bool Evaluate(string filename)
		{
			return this._Criterion.Evaluate(filename);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036B3 File Offset: 0x000018B3
		[Conditional("SelectorTrace")]
		private void SelectorTrace(string format, params object[] args)
		{
			if (this._Criterion != null && this._Criterion.Verbose)
			{
				Console.WriteLine(format, args);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036D1 File Offset: 0x000018D1
		public ICollection<string> SelectFiles(string directory)
		{
			return this.SelectFiles(directory, false);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036DC File Offset: 0x000018DC
		public ReadOnlyCollection<string> SelectFiles(string directory, bool recurseDirectories)
		{
			if (this._Criterion == null)
			{
				throw new ArgumentException("SelectionCriteria has not been set");
			}
			List<string> list = new List<string>();
			try
			{
				if (Directory.Exists(directory))
				{
					string[] files = Directory.GetFiles(directory);
					foreach (string text in files)
					{
						if (this.Evaluate(text))
						{
							list.Add(text);
						}
					}
					if (recurseDirectories)
					{
						string[] directories = Directory.GetDirectories(directory);
						foreach (string text2 in directories)
						{
							if (this.TraverseReparsePoints)
							{
								if (this.Evaluate(text2))
								{
									list.Add(text2);
								}
								list.AddRange(this.SelectFiles(text2, recurseDirectories));
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000037BC File Offset: 0x000019BC
		private bool Evaluate(ZipEntry entry)
		{
			return this._Criterion.Evaluate(entry);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037D8 File Offset: 0x000019D8
		public ICollection<ZipEntry> SelectEntries(ZipFile zip)
		{
			if (zip == null)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			foreach (ZipEntry zipEntry in zip)
			{
				if (this.Evaluate(zipEntry))
				{
					list.Add(zipEntry);
				}
			}
			return list;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003840 File Offset: 0x00001A40
		public ICollection<ZipEntry> SelectEntries(ZipFile zip, string directoryPathInArchive)
		{
			if (zip == null)
			{
				throw new ArgumentNullException("zip");
			}
			List<ZipEntry> list = new List<ZipEntry>();
			string text = ((directoryPathInArchive == null) ? null : directoryPathInArchive.Replace("/", "\\"));
			if (text != null)
			{
				while (text.EndsWith("\\"))
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			foreach (ZipEntry zipEntry in zip)
			{
				if ((directoryPathInArchive == null || Path.GetDirectoryName(zipEntry.FileName) == directoryPathInArchive || Path.GetDirectoryName(zipEntry.FileName) == text) && this.Evaluate(zipEntry))
				{
					list.Add(zipEntry);
				}
			}
			return list;
		}

		// Token: 0x0400004F RID: 79
		internal SelectionCriterion _Criterion;

		// Token: 0x02000020 RID: 32
		private enum ParseState
		{
			// Token: 0x04000052 RID: 82
			Start,
			// Token: 0x04000053 RID: 83
			OpenParen,
			// Token: 0x04000054 RID: 84
			CriterionDone,
			// Token: 0x04000055 RID: 85
			ConjunctionPending,
			// Token: 0x04000056 RID: 86
			Whitespace
		}

		// Token: 0x02000021 RID: 33
		private static class RegexAssertions
		{
			// Token: 0x04000057 RID: 87
			public static readonly string PrecededByOddNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*'[^']*)";

			// Token: 0x04000058 RID: 88
			public static readonly string FollowedByOddNumberOfSingleQuotesAndLineEnd = "(?=[^']*'(?:[^']*'[^']*')*[^']*$)";

			// Token: 0x04000059 RID: 89
			public static readonly string PrecededByEvenNumberOfSingleQuotes = "(?<=(?:[^']*'[^']*')*[^']*)";

			// Token: 0x0400005A RID: 90
			public static readonly string FollowedByEvenNumberOfSingleQuotesAndLineEnd = "(?=(?:[^']*'[^']*')*[^']*$)";
		}
	}
}
