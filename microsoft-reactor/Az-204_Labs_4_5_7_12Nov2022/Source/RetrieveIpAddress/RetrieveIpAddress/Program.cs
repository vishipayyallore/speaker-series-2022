using System.Net;
using System.Net.NetworkInformation;
using static System.Console;

// Check if network is available
if (NetworkInterface.GetIsNetworkAvailable())
{
    WriteLine("Current IP Addresses:");

    // Get host entry for current hostname
    string hostname = Dns.GetHostName();
    IPHostEntry host = Dns.GetHostEntry(hostname);

    // Iterate over each IP address and render their values
    foreach (IPAddress address in host.AddressList)
    {
        WriteLine($"\t{address}");
    }
}
else
{
    WriteLine("No Network Connection");
}