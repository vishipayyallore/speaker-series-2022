sshKey=$(cat '/mnt/c/Users/<UserName>/.ssh/id_rsa.pub')
az deployment group create --resource-group rg-womd-robbie-001 --template-file ubuntuvm.deploy.json --parameters ubuntuvm.parameters.json --parameters adminPublicKey="$sshKey" --mode Incremental

