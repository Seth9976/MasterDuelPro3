using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000045 RID: 69
	public class ZipNameTransform : INameTransform
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000080C2 File Offset: 0x000062C2
		public ZipNameTransform()
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A4CF File Offset: 0x000086CF
		public ZipNameTransform(string trimPrefix)
		{
			this.TrimPrefix = trimPrefix;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A4E0 File Offset: 0x000086E0
		static ZipNameTransform()
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			int num = invalidPathChars.Length + 2;
			ZipNameTransform.InvalidEntryCharsRelaxed = new char[num];
			Array.Copy(invalidPathChars, 0, ZipNameTransform.InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
			ZipNameTransform.InvalidEntryCharsRelaxed[num - 1] = '*';
			ZipNameTransform.InvalidEntryCharsRelaxed[num - 2] = '?';
			num = invalidPathChars.Length + 4;
			ZipNameTransform.InvalidEntryChars = new char[num];
			Array.Copy(invalidPathChars, 0, ZipNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
			ZipNameTransform.InvalidEntryChars[num - 1] = ':';
			ZipNameTransform.InvalidEntryChars[num - 2] = '\\';
			ZipNameTransform.InvalidEntryChars[num - 3] = '*';
			ZipNameTransform.InvalidEntryChars[num - 4] = '?';
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A577 File Offset: 0x00008777
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			if (name.Length > 0)
			{
				if (!name.EndsWith("/", StringComparison.Ordinal))
				{
					name += "/";
				}
				return name;
			}
			throw new ZipException("Cannot have an empty directory name");
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A5B4 File Offset: 0x000087B4
		public string TransformFile(string name)
		{
			if (name != null)
			{
				string text = name.ToLower();
				if (this.trimPrefix_ != null && text.IndexOf(this.trimPrefix_, StringComparison.Ordinal) == 0)
				{
					name = name.Substring(this.trimPrefix_.Length);
				}
				name = name.Replace("\\", "/");
				name = PathUtils.DropPathRoot(name);
				name = name.Trim(new char[] { '/' });
				for (int i = name.IndexOf("//", StringComparison.Ordinal); i >= 0; i = name.IndexOf("//", StringComparison.Ordinal))
				{
					name = name.Remove(i, 1);
				}
				name = ZipNameTransform.MakeValidName(name, '_');
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A663 File Offset: 0x00008863
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000A66B File Offset: 0x0000886B
		public string TrimPrefix
		{
			get
			{
				return this.trimPrefix_;
			}
			set
			{
				this.trimPrefix_ = value;
				if (this.trimPrefix_ != null)
				{
					this.trimPrefix_ = this.trimPrefix_.ToLower();
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A690 File Offset: 0x00008890
		private static string MakeValidName(string name, char replacement)
		{
			int i = name.IndexOfAny(ZipNameTransform.InvalidEntryChars);
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
						i = name.IndexOfAny(ZipNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			if (name.Length > 65535)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A6FC File Offset: 0x000088FC
		public static bool IsValidName(string name, bool relaxed)
		{
			bool flag = name != null;
			if (flag)
			{
				if (relaxed)
				{
					flag = name.IndexOfAny(ZipNameTransform.InvalidEntryCharsRelaxed) < 0;
				}
				else
				{
					flag = name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
				}
			}
			return flag;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A743 File Offset: 0x00008943
		public static bool IsValidName(string name)
		{
			return name != null && name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
		}

		// Token: 0x0400014E RID: 334
		private string trimPrefix_;

		// Token: 0x0400014F RID: 335
		private static readonly char[] InvalidEntryChars;

		// Token: 0x04000150 RID: 336
		private static readonly char[] InvalidEntryCharsRelaxed;
	}
}
