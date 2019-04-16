using System;
using System.Configuration;

namespace Mineware.Systems.WindowsServices
{
	public class Global
	{

		public static string Encrypt(string decrypted)
		{
			var encrypted = "";
			for (var i = 0; i < decrypted.Length; i++)
			{
				if ((i % 2) == 0)
				{
					encrypted += (char)(decrypted[i] - 3);
				}
				else
				{
					encrypted += (char)(decrypted[i] + 3);
				}
			}
			return encrypted;
		}

		public static string Decrypt(string encrypted)
		{
			var decrypted = "";
			for (var i = 0; i < encrypted.Length; i++)
			{
				if ((i % 2) == 0)
				{
					decrypted += (char)(encrypted[i] + 3);
				}
				else
				{
					decrypted += (char)(encrypted[i] - 3);
				}
			}
			return decrypted;
		}



		public static string GetSettingValueAsString(string appSetting)
		{
			var setting = ConfigurationManager.AppSettings[appSetting];
			if (setting == null)
			{
				throw new InvalidOperationException($"{appSetting} setting not found");
			}

			return setting;
		}

		public static int GetSettingValueAsInt(string appSetting)
		{
			var setting = ConfigurationManager.AppSettings[appSetting];
			if (setting == null)
			{
				throw new InvalidOperationException($"{appSetting} setting not found");
			}

			int value;
			if (!int.TryParse(setting, out value))
			{
				throw new InvalidOperationException($"{appSetting} Setting invalid");
			}

			return value;
		}
	}
}