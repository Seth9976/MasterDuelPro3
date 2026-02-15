using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200024A RID: 586
	public struct NameAndParameters
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00061CF3 File Offset: 0x0005FEF3
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x00061CFB File Offset: 0x0005FEFB
		public string name { readonly get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00061D04 File Offset: 0x0005FF04
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x00061D0C File Offset: 0x0005FF0C
		public ReadOnlyArray<NamedValue> parameters { readonly get; set; }

		// Token: 0x06001569 RID: 5481 RVA: 0x00061D18 File Offset: 0x0005FF18
		public override string ToString()
		{
			if (this.parameters.Count == 0)
			{
				return this.name;
			}
			string parameterString = string.Join(",", this.parameters.Select((NamedValue x) => x.ToString()).ToArray<string>());
			return this.name + "(" + parameterString + ")";
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00061D94 File Offset: 0x0005FF94
		public static IEnumerable<NameAndParameters> ParseMultiple(string text)
		{
			List<NameAndParameters> list = null;
			if (!NameAndParameters.ParseMultiple(text, ref list))
			{
				return Enumerable.Empty<NameAndParameters>();
			}
			return list;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00061DB4 File Offset: 0x0005FFB4
		internal static bool ParseMultiple(string text, ref List<NameAndParameters> list)
		{
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (list == null)
			{
				list = new List<NameAndParameters>();
			}
			else
			{
				list.Clear();
			}
			int index = 0;
			int textLength = text.Length;
			while (index < textLength)
			{
				list.Add(NameAndParameters.ParseNameAndParameters(text, ref index, false));
			}
			return true;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00061E08 File Offset: 0x00060008
		internal static string ParseName(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int index = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref index, true).name;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00061E38 File Offset: 0x00060038
		public static NameAndParameters Parse(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int index = 0;
			return NameAndParameters.ParseNameAndParameters(text, ref index, false);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00061E60 File Offset: 0x00060060
		private static NameAndParameters ParseNameAndParameters(string text, ref int index, bool nameOnly = false)
		{
			int textLength = text.Length;
			while (index < textLength && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			int nameStart = index;
			while (index < textLength)
			{
				char nextChar = text[index];
				if (nextChar == '(' || nextChar == ","[0] || char.IsWhiteSpace(nextChar))
				{
					break;
				}
				index++;
			}
			if (index - nameStart == 0)
			{
				throw new ArgumentException(string.Format("Expecting name at position {0} in '{1}'", nameStart, text), "text");
			}
			string name = text.Substring(nameStart, index - nameStart);
			if (nameOnly)
			{
				return new NameAndParameters
				{
					name = name
				};
			}
			while (index < textLength && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			NamedValue[] parameters = null;
			if (index < textLength && text[index] == '(')
			{
				index++;
				int closeParenIndex = text.IndexOf(')', index);
				if (closeParenIndex == -1)
				{
					throw new ArgumentException(string.Format("Expecting ')' after '(' at position {0} in '{1}'", index, text), "text");
				}
				parameters = NamedValue.ParseMultiple(text.Substring(index, closeParenIndex - index));
				index = closeParenIndex + 1;
			}
			if (index < textLength && (text[index] == ',' || text[index] == ';'))
			{
				index++;
			}
			return new NameAndParameters
			{
				name = name,
				parameters = new ReadOnlyArray<NamedValue>(parameters)
			};
		}
	}
}
