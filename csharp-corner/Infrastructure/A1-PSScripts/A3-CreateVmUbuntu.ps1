# Variables
# $SubscriptionName = "Your Subscription Name"
$RGName = "rg-az204-series-dev-002"
$LocationName = "CentralUS"
$BaseName = "aug2022ubuntu"
$VmName = "vm$($BaseName)"
$VNetName = "vnet$($BaseName)"
$SubNetName = "default"
$NsgName = "nsg$($BaseName)"
$NicName = "nic$($BaseName)"
$PublicDns = "publicdns$($BaseName)$(Get-Random)"
$PortsToOpen = 80, 22
$username = 'demouser'
$password = ConvertTo-SecureString 'NoPassword@1' -AsPlainText -Force
$NsgRuleForSsh = "NetworkSecurityGroupRuleForSSH"
$NsgRuleForWeb = "NetworkSecurityGroupRuleForWeb"

# Generate the SHH keys
ssh-keygen -t rsa -b 4096

# Connecting to Subscription
# Connect-AzAccount
# Connect-AzAccount -SubscriptionName $SubscriptionName
# Set-AzContext -SubscriptionName $SubscriptionName

# View All subscriptions
# Get-AzSubscription
# Get-AzVm
# Get-AzResourceGroup | Format-Table

New-AzResourceGroup -Name $RGName -Location $LocationName -Tag @{environment = "dev"; Contact = "Swamy" }

$CredentialsForVm = New-Object System.Management.Automation.PSCredential ($username, $password)

# Create an inbound network security group rule for port 22
$nsgRuleSSH = New-AzNetworkSecurityRuleConfig -Name $NsgRuleForSsh -Protocol "Tcp" -Direction "Inbound" -Priority 1000 `
  -SourceAddressPrefix * -SourcePortRange * -DestinationAddressPrefix * -DestinationPortRange $PortsToOpen[1] -Access "Allow"

# Create an inbound network security group rule for port 80
$nsgRuleWeb = New-AzNetworkSecurityRuleConfig -Name $NsgRuleForWeb -Protocol "Tcp" -Direction "Inbound" -Priority 1001 `
  -SourceAddressPrefix * -SourcePortRange * -DestinationAddressPrefix * -DestinationPortRange $PortsToOpen[0] -Access "Allow"

# Create a network security group
$nsg = New-AzNetworkSecurityGroup -ResourceGroupName $RGName -Location $LocationName -Name $NsgName `
  -SecurityRules $nsgRuleSSH, $nsgRuleWeb

# Create a subnet configuration
$subnetConfig = New-AzVirtualNetworkSubnetConfig -Name $SubNetName -AddressPrefix 192.168.1.0/24 -NetworkSecurityGroup $nsg

# Create a public IP address and specify a DNS name
$pip = New-AzPublicIpAddress -ResourceGroupName $RGName -Location $LocationName -AllocationMethod Static `
  -IdleTimeoutInMinutes 4 -Name $PublicDns

# Create a virtual network
$vnet = New-AzVirtualNetwork -ResourceGroupName $RGName -Location $LocationName -Name $VNetName `
  -AddressPrefix 192.168.0.0/16 -Subnet $subnetConfig

# Create a virtual network card and associate with public IP address and NSG
$nic = New-AzNetworkInterface -Name $NicName -ResourceGroupName $RGName -Location $LocationName `
  -SubnetId $vnet.Subnets[0].Id -PublicIpAddressId $pip.Id -NetworkSecurityGroupId $nsg.Id

#### Verify the VM Size before working on the next command
##### Get-AzComputeResourceSku | where {$_.Locations -icontains "centralus"}

# Create a virtual machine configuration ##### -DisablePasswordAuthentication
$vmConfig = New-AzVMConfig -VMName $VmName -VMSize "Standard_D1_v2" | `
  Set-AzVMOperatingSystem -Linux -ComputerName $VmName -Credential $CredentialsForVm  | `
  Set-AzVMSourceImage -PublisherName "Canonical" -Offer "UbuntuServer" -Skus "18.04-LTS" -Version "latest" | `
  Add-AzVMNetworkInterface -Id $nic.Id

#### MODIFY THE <USERNAME>
# Configure the SSH key
# $sshPublicKey = cat C:\Users\YourUser\.ssh\id_rsa.pub ## When On Local Laptop 
$sshPublicKey = cat /home/<USERNAME>/.ssh/id_rsa.pub ## When inside Azure Cloud Shell
Add-AzVMSshPublicKey -VM $vmconfig -KeyData $sshPublicKey -Path "/home/demouser/.ssh/authorized_keys"

New-AzVM -ResourceGroupName $RGName -Location $LocationName -VM $vmConfig

Get-AzPublicIpAddress -ResourceGroupName $RGName -Name $PublicDns | Select-Object IpAddress

> ssh -i C:\Users\YourUser\.ssh\id_rsa demouser@VmPublicIpAddress ## From Local Laptop
> ssh -i /home/YourUser/.ssh/id_rsa demouser@VmPublicIpAddress ## From Local Laptop

##### MODIFY THE <USERNAME> AND <VmPublicIpAddress>
ssh -i /home/<USERNAME>/.ssh/id_rsa demouser@<VmPublicIpAddress>

##### Inside the Ubuntu VM
```
sudo apt-get -y update
sudo apt-get -y install nginx
```
##### Inside the Ubuntu VM

##### From Our Local Laptop/PC
# visit the URL
curl localhost
http://<VmPublicIpAddress>
##### From Our Local Laptop/PC
