using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_WebApiFramework
{

	public struct NewUser
	{
		public string username { get; set; }
		public string password { get; set; }
		public string email { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(NewUser left, NewUser right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NewUser left, NewUser right)
		{
			return !(left == right);
		}
	}
	public struct oauth2
	{
		public string token { get; set; }
		public string salt { get; set; }
		public string secret { get; set; }
		public string udid { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(oauth2 left, oauth2 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(oauth2 left, oauth2 right)
		{
			return !(left == right);
		}
	}

	public struct AccountData
	{
		public string username { get; set; }
		public string password { get; set; }
		public string email { get; set; }
		public string accounttype { get; set; }
		public oauth2 oauth2 { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(AccountData left, AccountData right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(AccountData left, AccountData right)
		{
			return !(left == right);
		}
	}

	public struct ProfileData
	{
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string pfpurl { get; set; }
		public string bio { get; set; }
		public string usid { get; set; }
		public string status { get; set; }
		public string employment { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(ProfileData left, ProfileData right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ProfileData left, ProfileData right)
		{
			return !(left == right);
		}
	}

	public struct User
	{
		public string uuid { get; set; }
		public AccountData AccountData { get; set; }
		public ProfileData ProfileData { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(User left, User right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(User left, User right)
		{
			return !(left == right);
		}
	}
	public struct Error
	{
		public bool result { get; set; }
		public string response { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(Error left, Error right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Error left, Error right)
		{
			return !(left == right);
		}
	}
	public struct CreateUserReturn
	{
		public bool result { get; set; }
		public AccountData data { get; set; }

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(CreateUserReturn left, CreateUserReturn right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CreateUserReturn left, CreateUserReturn right)
		{
			return !(left == right);
		}
	}
}
