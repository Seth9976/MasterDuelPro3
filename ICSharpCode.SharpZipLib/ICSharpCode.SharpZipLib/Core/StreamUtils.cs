using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000BE RID: 190
	public static class StreamUtils
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001AEB2 File Offset: 0x000190B2
		public static void ReadFully(Stream stream, byte[] buffer)
		{
			StreamUtils.ReadFully(stream, buffer, 0, buffer.Length);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001AEC0 File Offset: 0x000190C0
		public static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				int num = stream.Read(buffer, offset, count);
				if (num <= 0)
				{
					throw new EndOfStreamException();
				}
				offset += num;
				count -= num;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001AF38 File Offset: 0x00019138
		public static int ReadRequestedBytes(Stream stream, byte[] buffer, int offset, int count)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			while (count > 0)
			{
				int num2 = stream.Read(buffer, offset, count);
				if (num2 <= 0)
				{
					break;
				}
				offset += num2;
				count -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public static void Copy(Stream source, Stream destination, byte[] buffer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length < 128)
			{
				throw new ArgumentException("Buffer is too small", "buffer");
			}
			bool flag = true;
			while (flag)
			{
				int num = source.Read(buffer, 0, buffer.Length);
				if (num > 0)
				{
					destination.Write(buffer, 0, num);
				}
				else
				{
					destination.Flush();
					flag = false;
				}
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001B02F File Offset: 0x0001922F
		public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name)
		{
			StreamUtils.Copy(source, destination, buffer, progressHandler, updateInterval, sender, name, -1L);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001B044 File Offset: 0x00019244
		public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name, long fixedTarget)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length < 128)
			{
				throw new ArgumentException("Buffer is too small", "buffer");
			}
			if (progressHandler == null)
			{
				throw new ArgumentNullException("progressHandler");
			}
			bool flag = true;
			DateTime dateTime = DateTime.Now;
			long num = 0L;
			long num2 = 0L;
			if (fixedTarget >= 0L)
			{
				num2 = fixedTarget;
			}
			else if (source.CanSeek)
			{
				num2 = source.Length - source.Position;
			}
			ProgressEventArgs progressEventArgs = new ProgressEventArgs(name, num, num2);
			progressHandler(sender, progressEventArgs);
			bool flag2 = true;
			while (flag)
			{
				int num3 = source.Read(buffer, 0, buffer.Length);
				if (num3 > 0)
				{
					num += (long)num3;
					flag2 = false;
					destination.Write(buffer, 0, num3);
				}
				else
				{
					destination.Flush();
					flag = false;
				}
				if (DateTime.Now - dateTime > updateInterval)
				{
					flag2 = true;
					dateTime = DateTime.Now;
					progressEventArgs = new ProgressEventArgs(name, num, num2);
					progressHandler(sender, progressEventArgs);
					flag = progressEventArgs.ContinueRunning;
				}
			}
			if (!flag2)
			{
				progressEventArgs = new ProgressEventArgs(name, num, num2);
				progressHandler(sender, progressEventArgs);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001B174 File Offset: 0x00019374
		internal static async Task WriteProcToStreamAsync(this Stream targetStream, MemoryStream bufferStream, Action<Stream> writeProc, CancellationToken ct)
		{
			bufferStream.SetLength(0L);
			writeProc(bufferStream);
			bufferStream.Position = 0L;
			await bufferStream.CopyToAsync(targetStream, 81920, ct).ConfigureAwait(false);
			bufferStream.SetLength(0L);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001B1D0 File Offset: 0x000193D0
		internal static async Task WriteProcToStreamAsync(this Stream targetStream, Action<Stream> writeProc, CancellationToken ct)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				await targetStream.WriteProcToStreamAsync(ms, writeProc, ct).ConfigureAwait(false);
			}
			MemoryStream ms = null;
		}
	}
}
