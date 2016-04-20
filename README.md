# Securitycore

Installation guide:
	1) SIM - create evilcore.local
	2) SIM - create safecore.local
	3) SQL
		a. Create database "UserInteractions" on the same SQL instance
		b. Create the account "UserInteractionDB2" with password "User12345DB"
		c. Assign DBO permissions to the UserInteractionDB2 account for the UserInteractions table
	4) Install URL Rewrite 2.0
	5) Add the following assemblies to <solutionfolder>\lib\sitecore:
		a. Sitecore.Analytics.dll
		b. Sitecore.Analytics.Model.dll
		c. Sitecore.Contentsearch.dll
		d. Sitecore.Kernel.dll
		e. Sitecore.Mvc.Analytics.dll
		f. Sitecore.Mvc.dll
		g. Sitecore.Services.Client.dll
		h. Sitecore.Services.core.dll
		i. Sitecore.Services.Infrastructure.dll
	6) Add certificates
		a. Open visual studio command prompt with admin permissions
		b. Navigate to the certificates folder
		c. Run createCARoot.bat
		d. Run create domain cert.bat safecore
		e. Run clientcert safecore
		f. Add CARoot certificate to the certificate store
		g. Add safecore.pfx to IIS:
			i. Server certificates
			ii. Add certificate
			iii. Select safecore.pfx
			iv. Password: Test123
	7) Add https binding to safecore.local
		a. Edit bindings
		b. Select https
		c. Add safecore.local
		d. Use the safecore.local certificate
	8) Update database
		a. Enable-Migrations –EnableAutomaticMigrations
		b. Update-Database -Verbose -Force
	9) Publish evilcore binaries
	10) Publish all evilcore content (using TDS/Unicorn)
	11) Publish safecore binaries
	12) Publish safecore content
	

Evilcore specific:

	1) Connectionstrings: add - 
	2) Add the following accounts:
	3) Add the following …

Safecore.local
	1) Add Connectionstrings
		a. Add: <add name="UserInteractionContext" connectionString="Data Source=.\;Initial Catalog=UserInteractions;Database=UserInteractions;uid=UserInteractionDB2;pwd=User12345DB" /> to the connectionstrings
	2) Encrypt it: open command prompt
		a. Navigate to c:\windows\microsoft.net\Framework\<version>
		b. Aspnet_regii -pe "connectionStrings" -site "safecore.local" -app -"\" -prov "RsaProtectedConfigurationProvider"
	3) Add new sql provider
	
	
	
