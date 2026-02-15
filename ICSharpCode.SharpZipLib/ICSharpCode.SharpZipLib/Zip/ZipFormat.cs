using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200003D RID: 61
	internal static class ZipFormat
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x000089E0 File Offset: 0x00006BE0
		internal static int WriteLocalHeader(Stream stream, ZipEntry entry, out EntryPatchData patchData, bool headerInfoAvailable, bool patchEntryHeader, long streamOffset, StringCodec stringCodec)
		{
			patchData = default(EntryPatchData);
			stream.WriteLEInt(67324752);
			stream.WriteLEShort(entry.Version);
			stream.WriteLEShort(entry.Flags);
			stream.WriteLEShort((int)((byte)entry.CompressionMethodForHeader));
			stream.WriteLEInt((int)entry.DosTime);
			if (headerInfoAvailable)
			{
				stream.WriteLEInt((int)entry.Crc);
				if (entry.LocalHeaderRequiresZip64)
				{
					stream.WriteLEInt(-1);
					stream.WriteLEInt(-1);
				}
				else
				{
					stream.WriteLEInt((int)entry.CompressedSize + entry.EncryptionOverheadSize);
					stream.WriteLEInt((int)entry.Size);
				}
			}
			else
			{
				if (patchEntryHeader)
				{
					patchData.CrcPatchOffset = streamOffset + stream.Position;
				}
				stream.WriteLEInt(0);
				if (patchEntryHeader)
				{
					patchData.SizePatchOffset = streamOffset + stream.Position;
				}
				if (entry.LocalHeaderRequiresZip64 && patchEntryHeader)
				{
					stream.WriteLEInt(-1);
					stream.WriteLEInt(-1);
				}
				else
				{
					stream.WriteLEInt(0);
					stream.WriteLEInt(0);
				}
			}
			byte[] bytes = stringCodec.ZipEncoding(entry.IsUnicodeText).GetBytes(entry.Name);
			if (bytes.Length > 65535)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			if (entry.LocalHeaderRequiresZip64)
			{
				zipExtraData.StartNewEntry();
				if (headerInfoAvailable)
				{
					zipExtraData.AddLeLong(entry.Size);
					zipExtraData.AddLeLong(entry.CompressedSize + (long)entry.EncryptionOverheadSize);
				}
				else
				{
					zipExtraData.AddLeLong(0L);
					zipExtraData.AddLeLong(0L);
				}
				zipExtraData.AddNewEntry(1);
				if (!zipExtraData.Find(1))
				{
					throw new ZipException("Internal error cant find extra data");
				}
				patchData.SizePatchOffset = (long)zipExtraData.CurrentReadIndex;
			}
			else
			{
				zipExtraData.Delete(1);
			}
			if (entry.AESKeySize > 0)
			{
				ZipFormat.AddExtraDataAES(entry, zipExtraData);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			stream.WriteLEShort(bytes.Length);
			stream.WriteLEShort(entryData.Length);
			if (bytes.Length != 0)
			{
				stream.Write(bytes, 0, bytes.Length);
			}
			if (entry.LocalHeaderRequiresZip64 && patchEntryHeader)
			{
				patchData.SizePatchOffset += streamOffset + stream.Position;
			}
			if (entryData.Length != 0)
			{
				stream.Write(entryData, 0, entryData.Length);
			}
			return 30 + bytes.Length + entryData.Length;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008BF4 File Offset: 0x00006DF4
		internal static long LocateBlockWithSignature(Stream stream, int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long num = endLocation - (long)minimumBlockSize;
			if (num < 0L)
			{
				return -1L;
			}
			long num2 = Math.Max(num - (long)maximumVariableData, 0L);
			while (num >= num2)
			{
				long num3 = num;
				num = num3 - 1L;
				stream.Seek(num3, SeekOrigin.Begin);
				if (stream.ReadLEInt() == signature)
				{
					return stream.Position;
				}
			}
			return -1L;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008C40 File Offset: 0x00006E40
		public static async Task WriteZip64EndOfCentralDirectoryAsync(Stream stream, long noOfEntries, long sizeEntries, long centralDirOffset, CancellationToken cancellationToken)
		{
			await stream.WriteProcToStreamAsync(delegate(Stream s)
			{
				ZipFormat.WriteZip64EndOfCentralDirectory(s, noOfEntries, sizeEntries, centralDirOffset);
			}, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008CA4 File Offset: 0x00006EA4
		internal static void WriteZip64EndOfCentralDirectory(Stream stream, long noOfEntries, long sizeEntries, long centralDirOffset)
		{
			long num = centralDirOffset + sizeEntries;
			stream.WriteLEInt(101075792);
			stream.WriteLELong(44L);
			stream.WriteLEShort(51);
			stream.WriteLEShort(45);
			stream.WriteLEInt(0);
			stream.WriteLEInt(0);
			stream.WriteLELong(noOfEntries);
			stream.WriteLELong(noOfEntries);
			stream.WriteLELong(sizeEntries);
			stream.WriteLELong(centralDirOffset);
			stream.WriteLEInt(117853008);
			stream.WriteLEInt(0);
			stream.WriteLELong(num);
			stream.WriteLEInt(1);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008D24 File Offset: 0x00006F24
		public static async Task WriteEndOfCentralDirectoryAsync(Stream stream, long noOfEntries, long sizeEntries, long start, byte[] comment, CancellationToken cancellationToken)
		{
			await stream.WriteProcToStreamAsync(delegate(Stream s)
			{
				ZipFormat.WriteEndOfCentralDirectory(s, noOfEntries, sizeEntries, start, comment);
			}, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008D94 File Offset: 0x00006F94
		internal static void WriteEndOfCentralDirectory(Stream stream, long noOfEntries, long sizeEntries, long start, byte[] comment)
		{
			if (noOfEntries >= 65535L || start >= (long)((ulong)(-1)) || sizeEntries >= (long)((ulong)(-1)))
			{
				ZipFormat.WriteZip64EndOfCentralDirectory(stream, noOfEntries, sizeEntries, start);
			}
			stream.WriteLEInt(101010256);
			stream.WriteLEShort(0);
			stream.WriteLEShort(0);
			if (noOfEntries >= 65535L)
			{
				stream.WriteLEUshort(ushort.MaxValue);
				stream.WriteLEUshort(ushort.MaxValue);
			}
			else
			{
				stream.WriteLEShort((int)((short)noOfEntries));
				stream.WriteLEShort((int)((short)noOfEntries));
			}
			if (sizeEntries >= (long)((ulong)(-1)))
			{
				stream.WriteLEUint(uint.MaxValue);
			}
			else
			{
				stream.WriteLEInt((int)sizeEntries);
			}
			if (start >= (long)((ulong)(-1)))
			{
				stream.WriteLEUint(uint.MaxValue);
			}
			else
			{
				stream.WriteLEInt((int)start);
			}
			int num = ((comment != null) ? comment.Length : 0);
			if (num > 65535)
			{
				throw new ZipException(string.Format("Comment length ({0}) is larger than 64K", num));
			}
			stream.WriteLEShort(num);
			if (num > 0)
			{
				stream.Write(comment, 0, num);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008E74 File Offset: 0x00007074
		internal static int WriteDataDescriptor(Stream stream, ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			int num = 0;
			if ((entry.Flags & 8) != 0)
			{
				stream.WriteLEInt(134695760);
				stream.WriteLEInt((int)entry.Crc);
				num += 8;
				if (entry.LocalHeaderRequiresZip64)
				{
					stream.WriteLELong(entry.CompressedSize);
					stream.WriteLELong(entry.Size);
					num += 16;
				}
				else
				{
					stream.WriteLEInt((int)entry.CompressedSize);
					stream.WriteLEInt((int)entry.Size);
					num += 8;
				}
			}
			return num;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008F00 File Offset: 0x00007100
		internal static void ReadDataDescriptor(Stream stream, bool zip64, DescriptorData data)
		{
			if (stream.ReadLEInt() != 134695760)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			data.Crc = (long)stream.ReadLEInt();
			if (zip64)
			{
				data.CompressedSize = stream.ReadLELong();
				data.Size = stream.ReadLELong();
				return;
			}
			data.CompressedSize = (long)stream.ReadLEInt();
			data.Size = (long)stream.ReadLEInt();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008F68 File Offset: 0x00007168
		internal static int WriteEndEntry(Stream stream, ZipEntry entry, StringCodec stringCodec)
		{
			stream.WriteLEInt(33639248);
			stream.WriteLEShort((entry.HostSystem << 8) | entry.VersionMadeBy);
			stream.WriteLEShort(entry.Version);
			stream.WriteLEShort(entry.Flags);
			stream.WriteLEShort((int)((short)entry.CompressionMethodForHeader));
			stream.WriteLEInt((int)entry.DosTime);
			stream.WriteLEInt((int)entry.Crc);
			if (entry.IsZip64Forced() || entry.CompressedSize >= (long)((ulong)(-1)))
			{
				stream.WriteLEInt(-1);
			}
			else
			{
				stream.WriteLEInt((int)entry.CompressedSize);
			}
			if (entry.IsZip64Forced() || entry.Size >= (long)((ulong)(-1)))
			{
				stream.WriteLEInt(-1);
			}
			else
			{
				stream.WriteLEInt((int)entry.Size);
			}
			byte[] bytes = stringCodec.ZipOutputEncoding.GetBytes(entry.Name);
			if (bytes.Length > 65535)
			{
				throw new ZipException("Name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			if (entry.CentralHeaderRequiresZip64)
			{
				zipExtraData.StartNewEntry();
				if (entry.IsZip64Forced() || entry.Size >= (long)((ulong)(-1)))
				{
					zipExtraData.AddLeLong(entry.Size);
				}
				if (entry.IsZip64Forced() || entry.CompressedSize >= (long)((ulong)(-1)))
				{
					zipExtraData.AddLeLong(entry.CompressedSize);
				}
				if (entry.Offset >= (long)((ulong)(-1)))
				{
					zipExtraData.AddLeLong(entry.Offset);
				}
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			if (entry.AESKeySize > 0)
			{
				ZipFormat.AddExtraDataAES(entry, zipExtraData);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			byte[] array = ((entry.Comment != null) ? stringCodec.ZipOutputEncoding.GetBytes(entry.Comment) : Empty.Array<byte>());
			if (array.Length > 65535)
			{
				throw new ZipException("Comment too long.");
			}
			stream.WriteLEShort(bytes.Length);
			stream.WriteLEShort(entryData.Length);
			stream.WriteLEShort(array.Length);
			stream.WriteLEShort(0);
			stream.WriteLEShort(0);
			if (entry.ExternalFileAttributes != -1)
			{
				stream.WriteLEInt(entry.ExternalFileAttributes);
			}
			else if (entry.IsDirectory)
			{
				stream.WriteLEInt(16);
			}
			else
			{
				stream.WriteLEInt(0);
			}
			if (entry.Offset >= (long)((ulong)(-1)))
			{
				stream.WriteLEInt(-1);
			}
			else
			{
				stream.WriteLEInt((int)entry.Offset);
			}
			if (bytes.Length != 0)
			{
				stream.Write(bytes, 0, bytes.Length);
			}
			if (entryData.Length != 0)
			{
				stream.Write(entryData, 0, entryData.Length);
			}
			if (array.Length != 0)
			{
				stream.Write(array, 0, array.Length);
			}
			return 46 + bytes.Length + entryData.Length + array.Length;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000091C9 File Offset: 0x000073C9
		internal static void AddExtraDataAES(ZipEntry entry, ZipExtraData extraData)
		{
			extraData.StartNewEntry();
			extraData.AddLeShort(2);
			extraData.AddLeShort(17729);
			extraData.AddData(entry.AESEncryptionStrength);
			extraData.AddLeShort((int)entry.CompressionMethod);
			extraData.AddNewEntry(39169);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009208 File Offset: 0x00007408
		internal static async Task PatchLocalHeaderAsync(Stream stream, ZipEntry entry, EntryPatchData patchData, CancellationToken ct)
		{
			long initialPos = stream.Position;
			stream.Seek(patchData.CrcPatchOffset, SeekOrigin.Begin);
			await stream.WriteLEIntAsync((int)entry.Crc, ct).ConfigureAwait(false);
			if (entry.LocalHeaderRequiresZip64)
			{
				if (patchData.SizePatchOffset == -1L)
				{
					throw new ZipException("Entry requires zip64 but this has been turned off");
				}
				stream.Seek(patchData.SizePatchOffset, SeekOrigin.Begin);
				await stream.WriteLELongAsync(entry.Size, ct).ConfigureAwait(false);
				await stream.WriteLELongAsync(entry.CompressedSize, ct).ConfigureAwait(false);
			}
			else
			{
				await stream.WriteLEIntAsync((int)entry.CompressedSize, ct).ConfigureAwait(false);
				await stream.WriteLEIntAsync((int)entry.Size, ct).ConfigureAwait(false);
			}
			stream.Seek(initialPos, SeekOrigin.Begin);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009264 File Offset: 0x00007464
		internal static void PatchLocalHeaderSync(Stream stream, ZipEntry entry, EntryPatchData patchData)
		{
			long position = stream.Position;
			stream.Seek(patchData.CrcPatchOffset, SeekOrigin.Begin);
			stream.WriteLEInt((int)entry.Crc);
			if (entry.LocalHeaderRequiresZip64)
			{
				if (patchData.SizePatchOffset == -1L)
				{
					throw new ZipException("Entry requires zip64 but this has been turned off");
				}
				stream.Seek(patchData.SizePatchOffset, SeekOrigin.Begin);
				stream.WriteLELong(entry.Size);
				stream.WriteLELong(entry.CompressedSize);
			}
			else
			{
				stream.WriteLEInt((int)entry.CompressedSize);
				stream.WriteLEInt((int)entry.Size);
			}
			stream.Seek(position, SeekOrigin.Begin);
		}
	}
}
