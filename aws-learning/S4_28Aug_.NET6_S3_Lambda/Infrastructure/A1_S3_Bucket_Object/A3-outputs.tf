output "s3_bucket_id" {
  value = aws_s3_bucket.eshop.id
}

output "s3_bucket_tags" {
  value = aws_s3_bucket.eshop.tags_all
}

