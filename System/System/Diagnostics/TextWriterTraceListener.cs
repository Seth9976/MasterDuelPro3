using System;
using System.IO;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as <see cref="T:System.IO.FileStream" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015F RID: 351
	public class TextWriterTraceListener : TraceListener
	{
		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600082E RID: 2094 RVA: 0x0002C760 File Offset: 0x0002A960
		public override void Write(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.Write(message);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> followed by a line terminator. The default line terminator is a carriage return followed by a line feed (\r\n).</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600082F RID: 2095 RVA: 0x0002C7A8 File Offset: 0x0002A9A8
		public override void WriteLine(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.WriteLine(message);
				base.NeedIndent = true;
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002C81C File Offset: 0x0002AA1C
		internal bool EnsureWriter()
		{
			bool flag = true;
			if (this.writer == null)
			{
				flag = false;
				if (this.fileName == null)
				{
					return flag;
				}
				Encoding encodingWithFallback = TextWriterTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
				string text = Path.GetFullPath(this.fileName);
				string directoryName = Path.GetDirectoryName(text);
				string text2 = Path.GetFileName(text);
				for (int i = 0; i < 2; i++)
				{
					try
					{
						this.writer = new StreamWriter(text, true, encodingWithFallback, 4096);
						flag = true;
						break;
					}
					catch (IOException)
					{
						text2 = Guid.NewGuid().ToString() + text2;
						text = Path.Combine(directoryName, text2);
					}
					catch (UnauthorizedAccessException)
					{
						break;
					}
					catch (Exception)
					{
						break;
					}
				}
				if (!flag)
				{
					this.fileName = null;
				}
			}
			return flag;
		}

		// Token: 0x04000641 RID: 1601
		internal TextWriter writer;

		// Token: 0x04000642 RID: 1602
		private string fileName;
	}
}
