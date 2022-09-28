output "dynamodb_table_name" {
  value = aws_dynamodb_table.employees.name
}

output "dynamodb_table_item" {
  value = aws_dynamodb_table_item.employeesdata
}

