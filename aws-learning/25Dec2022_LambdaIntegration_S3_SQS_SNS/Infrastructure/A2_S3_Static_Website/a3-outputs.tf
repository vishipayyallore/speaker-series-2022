output "s3_bucket_id" {
  value = aws_s3_bucket.eshop.id
}

output "s3_bucket_tags" {
  value = aws_s3_bucket.eshop.tags_all
}

output "s3_bucket_website_endpoint" {
  value = aws_s3_bucket_website_configuration.eshop.website_endpoint
}
