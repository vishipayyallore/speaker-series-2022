az deployment group create --resource-group rg-womd-robbie-001 --template-file sql-server.deploy.json --parameters sql-server.parameters.json --mode Incremental