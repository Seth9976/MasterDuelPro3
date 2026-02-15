using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Versioning;
using System.Text;

namespace System.Resources
{
	/// <summary>Writes resources in the system-default format to an output file or an output stream. This class cannot be inherited.</summary>
	// Token: 0x020005D7 RID: 1495
	[ComVisible(true)]
	public sealed class ResourceWriter : IResourceWriter, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceWriter" /> class that writes the resources to the specified file.</summary>
		/// <param name="fileName">The output file name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is null. </exception>
		// Token: 0x06002C54 RID: 11348 RVA: 0x000B01A4 File Offset: 0x000AE3A4
		public ResourceWriter(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this._output = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			this._resourceList = new Dictionary<string, object>(1000, FastResourceComparer.Default);
			this._caseInsensitiveDups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceWriter" /> class that writes the resources to the provided stream.</summary>
		/// <param name="stream">The output stream. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is not writable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is null. </exception>
		// Token: 0x06002C55 RID: 11349 RVA: 0x000B01FC File Offset: 0x000AE3FC
		public ResourceWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			this._output = stream;
			this._resourceList = new Dictionary<string, object>(1000, FastResourceComparer.Default);
			this._caseInsensitiveDups = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Saves the resources to the output stream and then closes it.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization of the object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002C56 RID: 11350 RVA: 0x000B0261 File Offset: 0x000AE461
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000B026A File Offset: 0x000AE46A
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._resourceList != null)
				{
					this.Generate();
				}
				if (this._output != null)
				{
					this._output.Close();
				}
			}
			this._output = null;
			this._caseInsensitiveDups = null;
		}

		/// <summary>Allows users to close the resource file or stream, explicitly releasing resources.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization of the object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002C58 RID: 11352 RVA: 0x000B0261 File Offset: 0x000AE461
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Saves all resources to the output stream in the system default format.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error occurred during serialization of the object. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Resources.ResourceWriter" /> has been closed and its hash table is unavailable. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002C59 RID: 11353 RVA: 0x000B02A0 File Offset: 0x000AE4A0
		public void Generate()
		{
			if (this._resourceList == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The resource writer has already been closed and cannot be edited."));
			}
			BinaryWriter binaryWriter = new BinaryWriter(this._output, Encoding.UTF8);
			List<string> list = new List<string>();
			binaryWriter.Write(ResourceManager.MagicNumber);
			binaryWriter.Write(ResourceManager.HeaderVersionNumber);
			MemoryStream memoryStream = new MemoryStream(240);
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
			binaryWriter2.Write(MultitargetingHelpers.GetAssemblyQualifiedName(typeof(ResourceReader), this.typeConverter));
			binaryWriter2.Write(ResourceManager.ResSetTypeName);
			binaryWriter2.Flush();
			binaryWriter.Write((int)memoryStream.Length);
			binaryWriter.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			binaryWriter.Write(2);
			int num = this._resourceList.Count;
			if (this._preserializedData != null)
			{
				num += this._preserializedData.Count;
			}
			binaryWriter.Write(num);
			int[] array = new int[num];
			int[] array2 = new int[num];
			int num2 = 0;
			MemoryStream memoryStream2 = new MemoryStream(num * 40);
			BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream2, Encoding.Unicode);
			Stream stream = null;
			try
			{
				string tempFileName = Path.GetTempFileName();
				File.SetAttributes(tempFileName, FileAttributes.Temporary | FileAttributes.NotContentIndexed);
				stream = new FileStream(tempFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose | FileOptions.SequentialScan);
			}
			catch (UnauthorizedAccessException)
			{
				stream = new MemoryStream();
			}
			catch (IOException)
			{
				stream = new MemoryStream();
			}
			using (stream)
			{
				BinaryWriter binaryWriter4 = new BinaryWriter(stream, Encoding.UTF8);
				IFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
				SortedList sortedList = new SortedList(this._resourceList, FastResourceComparer.Default);
				if (this._preserializedData != null)
				{
					foreach (KeyValuePair<string, ResourceWriter.PrecannedResource> keyValuePair in this._preserializedData)
					{
						sortedList.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				IDictionaryEnumerator enumerator2 = sortedList.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					array[num2] = FastResourceComparer.HashFunction((string)enumerator2.Key);
					array2[num2++] = (int)binaryWriter3.Seek(0, SeekOrigin.Current);
					binaryWriter3.Write((string)enumerator2.Key);
					binaryWriter3.Write((int)binaryWriter4.Seek(0, SeekOrigin.Current));
					object value = enumerator2.Value;
					ResourceTypeCode resourceTypeCode = this.FindTypeCode(value, list);
					ResourceWriter.Write7BitEncodedInt(binaryWriter4, (int)resourceTypeCode);
					ResourceWriter.PrecannedResource precannedResource = value as ResourceWriter.PrecannedResource;
					if (precannedResource != null)
					{
						binaryWriter4.Write(precannedResource.Data);
					}
					else
					{
						this.WriteValue(resourceTypeCode, value, binaryWriter4, formatter);
					}
				}
				binaryWriter.Write(list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					binaryWriter.Write(list[i]);
				}
				Array.Sort<int, int>(array, array2);
				binaryWriter.Flush();
				int num3 = (int)binaryWriter.BaseStream.Position & 7;
				if (num3 > 0)
				{
					for (int j = 0; j < 8 - num3; j++)
					{
						binaryWriter.Write("PAD"[j % 3]);
					}
				}
				foreach (int num4 in array)
				{
					binaryWriter.Write(num4);
				}
				foreach (int num5 in array2)
				{
					binaryWriter.Write(num5);
				}
				binaryWriter.Flush();
				binaryWriter3.Flush();
				binaryWriter4.Flush();
				int num6 = (int)(binaryWriter.Seek(0, SeekOrigin.Current) + memoryStream2.Length);
				num6 += 4;
				binaryWriter.Write(num6);
				binaryWriter.Write(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length);
				binaryWriter3.Close();
				stream.Position = 0L;
				stream.CopyTo(binaryWriter.BaseStream);
				binaryWriter4.Close();
			}
			binaryWriter.Flush();
			this._resourceList = null;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000B06D0 File Offset: 0x000AE8D0
		private ResourceTypeCode FindTypeCode(object value, List<string> types)
		{
			if (value == null)
			{
				return ResourceTypeCode.Null;
			}
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return ResourceTypeCode.String;
			}
			if (type == typeof(int))
			{
				return ResourceTypeCode.Int32;
			}
			if (type == typeof(bool))
			{
				return ResourceTypeCode.Boolean;
			}
			if (type == typeof(char))
			{
				return ResourceTypeCode.Char;
			}
			if (type == typeof(byte))
			{
				return ResourceTypeCode.Byte;
			}
			if (type == typeof(sbyte))
			{
				return ResourceTypeCode.SByte;
			}
			if (type == typeof(short))
			{
				return ResourceTypeCode.Int16;
			}
			if (type == typeof(long))
			{
				return ResourceTypeCode.Int64;
			}
			if (type == typeof(ushort))
			{
				return ResourceTypeCode.UInt16;
			}
			if (type == typeof(uint))
			{
				return ResourceTypeCode.UInt32;
			}
			if (type == typeof(ulong))
			{
				return ResourceTypeCode.UInt64;
			}
			if (type == typeof(float))
			{
				return ResourceTypeCode.Single;
			}
			if (type == typeof(double))
			{
				return ResourceTypeCode.Double;
			}
			if (type == typeof(decimal))
			{
				return ResourceTypeCode.Decimal;
			}
			if (type == typeof(DateTime))
			{
				return ResourceTypeCode.DateTime;
			}
			if (type == typeof(TimeSpan))
			{
				return ResourceTypeCode.TimeSpan;
			}
			if (type == typeof(byte[]))
			{
				return ResourceTypeCode.ByteArray;
			}
			if (type == typeof(ResourceWriter.StreamWrapper))
			{
				return ResourceTypeCode.Stream;
			}
			string text;
			if (type == typeof(ResourceWriter.PrecannedResource))
			{
				text = ((ResourceWriter.PrecannedResource)value).TypeName;
				if (text.StartsWith("ResourceTypeCode.", StringComparison.Ordinal))
				{
					text = text.Substring(17);
					return (ResourceTypeCode)Enum.Parse(typeof(ResourceTypeCode), text);
				}
			}
			else
			{
				text = MultitargetingHelpers.GetAssemblyQualifiedName(type, this.typeConverter);
			}
			int num = types.IndexOf(text);
			if (num == -1)
			{
				num = types.Count;
				types.Add(text);
			}
			return num + ResourceTypeCode.StartOfUserTypes;
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000B08D4 File Offset: 0x000AEAD4
		private void WriteValue(ResourceTypeCode typeCode, object value, BinaryWriter writer, IFormatter objFormatter)
		{
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return;
			case ResourceTypeCode.String:
				writer.Write((string)value);
				return;
			case ResourceTypeCode.Boolean:
				writer.Write((bool)value);
				return;
			case ResourceTypeCode.Char:
				writer.Write((ushort)((char)value));
				return;
			case ResourceTypeCode.Byte:
				writer.Write((byte)value);
				return;
			case ResourceTypeCode.SByte:
				writer.Write((sbyte)value);
				return;
			case ResourceTypeCode.Int16:
				writer.Write((short)value);
				return;
			case ResourceTypeCode.UInt16:
				writer.Write((ushort)value);
				return;
			case ResourceTypeCode.Int32:
				writer.Write((int)value);
				return;
			case ResourceTypeCode.UInt32:
				writer.Write((uint)value);
				return;
			case ResourceTypeCode.Int64:
				writer.Write((long)value);
				return;
			case ResourceTypeCode.UInt64:
				writer.Write((ulong)value);
				return;
			case ResourceTypeCode.Single:
				writer.Write((float)value);
				return;
			case ResourceTypeCode.Double:
				writer.Write((double)value);
				return;
			case ResourceTypeCode.Decimal:
				writer.Write((decimal)value);
				return;
			case ResourceTypeCode.DateTime:
			{
				long num = ((DateTime)value).ToBinary();
				writer.Write(num);
				return;
			}
			case ResourceTypeCode.TimeSpan:
				writer.Write(((TimeSpan)value).Ticks);
				return;
			case ResourceTypeCode.ByteArray:
			{
				byte[] array = (byte[])value;
				writer.Write(array.Length);
				writer.Write(array, 0, array.Length);
				return;
			}
			case ResourceTypeCode.Stream:
			{
				ResourceWriter.StreamWrapper streamWrapper = (ResourceWriter.StreamWrapper)value;
				if (streamWrapper.m_stream.GetType() == typeof(MemoryStream))
				{
					MemoryStream memoryStream = (MemoryStream)streamWrapper.m_stream;
					if (memoryStream.Length > 2147483647L)
					{
						throw new ArgumentException(Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
					}
					int num2;
					int num3;
					memoryStream.InternalGetOriginAndLength(out num2, out num3);
					byte[] array2 = memoryStream.InternalGetBuffer();
					writer.Write(num3);
					writer.Write(array2, num2, num3);
					return;
				}
				else
				{
					Stream stream = streamWrapper.m_stream;
					if (stream.Length > 2147483647L)
					{
						throw new ArgumentException(Environment.GetResourceString("Stream length must be non-negative and less than 2^31 - 1 - origin."));
					}
					stream.Position = 0L;
					writer.Write((int)stream.Length);
					byte[] array3 = new byte[4096];
					int num4;
					while ((num4 = stream.Read(array3, 0, array3.Length)) != 0)
					{
						writer.Write(array3, 0, num4);
					}
					if (streamWrapper.m_closeAfterWrite)
					{
						stream.Close();
						return;
					}
					return;
				}
				break;
			}
			}
			objFormatter.Serialize(writer.BaseStream, value);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000B0B74 File Offset: 0x000AED74
		private static void Write7BitEncodedInt(BinaryWriter store, int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				store.Write((byte)(num | 128U));
			}
			store.Write((byte)num);
		}

		// Token: 0x04001680 RID: 5760
		private Func<Type, string> typeConverter;

		// Token: 0x04001681 RID: 5761
		private Dictionary<string, object> _resourceList;

		// Token: 0x04001682 RID: 5762
		internal Stream _output;

		// Token: 0x04001683 RID: 5763
		private Dictionary<string, object> _caseInsensitiveDups;

		// Token: 0x04001684 RID: 5764
		private Dictionary<string, ResourceWriter.PrecannedResource> _preserializedData;

		// Token: 0x020005D8 RID: 1496
		private class PrecannedResource
		{
			// Token: 0x04001685 RID: 5765
			internal string TypeName;

			// Token: 0x04001686 RID: 5766
			internal byte[] Data;
		}

		// Token: 0x020005D9 RID: 1497
		private class StreamWrapper
		{
			// Token: 0x04001687 RID: 5767
			internal Stream m_stream;

			// Token: 0x04001688 RID: 5768
			internal bool m_closeAfterWrite;
		}
	}
}
