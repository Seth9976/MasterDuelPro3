using System;
using System.Linq;
using System.Text;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000235 RID: 565
	internal static class CSharpCodeHelpers
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x0005E6B0 File Offset: 0x0005C8B0
		public static bool IsProperIdentifier(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			if (char.IsDigit(name[0]))
			{
				return false;
			}
			foreach (char ch in name)
			{
				if (!char.IsLetterOrDigit(ch) && ch != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0005E700 File Offset: 0x0005C900
		public static bool IsEmptyOrProperIdentifier(string name)
		{
			return string.IsNullOrEmpty(name) || CSharpCodeHelpers.IsProperIdentifier(name);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0005E712 File Offset: 0x0005C912
		public static bool IsEmptyOrProperNamespaceName(string name)
		{
			return string.IsNullOrEmpty(name) || name.Split('.', StringSplitOptions.None).All(new Func<string, bool>(CSharpCodeHelpers.IsProperIdentifier));
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005E738 File Offset: 0x0005C938
		public static string MakeIdentifier(string name, string suffix = "")
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (char.IsDigit(name[0]))
			{
				name = "_" + name;
			}
			bool nameHasInvalidCharacters = false;
			foreach (char ch in name)
			{
				if (!char.IsLetterOrDigit(ch) && ch != '_')
				{
					nameHasInvalidCharacters = true;
					break;
				}
			}
			if (nameHasInvalidCharacters)
			{
				StringBuilder buffer = new StringBuilder();
				foreach (char ch2 in name)
				{
					if (char.IsLetterOrDigit(ch2) || ch2 == '_')
					{
						buffer.Append(ch2);
					}
				}
				name = buffer.ToString();
			}
			return name + suffix;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0005E7F0 File Offset: 0x0005C9F0
		public static string MakeTypeName(string name, string suffix = "")
		{
			string symbolName = CSharpCodeHelpers.MakeIdentifier(name, suffix);
			if (char.IsLower(symbolName[0]))
			{
				symbolName = char.ToUpper(symbolName[0]).ToString() + symbolName.Substring(1);
			}
			return symbolName;
		}
	}
}
