using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents information about an operating system, such as the version and platform identifier. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017C RID: 380
	[Serializable]
	public sealed class OperatingSystem : ISerializable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OperatingSystem" /> class, using the specified platform identifier value and version object.</summary>
		/// <param name="platform">One of the <see cref="T:System.PlatformID" /> values that indicates the operating system platform. </param>
		/// <param name="version">A <see cref="T:System.Version" /> object that indicates the version of the operating system. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="platform" /> is not a <see cref="T:System.PlatformID" /> enumeration value.</exception>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00039F58 File Offset: 0x00038158
		public OperatingSystem(PlatformID platform, Version version)
			: this(platform, version, null)
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00039F64 File Offset: 0x00038164
		internal OperatingSystem(PlatformID platform, Version version, string servicePack)
		{
			if (platform < PlatformID.Win32S || platform > PlatformID.MacOSX)
			{
				throw new ArgumentOutOfRangeException("platform", platform, SR.Format("Illegal enum value: {0}.", platform));
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = version;
			this._servicePack = servicePack;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize this instance.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x000145B3 File Offset: 0x000127B3
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.PlatformID" /> enumeration value that identifies the operating system platform.</summary>
		/// <returns>One of the <see cref="T:System.PlatformID" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00039FC9 File Offset: 0x000381C9
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		/// <summary>Gets a <see cref="T:System.Version" /> object that identifies the operating system.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that describes the major version, minor version, build, and revision numbers for the operating system.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00039FD1 File Offset: 0x000381D1
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>Creates an <see cref="T:System.OperatingSystem" /> object that is identical to this instance.</summary>
		/// <returns>An <see cref="T:System.OperatingSystem" /> object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DAC RID: 3500 RVA: 0x00039FD9 File Offset: 0x000381D9
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version, this._servicePack);
		}

		/// <summary>Converts the value of this <see cref="T:System.OperatingSystem" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the values returned by the <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" />, and <see cref="P:System.OperatingSystem.ServicePack" /> properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DAD RID: 3501 RVA: 0x00039FF2 File Offset: 0x000381F2
		public override string ToString()
		{
			return this.VersionString;
		}

		/// <summary>Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system. </summary>
		/// <returns>The string representation of the values returned by the <see cref="P:System.OperatingSystem.Platform" />, <see cref="P:System.OperatingSystem.Version" />, and <see cref="P:System.OperatingSystem.ServicePack" /> properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00039FFC File Offset: 0x000381FC
		public string VersionString
		{
			get
			{
				if (this._versionString == null)
				{
					string text;
					switch (this._platform)
					{
					case PlatformID.Win32S:
						text = "Microsoft Win32S ";
						break;
					case PlatformID.Win32Windows:
						text = ((this._version.Major > 4 || (this._version.Major == 4 && this._version.Minor > 0)) ? "Microsoft Windows 98 " : "Microsoft Windows 95 ");
						break;
					case PlatformID.Win32NT:
						text = "Microsoft Windows NT ";
						break;
					case PlatformID.WinCE:
						text = "Microsoft Windows CE ";
						break;
					case PlatformID.Unix:
						text = "Unix ";
						break;
					case PlatformID.Xbox:
						text = "Xbox ";
						break;
					case PlatformID.MacOSX:
						text = "Mac OS X ";
						break;
					default:
						text = "<unknown> ";
						break;
					}
					this._versionString = (string.IsNullOrEmpty(this._servicePack) ? (text + this._version.ToString()) : (text + this._version.ToString(3) + " " + this._servicePack));
				}
				return this._versionString;
			}
		}

		// Token: 0x040005A1 RID: 1441
		private readonly Version _version;

		// Token: 0x040005A2 RID: 1442
		private readonly PlatformID _platform;

		// Token: 0x040005A3 RID: 1443
		private readonly string _servicePack;

		// Token: 0x040005A4 RID: 1444
		private string _versionString;
	}
}
