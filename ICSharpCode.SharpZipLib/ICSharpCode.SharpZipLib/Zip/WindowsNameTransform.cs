using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200000C RID: 12
	public class WindowsNameTransform : INameTransform
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002F76 File Offset: 0x00001176
		public WindowsNameTransform(string baseDirectory, bool allowParentTraversal = false)
		{
			if (baseDirectory == null)
			{
				throw new ArgumentNullException("baseDirectory", "Directory name is invalid");
			}
			this.BaseDirectory = baseDirectory;
			this.AllowParentTraversal = allowParentTraversal;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FA8 File Offset: 0x000011A8
		public WindowsNameTransform()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002FB8 File Offset: 0x000011B8
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002FC0 File Offset: 0x000011C0
		public string BaseDirectory
		{
			get
			{
				return this._baseDirectory;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._baseDirectory = Path.GetFullPath(value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002FDC File Offset: 0x000011DC
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002FE4 File Offset: 0x000011E4
		public bool AllowParentTraversal
		{
			get
			{
				return this._allowParentTraversal;
			}
			set
			{
				this._allowParentTraversal = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002FED File Offset: 0x000011ED
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002FF5 File Offset: 0x000011F5
		public bool TrimIncomingPaths
		{
			get
			{
				return this._trimIncomingPaths;
			}
			set
			{
				this._trimIncomingPaths = value;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003000 File Offset: 0x00001200
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			if (name.Length > 0)
			{
				while (name.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				{
					name = name.Remove(name.Length - 1, 1);
				}
				return name;
			}
			throw new InvalidNameException("Cannot have an empty directory name");
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003058 File Offset: 0x00001258
		public string TransformFile(string name)
		{
			if (name != null)
			{
				name = WindowsNameTransform.MakeValidName(name, this._replacementChar);
				if (this._trimIncomingPaths)
				{
					name = Path.GetFileName(name);
				}
				if (this._baseDirectory != null)
				{
					name = Path.Combine(this._baseDirectory, name);
					string text = Path.GetFullPath(this._baseDirectory);
					if (text[text.Length - 1] != Path.DirectorySeparatorChar)
					{
						text += Path.DirectorySeparatorChar.ToString();
					}
					if (!this._allowParentTraversal && !Path.GetFullPath(name).StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
					{
						throw new InvalidNameException("Parent traversal in paths is not allowed");
					}
				}
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030FE File Offset: 0x000012FE
		public static bool IsValidName(string name)
		{
			return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_'), StringComparison.Ordinal) == 0;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003124 File Offset: 0x00001324
		public static string MakeValidName(string name, char replacement)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			name = PathUtils.DropPathRoot(name.Replace("/", Path.DirectorySeparatorChar.ToString()));
			while (name.Length > 0)
			{
				if (name[0] != Path.DirectorySeparatorChar)
				{
					break;
				}
				name = name.Remove(0, 1);
			}
			while (name.Length > 0 && name[name.Length - 1] == Path.DirectorySeparatorChar)
			{
				name = name.Remove(name.Length - 1, 1);
			}
			int i;
			for (i = name.IndexOf(string.Format("{0}{0}", Path.DirectorySeparatorChar), StringComparison.Ordinal); i >= 0; i = name.IndexOf(string.Format("{0}{0}", Path.DirectorySeparatorChar), StringComparison.Ordinal))
			{
				name = name.Remove(i, 1);
			}
			i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars);
			if (i >= 0)
			{
				StringBuilder stringBuilder = new StringBuilder(name);
				while (i >= 0)
				{
					stringBuilder[i] = replacement;
					if (i >= name.Length)
					{
						i = -1;
					}
					else
					{
						i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			if (name.Length > 260)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003257 File Offset: 0x00001457
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003260 File Offset: 0x00001460
		public char Replacement
		{
			get
			{
				return this._replacementChar;
			}
			set
			{
				for (int i = 0; i < WindowsNameTransform.InvalidEntryChars.Length; i++)
				{
					if (WindowsNameTransform.InvalidEntryChars[i] == value)
					{
						throw new ArgumentException("invalid path character");
					}
				}
				if (value == Path.DirectorySeparatorChar || value == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException("invalid replacement character");
				}
				this._replacementChar = value;
			}
		}

		// Token: 0x04000023 RID: 35
		private const int MaxPath = 260;

		// Token: 0x04000024 RID: 36
		private string _baseDirectory;

		// Token: 0x04000025 RID: 37
		private bool _trimIncomingPaths;

		// Token: 0x04000026 RID: 38
		private char _replacementChar = '_';

		// Token: 0x04000027 RID: 39
		private bool _allowParentTraversal;

		// Token: 0x04000028 RID: 40
		private static readonly char[] InvalidEntryChars = new char[]
		{
			'"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005',
			'\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
			'\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019',
			'\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f', '*', '?', ':'
		};
	}
}
