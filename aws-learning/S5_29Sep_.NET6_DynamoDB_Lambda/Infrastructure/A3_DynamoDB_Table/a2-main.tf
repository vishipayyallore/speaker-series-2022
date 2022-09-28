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

resource "aws_dynamodb_table" "employees" {
  name           = var.dynamodb_table_name
  read_capacity  = var.dynamodb_table_read_capacity
  write_capacity = var.dynamodb_table_write_capacity
  hash_key       = var.dynamodb_table_hash_key

  attribute {
    name = "employeeId"
    type = "S"
  }

  tags = {
    environment = "dev"
  }

}

resource "aws_dynamodb_table_item" "employeesdata" {
  table_name = aws_dynamodb_table.employees.name
  hash_key   = aws_dynamodb_table.employees.hash_key

  for_each = local.employee_rows

  item = jsonencode(each.value)
}
