terraform {
  required_version = ">= 1.3.0"

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "4.32.0"
    }
  }
}

provider "aws" {
  region = var.region
}

resource "aws_dynamodb_table" "example" {
  name           = var.dynamodb_table_name
  read_capacity  = var.dynamodb_table_read_capacity
  write_capacity = var.dynamodb_table_write_capacity
  hash_key       = var.dynamodb_table_hash_key

  attribute {
    name = "employeeId"
    type = "S"
  }

}

resource "aws_dynamodb_table_item" "example" {
  table_name = aws_dynamodb_table.example.name
  hash_key   = aws_dynamodb_table.example.hash_key

  item = <<ITEM
            {
            "employeeId": {"S": "E101"},
            "name": {"S": "Sri Varu"},
            "age": {"N": "18"},
            "salary": {"N": "1234.5678"},
            "designation": {"S": "CEO"}
            },
            {
            "employeeId": {"S": "E102"},
            "name": {"S": "Scott Rudy"},
            "age": {"N": "18"},
            "salary": {"N": "2345.6789"},
            "designation": {"S": "Architect"}
            }
        ITEM
}
