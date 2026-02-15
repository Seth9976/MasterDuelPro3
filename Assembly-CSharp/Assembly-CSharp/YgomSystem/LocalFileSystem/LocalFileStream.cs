using System;
using System.IO;
using System.Threading.Tasks;

namespace YgomSystem.LocalFileSystem
{
	// Token: 0x0200074B RID: 1867
	public class LocalFileStream : IDisposable
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x0000216A File Offset: 0x0000036A
		public string nativePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000029CC File Offset: 0x00000BCC
		public StreamOpenMode openMode
		{
			get
			{
				return StreamOpenMode.Create;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x0000216A File Offset: 0x0000036A
		public Stream ioStream
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000F1669 File Offset: 0x000EF869
		public long Position
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000F1669 File Offset: 0x000EF869
		public long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000F1669 File Offset: 0x000EF869
		public long Remain
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x00002739 File Offset: 0x00000939
		private LocalFileStream()
		{
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x0000216D File Offset: 0x0000036D
		private void initialize(string _nativePath, FileLocation _location, StreamOpenMode _openMode)
		{
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x0000216D File Offset: 0x0000036D
		private void openStreamByLocation(Storage _storage, string _name, FileNameType _nameType, StreamOpenMode _openMode)
		{
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x0000216D File Offset: 0x0000036D
		private void openStreamByNativePath(string _nativePath, StreamOpenMode _openMode)
		{
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000F375C File Offset: 0x000F195C
		~LocalFileStream()
		{
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x00002739 File Offset: 0x00000939
		public LocalFileStream(Storage storage, string name, FileNameType nameType, StreamOpenMode openMode)
		{
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x00002739 File Offset: 0x00000939
		public LocalFileStream(Storage storage, string name, StreamOpenMode openMode)
		{
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x00002739 File Offset: 0x00000939
		public LocalFileStream(FileLocation location, StreamOpenMode openMode)
		{
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x00002739 File Offset: 0x00000939
		public LocalFileStream(string nativePath, StreamOpenMode openMode)
		{
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000F3784 File Offset: 0x000F1984
		public FileLocation GetLocation()
		{
			return default(FileLocation);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x0000216D File Offset: 0x0000036D
		public void Write(byte[] data, int offset, int count)
		{
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x0000216A File Offset: 0x0000036A
		public Task WriteAsync(byte[] data, int offset, int count)
		{
			return null;
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteAsyncCallback(byte[] data, int offset, int count, Action finishCallback, Action<Exception> errorCallback)
		{
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteAllBytes(byte[] writeData)
		{
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x0000216A File Offset: 0x0000036A
		public Task WriteAllBytesAsync(byte[] writeData)
		{
			return null;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteAllBytesCallback(byte[] writeData, Action finishCallback, Action<Exception> errorCallback)
		{
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteByte(byte data)
		{
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteShort(short val)
		{
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteUShort(ushort val)
		{
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteInt(int val)
		{
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteUInt(uint val)
		{
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteLong(long val)
		{
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteULong(ulong val)
		{
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteFloat(float val)
		{
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteDouble(double val)
		{
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteBool(bool val)
		{
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteChar(char val)
		{
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0000216D File Offset: 0x0000036D
		public void WriteString(string str)
		{
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x0000216D File Offset: 0x0000036D
		public void Fill(byte data, int count)
		{
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x0000216A File Offset: 0x0000036A
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return null;
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReadAsyncCallback(byte[] buffer, int offset, int count, Action<int> finishCallback, Action<Exception> errorCallback)
		{
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] ReadBytes(int count = 0)
		{
			return null;
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] ReadAllBytes()
		{
			return null;
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x0000216A File Offset: 0x0000036A
		public Task<byte[]> ReadAllBytesAsync()
		{
			return null;
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReadAllBytesCallback(Action<byte[]> finishCallback, Action<Exception> errorCallback)
		{
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x0000216A File Offset: 0x0000036A
		public ReadRequest RequestReadAllBytes()
		{
			return null;
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x000F379C File Offset: 0x000F199C
		private T readType<T>(Func<byte[], int, T> bitConverter, int sizeOfType) where T : struct
		{
			return default(T);
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000029CC File Offset: 0x00000BCC
		public byte ReadByte()
		{
			return 0;
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000029CC File Offset: 0x00000BCC
		public short ReadShort()
		{
			return 0;
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000029CC File Offset: 0x00000BCC
		public ushort ReadUShort()
		{
			return 0;
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ReadInt()
		{
			return 0;
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000029CC File Offset: 0x00000BCC
		public uint ReadUInt()
		{
			return 0U;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000F1669 File Offset: 0x000EF869
		public long ReadLong()
		{
			return 0L;
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000F1669 File Offset: 0x000EF869
		public ulong ReadULong()
		{
			return 0UL;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float ReadFloat()
		{
			return 0f;
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000F165E File Offset: 0x000EF85E
		public double ReadDouble()
		{
			return 0.0;
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ReadBool()
		{
			return false;
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000029CC File Offset: 0x00000BCC
		public char ReadChar()
		{
			return '\0';
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0000216A File Offset: 0x0000036A
		public string ReadString()
		{
			return null;
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000F37B2 File Offset: 0x000F19B2
		private bool tryReadType<T>(Func<T> reader, out T result) where T : struct
		{
			result = default(T);
			return false;
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000F37BC File Offset: 0x000F19BC
		public bool TryReadByte(out byte result)
		{
			result = 0;
			return false;
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000F37C2 File Offset: 0x000F19C2
		public bool TryReadShort(out short result)
		{
			result = 0;
			return false;
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000F37C2 File Offset: 0x000F19C2
		public bool TryReadUShort(out ushort result)
		{
			result = 0;
			return false;
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		public bool TryReadInt(out int result)
		{
			result = 0;
			return false;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		public bool TryReadUInt(out uint result)
		{
			result = 0U;
			return false;
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F37C8 File Offset: 0x000F19C8
		public bool TryReadLong(out long result)
		{
			result = 0L;
			return false;
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000F37C8 File Offset: 0x000F19C8
		public bool TryReadULong(out ulong result)
		{
			result = 0UL;
			return false;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000F37CF File Offset: 0x000F19CF
		public bool TryReadFloat(out float result)
		{
			result = 0f;
			return false;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000F37D9 File Offset: 0x000F19D9
		public bool TryReadDouble(out double result)
		{
			result = 0.0;
			return false;
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000F37BC File Offset: 0x000F19BC
		public bool TryReadBool(out bool result)
		{
			result = false;
			return false;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000F37C2 File Offset: 0x000F19C2
		public bool TryReadChar(out char result)
		{
			result = '\0';
			return false;
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000F37E7 File Offset: 0x000F19E7
		public bool TryReadString(out string result)
		{
			result = null;
			return false;
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000F1669 File Offset: 0x000EF869
		public long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x0000216D File Offset: 0x0000036D
		public void Head()
		{
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x0000216D File Offset: 0x0000036D
		public void Tail()
		{
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x0000216D File Offset: 0x0000036D
		public void Flush()
		{
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x0000216A File Offset: 0x0000036A
		public Task FlushAsync()
		{
			return null;
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x0000216D File Offset: 0x0000036D
		public void Dispose()
		{
		}

		// Token: 0x0400343A RID: 13370
		private Stream m_ioStream;

		// Token: 0x0400343B RID: 13371
		private FileLocation m_location;

		// Token: 0x0400343C RID: 13372
		private string m_nativePath;

		// Token: 0x0400343D RID: 13373
		private StreamOpenMode m_openMode;

		// Token: 0x0400343E RID: 13374
		private ReadRequest m_asyncReadRequest;
	}
}
