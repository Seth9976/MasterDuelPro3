using System;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x0200031D RID: 797
	[Serializable]
	internal sealed class SecurityDocument
	{
		// Token: 0x06001C5C RID: 7260 RVA: 0x0006EB53 File Offset: 0x0006CD53
		public SecurityDocument(int numData)
		{
			this.m_data = new byte[numData];
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0006EB68 File Offset: 0x0006CD68
		public void GuaranteeSize(int size)
		{
			if (this.m_data.Length < size)
			{
				byte[] array = new byte[(size / 32 + 1) * 32];
				Array.Copy(this.m_data, 0, array, 0, this.m_data.Length);
				this.m_data = array;
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006EBAC File Offset: 0x0006CDAC
		public void AddString(string str, ref int position)
		{
			this.GuaranteeSize(position + str.Length * 2 + 2);
			for (int i = 0; i < str.Length; i++)
			{
				this.m_data[position + 2 * i] = (byte)(str[i] >> 8);
				this.m_data[position + 2 * i + 1] = (byte)(str[i] & 'ÿ');
			}
			this.m_data[position + str.Length * 2] = 0;
			this.m_data[position + str.Length * 2 + 1] = 0;
			position += str.Length * 2 + 2;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0006EC48 File Offset: 0x0006CE48
		public void AppendString(string str, ref int position)
		{
			if (position <= 1 || this.m_data[position - 1] != 0 || this.m_data[position - 2] != 0)
			{
				throw new XmlSyntaxException();
			}
			position -= 2;
			this.AddString(str, ref position);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0006EC7D File Offset: 0x0006CE7D
		public static int EncodedStringSize(string str)
		{
			return str.Length * 2 + 2;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0006EC8C File Offset: 0x0006CE8C
		public string GetString(ref int position, bool bCreate)
		{
			int num = position;
			while (num < this.m_data.Length - 1 && (this.m_data[num] != 0 || this.m_data[num + 1] != 0))
			{
				num += 2;
			}
			Tokenizer.StringMaker sharedStringMaker = SharedStatics.GetSharedStringMaker();
			string text;
			try
			{
				if (bCreate)
				{
					sharedStringMaker._outStringBuilder = null;
					sharedStringMaker._outIndex = 0;
					for (int i = position; i < num; i += 2)
					{
						char c = (char)(((int)this.m_data[i] << 8) | (int)this.m_data[i + 1]);
						if (sharedStringMaker._outIndex < 512)
						{
							char[] outChars = sharedStringMaker._outChars;
							Tokenizer.StringMaker stringMaker = sharedStringMaker;
							int outIndex = stringMaker._outIndex;
							stringMaker._outIndex = outIndex + 1;
							outChars[outIndex] = c;
						}
						else
						{
							if (sharedStringMaker._outStringBuilder == null)
							{
								sharedStringMaker._outStringBuilder = new StringBuilder();
							}
							sharedStringMaker._outStringBuilder.Append(sharedStringMaker._outChars, 0, 512);
							sharedStringMaker._outChars[0] = c;
							sharedStringMaker._outIndex = 1;
						}
					}
				}
				position = num + 2;
				if (bCreate)
				{
					text = sharedStringMaker.MakeString();
				}
				else
				{
					text = null;
				}
			}
			finally
			{
				SharedStatics.ReleaseSharedStringMaker(ref sharedStringMaker);
			}
			return text;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0006EDA0 File Offset: 0x0006CFA0
		public void AddToken(byte b, ref int position)
		{
			this.GuaranteeSize(position + 1);
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			data[num] = b;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0006EDC8 File Offset: 0x0006CFC8
		public SecurityElement GetRootElement()
		{
			return this.GetElement(0, true);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0006EDD2 File Offset: 0x0006CFD2
		public SecurityElement GetElement(int position, bool bCreate)
		{
			return this.InternalGetElement(ref position, bCreate);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
		internal SecurityElement InternalGetElement(ref int position, bool bCreate)
		{
			if (this.m_data.Length <= position)
			{
				throw new XmlSyntaxException();
			}
			byte[] data = this.m_data;
			int num = position;
			position = num + 1;
			if (data[num] != 1)
			{
				throw new XmlSyntaxException();
			}
			SecurityElement securityElement = null;
			string @string = this.GetString(ref position, bCreate);
			if (bCreate)
			{
				securityElement = new SecurityElement(@string);
			}
			while (this.m_data[position] == 2)
			{
				position++;
				string string2 = this.GetString(ref position, bCreate);
				string string3 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddAttribute(string2, string3);
				}
			}
			if (this.m_data[position] == 3)
			{
				position++;
				string string4 = this.GetString(ref position, bCreate);
				if (bCreate)
				{
					securityElement.m_strText = string4;
				}
			}
			while (this.m_data[position] != 4)
			{
				SecurityElement securityElement2 = this.InternalGetElement(ref position, bCreate);
				if (bCreate)
				{
					securityElement.AddChild(securityElement2);
				}
			}
			position++;
			return securityElement;
		}

		// Token: 0x04000D14 RID: 3348
		internal byte[] m_data;
	}
}
