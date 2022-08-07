# Variables
# $SubscriptionName = "YourSubscriptionName"
$RGName = "rg-az204-series-dev-001"
$LocationName = "EastUS"
$BaseName = "aug2022win"
$VmName = "vm$($BaseName)"
$VNetName = "vnet$($BaseName)"
$SubNetName = "default"
$NsgName = "nsg$($BaseName)"
$PublicDns = "publicdns$($BaseName)$(Get-Random)"
$PortsToOpen = 80, 3389
$ImageName = "Win2019Datacenter" 

##### For Help
# get-help New-AzResourceGroup

# Connecting to Subscription
# Connect-AzAccount
# Connect-AzAccount -SubscriptionName $SubscriptionName
# Set-AzContext -SubscriptionName $SubscriptionName

# View All subscriptions
# Get-AzSubscription
# Get-AzVm
# Get-AzResourceGroup | Format-Table

New-AzResourceGroup -Name $RGName -Location $LocationName -Tag @{environment = "dev"; Contact = "Swamy" }

$username = 'demouser'
$password = ConvertTo-SecureString 'NoPassword@1' -AsPlainText -Force
$CredentialsForVm = New-Object System.Management.Automation.PSCredential ($username, $password)

New-AzVm -ResourceGroupName $RGName -Name $VmName -Location $LocationName `
    -Credential $CredentialsForVm -Image $ImageName `
    -VirtualNetworkName $VNetName -SubnetName $SubNetName -SecurityGroupName $NsgName `
    -PublicIpAddressName $PublicDns -OpenPorts $PortsToOpen

# New-AzVm -ResourceGroupName $RGName -Name $VmName -Location $LocationName `
#     -Credential (Get-Credential) -Image $ImageName `
#     -VirtualNetworkName $VNetName -SubnetName $SubNetName -SecurityGroupName $NsgName `
#     -PublicIpAddressName $PublicDns -OpenPorts $PortsToOpen

Get-AzPublicIpAddress `
    -ResourceGroupName $RGName `
    -Name $PublicDns | Select-Object IpAddress

mstsc /v:publicIpAddress

# From within the newly created VM
# PS:> 
Install-WindowsFeature -name Web-Server -IncludeManagementTools

# From within the newly created VM

# From your Laptop/PC visit the URL
http://IpAddress-Of-Newly-Created-VM

# Stop-AzVm -Name $VmName -ResourceGroupName $RGName
# Remove-AzVM -Name $VmName -ResourceGroupName $RGName
# Remove-AzResourceGroup -Name $RGName
