using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Provides evidence about the hash value for an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000343 RID: 835
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : EvidenceBase, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Hash" /> class.</summary>
		/// <param name="assembly">The assembly for which to compute the hash value. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The hash cannot be generated.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is not a run-time <see cref="T:System.Reflection.Assembly" /> object.</exception>
		// Token: 0x06001D77 RID: 7543 RVA: 0x00073C05 File Offset: 0x00071E05
		public Hash(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.assembly = assembly;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000726B6 File Offset: 0x000708B6
		internal Hash()
		{
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00073C28 File Offset: 0x00071E28
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			this.data = (byte[])info.GetValue("RawData", typeof(byte[]));
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001D7A RID: 7546 RVA: 0x00073C50 File Offset: 0x00071E50
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("RawData", this.GetData());
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Hash" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Hash" />.</returns>
		// Token: 0x06001D7B RID: 7547 RVA: 0x00073C74 File Offset: 0x00071E74
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = this.GetData();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			securityElement.AddChild(new SecurityElement("RawData", stringBuilder.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00073CF4 File Offset: 0x00071EF4
		private byte[] GetData()
		{
			if (this.assembly == null && this.data == null)
			{
				throw new SecurityException(Locale.GetText("No assembly data."));
			}
			if (this.data == null)
			{
				FileStream fileStream = new FileStream(this.assembly.Location, FileMode.Open, FileAccess.Read);
				this.data = new byte[fileStream.Length];
				fileStream.Read(this.data, 0, (int)fileStream.Length);
			}
			return this.data;
		}

		// Token: 0x04000D9F RID: 3487
		private Assembly assembly;

		// Token: 0x04000DA0 RID: 3488
		private byte[] data;
	}
}
