using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace System.Resources
{
	/// <summary>Enumerates the resources in a binary resources (.resources) file by reading sequential resource name/value pairs.</summary>
	// Token: 0x020005D4 RID: 1492
	[ComVisible(true)]
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x06002C24 RID: 11300 RVA: 0x000AE885 File Offset: 0x000ACA85
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		/// <summary>Releases all operating system resources associated with this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		// Token: 0x06002C25 RID: 11301 RVA: 0x000AE8B7 File Offset: 0x000ACAB7
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Resources.ResourceReader" /> class.</summary>
		// Token: 0x06002C26 RID: 11302 RVA: 0x000AE8C0 File Offset: 0x000ACAC0
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000AE8C8 File Offset: 0x000ACAC8
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000AE92C File Offset: 0x000ACB2C
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | ((int)((byte*)p)[1] << 8) | ((int)((byte*)p)[2] << 16) | ((int)((byte*)p)[3] << 24);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000AE954 File Offset: 0x000ACB54
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000AE995 File Offset: 0x000ACB95
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000AE9BC File Offset: 0x000ACBBC
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into name section.", new object[] { num }));
			}
			return num;
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has already been closed and cannot be accessed. </exception>
		// Token: 0x06002C2C RID: 11308 RVA: 0x000AEA23 File Offset: 0x000ACC23
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has been closed or disposed, and cannot be accessed. </exception>
		// Token: 0x06002C2D RID: 11309 RVA: 0x000AEA2B File Offset: 0x000ACC2B
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000AEA4B File Offset: 0x000ACC4B
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000AEA54 File Offset: 0x000ACC54
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { num5 }));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000AEBC8 File Offset: 0x000ACDC8
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. A resource name extends past the end of the stream."));
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Resource name extends past the end of the file."));
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000AEC90 File Offset: 0x000ACE90
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array3;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String for name index '{0}' extends past the end of the file.", new object[] { index }));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string text;
					if (!BitConverter.IsLittleEndian)
					{
						byte* ptr = (byte*)positionPointer;
						byte[] array = new byte[num2];
						for (int i = 0; i < num2; i += 2)
						{
							array[i] = (ptr + i)[1];
							array[i + 1] = ptr[i];
						}
						byte[] array2;
						byte* ptr2;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array2[0];
						}
						text = new string((char*)ptr2, 0, num2 / 2);
						array2 = null;
					}
					else
					{
						text = new string(positionPointer, 0, num2 / 2);
					}
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { dataOffset }));
					}
					return text;
				}
				else
				{
					array3 = new byte[num2];
					int num3;
					for (int j = num2; j > 0; j -= num3)
					{
						num3 = this._store.Read(array3, num2 - j, j);
						if (num3 == 0)
						{
							throw new EndOfStreamException(Environment.GetResourceString("Corrupt .resources file. The resource name for name index {0} extends past the end of the stream.", new object[] { index }));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { dataOffset }));
					}
				}
			}
			return Encoding.Unicode.GetString(array3, 0, num2);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000AEF00 File Offset: 0x000AD100
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object obj;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { num2 }));
				}
				if (this._version == 1)
				{
					obj = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					obj = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return obj;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000AEFCC File Offset: 0x000AD1CC
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string text = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Resource was of type '{0}' instead of String - call GetObject instead.", new object[] { this.FindType(num).FullName }));
				}
				text = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string text2;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						text2 = resourceTypeCode.ToString();
					}
					else
					{
						text2 = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(Environment.GetResourceString("Resource was of type '{0}' instead of String - call GetObject instead.", new object[] { text2 }));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					text = this._store.ReadString();
				}
			}
			return text;
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000AF0B8 File Offset: 0x000AD2B8
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000AF0E0 File Offset: 0x000AD2E0
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000AF118 File Offset: 0x000AD318
		internal object LoadObjectV1(int pos)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex2);
			}
			return obj;
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000AF170 File Offset: 0x000AD370
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			RuntimeType runtimeType = this.FindType(num);
			if (runtimeType == typeof(string))
			{
				return this._store.ReadString();
			}
			if (runtimeType == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (runtimeType == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (runtimeType == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (runtimeType == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (runtimeType == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (runtimeType == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (runtimeType == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (runtimeType == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (runtimeType == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (runtimeType == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (runtimeType == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (runtimeType == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (runtimeType == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000AF3C8 File Offset: 0x000AD5C8
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex2);
			}
			return obj;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000AF424 File Offset: 0x000AD624
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
				return DateTime.FromBinary(this._store.ReadInt64());
			case ResourceTypeCode.TimeSpan:
				return new TimeSpan(this._store.ReadInt64());
			case ResourceTypeCode.ByteArray:
			{
				int num = this._store.ReadInt32();
				if (num < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num }));
				}
				if (this._ums == null)
				{
					if ((long)num > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num }));
					}
					return this._store.ReadBytes(num);
				}
				else
				{
					if ((long)num > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num }));
					}
					byte[] array = new byte[num];
					this._ums.Read(array, 0, num);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num2 = this._store.ReadInt32();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num2 }));
				}
				if (this._ums == null)
				{
					return new PinnedBufferMemoryStream(this._store.ReadBytes(num2));
				}
				if ((long)num2 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num2 }));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num2, (long)num2, FileAccess.Read);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."));
			}
			int num3 = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(num3);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000AF794 File Offset: 0x000AD994
		private object DeserializeObject(int typeIndex)
		{
			RuntimeType runtimeType = this.FindType(typeIndex);
			object obj = this._objFormatter.Deserialize(this._store.BaseStream);
			if (obj.GetType() != runtimeType)
			{
				throw new BadImageFormatException(Environment.GetResourceString("The type serialized in the .resources file was not the same type that the .resources file said it contained. Expected '{0}' but read '{1}'.", new object[]
				{
					runtimeType.FullName,
					obj.GetType().FullName
				}));
			}
			return obj;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000AF7FC File Offset: 0x000AD9FC
		private void ReadResources()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this._objFormatter = binaryFormatter;
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."), ex);
			}
			catch (IndexOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."), ex2);
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000AF868 File Offset: 0x000ADA68
		private unsafe void _ReadResources()
		{
			if (this._store.ReadInt32() != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream is not a valid resource file."));
			}
			int num = this._store.ReadInt32();
			int num2 = this._store.ReadInt32();
			if (num2 < 0 || num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			if (num > 1)
			{
				this._store.BaseStream.Seek((long)num2, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				AssemblyName assemblyName = new AssemblyName(ResourceManager.MscorlibName);
				if (!ResourceManager.CompareNames(text, ResourceManager.ResReaderTypeName, assemblyName))
				{
					throw new NotSupportedException(Environment.GetResourceString("This .resources file should not be read with this reader. The resource reader type is \"{0}\".", new object[] { text }));
				}
				this.SkipString();
			}
			int num3 = this._store.ReadInt32();
			if (num3 != 2 && num3 != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("The ResourceReader class does not know how to read this version of .resources files. Expected version: {0}  This file: {1}", new object[] { 2, num3 }));
			}
			this._version = num3;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			int num4 = this._store.ReadInt32();
			if (num4 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			this._typeTable = new RuntimeType[num4];
			this._typeNamePositions = new int[num4];
			for (int i = 0; i < num4; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			int num5 = (int)this._store.BaseStream.Position & 7;
			if (num5 != 0)
			{
				for (int j = 0; j < 8 - num5; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
				}
				int num6 = 4 * this._numResources;
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num6, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num7 = this._store.ReadInt32();
					if (num7 < 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
					}
					this._namePositions[l] = num7;
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
				}
				int num8 = 4 * this._numResources;
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num8, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000AFBE8 File Offset: 0x000ADDE8
		private RuntimeType FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't exist."));
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string text = this._store.ReadString();
					this._typeTable[typeIndex] = (RuntimeType)Type.GetType(text, true);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		// Token: 0x0400166B RID: 5739
		private BinaryReader _store;

		// Token: 0x0400166C RID: 5740
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x0400166D RID: 5741
		private long _nameSectionOffset;

		// Token: 0x0400166E RID: 5742
		private long _dataSectionOffset;

		// Token: 0x0400166F RID: 5743
		private int[] _nameHashes;

		// Token: 0x04001670 RID: 5744
		private unsafe int* _nameHashesPtr;

		// Token: 0x04001671 RID: 5745
		private int[] _namePositions;

		// Token: 0x04001672 RID: 5746
		private unsafe int* _namePositionsPtr;

		// Token: 0x04001673 RID: 5747
		private RuntimeType[] _typeTable;

		// Token: 0x04001674 RID: 5748
		private int[] _typeNamePositions;

		// Token: 0x04001675 RID: 5749
		private BinaryFormatter _objFormatter;

		// Token: 0x04001676 RID: 5750
		private int _numResources;

		// Token: 0x04001677 RID: 5751
		private UnmanagedMemoryStream _ums;

		// Token: 0x04001678 RID: 5752
		private int _version;

		// Token: 0x020005D5 RID: 1493
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06002C3E RID: 11326 RVA: 0x000AFC9C File Offset: 0x000ADE9C
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x06002C3F RID: 11327 RVA: 0x000AFCBC File Offset: 0x000ADEBC
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000AFD18 File Offset: 0x000ADF18
			public object Key
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000AFD8E File Offset: 0x000ADF8E
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000AFD9B File Offset: 0x000ADF9B
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000AFDA4 File Offset: 0x000ADFA4
			public DictionaryEntry Entry
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string text;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							text = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(text, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(text, obj);
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000AFED4 File Offset: 0x000AE0D4
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x06002C45 RID: 11333 RVA: 0x000AFF44 File Offset: 0x000AE144
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x04001679 RID: 5753
			private ResourceReader _reader;

			// Token: 0x0400167A RID: 5754
			private bool _currentIsValid;

			// Token: 0x0400167B RID: 5755
			private int _currentName;

			// Token: 0x0400167C RID: 5756
			private int _dataPosition;
		}
	}
}
