using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
	// Token: 0x02000143 RID: 323
	internal class Mime
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x000384F4 File Offset: 0x000366F4
		private Mime()
		{
			Mime.Aliases = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.SubClasses = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalPatternsShort = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalPatternsLong = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalLiterals = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.GlobalSufPref = new NameValueCollection(StringComparer.CurrentCultureIgnoreCase);
			Mime.Matches80Plus = new ArrayList();
			Mime.MatchesBelow80 = new ArrayList();
			int num = new FDOMimeConfigReader().Init();
			if (num >= 32)
			{
				this.buffer = new byte[num];
				this.mime_available = true;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x000385AE File Offset: 0x000367AE
		public static bool MimeAvailable
		{
			get
			{
				return Mime.Instance.mime_available;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000385BC File Offset: 0x000367BC
		public static string GetMimeTypeForFile(string filename)
		{
			object obj = Mime.lock_object;
			lock (obj)
			{
				Mime.Instance.StartByFileName(filename);
			}
			return Mime.Instance.global_result;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003860C File Offset: 0x0003680C
		public static string GetMimeAlias(string mimetype)
		{
			return Mime.Aliases[mimetype];
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003861C File Offset: 0x0003681C
		public static void CleanFileCache()
		{
			object obj = Mime.lock_object;
			lock (obj)
			{
				Mime.Instance.mime_file_cache.Clear();
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00038664 File Offset: 0x00036864
		private void StartByFileName(string filename)
		{
			if (this.mime_file_cache.ContainsKey(filename))
			{
				this.global_result = this.mime_file_cache[filename];
				return;
			}
			this.current_file_name = filename;
			this.is_zero_file = false;
			this.global_result = "application/octet-stream";
			this.GoByFileName();
			this.mime_file_cache.Add(this.current_file_name, this.global_result);
			if (this.mime_file_cache.Count > 3000)
			{
				IEnumerator enumerator = this.mime_file_cache.GetEnumerator();
				int num = 2500;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					this.mime_file_cache.Remove(obj.ToString());
					num--;
					if (num == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00038714 File Offset: 0x00036914
		private void GoByFileName()
		{
			if (!Mime.MimeAvailable || !this.OpenFile())
			{
				this.CheckGlobalPatterns();
				return;
			}
			if (!this.is_zero_file && this.CheckMatch80Plus())
			{
				return;
			}
			if (this.CheckGlobalPatterns())
			{
				return;
			}
			if (this.is_zero_file)
			{
				return;
			}
			if (this.CheckMatchBelow80())
			{
				return;
			}
			this.CheckForBinaryOrText();
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003876C File Offset: 0x0003696C
		private bool CheckMatch80Plus()
		{
			foreach (object obj in Mime.Matches80Plus)
			{
				Match match = (Match)obj;
				if (this.TestMatch(match))
				{
					this.global_result = match.MimeType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000387DC File Offset: 0x000369DC
		private bool FastEndsWidth(string input, string value)
		{
			if (value.Length > input.Length)
			{
				return false;
			}
			int num = input.Length - 1;
			for (int i = value.Length - 1; i > -1; i--)
			{
				if (value[i] != input[num])
				{
					return false;
				}
				num--;
			}
			return true;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003882C File Offset: 0x00036A2C
		private bool FastStartsWith(string input, string value)
		{
			if (value.Length > input.Length)
			{
				return false;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] != input[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00038870 File Offset: 0x00036A70
		private int FastIndexOf(string input, char value)
		{
			if (input.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000388A8 File Offset: 0x00036AA8
		private int FastIndexOf(string input, string value)
		{
			if (input.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < input.Length - value.Length; i++)
			{
				if (input[i] == value[0])
				{
					int num = 0;
					int num2 = 1;
					while (num2 < value.Length && input[i + num2] == value[num2])
					{
						num++;
						num2++;
					}
					if (num == value.Length - 1)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00038920 File Offset: 0x00036B20
		private void CheckGlobalResult()
		{
			int num = this.FastIndexOf(this.global_result, ',');
			if (num != -1)
			{
				this.global_result = this.global_result.Substring(0, num);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00038954 File Offset: 0x00036B54
		private bool CheckGlobalPatterns()
		{
			string fileName = Path.GetFileName(this.current_file_name);
			for (int i = 0; i < Mime.GlobalLiterals.Count; i++)
			{
				string key = Mime.GlobalLiterals.GetKey(i);
				if (this.FastIndexOf(key, '[') == -1)
				{
					if (this.FastIndexOf(fileName, key) != -1)
					{
						this.global_result = Mime.GlobalLiterals[i];
						this.CheckGlobalResult();
						return true;
					}
				}
				else if (Regex.IsMatch(fileName, key))
				{
					this.global_result = Mime.GlobalLiterals[i];
					this.CheckGlobalResult();
					return true;
				}
			}
			if (this.FastIndexOf(fileName, '.') != -1)
			{
				for (int j = 0; j < Mime.GlobalPatternsLong.Count; j++)
				{
					string key2 = Mime.GlobalPatternsLong.GetKey(j);
					if (this.FastEndsWidth(fileName, key2))
					{
						this.global_result = Mime.GlobalPatternsLong[j];
						this.CheckGlobalResult();
						return true;
					}
					if (this.FastEndsWidth(fileName.ToLower(), key2))
					{
						this.global_result = Mime.GlobalPatternsLong[j];
						this.CheckGlobalResult();
						return true;
					}
				}
				string extension = Path.GetExtension(this.current_file_name);
				if (extension.Length != 0)
				{
					string text = Mime.GlobalPatternsShort[extension];
					if (text != null)
					{
						this.global_result = text;
						this.CheckGlobalResult();
						return true;
					}
					text = Mime.GlobalPatternsShort[extension.ToLower()];
					if (text != null)
					{
						this.global_result = text;
						this.CheckGlobalResult();
						return true;
					}
				}
			}
			for (int k = 0; k < Mime.GlobalSufPref.Count; k++)
			{
				string key3 = Mime.GlobalSufPref.GetKey(k);
				if (key3[0] == '*')
				{
					if (this.FastEndsWidth(fileName, key3.Replace("*", string.Empty)))
					{
						this.global_result = Mime.GlobalSufPref[k];
						this.CheckGlobalResult();
						return true;
					}
				}
				else if (this.FastStartsWith(fileName, key3.Replace("*", string.Empty)))
				{
					this.global_result = Mime.GlobalSufPref[k];
					this.CheckGlobalResult();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00038B64 File Offset: 0x00036D64
		private bool CheckMatchBelow80()
		{
			foreach (object obj in Mime.MatchesBelow80)
			{
				Match match = (Match)obj;
				if (this.TestMatch(match))
				{
					this.global_result = match.MimeType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00038BD4 File Offset: 0x00036DD4
		private void CheckForBinaryOrText()
		{
			for (int i = 0; i < 32; i++)
			{
				char c = Convert.ToChar(this.buffer[i]);
				if (c != '\t' && c != '\n' && c != '\r' && c != '\f' && c < ' ')
				{
					this.global_result = "application/octet-stream";
					return;
				}
			}
			this.global_result = "text/plain";
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00038C2C File Offset: 0x00036E2C
		private bool TestMatch(Match match)
		{
			foreach (object obj in match.Matchlets)
			{
				Matchlet matchlet = (Matchlet)obj;
				if (this.TestMatchlet(matchlet))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00038C90 File Offset: 0x00036E90
		private bool TestMatchlet(Matchlet matchlet)
		{
			if (matchlet.Offset + matchlet.ByteValue.Length > this.bytes_read)
			{
				return false;
			}
			for (int i = 0; i < matchlet.OffsetLength; i++)
			{
				if (matchlet.Offset + i + matchlet.ByteValue.Length > this.bytes_read)
				{
					return false;
				}
				if (matchlet.Mask == null)
				{
					if (this.buffer[matchlet.Offset + i] == matchlet.ByteValue[0])
					{
						if (matchlet.ByteValue.Length == 1)
						{
							if (matchlet.Matchlets.Count > 0)
							{
								using (IEnumerator enumerator = matchlet.Matchlets.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										object obj = enumerator.Current;
										Matchlet matchlet2 = (Matchlet)obj;
										if (this.TestMatchlet(matchlet2))
										{
											return true;
										}
									}
									goto IL_00C6;
								}
							}
							return true;
						}
						IL_00C6:
						int num = 0;
						if (matchlet.ByteValue.Length > 2)
						{
							if (this.buffer[matchlet.Offset + i + matchlet.ByteValue.Length - 1] != matchlet.ByteValue[matchlet.ByteValue.Length - 1])
							{
								return false;
							}
							num = 1;
						}
						for (int j = 1; j < matchlet.ByteValue.Length - num; j++)
						{
							if (this.buffer[matchlet.Offset + i + j] != matchlet.ByteValue[j])
							{
								return false;
							}
						}
						if (matchlet.Matchlets.Count > 0)
						{
							using (IEnumerator enumerator = matchlet.Matchlets.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj2 = enumerator.Current;
									Matchlet matchlet3 = (Matchlet)obj2;
									if (this.TestMatchlet(matchlet3))
									{
										return true;
									}
								}
								goto IL_0337;
							}
						}
						return true;
					}
				}
				else if ((this.buffer[matchlet.Offset + i] & matchlet.Mask[0]) == (matchlet.ByteValue[0] & matchlet.Mask[0]))
				{
					if (matchlet.ByteValue.Length == 1)
					{
						if (matchlet.Matchlets.Count > 0)
						{
							using (IEnumerator enumerator = matchlet.Matchlets.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj3 = enumerator.Current;
									Matchlet matchlet4 = (Matchlet)obj3;
									if (this.TestMatchlet(matchlet4))
									{
										return true;
									}
								}
								goto IL_022E;
							}
						}
						return true;
					}
					IL_022E:
					int num2 = 0;
					if (matchlet.ByteValue.Length > 2)
					{
						if ((this.buffer[matchlet.Offset + i + matchlet.ByteValue.Length - 1] & matchlet.Mask[matchlet.ByteValue.Length - 1]) != (matchlet.ByteValue[matchlet.ByteValue.Length - 1] & matchlet.Mask[matchlet.ByteValue.Length - 1]))
						{
							return false;
						}
						num2 = 1;
					}
					for (int k = 1; k < matchlet.ByteValue.Length - num2; k++)
					{
						if ((this.buffer[matchlet.Offset + i + k] & matchlet.Mask[k]) != (matchlet.ByteValue[k] & matchlet.Mask[k]))
						{
							return false;
						}
					}
					if (matchlet.Matchlets.Count > 0)
					{
						using (IEnumerator enumerator = matchlet.Matchlets.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj4 = enumerator.Current;
								Matchlet matchlet5 = (Matchlet)obj4;
								if (this.TestMatchlet(matchlet5))
								{
									return true;
								}
							}
							goto IL_0337;
						}
					}
					return true;
				}
				IL_0337:;
			}
			return false;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003901C File Offset: 0x0003721C
		private bool OpenFile()
		{
			try
			{
				this.file_stream = new FileStream(this.current_file_name, FileMode.Open, FileAccess.Read);
				if (this.file_stream.Length == 0L)
				{
					this.global_result = "application/x-zerosize";
					this.is_zero_file = true;
				}
				else
				{
					this.bytes_read = this.file_stream.Read(this.buffer, 0, this.buffer.Length);
					if (this.bytes_read < this.buffer.Length)
					{
						Array.Clear(this.buffer, this.bytes_read, this.buffer.Length - this.bytes_read);
					}
				}
				this.file_stream.Close();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000827 RID: 2087
		public static Mime Instance = new Mime();

		// Token: 0x04000828 RID: 2088
		private string current_file_name;

		// Token: 0x04000829 RID: 2089
		private string global_result = "application/octet-stream";

		// Token: 0x0400082A RID: 2090
		private FileStream file_stream;

		// Token: 0x0400082B RID: 2091
		private byte[] buffer;

		// Token: 0x0400082C RID: 2092
		private StringDictionary mime_file_cache = new StringDictionary();

		// Token: 0x0400082D RID: 2093
		private static object lock_object = new object();

		// Token: 0x0400082E RID: 2094
		private bool is_zero_file;

		// Token: 0x0400082F RID: 2095
		private int bytes_read;

		// Token: 0x04000830 RID: 2096
		private bool mime_available;

		// Token: 0x04000831 RID: 2097
		public static NameValueCollection Aliases;

		// Token: 0x04000832 RID: 2098
		public static NameValueCollection SubClasses;

		// Token: 0x04000833 RID: 2099
		public static NameValueCollection GlobalPatternsShort;

		// Token: 0x04000834 RID: 2100
		public static NameValueCollection GlobalPatternsLong;

		// Token: 0x04000835 RID: 2101
		public static NameValueCollection GlobalLiterals;

		// Token: 0x04000836 RID: 2102
		public static NameValueCollection GlobalSufPref;

		// Token: 0x04000837 RID: 2103
		public static ArrayList Matches80Plus;

		// Token: 0x04000838 RID: 2104
		public static ArrayList MatchesBelow80;
	}
}
