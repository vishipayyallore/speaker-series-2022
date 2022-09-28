output "dynamodb_table_item" {
  value = aws_dynamodb_table_item.example.item
}

output "dynamodb_table_name" {
  value = aws_dynamodb_table_item.example.table_name
}
