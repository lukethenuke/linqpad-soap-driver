using System.Collections.Generic;
using System.Xml.Linq;
using LINQPad.Extensibility.DataContext;
using System.Net;

namespace Driver
{
	public class ConnectionModel
	{
		readonly IConnectionInfo connectionInfo;
		readonly IEnumerable<string> knownUris;

		public ConnectionModel(IConnectionInfo connectionInfo,
			IEnumerable<string> knownUris = null)
		{
			this.connectionInfo = connectionInfo;
			this.knownUris = knownUris ?? new string[0];
		}

		XElement DriverData
		{
			get { return connectionInfo.DriverData; }
		}

		public bool Persist
		{
			get { return connectionInfo.Persist; }
			set { connectionInfo.Persist = value; }
		}

		public string Uri
		{
			get { return (string)DriverData.Element ("Uri") ?? ""; }
			set { DriverData.SetElementValue ("Uri", value); }
		}

		public string BindingName
		{
			get { return (string) DriverData.Element("Binding") ?? ""; }
			set { DriverData.SetElementValue ("Binding", value); }
		}

		public bool UseNetworkCredentials
		{
			get { return (bool)((bool?)DriverData.Element("UseNetworkCredentials") ?? false); }
			set { DriverData.SetElementValue("UseNetworkCredentials", value); }
		}

		public string Username
		{
			get { return (string)DriverData.Element("Username") ?? ""; }
			set { DriverData.SetElementValue("Username", value); }
		}

		public string Password
		{
			get { return (string)DriverData.Element("Password") ?? ""; }
			set { DriverData.SetElementValue("Password", value); }
		}

		public string Domain
		{
			get { return (string)DriverData.Element("Domain") ?? ""; }
			set { DriverData.SetElementValue("Domain", value); }
		}

		public IEnumerable<string> KnownUris
		{
			get { return knownUris; }
		}

		public ICredentials GetCredentials()
		{
			if (this.UseNetworkCredentials)
			{
				if (!string.IsNullOrEmpty(this.Domain))
				{
					return new NetworkCredential(this.Username, this.Password, this.Domain);
				}
				else
				{
					return new NetworkCredential(this.Username, this.Password);
				}
			}

			return CredentialCache.DefaultCredentials;
		}
	}
}
