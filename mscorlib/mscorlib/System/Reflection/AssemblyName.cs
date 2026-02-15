using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Mono;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	/// <summary>Describes an assembly's unique identity in full.</summary>
	// Token: 0x02000635 RID: 1589
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyName : ICloneable, ISerializable, IDeserializationCallback, _AssemblyName
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class.</summary>
		// Token: 0x06002F19 RID: 12057 RVA: 0x000B4707 File Offset: 0x000B2907
		public AssemblyName()
		{
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
		}

		// Token: 0x06002F1A RID: 12058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ParseAssemblyName(IntPtr name, out MonoAssemblyName aname, out bool is_version_definited, out bool is_token_defined);

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class with the specified display name.</summary>
		/// <param name="assemblyName">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is a zero length string. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The referenced assembly could not be found, or could not be loaded.</exception>
		// Token: 0x06002F1B RID: 12059 RVA: 0x000B4718 File Offset: 0x000B2918
		public unsafe AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length < 1)
			{
				throw new ArgumentException("assemblyName cannot have zero length.");
			}
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(assemblyName))
			{
				MonoAssemblyName monoAssemblyName;
				bool flag;
				bool flag2;
				if (!AssemblyName.ParseAssemblyName(safeStringMarshal.Value, out monoAssemblyName, out flag, out flag2))
				{
					throw new FileLoadException("The assembly name is invalid.");
				}
				try
				{
					this.FillName(&monoAssemblyName, null, flag, false, flag2, false);
				}
				finally
				{
					RuntimeMarshal.FreeAssemblyName(ref monoAssemblyName, false);
				}
			}
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000B47B8 File Offset: 0x000B29B8
		internal AssemblyName(SerializationInfo si, StreamingContext sc)
		{
			this.name = si.GetString("_Name");
			this.codebase = si.GetString("_CodeBase");
			this.version = (Version)si.GetValue("_Version", typeof(Version));
			this.publicKey = (byte[])si.GetValue("_PublicKey", typeof(byte[]));
			this.keyToken = (byte[])si.GetValue("_PublicKeyToken", typeof(byte[]));
			this.hashalg = (AssemblyHashAlgorithm)si.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm));
			this.keypair = (StrongNameKeyPair)si.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this.versioncompat = (AssemblyVersionCompatibility)si.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility));
			this.flags = (AssemblyNameFlags)si.GetValue("_Flags", typeof(AssemblyNameFlags));
			int @int = si.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this.cultureinfo = new CultureInfo(@int);
			}
		}

		/// <summary>Gets or sets the simple name of the assembly. This is usually, but not necessarily, the file name of the manifest file of the assembly, minus its extension.</summary>
		/// <returns>The simple name of the assembly.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000B48E9 File Offset: 0x000B2AE9
		// (set) Token: 0x06002F1E RID: 12062 RVA: 0x000B48F1 File Offset: 0x000B2AF1
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the location of the assembly as a URL.</summary>
		/// <returns>A string that is the URL location of the assembly. </returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000B48FA File Offset: 0x000B2AFA
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x000B4902 File Offset: 0x000B2B02
		public string CodeBase
		{
			get
			{
				return this.codebase;
			}
			set
			{
				this.codebase = value;
			}
		}

		/// <summary>Gets or sets the culture supported by the assembly.</summary>
		/// <returns>An object that represents the culture supported by the assembly.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000B490B File Offset: 0x000B2B0B
		// (set) Token: 0x06002F22 RID: 12066 RVA: 0x000B4913 File Offset: 0x000B2B13
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureinfo;
			}
			set
			{
				this.cultureinfo = value;
			}
		}

		/// <summary>Gets or sets the attributes of the assembly.</summary>
		/// <returns>A value that represents the attributes of the assembly.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000B491C File Offset: 0x000B2B1C
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000B4924 File Offset: 0x000B2B24
		public AssemblyNameFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		/// <summary>Gets the full name of the assembly, also known as the display name.</summary>
		/// <returns>A string that is the full name of the assembly, also known as the display name.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000B4930 File Offset: 0x000B2B30
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (char.IsWhiteSpace(this.name[0]))
				{
					stringBuilder.Append("\"" + this.name + "\"");
				}
				else
				{
					stringBuilder.Append(this.name);
				}
				if (this.Version != null)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(this.Version.ToString());
				}
				if (this.cultureinfo != null)
				{
					stringBuilder.Append(", Culture=");
					if (this.cultureinfo.LCID == CultureInfo.InvariantCulture.LCID)
					{
						stringBuilder.Append("neutral");
					}
					else
					{
						stringBuilder.Append(this.cultureinfo.Name);
					}
				}
				byte[] array = this.InternalGetPublicKeyToken();
				if (array != null)
				{
					if (array.Length == 0)
					{
						stringBuilder.Append(", PublicKeyToken=null");
					}
					else
					{
						stringBuilder.Append(", PublicKeyToken=");
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
					}
				}
				if ((this.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					stringBuilder.Append(", Retargetable=Yes");
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets the public and private cryptographic key pair that is used to create a strong name signature for the assembly.</summary>
		/// <returns>The public and private cryptographic key pair to be used to create a strong name for the assembly.</returns>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000B4A74 File Offset: 0x000B2C74
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this.keypair;
			}
		}

		/// <summary>Gets or sets the major, minor, build, and revision numbers of the assembly.</summary>
		/// <returns>An object that represents the major, minor, build, and revision numbers of the assembly.</returns>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000B4A7C File Offset: 0x000B2C7C
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x000B4A84 File Offset: 0x000B2C84
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				if (value == null)
				{
					this.major = (this.minor = (this.build = (this.revision = 0)));
					return;
				}
				this.major = value.Major;
				this.minor = value.Minor;
				this.build = value.Build;
				this.revision = value.Revision;
			}
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name cannot be determined.</returns>
		// Token: 0x06002F29 RID: 12073 RVA: 0x000B4AF4 File Offset: 0x000B2CF4
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		/// <summary>Gets the public key of the assembly.</summary>
		/// <returns>A byte array that contains the public key of the assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">A public key was provided (for example, by using the <see cref="M:System.Reflection.AssemblyName.SetPublicKey(System.Byte[])" /> method), but no public key token was provided. </exception>
		// Token: 0x06002F2A RID: 12074 RVA: 0x000B4B13 File Offset: 0x000B2D13
		public byte[] GetPublicKey()
		{
			return this.publicKey;
		}

		/// <summary>Gets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <returns>A byte array that contains the public key token.</returns>
		// Token: 0x06002F2B RID: 12075 RVA: 0x000B4B1C File Offset: 0x000B2D1C
		public byte[] GetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return EmptyArray<byte>.Value;
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			this.keyToken = this.ComputePublicKeyToken();
			return this.keyToken;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000B4B78 File Offset: 0x000B2D78
		private bool IsPublicKeyValid
		{
			get
			{
				if (this.publicKey.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < this.publicKey.Length)
					{
						num += (int)this.publicKey[i++];
					}
					if (num == 4)
					{
						return true;
					}
				}
				byte b = this.publicKey[0];
				if (b != 0)
				{
					if (b == 6)
					{
						return CryptoConvert.TryImportCapiPublicKeyBlob(this.publicKey, 0);
					}
					if (b != 7)
					{
					}
				}
				else if (this.publicKey.Length > 12 && this.publicKey[12] == 6)
				{
					return CryptoConvert.TryImportCapiPublicKeyBlob(this.publicKey, 12);
				}
				return false;
			}
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000B4C04 File Offset: 0x000B2E04
		private byte[] InternalGetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return EmptyArray<byte>.Value;
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			return this.ComputePublicKeyToken();
		}

		// Token: 0x06002F2E RID: 12078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void get_public_token(byte* token, byte* pubkey, int len);

		// Token: 0x06002F2F RID: 12079 RVA: 0x000B4C54 File Offset: 0x000B2E54
		private unsafe byte[] ComputePublicKeyToken()
		{
			byte[] array2;
			byte[] array = (array2 = new byte[8]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte[] array3;
			byte* ptr2;
			if ((array3 = this.publicKey) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			AssemblyName.get_public_token(ptr, ptr2, this.publicKey.Length);
			array3 = null;
			array2 = null;
			return array;
		}

		/// <summary>Sets the public key identifying the assembly.</summary>
		/// <param name="publicKey">A byte array containing the public key of the assembly. </param>
		// Token: 0x06002F30 RID: 12080 RVA: 0x000B4CAF File Offset: 0x000B2EAF
		public void SetPublicKey(byte[] publicKey)
		{
			if (publicKey == null)
			{
				this.flags ^= AssemblyNameFlags.PublicKey;
			}
			else
			{
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			this.publicKey = publicKey;
		}

		/// <summary>Sets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <param name="publicKeyToken">A byte array containing the public key token of the assembly. </param>
		// Token: 0x06002F31 RID: 12081 RVA: 0x000B4CD9 File Offset: 0x000B2ED9
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this.keyToken = publicKeyToken;
		}

		/// <summary>Gets serialization information with all the data needed to recreate an instance of this AssemblyName.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002F32 RID: 12082 RVA: 0x000B4CE4 File Offset: 0x000B2EE4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this.name);
			info.AddValue("_PublicKey", this.publicKey);
			info.AddValue("_PublicKeyToken", this.keyToken);
			info.AddValue("_CultureInfo", (this.cultureinfo != null) ? this.cultureinfo.LCID : (-1));
			info.AddValue("_CodeBase", this.codebase);
			info.AddValue("_Version", this.Version);
			info.AddValue("_HashAlgorithm", this.hashalg);
			info.AddValue("_HashAlgorithmForControl", AssemblyHashAlgorithm.None);
			info.AddValue("_StrongNameKeyPair", this.keypair);
			info.AddValue("_VersionCompatibility", this.versioncompat);
			info.AddValue("_Flags", this.flags);
			info.AddValue("_HashForControl", null);
		}

		/// <summary>Makes a copy of this <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>An object that is a copy of this <see cref="T:System.Reflection.AssemblyName" /> object.</returns>
		// Token: 0x06002F33 RID: 12083 RVA: 0x000B4DE8 File Offset: 0x000B2FE8
		public object Clone()
		{
			return new AssemblyName
			{
				name = this.name,
				codebase = this.codebase,
				major = this.major,
				minor = this.minor,
				build = this.build,
				revision = this.revision,
				version = this.version,
				cultureinfo = this.cultureinfo,
				flags = this.flags,
				hashalg = this.hashalg,
				keypair = this.keypair,
				publicKey = this.publicKey,
				keyToken = this.keyToken,
				versioncompat = this.versioncompat,
				processor_architecture = this.processor_architecture
			};
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06002F34 RID: 12084 RVA: 0x000B4EAE File Offset: 0x000B30AE
		public void OnDeserialization(object sender)
		{
			this.Version = this.version;
		}

		/// <summary>Gets or sets a value that indicates what type of content the assembly contains.</summary>
		/// <returns>A value that indicates what type of content the assembly contains.</returns>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000B4EBC File Offset: 0x000B30BC
		[ComVisible(false)]
		public AssemblyContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06002F36 RID: 12086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern MonoAssemblyName* GetNativeName(IntPtr assembly_ptr);

		// Token: 0x06002F37 RID: 12087 RVA: 0x000B4EC4 File Offset: 0x000B30C4
		internal unsafe void FillName(MonoAssemblyName* native, string codeBase, bool addVersion, bool addPublickey, bool defaultToken, bool assemblyRef)
		{
			this.name = RuntimeMarshal.PtrToUtf8String(native->name);
			this.major = (int)native->major;
			this.minor = (int)native->minor;
			this.build = (int)native->build;
			this.revision = (int)native->revision;
			this.flags = (AssemblyNameFlags)native->flags;
			this.hashalg = (AssemblyHashAlgorithm)native->hash_alg;
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
			this.processor_architecture = (ProcessorArchitecture)native->arch;
			if (addVersion)
			{
				this.version = new Version(this.major, this.minor, this.build, this.revision);
			}
			this.codebase = codeBase;
			if (native->culture != IntPtr.Zero)
			{
				this.cultureinfo = CultureInfo.CreateCulture(RuntimeMarshal.PtrToUtf8String(native->culture), assemblyRef);
			}
			if (native->public_key != IntPtr.Zero)
			{
				this.publicKey = RuntimeMarshal.DecodeBlobArray(native->public_key);
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			else if (addPublickey)
			{
				this.publicKey = EmptyArray<byte>.Value;
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			if (native->public_key_token.FixedElementField != 0)
			{
				byte[] array = new byte[8];
				int i = 0;
				int num = 0;
				while (i < 8)
				{
					array[i] = (byte)(RuntimeMarshal.AsciHexDigitValue((int)(*((ref native->public_key_token.FixedElementField) + num++))) << 4);
					byte[] array2 = array;
					int num2 = i;
					array2[num2] |= (byte)RuntimeMarshal.AsciHexDigitValue((int)(*((ref native->public_key_token.FixedElementField) + num++)));
					i++;
				}
				this.keyToken = array;
				return;
			}
			if (defaultToken)
			{
				this.keyToken = EmptyArray<byte>.Value;
			}
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000B5060 File Offset: 0x000B3260
		internal unsafe static AssemblyName Create(Assembly assembly, bool fillCodebase)
		{
			AssemblyName assemblyName = new AssemblyName();
			MonoAssemblyName* nativeName = AssemblyName.GetNativeName(assembly.MonoAssembly);
			assemblyName.FillName(nativeName, fillCodebase ? assembly.CodeBase : null, true, true, true, false);
			return assemblyName;
		}

		// Token: 0x0400182F RID: 6191
		private string name;

		// Token: 0x04001830 RID: 6192
		private string codebase;

		// Token: 0x04001831 RID: 6193
		private int major;

		// Token: 0x04001832 RID: 6194
		private int minor;

		// Token: 0x04001833 RID: 6195
		private int build;

		// Token: 0x04001834 RID: 6196
		private int revision;

		// Token: 0x04001835 RID: 6197
		private CultureInfo cultureinfo;

		// Token: 0x04001836 RID: 6198
		private AssemblyNameFlags flags;

		// Token: 0x04001837 RID: 6199
		private AssemblyHashAlgorithm hashalg;

		// Token: 0x04001838 RID: 6200
		private StrongNameKeyPair keypair;

		// Token: 0x04001839 RID: 6201
		private byte[] publicKey;

		// Token: 0x0400183A RID: 6202
		private byte[] keyToken;

		// Token: 0x0400183B RID: 6203
		private AssemblyVersionCompatibility versioncompat;

		// Token: 0x0400183C RID: 6204
		private Version version;

		// Token: 0x0400183D RID: 6205
		private ProcessorArchitecture processor_architecture;

		// Token: 0x0400183E RID: 6206
		private AssemblyContentType contentType;
	}
}
