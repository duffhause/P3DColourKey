using System.Linq;

namespace P3DColourKey
{
	class Hash
	{
		public static string GetHashSHA1(byte[] data)
		{
			using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
			{
				return string.Concat(sha1.ComputeHash(data).Select(x => x.ToString("X2")));
			}
		}
	}
}
