using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;

namespace System.Security.Claims
{
	/// <summary>An <see cref="T:System.Security.Principal.IPrincipal" /> implementation that supports multiple claims-based identities.</summary>
	// Token: 0x020003E2 RID: 994
	[ComVisible(true)]
	[Serializable]
	public class ClaimsPrincipal : IPrincipal
	{
		// Token: 0x060021D1 RID: 8657 RVA: 0x0008CEDC File Offset: 0x0008B0DC
		private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 is WindowsIdentity)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
				if (claimsIdentity == null)
				{
					claimsIdentity = claimsIdentity2;
				}
			}
			return claimsIdentity;
		}

		/// <summary>Gets and sets the delegate used to select the claims principal returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Current" /> property.</summary>
		/// <returns>The delegate. The default is null.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0008CF40 File Offset: 0x0008B140
		public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
		{
			get
			{
				return ClaimsPrincipal.s_principalSelector;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class.</summary>
		// Token: 0x060021D3 RID: 8659 RVA: 0x0008CF47 File Offset: 0x0008B147
		public ClaimsPrincipal()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060021D4 RID: 8660 RVA: 0x0008CF65 File Offset: 0x0008B165
		protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0008CF99 File Offset: 0x0008B199
		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaimsIdentities = this.SerializeIdentities();
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0008CFB0 File Offset: 0x0008B1B0
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.DeserializeIdentities(this.m_serializedClaimsIdentities);
			this.m_serializedClaimsIdentities = null;
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0008CFD0 File Offset: 0x0008B1D0
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "System.Security.ClaimsPrincipal.Identities"))
				{
					if (name == "System.Security.ClaimsPrincipal.Version")
					{
						this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
					}
				}
				else
				{
					this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
				}
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0008D044 File Offset: 0x0008B244
		private void DeserializeIdentities(string identities)
		{
			this.m_identities = new List<ClaimsIdentity>();
			if (!string.IsNullOrEmpty(identities))
			{
				List<string> list = null;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(identities)))
				{
					list = (List<string>)binaryFormatter.Deserialize(memoryStream, null, false);
					for (int i = 0; i < list.Count; i += 2)
					{
						ClaimsIdentity claimsIdentity = null;
						using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(list[i + 1])))
						{
							claimsIdentity = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream2, null, false);
						}
						if (!string.IsNullOrEmpty(list[i]))
						{
							long num;
							if (!long.TryParse(list[i], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
							{
								throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
							}
							claimsIdentity = new WindowsIdentity(claimsIdentity, new IntPtr(num));
						}
						this.m_identities.Add(claimsIdentity);
					}
				}
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0008D154 File Offset: 0x0008B354
		private string SerializeIdentities()
		{
			List<string> list = new List<string>();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			foreach (ClaimsIdentity claimsIdentity in this.m_identities)
			{
				if (claimsIdentity.GetType() == typeof(WindowsIdentity))
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					list.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString(NumberFormatInfo.InvariantInfo));
					using (MemoryStream memoryStream = new MemoryStream())
					{
						binaryFormatter.Serialize(memoryStream, windowsIdentity.CloneAsBase(), null, false);
						list.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
						continue;
					}
				}
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					list.Add("");
					binaryFormatter.Serialize(memoryStream2, claimsIdentity, null, false);
					list.Add(Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
			string text;
			using (MemoryStream memoryStream3 = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream3, list, null, false);
				text = Convert.ToBase64String(memoryStream3.GetBuffer(), 0, (int)memoryStream3.Length);
			}
			return text;
		}

		/// <summary>Gets the primary claims identity associated with this claims principal.</summary>
		/// <returns>The primary claims identity associated with this claims principal.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x0008D2DC File Offset: 0x0008B4DC
		public virtual IIdentity Identity
		{
			get
			{
				if (ClaimsPrincipal.s_identitySelector != null)
				{
					return ClaimsPrincipal.s_identitySelector(this.m_identities);
				}
				return ClaimsPrincipal.SelectPrimaryIdentity(this.m_identities);
			}
		}

		// Token: 0x04001038 RID: 4152
		[OptionalField(VersionAdded = 2)]
		private string m_version = "1.0";

		// Token: 0x04001039 RID: 4153
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaimsIdentities;

		// Token: 0x0400103A RID: 4154
		[NonSerialized]
		private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();

		// Token: 0x0400103B RID: 4155
		[NonSerialized]
		private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);

		// Token: 0x0400103C RID: 4156
		[NonSerialized]
		private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;
	}
}
