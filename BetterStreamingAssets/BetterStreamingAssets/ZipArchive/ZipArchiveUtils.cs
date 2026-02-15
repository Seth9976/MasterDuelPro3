using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x02000013 RID: 19
	public static class ZipArchiveUtils
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003330 File Offset: 0x00001530
		public static void ReadEndOfCentralDirectory(Stream stream, BinaryReader reader, out long expectedNumberOfEntries, out long centralDirectoryStart)
		{
			try
			{
				stream.Seek(-18L, SeekOrigin.End);
				if (!ZipHelper.SeekBackwardsToSignature(stream, 101010256U))
				{
					throw new ZipArchiveException("SignatureConstant");
				}
				long eocdStart = stream.Position;
				ZipEndOfCentralDirectoryBlock eocd;
				ZipEndOfCentralDirectoryBlock.TryReadBlock(reader, out eocd);
				if (eocd.NumberOfThisDisk != eocd.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
				{
					throw new ZipArchiveException("SplitSpanned");
				}
				centralDirectoryStart = (long)((ulong)eocd.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber);
				if (eocd.NumberOfEntriesInTheCentralDirectory != eocd.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
				{
					throw new ZipArchiveException("SplitSpanned");
				}
				expectedNumberOfEntries = (long)((ulong)eocd.NumberOfEntriesInTheCentralDirectory);
				if (eocd.NumberOfThisDisk == 65535 || eocd.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == 4294967295U || eocd.NumberOfEntriesInTheCentralDirectory == 65535)
				{
					stream.Seek(eocdStart - 16L, SeekOrigin.Begin);
					if (ZipHelper.SeekBackwardsToSignature(stream, 117853008U))
					{
						Zip64EndOfCentralDirectoryLocator locator;
						Zip64EndOfCentralDirectoryLocator.TryReadBlock(reader, out locator);
						if (locator.OffsetOfZip64EOCD > 9223372036854775807UL)
						{
							throw new ZipArchiveException("FieldTooBigOffsetToZip64EOCD");
						}
						long zip64EOCDOffset = (long)locator.OffsetOfZip64EOCD;
						stream.Seek(zip64EOCDOffset, SeekOrigin.Begin);
						Zip64EndOfCentralDirectoryRecord record;
						if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(reader, out record))
						{
							throw new ZipArchiveException("Zip64EOCDNotWhereExpected");
						}
						if (record.NumberOfEntriesTotal > 9223372036854775807UL)
						{
							throw new ZipArchiveException("FieldTooBigNumEntries");
						}
						if (record.OffsetOfCentralDirectory > 9223372036854775807UL)
						{
							throw new ZipArchiveException("FieldTooBigOffsetToCD");
						}
						if (record.NumberOfEntriesTotal != record.NumberOfEntriesOnThisDisk)
						{
							throw new ZipArchiveException("SplitSpanned");
						}
						expectedNumberOfEntries = (long)record.NumberOfEntriesTotal;
						centralDirectoryStart = (long)record.OffsetOfCentralDirectory;
					}
				}
				if (centralDirectoryStart > stream.Length)
				{
					throw new ZipArchiveException("FieldTooBigOffsetToCD");
				}
			}
			catch (EndOfStreamException ex)
			{
				throw new ZipArchiveException("CDCorrupt", ex);
			}
			catch (IOException ex2)
			{
				throw new ZipArchiveException("CDCorrupt", ex2);
			}
		}
	}
}
