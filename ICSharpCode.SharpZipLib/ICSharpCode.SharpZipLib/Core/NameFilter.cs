using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000B8 RID: 184
	public class NameFilter : IScanFilter
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A719 File Offset: 0x00018919
		public NameFilter(string filter)
		{
			this.filter_ = filter;
			this.inclusions_ = new List<Regex>();
			this.exclusions_ = new List<Regex>();
			this.Compile();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A744 File Offset: 0x00018944
		public static bool IsValidExpression(string expression)
		{
			bool flag = true;
			try
			{
				new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A774 File Offset: 0x00018974
		public static bool IsValidFilterExpression(string toTest)
		{
			bool flag = true;
			try
			{
				if (toTest != null)
				{
					string[] array = NameFilter.SplitQuoted(toTest);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null && array[i].Length > 0)
						{
							string text;
							if (array[i][0] == '+')
							{
								text = array[i].Substring(1, array[i].Length - 1);
							}
							else if (array[i][0] == '-')
							{
								text = array[i].Substring(1, array[i].Length - 1);
							}
							else
							{
								text = array[i];
							}
							new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Singleline);
						}
					}
				}
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001A818 File Offset: 0x00018A18
		public static string[] SplitQuoted(string original)
		{
			char c = '\\';
			char[] array = new char[] { ';' };
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(original))
			{
				int i = -1;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < original.Length)
				{
					i++;
					if (i >= original.Length)
					{
						list.Add(stringBuilder.ToString());
					}
					else if (original[i] == c)
					{
						i++;
						if (i >= original.Length)
						{
							throw new ArgumentException("Missing terminating escape character", "original");
						}
						if (Array.IndexOf<char>(array, original[i]) < 0)
						{
							stringBuilder.Append(c);
						}
						stringBuilder.Append(original[i]);
					}
					else if (Array.IndexOf<char>(array, original[i]) >= 0)
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(original[i]);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001A90B File Offset: 0x00018B0B
		public override string ToString()
		{
			return this.filter_;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001A914 File Offset: 0x00018B14
		public bool IsIncluded(string name)
		{
			bool flag = false;
			if (this.inclusions_.Count == 0)
			{
				flag = true;
			}
			else
			{
				using (List<Regex>.Enumerator enumerator = this.inclusions_.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsMatch(name))
						{
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A980 File Offset: 0x00018B80
		public bool IsExcluded(string name)
		{
			bool flag = false;
			using (List<Regex>.Enumerator enumerator = this.exclusions_.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(name))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A9DC File Offset: 0x00018BDC
		public bool IsMatch(string name)
		{
			return this.IsIncluded(name) && !this.IsExcluded(name);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		private void Compile()
		{
			if (this.filter_ == null)
			{
				return;
			}
			string[] array = NameFilter.SplitQuoted(this.filter_);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && array[i].Length > 0)
				{
					bool flag = array[i][0] != '-';
					string text;
					if (array[i][0] == '+')
					{
						text = array[i].Substring(1, array[i].Length - 1);
					}
					else if (array[i][0] == '-')
					{
						text = array[i].Substring(1, array[i].Length - 1);
					}
					else
					{
						text = array[i];
					}
					if (flag)
					{
						this.inclusions_.Add(new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
					}
					else
					{
						this.exclusions_.Add(new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
					}
				}
			}
		}

		// Token: 0x04000441 RID: 1089
		private string filter_;

		// Token: 0x04000442 RID: 1090
		private List<Regex> inclusions_;

		// Token: 0x04000443 RID: 1091
		private List<Regex> exclusions_;
	}
}
