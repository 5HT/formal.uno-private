README for setting up .NET ASAP CLIENT AND SERVER
-------------------------------------------------

To run the .NET ASAP client and server, you will need IIS 5.0 and Microsoft .NET Framework 1.1

Follow the steps below to set it up:

1. Unzip the client.zip and server.zip into C:\Inetpub\wwwroot (IIS's document root)

2. Edit ASAPServer\Web.config and ASAPClient\Web.config

Change the 4th line in both files from 
            <add key="hostURL" value="http://jeffc" />
to 
            <add key="hostURL" value="http://ip_addr_of_your_computer" />

3. Restart the IIS "World Wide Web Publishing Service".
 
Access the client as:
http://ip_addr_of_your_computer/ASAPClient

To test the client against your server, change the default factory URI on the client page to your factory URI.

For information on how to use the client, refer to the latest DemoScenario document on the OASIS ASAP TC website.