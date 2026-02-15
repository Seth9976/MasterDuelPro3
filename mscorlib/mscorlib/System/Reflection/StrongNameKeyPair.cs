using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	/// <summary>Encapsulates access to a public or private key pair used to sign strong name assemblies.</summary>
	// Token: 0x0200064C RID: 1612
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06003099 RID: 12441 RVA: 0x000B7D28 File Offset: 0x000B5F28
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
			this._keyPairContainer = info.GetString("_keyPairContainer");
			this._keyPairExported = info.GetBoolean("_keyPairExported");
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data required to reinstantiate the current <see cref="T:System.Reflection.StrongNameKeyPair" /> object.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600309A RID: 12442 RVA: 0x000B7DA0 File Offset: 0x000B5FA0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_publicKey", this._publicKey, typeof(byte[]));
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray, typeof(byte[]));
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback.</param>
		// Token: 0x0600309B RID: 12443 RVA: 0x00002C89 File Offset: 0x00000E89
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000B7E08 File Offset: 0x000B6008
		private RSA GetRSA()
		{
			if (this._rsa != null)
			{
				return this._rsa;
			}
			if (this._keyPairArray != null)
			{
				try
				{
					this._rsa = CryptoConvert.FromCapiKeyBlob(this._keyPairArray);
					goto IL_005A;
				}
				catch
				{
					this._keyPairArray = null;
					goto IL_005A;
				}
			}
			if (this._keyPairContainer != null)
			{
				this._rsa = new RSACryptoServiceProvider(new CspParameters
				{
					KeyContainerName = this._keyPairContainer
				});
			}
			IL_005A:
			return this._rsa;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000B7E88 File Offset: 0x000B6088
		internal StrongName StrongName()
		{
			RSA rsa = this.GetRSA();
			if (rsa != null)
			{
				return new StrongName(rsa);
			}
			if (this._publicKey != null)
			{
				return new StrongName(this._publicKey);
			}
			return null;
		}

		// Token: 0x04001893 RID: 6291
		private byte[] _publicKey;

		// Token: 0x04001894 RID: 6292
		private string _keyPairContainer;

		// Token: 0x04001895 RID: 6293
		private bool _keyPairExported;

		// Token: 0x04001896 RID: 6294
		private byte[] _keyPairArray;

		// Token: 0x04001897 RID: 6295
		[NonSerialized]
		private RSA _rsa;
	}
}
