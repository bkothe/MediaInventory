# MediaInventory

IIS Hosting

Version 10 install URL Re-write module: https://www.microsoft.com/en-us/download/confirmation.aspx?id=47337

Add to Web.config
```
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="AngularJs" stopProcessing="true">
          <match url="^.*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
            <add input="{REQUEST_URI}" pattern="^/(api)" negate="true" />
          </conditions>
          <action type="Rewrite" url="/index.html" />
        </rule>
        <rule name="RootRedirect" stopProcessing="true">
          <match url="^$" />
          <action type="Rewrite" url="/index.html" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
```
