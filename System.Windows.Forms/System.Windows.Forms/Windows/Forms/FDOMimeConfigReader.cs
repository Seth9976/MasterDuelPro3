using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000144 RID: 324
	internal class FDOMimeConfigReader
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x000390EC File Offset: 0x000372EC
		public int Init()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform != 4 && platform != 6 && platform != 128)
			{
				return -1;
			}
			this.CheckFDOMimePaths();
			if (!this.fdo_mime_available)
			{
				return -1;
			}
			this.ReadMagicData();
			this.ReadGlobsData();
			this.ReadSubclasses();
			this.ReadAliases();
			this.shared_mime_paths = null;
			this.br = null;
			return this.max_offset_and_range;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00039154 File Offset: 0x00037354
		private void CheckFDOMimePaths()
		{
			if (Directory.Exists("/usr/share/mime"))
			{
				this.shared_mime_paths.Add("/usr/share/mime/");
			}
			else if (Directory.Exists("/usr/local/share/mime"))
			{
				this.shared_mime_paths.Add("/usr/local/share/mime/");
			}
			if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.local/share/mime"))
			{
				this.shared_mime_paths.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.local/share/mime/");
			}
			if (this.shared_mime_paths.Count == 0)
			{
				return;
			}
			this.fdo_mime_available = true;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000391E8 File Offset: 0x000373E8
		private void ReadMagicData()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/magic"))
				{
					try
					{
						FileStream fileStream = File.OpenRead(text + "/magic");
						this.br = new BinaryReader(fileStream);
						if (this.CheckMagicHeader())
						{
							this.MakeMatches();
						}
						this.br.Close();
						fileStream.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00039298 File Offset: 0x00037498
		private void MakeMatches()
		{
			Matchlet[] array = new Matchlet[30];
			while (this.br.PeekChar() != -1)
			{
				int num = -1;
				string text = this.ReadPriorityAndMimeType(ref num);
				if (text != null)
				{
					Match match = new Match();
					match.Priority = num;
					match.MimeType = text;
					do
					{
						int num2 = 0;
						if (this.br.PeekChar() != 62)
						{
							StringBuilder stringBuilder = new StringBuilder();
							while (this.br.PeekChar() != 62)
							{
								char c = this.br.ReadChar();
								stringBuilder.Append(c);
							}
							num2 = Convert.ToInt32(stringBuilder.ToString());
						}
						int num3 = 0;
						if (this.br.PeekChar() == 62)
						{
							this.br.ReadChar();
							num3 = this.ReadValue();
						}
						int num4 = 0;
						byte[] array2 = null;
						if (this.br.PeekChar() == 61)
						{
							this.br.ReadChar();
							int num5 = (int)this.br.ReadByte();
							byte b = this.br.ReadByte();
							num4 = num5 * 256 + (int)b;
							array2 = this.br.ReadBytes(num4);
						}
						byte[] array3 = null;
						if (this.br.PeekChar() == 38)
						{
							this.br.ReadChar();
							array3 = this.br.ReadBytes(num4);
						}
						if (this.br.PeekChar() == 126)
						{
							this.br.ReadChar();
							char c = this.br.ReadChar();
							int num6 = Convert.ToInt32((int)(c - '0'));
							if (num6 > 1 && BitConverter.IsLittleEndian)
							{
								if (num6 == 2)
								{
									if (array2 != null)
									{
										for (int i = 0; i < array2.Length; i += 2)
										{
											byte b2 = array2[i];
											byte b3 = array2[i + 1];
											array2[i] = b3;
											array2[i + 1] = b2;
										}
									}
									if (array3 != null)
									{
										for (int j = 0; j < array3.Length; j += 2)
										{
											byte b4 = array3[j];
											byte b5 = array3[j + 1];
											array3[j] = b5;
											array3[j + 1] = b4;
										}
									}
								}
								else if (num6 == 4)
								{
									if (array2 != null)
									{
										for (int k = 0; k < array2.Length; k += 4)
										{
											byte b6 = array2[k];
											byte b7 = array2[k + 1];
											byte b8 = array2[k + 2];
											byte b9 = array2[k + 3];
											array2[k] = b9;
											array2[k + 1] = b8;
											array2[k + 2] = b7;
											array2[k + 3] = b6;
										}
									}
									if (array3 != null)
									{
										for (int l = 0; l < array3.Length; l += 4)
										{
											byte b10 = array3[l];
											byte b11 = array3[l + 1];
											byte b12 = array3[l + 2];
											byte b13 = array3[l + 3];
											array3[l] = b13;
											array3[l + 1] = b12;
											array3[l + 2] = b11;
											array3[l + 3] = b10;
										}
									}
								}
							}
						}
						int num7 = 1;
						if (this.br.PeekChar() == 43)
						{
							this.br.ReadChar();
							num7 = this.ReadValue();
						}
						this.br.ReadChar();
						array[num2] = new Matchlet();
						array[num2].Offset = num3;
						array[num2].OffsetLength = num7;
						array[num2].ByteValue = array2;
						if (array3 != null)
						{
							array[num2].Mask = array3;
						}
						if (num2 == 0)
						{
							match.Matchlets.Add(array[num2]);
						}
						else
						{
							array[num2 - 1].Matchlets.Add(array[num2]);
						}
						if (this.max_offset_and_range < array[num2].Offset + array[num2].OffsetLength + array[num2].ByteValue.Length + 1)
						{
							this.max_offset_and_range = array[num2].Offset + array[num2].OffsetLength + array[num2].ByteValue.Length + 1;
						}
					}
					while (this.br.PeekChar() != 91);
					if (num < 80)
					{
						Mime.MatchesBelow80.Add(match);
					}
					else
					{
						Mime.Matches80Plus.Add(match);
					}
				}
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003967C File Offset: 0x0003787C
		private void ReadGlobsData()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/globs"))
				{
					try
					{
						StreamReader streamReader = new StreamReader(text + "/globs");
						while (streamReader.Peek() != -1)
						{
							string text2 = streamReader.ReadLine().Trim();
							if (!text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[] { ':' });
								if (array[1].IndexOf('*') > -1 && array[1].IndexOf('.') == -1)
								{
									Mime.GlobalSufPref.Add(array[1], array[0]);
								}
								else if (array[1].IndexOf('*') == -1)
								{
									Mime.GlobalLiterals.Add(array[1], array[0]);
								}
								else if (array[1].Split(new char[] { '.' }).Length > 2)
								{
									Mime.GlobalPatternsLong.Add(array[1].Remove(0, 1), array[0]);
								}
								else
								{
									Mime.GlobalPatternsShort.Add(array[1].Remove(0, 1), array[0]);
								}
							}
						}
						streamReader.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00039808 File Offset: 0x00037A08
		private void ReadSubclasses()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/subclasses"))
				{
					try
					{
						StreamReader streamReader = new StreamReader(text + "/subclasses");
						while (streamReader.Peek() != -1)
						{
							string text2 = streamReader.ReadLine().Trim();
							if (!text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[] { ' ' });
								Mime.SubClasses.Add(array[0], array[1]);
							}
						}
						streamReader.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000398E0 File Offset: 0x00037AE0
		private void ReadAliases()
		{
			foreach (string text in this.shared_mime_paths)
			{
				if (File.Exists(text + "/aliases"))
				{
					try
					{
						StreamReader streamReader = new StreamReader(text + "/aliases");
						while (streamReader.Peek() != -1)
						{
							string text2 = streamReader.ReadLine().Trim();
							if (!text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[] { ' ' });
								Mime.Aliases.Add(array[0], array[1]);
							}
						}
						streamReader.Close();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000399B8 File Offset: 0x00037BB8
		private int ReadValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.br.PeekChar() != 61 && this.br.PeekChar() != 10)
			{
				char c = this.br.ReadChar();
				stringBuilder.Append(c);
			}
			return Convert.ToInt32(stringBuilder.ToString());
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00039A0C File Offset: 0x00037C0C
		private string ReadPriorityAndMimeType(ref int priority)
		{
			if (this.br.ReadChar() == '[')
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (;;)
				{
					char c = this.br.ReadChar();
					if (c == ':')
					{
						break;
					}
					stringBuilder.Append(c);
				}
				priority = Convert.ToInt32(stringBuilder.ToString());
				StringBuilder stringBuilder2 = new StringBuilder();
				for (;;)
				{
					char c2 = this.br.ReadChar();
					if (c2 == ']')
					{
						break;
					}
					stringBuilder2.Append(c2);
				}
				if (this.br.ReadChar() == '\n')
				{
					return stringBuilder2.ToString();
				}
			}
			return null;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00039A90 File Offset: 0x00037C90
		private bool CheckMagicHeader()
		{
			return !(new string(this.br.ReadChars(10)) != "MIME-Magic") && this.br.ReadByte() == 0 && this.br.ReadChar() == '\n';
		}

		// Token: 0x04000839 RID: 2105
		private bool fdo_mime_available;

		// Token: 0x0400083A RID: 2106
		private StringCollection shared_mime_paths = new StringCollection();

		// Token: 0x0400083B RID: 2107
		private BinaryReader br;

		// Token: 0x0400083C RID: 2108
		private int max_offset_and_range;
	}
}
